using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SwapiClient.Domain
{
    public class SwapiPeopleResponse
    {
        public int Count { get; set; }
        public List<SwapiPeople> Results { get; set; }
    }
    public class SwapiPeople
    {
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("birth_year")]
        public string BirthYear { get; set; }
        [Newtonsoft.Json.JsonProperty("eye_color")]
        public string EyeColor { get; set; }
        public string Gender { get; set; }

        [Newtonsoft.Json.JsonProperty("hair_color")]
        public string HairColor { get; set; }

        
        public string Height { get; set; }

        
        public string Mass { get; set; }
        [Newtonsoft.Json.JsonProperty("skin_color")]
        public string SkinColor { get; set; }
        public string Homeworld { get; set; }
    }
}
