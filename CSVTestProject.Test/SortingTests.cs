using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CSVTestProject.BLL.Models;
using CSVTestProject.BLL.Providers;
using CSVTestProject.BLL.Interfaces;

namespace CSVTestProject.Test
{
    [TestClass]
    public class SortingTests
    {
        [TestMethod]
        public void TestGetPersonNameFrequency()
        {
            List<IImportable> testPeople = GetTestPeople();
            using (PersonProvider provider = new PersonProvider())
            {
                List<IExportable> nameFrequencies = provider.GetPersonNameFrequency(testPeople);
                Assert.IsTrue(
                        (
                        nameFrequencies[0].Frequency == 2 && nameFrequencies[0].Name == "Bauling" &&
                        nameFrequencies[1].Frequency == 2 && nameFrequencies[1].Name == "Simon" &&
                        nameFrequencies[2].Frequency == 1 && nameFrequencies[2].Name == "Chipmunk" &&
                        nameFrequencies[3].Frequency == 1 && nameFrequencies[3].Name == "John" &&
                        nameFrequencies[4].Frequency == 1 && nameFrequencies[4].Name == "Jones" &&
                        nameFrequencies[5].Frequency == 1 && nameFrequencies[5].Name == "Leonard" &&
                        nameFrequencies[6].Frequency == 1 && nameFrequencies[6].Name == "Paul" &&
                        nameFrequencies[7].Frequency == 1 && nameFrequencies[7].Name == "Sarah"
                        )
                    );
            }
        }

        [TestMethod]
        public void TestGetAddressListFromPeopleByStreetName()
        {
            List<IImportable> testPeople = GetTestPeople();
            using (PersonProvider provider = new PersonProvider())
            {
                List<IExportable> addresses = provider.GetAddressListFromPeopleByStreetName(testPeople);
                Assert.IsTrue(
                        (
                        addresses[0].Name == "15b Albatros Street, Fourways" &&
                        addresses[1].Name == "1 Jacana Street, Fourways" && 
                        addresses[2].Name == "1 Jacana Street, Fourways" &&
                        addresses[3].Name == "20 Pelican Drive, Fourways" &&
                        addresses[4].Name == "102 Swallow Crescent, Fourways"
                        )
                    );
            }
        }

        #region Mock test data to use in test methods

        internal List<IImportable> GetTestPeople()
        {
            List<IImportable> testPeople = new List<IImportable>();

            #region Add test people

            testPeople.Add(new Person() { FirstName = "Leonard", LastName = "Bauling", Address = "1 Jacana Street, Fourways", PhoneNumber = "123456789" });
            testPeople.Add(new Person() { FirstName = "Sarah", LastName = "Bauling", Address = "1 Jacana Street, Fourways", PhoneNumber = "234567891" });
            testPeople.Add(new Person() { FirstName = "John", LastName = "Jones", Address = "15b Albatros Street, Fourways", PhoneNumber = "345678912" });
            testPeople.Add(new Person() { FirstName = "Paul", LastName = "Simon", Address = "20 Pelican Drive, Fourways", PhoneNumber = "456789123" });
            testPeople.Add(new Person() { FirstName = "Simon", LastName = "Chipmunk", Address = "102 Swallow Crescent, Fourways", PhoneNumber = "567891234" });

            #endregion

            return testPeople;
        }

        #endregion
    }
}
