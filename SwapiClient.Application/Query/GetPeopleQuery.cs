using MediatR;
using SwapiClient.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapiClient.Application.Query
{
    public class GetPeopleQuery: IRequest<SwapiPeopleResponse>
    {
        public int PageNo { get; set; }
    }
}
