using System.Collections.Generic;
using UKPBS.Domain.Entities.ParliamentMembers;

namespace UKPBS.Services.ApiClients
{
    public interface IMemberApiClient
    {
        IEnumerable<Member> GetMembersByIds(int[] memberIds);
    }
}