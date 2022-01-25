using System.Collections.Generic;

namespace HousingRepairsOnlineApi.Helpers
{
    public class SoREngine : ISoREngine
    {
        private readonly IDictionary<string, IDictionary<string, dynamic>> soRMapping;

        public SoREngine(IDictionary<string, IDictionary<string, dynamic>> soRMapping)
        {
            this.soRMapping = soRMapping;
        }

        public string MapSorCode(string location, string problem, string issue)
        {
            string result;
            if (issue is null)
            {
                result = soRMapping[location][problem];
                return result;
            }

            result = soRMapping[location][problem][issue];

            return result;
        }
    }
}
