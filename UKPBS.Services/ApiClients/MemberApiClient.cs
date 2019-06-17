using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using UKPBS.Domain.Entities.ParliamentMembers;
using UKPBS.Services.Exceptions;

namespace UKPBS.Services.ApiClients
{
    public class MemberApiClient : ApiClient, IMemberApiClient
    {
        public MemberApiClient(IRestClient client) : base(client)
        {
        }

        public IEnumerable<Member> GetMembersByIds(int[] memberIds)
        {
            if (!memberIds?.Any() == true) throw new ArgumentNullException(nameof(memberIds));
            
            try
            {
                var tasks = memberIds?
                    .Select(memberId => ExecuteAsync<Members>(GetMemberRestRequest(memberId))).ToArray();
                Task.WaitAll(tasks);
                return tasks.Select(t => t.Result).Where(m => m.Member != null).Select(m => m.Member).ToList();
            }
            catch (Exception e)
            {
                throw new ExternalServiceException($"Error in retrieving members { string.Join(",", memberIds)} from Event API", e);
            }
        }

        private static RestRequest GetMemberRestRequest(int memberId)
        {
            var request = new RestRequest
            {
                Resource = $"membersdataplatform/services/mnis/members/query/id={memberId}"
            };
            request.AddHeader("Accept", "application/xml");

            return request;
        }
    }
}