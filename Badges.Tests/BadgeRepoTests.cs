using Badges.Models;
using Badges.Repos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Badges.Tests
{
    [TestClass]
    public class BadgeRepoTests
    {

        private readonly BadgeRepo _repo = new BadgeRepo();

        [TestInitialize]
        public void Arrange()
        {
            int[] badgeIDs = { 1, 2, 3, 4, 5, 6, 7 };
            List<string> doorAccess1 = new List<string>() { "A1", "A2", "B3", "B4" };
            List<string> doorAccess2 = new List<string>() { "A4", "D2", "E6", "D8" };
            List<string> doorAccess3 = new List<string>() { "A6", "F2", "H3", "R5" };
            List<string> doorAccess4 = new List<string>() { "A7", "G2", "G5", "T33" };
            List<string> doorAccess5 = new List<string>() { "A8", "E2", "F4", "Y12" };
            List<string> doorAccess6 = new List<string>() { "C3", "A5", "S43", "Z6" };
            List<string> doorAccess7 = new List<string>() { "A1", "A12", "C3", "G5" };

            Badge badge1 = new Badge(badgeIDs[0], doorAccess1);
            Badge badge2 = new Badge(badgeIDs[1], doorAccess2);
            Badge badge3 = new Badge(badgeIDs[2], doorAccess3);
            Badge badge4 = new Badge(badgeIDs[3], doorAccess4);
            Badge badge5 = new Badge(badgeIDs[4], doorAccess5);
            Badge badge6 = new Badge(badgeIDs[5], doorAccess6);
            Badge badge7 = new Badge(badgeIDs[6], doorAccess7);

            _repo.AddBadgeToDictionary(badge1);
            _repo.AddBadgeToDictionary(badge2);
            _repo.AddBadgeToDictionary(badge3);
            _repo.AddBadgeToDictionary(badge4);
            _repo.AddBadgeToDictionary(badge5);
            _repo.AddBadgeToDictionary(badge6);
            _repo.AddBadgeToDictionary(badge7);
        }

        [TestMethod]
        public void AddBadgeToDictionary_BadgeIsNull_ReturnFalse()
        {
            Badge badge8 = null;

            bool result = _repo.AddBadgeToDictionary(badge8);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddBadgeToDictionary_BadgeIsNotNull_ReturnTrue()
        {
            List<string> doorAccess = new List<string>() { "A1", "A2", "B3", "B4" };

            Badge badge8 = new Badge(12, doorAccess);

            bool result = _repo.AddBadgeToDictionary(badge8);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddDoorToBadge_BadgeIDDoesntExist_ReturnFalse()
        {

            bool result = _repo.AddDoorToBadge(12,"E5");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddDoorToBadge_BadgeIDDoesExist_ReturnTrue()
        {

            bool result = _repo.AddDoorToBadge(1, "E5");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveDoorFromBadge_DoorDoesExist_ReturnTrue()
        {

            bool result = _repo.RemoveDoorFromBadge(1, "A1");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveDoorFromBadge_DoorDoesntExist_ReturnFalse()
        {

            bool result = _repo.RemoveDoorFromBadge(1, "E8");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveAllDoorsFromBadge_DoorsExist_ReturnTrue()
        {

            bool result = _repo.RemoveAllDoorsFromBadge(1);

            Assert.IsTrue(result);
        }
    }
}
