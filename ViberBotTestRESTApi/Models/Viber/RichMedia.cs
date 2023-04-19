using Newtonsoft.Json;
using Viber.Bot.NetCore.Models;

namespace ViberWalkBot.Models.Viber
{
    public class RichMedia
    {
        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("ButtonsGroupColumns")]
        public int ButtonsGroupColumns { get; set; }

        [JsonProperty("ButtonsGroupRows")]
        public int ButtonsGroupRows { get; set; }

        [JsonProperty("BgColor")]
        public string BgColor { get; set; }

        [JsonProperty("Buttons")]
        public ICollection<ViberKeyboardButton> Buttons { get; set; }

    }
}
