using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIC_DATA.Models;

namespace UnitTest_ISIC_DATA.Fakes
{
    class FakePedigree  // Used for Testing TestmateController
    {
        public static List<Dog> CreateTestDogs()
        {
            List<Dog> dogs = new List<Dog>();

            Dog rakki = new Dog() { Id = 1, Name = "Rakki_Founder", Reg = "IS1", Sex = "M", LitterId = 0 };
            Dog tik = new Dog() { Id = 2, Name = "Tík_Founder", Reg = "IS2", Sex = "M", LitterId = 0 };
            Dog sampleDog = new Dog() { Id=3, Name = "Rakki", Reg = "IS3", Sex = "M", Father = new List<Litter>(), Mother = new List<Litter>()};

            Litter litter = new Litter() { Id = 1, FatherId = 1, MotherId = 2 };
            sampleDog.LitterId = litter.Id;

            sampleDog.Father.Add(litter);
            sampleDog.Mother.Add(litter);
            sampleDog.LitterId = 1;

            dogs.Add(rakki);
            dogs.Add(tik);
            dogs.Add(sampleDog);

            return dogs;
        }
    }
}
