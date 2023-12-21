using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapiClient.Domain
{
    public class PersonDetailedInfo: PersonBasicInfo
    {
        public string EyeColor { get; set; }
        public string SkinColor { get; set; }
        public string HairColor { get; set; }
        public string BirthYear { get; set; }
    }
}
