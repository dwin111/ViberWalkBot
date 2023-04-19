using Newtonsoft.Json;
using System.Text;
using Viber.Bot.NetCore.Models;
using Viber.Bot.NetCore.RestApi;
using ViberWalkBot.Models;
using ViberWalkBot.Models.Viber;
using ViberWalkBot.Services.Interface;
using static Viber.Bot.NetCore.Models.ViberMessage;
using static Viber.Bot.NetCore.Models.ViberUser;


namespace ViberWalkBot.Services
{
    public class ViberService : IViberService
    {
        private readonly IViberBotApi _viberBotApi;
        private readonly ILogger<ViberService> _logger;

        public ViberService(IViberBotApi viberBotApi, ILogger<ViberService> logger)
        {
            _viberBotApi = viberBotApi;
           _logger = logger;
        }

        public List<ViberKeyboardButton> GenericTableWithHeaders(IEnumerable<Walk> walks)
        {
            List<ViberKeyboardButton> Table = new List<ViberKeyboardButton>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = -1; j < walks.Count(); j++)
                {
                    if (i == 0)
                    {
                        if (j == -1)
                        {
                            var NameFirstLine = new ViberKeyboardButton()
                            {
                                Columns = 3,
                                Rows = 1,
                                BackgroundColor = "#FFFFFF",
                                ActionType = "none",
                                Text = "Назва",
                            };
                            Table.Add(NameFirstLine);
                        }
                        else
                        {
                            var NameFirstLine = new ViberKeyboardButton()
                            {
                                Columns = 3,
                                Rows = 1,
                                BackgroundColor = "#FFFFFF",
                                ActionType = "none",
                                Text = walks.ElementAt(j).Name,
                            };
                            Table.Add(NameFirstLine);
                        }
                    }
                    else if (i == 1)
                    {
                        if (j == -1)
                        {


                            var NameSecondLine = new ViberKeyboardButton()
                            {
                                Columns = 3,
                                Rows = 1,
                                BackgroundColor = "#FFFFFF",
                                ActionType = "none",
                                Text = "Кілометрів",
                            };
                            Table.Add(NameSecondLine);
                        }
                        else
                        {
                            var NameSecondLine = new ViberKeyboardButton()
                            {
                                Columns = 3,
                                Rows = 1,
                                BackgroundColor = "#FFFFFF",
                                ActionType = "none",
                                Text = Math.Round((walks.ElementAt(j).Distance / 1000), 1).ToString(),
                            };
                            Table.Add(NameSecondLine);
                        }

                    }
                    else if (i == 2)
                    {
                        if (j == -1)
                        {


                            var NameSecondLine = new ViberKeyboardButton()
                            {
                                Columns = 3,
                                Rows = 1,
                                BackgroundColor = "#FFFFFF",
                                ActionType = "none",
                                Text = "Хвилини",
                            };
                            Table.Add(NameSecondLine);
                        }
                        else
                        {
                            var NameSecondLine = new ViberKeyboardButton()
                            {
                                Columns = 3,
                                Rows = 1,
                                BackgroundColor = "#FFFFFF",
                                ActionType = "none",
                                Text = Math.Round(walks.ElementAt(j).Time, 1).ToString(),
                            };
                            Table.Add(NameSecondLine);
                        }
                    }

                }
            }

            return Table;
        }
        public List<ViberKeyboardButton> GenericTable(IEnumerable<Walk> walks)
        {
            List<ViberKeyboardButton> Table = new List<ViberKeyboardButton>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < walks.Count(); j++)
                {
                    if (i == 0)
                    {
                        var NameFirstLine = new ViberKeyboardButton()
                        {
                            Columns = 3,
                            Rows = 1,
                            BackgroundColor = "#FFFFFF",
                            ActionType = "none",
                            Text = walks.ElementAt(j).Name,
                        };
                        Table.Add(NameFirstLine);
                    }
                    else if (i == 1)
                    {
                        var NameSecondLine = new ViberKeyboardButton()
                        {
                            Columns = 3,
                            Rows = 1,
                            BackgroundColor = "#FFFFFF",
                            ActionType = "none",
                            Text = Math.Round((walks.ElementAt(j).Distance / 1000), 1).ToString(),
                        };
                        Table.Add(NameSecondLine);

                    }
                    else if (i == 2)
                    {
                        var NameSecondLine = new ViberKeyboardButton()
                        {
                            Columns = 3,
                            Rows = 1,
                            BackgroundColor = "#FFFFFF",
                            ActionType = "none",
                            Text = Math.Round(walks.ElementAt(j).Time, 1).ToString(),
                        };
                        Table.Add(NameSecondLine);
                    }

                }
            }

            return Table;
        }



        public async Task SendTable(string UserId, IEnumerable<Walk> Walks, string IMEI)
        {
            try
            {
                if (Walks.Count() < 7)
                {
                    var message = new CarouselContentMessage()
                    {
                        Receiver = UserId,
                        MinApiVersion = 7,
                        RichMedia = new RichMedia()
                        {
                            Type = "rich_media",
                            ButtonsGroupColumns = 3,
                            ButtonsGroupRows = Walks.Count() + 1,
                            BgColor = "#222222",
                            Buttons = GenericTableWithHeaders(Walks)
                        },
                        Keyboard = new ViberKeyboard()
                        {
                            DefaultHeight = false,
                            Buttons = new List<ViberKeyboardButton>()
                            {
                                  new ViberKeyboardButton()
                                    {
                                        Columns = 6,
                                        Rows = 1,
                                        BackgroundColor = "#FFFFFF",
                                        ActionType = "reply",
                                        ActionBody = $"ButtonClick&Back%{IMEI}",
                                        Text = "Назад",
                                    },
                            }
                        }

                    };

                    await SendMessageCarouselContent(message);
                }
                else
                {
                    var TableWithHeaders = new CarouselContentMessage()
                    {
                        Receiver = UserId,
                        MinApiVersion = 7,
                        RichMedia = new RichMedia()
                        {
                            Type = "rich_media",
                            ButtonsGroupColumns = 3,
                            ButtonsGroupRows = 7,
                            BgColor = "#222222",
                            Buttons = GenericTableWithHeaders(Walks.Take(6))
                        }

                    };
                    var TableOnlyBody = new CarouselContentMessage()
                    {
                        Receiver = UserId,
                        MinApiVersion = 7,
                        RichMedia = new RichMedia()
                        {
                            Type = "rich_media",
                            ButtonsGroupColumns = 3,
                            ButtonsGroupRows = 4,
                            BgColor = "#222222",
                            Buttons = GenericTable(Walks.Skip(6))
                        },
                        Keyboard = new ViberKeyboard()
                        {
                            DefaultHeight = false,
                            Buttons = new List<ViberKeyboardButton>()
                            {
                                  new ViberKeyboardButton()
                                    {
                                        Columns = 6,
                                        Rows = 1,
                                        BackgroundColor = "#FFFFFF",
                                        ActionType = "reply",
                                        ActionBody = $"ButtonClick&Back%{IMEI}",
                                        Text = "Назад",
                                    },
                            }
                        }

                    };

                    await SendMessageCarouselContent(TableWithHeaders);
                    await SendMessageCarouselContent(TableOnlyBody);
                }

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }

        }


        public async Task SendMessageCarouselContent(CarouselContentMessage message)
        {
            var _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://chatapi.viber.com/pa/"),
                DefaultRequestHeaders = { { "X-Viber-Auth-Token", SettingsBot.Token } }
            };
            var json = JsonConvert.SerializeObject(message);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"send_message", stringContent);
        }
        public async Task SendMessage(string UserId, string NameSenderBot, string AvatarURLBot, string TextMessage)
        {
            try
            {
                var message = new TextMessage()
                {
                    Receiver = UserId,
                    Sender = new ViberUser.User()
                    {
                        Name = NameSenderBot,
                        Avatar = AvatarURLBot
                    },
                    Text = TextMessage
                };

                await _viberBotApi.SendMessageAsync<ViberResponse.SendMessageResponse>(message);

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }

        }
        public async Task SendMessageAndKeyboard(string UserId, string NameSenderBot, string AvatarURLBot, string TextMessage, string TextMessageButton, string ActionBodyButton)
        {
            try
            {
                var message = new KeyboardMessage()
                {
                    Receiver = UserId,
                    Keyboard = new ViberKeyboard()
                    {

                        DefaultHeight = false,
                        Buttons = new List<ViberKeyboardButton>()
                        {
                              new ViberKeyboardButton()
                                {
                                    Columns = 6,
                                    Rows = 1,
                                    BackgroundColor = "#FFFFFF",
                                    ActionType = "reply",
                                    ActionBody = ActionBodyButton,
                                    Text = TextMessageButton,
                                    TextHorizontalAlign = "center",
                                    TextVerticalAlign  = "middle",
                                    TextOpacity= 60,
                                    TextSize= "regular"
                                },
                        }
                    },
                    Sender = new ViberUser.User()
                    {
                        Name = NameSenderBot,
                        Avatar = AvatarURLBot
                    },
                    Text = TextMessage
                };
                await _viberBotApi.SendMessageAsync<ViberResponse.SendMessageResponse>(message);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }

        }
    }
}
