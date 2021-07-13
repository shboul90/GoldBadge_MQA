using Claim.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claim.Repos
{
    public class ClaimRepo
    {
        private readonly Queue<Claims> _claimsQueue = new Queue<Claims>();

        public bool AddClaimToQueue(Claims claim)
        {
            if (claim is null)
            {
                return false;
            }
            else
            {
                _claimsQueue.Enqueue(claim);
                return true;
            }
        }
        public bool RemoveClaimFromQueue()
        {
            if (_claimsQueue is null || _claimsQueue.Count==0)
            {
                return false;
            }
            else
            {
                Claims remove = _claimsQueue.Dequeue();
                return true;
            }
        }

        public Queue<Claims> GetClaimsQueue()
        {
            return _claimsQueue;
        }
    }
}
