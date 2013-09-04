using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ISIC_DATA.Models
{
    public interface allISIC_DATARepository 
    {
        IEnumerable<IsicDog> GetIsicDogs();
       // void AddIsicDog(IsicDog dog);

        void SaveChanges();
    }
}