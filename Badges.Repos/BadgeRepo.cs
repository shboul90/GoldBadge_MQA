using Badges.Models;
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

        public bool AddBadgeToDictionary(Badge badge)
        {
            if (badge is null)
            {
                return false;
            }
            else
            {
                _badgeDictionary.Add(badge.BadgeID, badge.DoorsAccessible);
                return true;
            }
        }

        public bool AddDoorToBadge(int badgeID, string doorToAdd)
        {
            int initialDoorCount;

            if (IsBadgeIDPresent(badgeID) is false)
            {
                return false;
            }
            else
            {
                initialDoorCount = _badgeDictionary[badgeID].Count;

                _badgeDictionary[badgeID].Add(doorToAdd);

                return _badgeDictionary[badgeID].Count > initialDoorCount;
            }
        }

        public bool RemoveDoorFromBadge(int badgeID, string doorToRemove)
        {
            int initialDoorCount;

            if (IsBadgeIDPresent(badgeID) is false && IsDoorPresent(badgeID,doorToRemove) is false)
            {
                return false;
            }
            else
            {
                initialDoorCount = _badgeDictionary[badgeID].Count;

                _badgeDictionary[badgeID].Remove(doorToRemove);

                return _badgeDictionary[badgeID].Count < initialDoorCount;
            }
        }

        public bool RemoveAllDoorsFromBadge(int badgeID)
        {
            if (IsBadgeIDPresent(badgeID) is false)
            {
                return false;
            }
            else
            {
                _badgeDictionary[badgeID].Clear();

                return _badgeDictionary[badgeID].Count == 0;
            }
        }

        public bool IsBadgeIDPresent(int badgeID)
        {
            if (_badgeDictionary.ContainsKey(badgeID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsDoorPresent(int badgeID, string door)
        {
            if (_badgeDictionary.ContainsKey(badgeID) && _badgeDictionary[badgeID].Contains(door))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Dictionary<int, List<string>> GetListOfBadges()
        {
            return _badgeDictionary;
        }
    }
}
