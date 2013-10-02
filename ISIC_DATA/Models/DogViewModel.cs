using System.Collections.Generic;
using ISIC_DATA.Models;

namespace ISIC_DATA.Models
{   
    public class DogViewModel
    {
        public DogViewModel()
        {
            this.DogAndPersons = new List<DogAndPerson>() { new DogAndPerson() };            
        }

        public Litter Litter { get; set; }
        public List<DogAndPerson> DogAndPersons { get; set; }

    }
}