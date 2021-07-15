using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges.Repos
{
    public class BadgeRepo
    {
        private readonly Dictionary<int, List<string>> _badgeDictionary = new Dictionary<int, List<string>>();

        public bool AddBadgeToDictionary(int badgeID, List<string> listOfDoors)
        {
            if (badgeID == 0 || listOfDoors is null)
            {
                return false;
            }
            else
            {
                _badgeDictionary.Add(badgeID, listOfDoors);
                return true;
            }
        }

        public Dictionary<int, List<string>> GetBadgesByID(int badgeID)
        {
            return _badgeDictionary;
        }

        public Dictionary<int, List<string>> GetListOfBadges()
        {
            return _badgeDictionary;
        }
    }
}
