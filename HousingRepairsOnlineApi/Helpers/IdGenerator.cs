using System;

namespace HousingRepairsOnlineApi.Helpers
{
    public class IdGenerator : IIdGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
