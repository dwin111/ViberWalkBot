using Viber.Bot.NetCore.Models;
using ViberWalkBot.Models;
using ViberWalkBot.Models.Viber;

namespace ViberWalkBot.Services.Interface
{
    public interface IViberService
    {
        Task SendMessageCarouselContent(CarouselContentMessage message);
        Task SendMessage(string UserId, string NameSenderBot, string AvatarURLBot, string TextMessage);
        Task SendMessageAndKeyboard(string UserId, string NameSenderBot, string AvatarURLBot, string TextMessage, string TextMessageButton, string ActionBodyButton);
        Task SendTable(string UserId, IEnumerable<Walk> Walks, string IMEI);

        List<ViberKeyboardButton> GenericTable(IEnumerable<Walk> walks);
        List<ViberKeyboardButton> GenericTableWithHeaders(IEnumerable<Walk> walks);


    }
}
