using System.IO;
using Newtonsoft.Json;

namespace GoldenSunPasswordSender
{
    internal class Config
    {
        [JsonProperty("up")]
        public string Up { get; } = "{UP}";
        [JsonProperty("down")]
        public string Down { get; } = "{DOWN}";
        [JsonProperty("left")]
        public string Left { get; } = "{LEFT}";
        [JsonProperty("right")]
        public string Right { get; } = "{RIGHT}";
        [JsonProperty("confirm")]
        public string Confirm { get; } = "X";
        [JsonProperty("emulator")]
        public string Emulator { get; } = "mGBA";
        [JsonProperty("password")]
        public string Password { get; } = "password.txt";

        public static Config Default => new();

        public static Config Load() => JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));

        private Config() { }
    }
}
