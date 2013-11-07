using System.Collections.Generic;
using ISIC_DATA.Models;

namespace ISIC_DATA.Models
{   
    public class DogViewModel
    {
        public DogViewModel()
        {
            this.Dogs = new List<Dog>() { new Dog() };            
        }

        public Litter Litter { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}