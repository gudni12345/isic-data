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

            Person person = new Person() { Id = 1, Name = "Jón Jónsson" };
            Dog rakki = new Dog() { Id = 1, Name = "Rakki_Founder", Reg = "IS1", Sex = "M", LitterId = 1 };
            Dog tik = new Dog() { Id = 2, Name = "Tík_Founder", Reg = "IS2", Sex = "M", LitterId = 1 };
            dogs.Add(rakki);
            dogs.Add(tik);

            Litter litter1 = new Litter() { Id = 1, FatherId = 1, MotherId = 2, PersonId = 1 };
            Litter litter2 = new Litter() { Id = 2, FatherId = 1, MotherId = 2, PersonId = 1 };  
            Dog hvolpur1 = new Dog() { Id = 3, Name = "Hvolpur1", Reg = "IS3", Sex = "M", LitterId = 2 };
            Dog hvolpur2 = new Dog() { Id = 4, Name = "Hvolpur2", Reg = "IS4", Sex = "F", LitterId = 2 };
            dogs.Add(hvolpur1);
            dogs.Add(hvolpur2);

            return dogs;
        }
    }
}
