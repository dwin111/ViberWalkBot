using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Viber.Bot.NetCore.Infrastructure;
using Viber.Bot.NetCore.Models;
using Viber.Bot.NetCore.RestApi;
using ViberWalkBot;
using ViberWalkBot.Migrations;
using ViberWalkBot.Models;
using ViberWalkBot.Models.Viber;
using ViberWalkBot.Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Viber.Bot.NetCore.Models.ViberMessage;

[Route("/")]
[ApiController]
public class ViberController : ControllerBase
{
    private readonly ILogger<ViberController> _logger;
    private readonly ITrackLocationService _trackLocationService;
    private readonly IWalkService _walkService;
    private readonly IViberService _viberService;

    private const int NumberOfCharactersInIMEI = 15;


    public ViberController (ILogger<ViberController> logger, ITrackLocationService trackLocationService, IWalkService walkService, IViberService viberService)
    {
        _logger = logger;
        _trackLocationService = trackLocationService;
        _walkService = walkService;
        _viberService = viberService;
    }

    [HttpPost]
    public async Task<IActionResult> WebHook()
    {
        try
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            ViberCallbackData update = JsonConvert.DeserializeObject<ViberCallbackData>(json);

            var str = String.Empty;

            switch (update.Event)
            {
                case "conversation_started":
                    {
                        await _viberService.SendMessage(update.User.Id, SettingsBot.NameBot, "", "Введіть IMEI");
                        break;
                    }
                case "message":
                    {
                        return await MessageHandler(update);
                    }
                default: break;
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
            return BadRequest(ex.Message);
        }

    }

    private async Task<IActionResult> MessageHandler(ViberCallbackData update)
    {
        try
        {
            var mess = update.Message as TextMessage;
            string str = mess.Text;

            if (Regex.Matches(str, "ButtonClick").Count != 0)
            {

                await ButtonClickHandler(update, str.Split("&")[1]);
                return Ok();
            }
            else if (str.Count() != NumberOfCharactersInIMEI && !int.TryParse(str, out int number))
            {
                await _viberService.SendMessage(update.Sender.Id, $"{update.Sender.Name}", $"{update.Sender.Avatar}", "Це не схоже на IMEI. Введіть будь ласка коректне IMEI");
                return Ok();
            }
            else
            {
                await AllWalkInfo(update, str);
                return Ok();
            }
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
            return BadRequest(ex.Message);
        }
        
    }

    private async Task AllWalkInfo(ViberCallbackData update, string str)
    {
        try
        {
            double AllDistance = 0;
            double AllTime = 0;
            var models = await _trackLocationService.GenerationWalks(str);

            foreach (var model in models)
            {
                AllDistance += model.Distance;
                AllTime += model.Time;
            }

            string sendMessage = $"Всього прогулянок: {models.Count}\n" +
                                 $"Всього км пройдено: {Math.Round(AllDistance / 1000, 1)}\n" +
                                 $"Всього часу, хв: {Math.Round(AllTime, 1)}";

            await _viberService.SendMessageAndKeyboard(update.Sender.Id, SettingsBot.NameBot, "", sendMessage, "ТОП 10 прогулянок", $"ButtonClick&Top10walk%{str}");
        }
        catch (Exception ex)
        {

            _logger.Log(LogLevel.Error, ex.Message);
        }
    }

    private async Task ButtonClickHandler(ViberCallbackData update, string comand)
    {
        try
        {
            string commandName = comand.Split("%")[0];
            string IMEI = comand.Split("%")[1];

            switch (commandName)
            {

                case "Top10walk":
                    {
                        var top10 = _walkService.GetTop10(IMEI, 10);
                        await _viberService.SendTable(update.Sender.Id, top10, IMEI);
                        break;
                    }
                case "Back":
                    {
                        await AllWalkInfo(update, IMEI);
                        break;
                    }

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
        }
       

    }


}