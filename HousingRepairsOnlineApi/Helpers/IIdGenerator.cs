using System;

namespace HousingRepairsOnlineApi.Helpers
{
    public interface IIdGenerator
    {
        public Guid Generate();
    }
}
