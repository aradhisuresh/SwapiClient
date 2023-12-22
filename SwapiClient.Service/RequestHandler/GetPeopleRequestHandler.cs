using MediatR;
using SwapiClient.Application.Query;
using SwapiClient.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapiClient.Application.RequestHandler
{
    public class GetPeopleRequestHandler : IRequestHandler<GetPeopleQuery, SwapiPeopleResponse>
    {
        private readonly ISwapiHttpClientHelper _httpClientHelper;
        public GetPeopleRequestHandler(ISwapiHttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }
        public async Task<SwapiPeopleResponse> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            return await _httpClientHelper.GetPeople(request.PageNo);
        }
    }
}
