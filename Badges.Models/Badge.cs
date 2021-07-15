using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges.Models
{
    public class Badge
    {
        public int BadgeID { get; set; }

        public List<string> DoorsAccessible { get; set; }
        
        public Badge()
        {

        }

        public Badge(int badgeID, List<string> doorsAccessible)
        {
            BadgeID = badgeID;
            DoorsAccessible = doorsAccessible;
        }
    }
}
