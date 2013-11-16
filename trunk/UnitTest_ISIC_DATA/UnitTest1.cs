using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISIC_DATA.Models;
using ISIC_DATA.Controllers;
using System.Collections.Generic;
using ISIC_DATA.DataAccess;
using UnitTest_ISIC_DATA.Fakes;
using ISIC_DATA.Tests.Fakes;

namespace UnitTest_ISIC_DATA
{
    
    [TestClass]
    public class UnitTest1
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
        public void TestMethod2()
        {
            var controller = CreateDogController();
            var testData = FakePedigree.CreateTestDogs();
            FakeDogRepository fakeDb = new FakeDogRepository(testData);

            int numberOfDogs = fakeDb.NumberOfDogs();
            Assert.AreEqual(numberOfDogs, 3);

            Dog dog = fakeDb.Find(3);
            Assert.AreEqual(dog.Name, "Rakki");
            Assert.AreEqual(dog.LitterId, 1);
          
            


        }
    }
}


/*     DogController CreateDogController()
        {
            var fakedb = new DogRepository(CreateFakeDogs());
            return new DogController(fakedb);
        }


        [TestMethod]
        public void TestMethod2()
        {
            var fakedb = new DogRepository(CreateFakeDogs());
            Dog dog = new Dog { Id =1, Name = "Rakki", Reg = "IS1", Sex = "M" };
            fakedb.
            
        }
    */