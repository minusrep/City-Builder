using System.Collections.Generic;
using Runtime.Colony.Citizens;

namespace Tests.Buildings
{
    public sealed class CitizenNeedServiceMock : ICitizenNeedService
    {
        public readonly List<(int id, string resource)> Calls = new();

        public void RestoreNeed(int citizenId, string resourceKey)
        {
            Calls.Add((citizenId, resourceKey));
        }
    }
}