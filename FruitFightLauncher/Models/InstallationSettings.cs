using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitFightLauncher.Models
{
    public class InstallationSettings
    {
        [JsonProperty("gameInstalled")]
        public bool GameInstalled { get; set; }

        [JsonProperty("gameVersionId")]
        public int GameVersionId { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static InstallationSettings FromJson(string json)
        {
            return JsonConvert.DeserializeObject<InstallationSettings>(json);
        }
    }
}
