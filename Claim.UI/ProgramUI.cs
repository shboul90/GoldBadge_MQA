using Claim.models;
using Claim.models.Enumerations;
using Claim.Repos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claim.UI
{
    class ProgramUI
    {
        private ClaimRepo _claimsItemsRepo = new ClaimRepo();

        public void Run()
        {
            SeedContentList();
            Menu();
        }

        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Choose a menu item:\n" +
                    "1. See all claims\n" +
                    "2. Take care of next claim\n" +
                    "3. Enter a new claim\n" +
                    "4. Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        SeeAllClaims();
                        break;

                    case "2":
                        TakeCareOfClaim();
                        break;

                    case "3":
                        EnterNewClaim();
                        break;

                    case "4":
                        Console.WriteLine("Goodbye!");
                        keepRunning = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;
                }

                Console.WriteLine("Please press any key to continue... ");
                Console.ReadLine();
                Console.Clear();
            }
        }
        private void EnterNewClaim()
        {
            Console.Clear();
            Queue<Claims> _QueueOfAllClaims = _claimsItemsRepo.GetClaimsQueue();
            Claims cItemUser = new Claims();

            Console.WriteLine("Enter the claim id: ");

            bool user = true;
            while (user)
            {
                string userIdAsString = Console.ReadLine();
                int num = -1;

                if (!int.TryParse(userIdAsString, out num))
                {
                    Console.WriteLine("Please enter an integer:");
                    user = true;
                }
                else
                {
                    cItemUser.ClaimID = int.Parse(userIdAsString);
                    user = false;
                }
            }

            Console.WriteLine("Enter the claim type: ");
            string userClaimTypeAsString = Console.ReadLine();

            if (userClaimTypeAsString.ToLower() == "car")
            {
                cItemUser.ClaimType = ClaimType.Car;
            }
            else if (userClaimTypeAsString.ToLower() == "house" || userClaimTypeAsString.ToLower() == "home")
            {
                cItemUser.ClaimType = ClaimType.Home;
            }
            else if (userClaimTypeAsString.ToLower() == "theft" || userClaimTypeAsString.ToLower() == "robbery")
            {
                cItemUser.ClaimType = ClaimType.Theft;
            }
            else
            {
                cItemUser.ClaimType = ClaimType.Other;
            }

            Console.WriteLine("Enter a claim description: ");
            cItemUser.Description = Console.ReadLine();

            Console.WriteLine("Amount of Damage: ");

            bool userIn = true;
            while (userIn)
            {
                string userAmountAsString = Console.ReadLine();
                string[] charsToRemove = new string[] { "$", " " };

                foreach (var c in charsToRemove)
                {
                    userAmountAsString = userAmountAsString.Replace(c, string.Empty);
                }

                decimal num = -0.5m;

                if (!decimal.TryParse(userAmountAsString, out num))
                {
                    Console.WriteLine("Please enter a valid value: ");
                    userIn = true;
                }
                else
                {
                    cItemUser.ClaimAmount = decimal.Parse(userAmountAsString);
                    userIn = false;
                }
            }

            Console.WriteLine("Date Of Accident: ");

            bool userInput = true;
            while (userInput)
            {
                string userDateOfAccidentAsString = Console.ReadLine();
                DateTime dateCheck = new DateTime();

                if (!DateTime.TryParse(userDateOfAccidentAsString, out dateCheck))
                {
                    Console.WriteLine("Invalid date, please retry: ");
                    userInput = true;
                }
                else
                {
                    DateTime userDateOfAccident = DateTime.Parse(userDateOfAccidentAsString);
                    if(userDateOfAccident>DateTime.Now)
                    {
                        Console.WriteLine("You can not enter a future date, please retry: ");
                        userInput = true;
                    }
                    else
                    {
                        cItemUser.DateOfIncident = userDateOfAccident;
                        userInput = false;
                    }
                }
            }

            Console.WriteLine("Date of Claim: ");

            bool userInputFinal = true;
            while (userInputFinal)
            {
                string userDateOfClaimAsString = Console.ReadLine();
                DateTime dateCheck = new DateTime();

                if (!DateTime.TryParse(userDateOfClaimAsString, out dateCheck))
                {
                    Console.WriteLine("Invalid date, please retry: ");
                    userInputFinal = true;
                }
                else
                {
                    DateTime userDateOfClaim = DateTime.Parse(userDateOfClaimAsString);
                    if (userDateOfClaim < cItemUser.DateOfIncident)
                    {
                        Console.WriteLine("The claim date can not be before the accident date, please retry: ");
                        userInputFinal = true;
                    }
                    else
                    {
                        cItemUser.DateOfClaim = userDateOfClaim;
                        userInputFinal = false;
                    }
                }
            }

            _claimsItemsRepo.AddClaimToQueue(cItemUser);
 
            Console.WriteLine($"This clam is {cItemUser.IsValid}");
        }  
    
        private void TakeCareOfClaim()
        {
            Console.Clear();
            Queue<Claims> _QueueOfAllClaims = _claimsItemsRepo.GetClaimsQueue();
            String[] header = { "ClaimID", "Type", "Description", "Amount", "DateOfAccident", "DateOfClaim", "IsValid" };

            Console.WriteLine("Here are the details for the next claim to be handled:\n\n" +
                $"{header[0]}: {_QueueOfAllClaims.Peek().ClaimID}\n\n" +
                $"{header[1]}: {_QueueOfAllClaims.Peek().ClaimType}\n\n" +
                $"{header[2]}: {_QueueOfAllClaims.Peek().Description}\n\n" +
                $"{header[3]}: ${_QueueOfAllClaims.Peek().ClaimAmount}\n\n" +
                $"{header[4]}: {_QueueOfAllClaims.Peek().DateOfIncident.ToString("M/dd/yyyy")}\n\n" +
                $"{header[5]}: {_QueueOfAllClaims.Peek().DateOfClaim.ToString("M/dd/yyyy")}\n\n" +
                $"{header[6]}: {_QueueOfAllClaims.Peek().IsValid}\n\n" +
                $"Do you want to deal with this claim now(y/n)?");
            bool user = true;
            while (user)
            {
                string userInput = Console.ReadLine();
                if (userInput.ToLower()=="y")
                {
                    _claimsItemsRepo.RemoveClaimFromQueue();
                    user = false;
                }
                else if(userInput.ToLower() == "n")
                {
                    user = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid entry: (y/n)?");
                    user = true;
                }
            }

        }

        private void SeeAllClaims()
        {
            Console.Clear();
            Queue<Claims> _QueueOfAllClaims = _claimsItemsRepo.GetClaimsQueue();
            String[] header = { "ClaimID", "Type", "Description", "Amount", "DateOfAccident", "DateOfClaim", "IsValid" };

            int[] columnWidth = TableBuilder(header, _QueueOfAllClaims);
            string[] widthH =
            {
                new string(' ', columnWidth[0] - header[0].Length),
                new string(' ', columnWidth[1] - header[1].Length),
                new string(' ', columnWidth[2] - header[2].Length),
                new string(' ', columnWidth[3] - header[3].Length),
                new string(' ', columnWidth[4] - header[4].Length),
                new string(' ', columnWidth[5] - header[5].Length),
            };

            Console.WriteLine($"{header[0]}   " + $"{widthH[0]}" +
                $"{header[1]}   " + $"{widthH[1]}" +
                $"{header[2]}   " + $"{widthH[2]}" +
                $"{header[3]}   " + $"{widthH[3]}" +
                $"{header[4]}   " + $"{widthH[4]}" +
                $"{header[5]}   " + $"{widthH[5]}" +
                $"{header[6]}");

            foreach (Claims content in _QueueOfAllClaims)
            {
                string[] widthC =
                {
                new string(' ', columnWidth[0] - content.ClaimID.ToString().Length),
                new string(' ', columnWidth[1] - content.ClaimType.ToString().Length),
                new string(' ', columnWidth[2] - content.Description.Length),
                new string(' ', columnWidth[3] - content.ClaimAmount.ToString().Length),
                new string(' ', columnWidth[4] - content.DateOfIncident.ToString("M/dd/yyyy").Length),
                new string(' ', columnWidth[5] - content.DateOfClaim.ToString("M/dd/yyyy").Length),
                };

                Console.WriteLine($"{content.ClaimID}   " + $"{widthC[0]}" +
                    $"{content.ClaimType}   " + $"{widthC[1]}" +
                    $"{content.Description}   " + $"{widthC[2]}" +
                    $"${content.ClaimAmount}  " + $"{widthC[3]}" +
                    $"{content.DateOfIncident.ToString("M/dd/yyyy")}   " + $"{widthC[4]}" +
                    $"{content.DateOfClaim.ToString("M/dd/yyyy")}   " + $"{widthC[5]}" +
                    $"{content.IsValid}" );
            }
        }

        private int[] TableBuilder(string[] header, Queue<Claims> _claims)
        {
            int[] tableWidth =
{
            header[0].Length,
            header[1].Length,
            header[2].Length,
            header[3].Length,
            header[4].Length,
            header[5].Length,
            };

            foreach (Claims content in _claims)
            {
                if (content.ClaimID.ToString().Length > tableWidth[0])
                {
                    tableWidth[0] = content.ClaimID.ToString().Length + 1;
                }

                if (content.ClaimType.ToString().Length > tableWidth[1])
                {
                    tableWidth[1] = content.ClaimType.ToString().Length + 1;
                }

                if (content.Description.Length > tableWidth[2])
                {
                    tableWidth[2] = content.Description.Length + 1;
                }

                if (content.ClaimAmount.ToString().Length > tableWidth[3])
                {
                    tableWidth[3] = content.ClaimAmount.ToString().Length + 1;
                }

                if (content.DateOfIncident.ToString("M/dd/yyyy").Length > tableWidth[4])
                {
                    tableWidth[4] = content.DateOfIncident.ToString("M/dd/yyyy").Length + 1;
                }

                if (content.DateOfClaim.ToString("M/dd/yyyy").Length > tableWidth[5])
                {
                    tableWidth[5] = content.DateOfClaim.ToString("M/dd/yyyy").Length + 1;
                }
            }

            return tableWidth;
        }

        private void SeedContentList()
        {
            Claims cItem1 = new Claims(3, ClaimType.Car, "Crashed into light pole", 32000m, new DateTime(2019, 11, 12), new DateTime(2021, 7, 8));
            Claims cItem2 = new Claims(5, ClaimType.Theft, "They broke in and got everything....", 1200000m, new DateTime(2021, 7, 2), new DateTime(2021, 7, 8));

            _claimsItemsRepo.AddClaimToQueue(cItem1);
            _claimsItemsRepo.AddClaimToQueue(cItem2);
        }
    }
}
