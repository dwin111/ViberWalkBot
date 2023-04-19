using Newtonsoft.Json;
using Viber.Bot.NetCore.Infrastructure;
using Viber.Bot.NetCore.Models;
using static Viber.Bot.NetCore.Models.ViberMessage;

namespace ViberWalkBot.Models.Viber
{
    public class CarouselContentMessage : MessageBase
    {
        public CarouselContentMessage() : base(ViberMessageType.CarouselContent)
        {}

        [JsonProperty("rich_media")]
        public RichMedia RichMedia { get; set; }

        [JsonProperty("keyboard")]
        public ViberKeyboard Keyboard { get; set; }

    }
}
