using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ISIC_DATA.Models
{
    public class ISIC_DATARepository : allISIC_DATARepository
    {
        private ISIC_DATAEntities m_db = new ISIC_DATAEntities();

        public IEnumerable<IsicDog> GetIsicDogs()
        {
            return m_db.IsicDogs;
        }
        public void AddIsicDog(IsicDog dog)
        {
            m_db.IsicDogs.Add(dog);
        }
        public IEnumerable<ISDog> GetISDogs()
        {
            return m_db.ISDogs;
        }
        public void AddISDog(ISDog dog)
        {
            m_db.ISDogs.Add(dog);
        }
        public IEnumerable<DKDog> GetDKDogs()
        {
            return m_db.DKDogs;
        }
        public void AddDKDog(DKDog dog)
        {
            m_db.DKDogs.Add(dog);
        }



        public void SaveChanges()
        {
            m_db.SaveChanges();
        }

    }
}