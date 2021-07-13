using Claim.models;
using Claim.models.Enumerations;
using Claim.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Claim.Tests
{
    [TestClass]
    public class ClaimsRepoTest
    {
        private readonly ClaimRepo _repo = new ClaimRepo();

        [TestInitialize]
        public void Arrange()
        {
            Claims cItem1 = new Claims(3, ClaimType.Car,"Crashed into light pole", 32000m,new DateTime(2019,6,12), new DateTime(2021, 7, 8));
            Claims cItem2 = new Claims(5, ClaimType.Theft, "They broke in and got everything....", 1200000m, new DateTime(2021, 7, 2), new DateTime(2021, 7, 8));
            _repo.AddClaimToQueue(cItem1);
            _repo.AddClaimToQueue(cItem2);
        }

        [TestMethod]
        public void AddClaimToQueue_ClaimIsNull_ReturnFalse()
        {
            Claims cItem3 = null;

            bool result = _repo.AddClaimToQueue(cItem3);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddClaimToQueue_ClaimIsNotNull_ReturnTrue()
        {
            Claims cItem3 = new Claims(12, ClaimType.Car, "Crashed into wall", 32000m, new DateTime(2021, 7, 6), new DateTime(2021, 7, 8));

            bool result = _repo.AddClaimToQueue(cItem3);

            Assert.IsTrue(result);
            Assert.AreEqual(true, cItem3.IsValid);
        }

        [TestMethod]
        public void RemoveClaimFromQueue_ClaimsQueueIsNull_ReturnFalse()
        {
            ClaimRepo _repo1 = new ClaimRepo();

            bool result = _repo.RemoveClaimFromQueue(_repo1.GetClaimsQueue());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveClaimFromQueue_ClaimsQueueIsNotNull_ReturnTrue()
        {
            bool result = _repo.RemoveClaimFromQueue(_repo.GetClaimsQueue());

            Assert.IsTrue(result);
        }
    }
}
