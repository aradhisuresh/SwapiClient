using SwapiClient.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapiClient.Application
{
    public interface ISwapiHttpClientHelper
    {
        ValueTask<SwapiPeopleResponse> GetPeople(int pageNo);
    }
}
