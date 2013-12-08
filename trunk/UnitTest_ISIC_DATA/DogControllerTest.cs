using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISIC_DATA.Models;
using ISIC_DATA.Controllers;
using System.Collections.Generic;
using ISIC_DATA.DataAccess;
using UnitTest_ISIC_DATA.Fakes;
using ISIC_DATA.Tests.Fakes;
using System.Web.Mvc;

namespace UnitTest_ISIC_DATA
{
    
    [TestClass]
    public class DogControllerTest
    {
        List<Dog> CreateFakeDogs() 
        {
            List<Dog> fakeDogs = new List<Dog>();
            Dog dog  = new Dog { Id=1, Name = "Rakki1", Reg = "IS1", Sex = "M", LitterId=1 };
            Dog dog2 = new Dog { Id=2, Name = "Rakki2", Reg = "IS2", Sex = "M", LitterId=2 };
            fakeDogs.Add(dog);
            fakeDogs.Add(dog2);

            return fakeDogs;
        }

        [TestMethod]
        public void Test_Dog_Model()
        {            
            Dog dogcorrect = new Dog { Name = "Rakki", Reg = "IS1", Sex = "M" };
            Dog dogincorrect = new Dog { Name = "Rakki", Reg = "IS1" }; //missing Gender
       
            Assert.AreEqual(dogcorrect.Name, "Rakki");
        }


        DogController CreateDogController()
        {
            var testData = FakePedigree.CreateTestDogs();
            var repository = new FakeDogRepository(testData);

            return new DogController(repository);
        }

        [TestMethod]
        public void DogTest()
        {
            var controller = CreateDogController();
            var testData = FakePedigree.CreateTestDogs();
            FakeDogRepository fakeDb = new FakeDogRepository(testData);

            int numberOfDogs = fakeDb.NumberOfDogs();           
            Dog dog = fakeDb.Find(3);

            Assert.AreEqual(numberOfDogs, 4);
            Assert.AreEqual(dog.Name, "Hvolpur1");
            Assert.AreEqual(dog.LitterId, 2);                     
        }

        [TestMethod]
        public void Pedigree_ValidView()
        {
            var controller = CreateDogController();
            var testData = FakePedigree.CreateTestDogs();
            FakeDogRepository fakeDb = new FakeDogRepository(testData);
            var result = controller.Pedigree() as ViewResult;
            Assert.IsNotNull(result);            
        }
    }
}

