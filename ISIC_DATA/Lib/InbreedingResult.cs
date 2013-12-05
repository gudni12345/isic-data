using System;
using System.Collections.Generic;

namespace ISIC_DATA.Lib
{

    public class InbreedingResult
    {
        public InbreedingResult()
        {

        }

        public InbreedingResult( int Id,double value )
        {
            this.Value = value; this.Id = Id; 
        }

        public double Value
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;

        }

        public string Name
        {
            get;
            set;
        }
    }

}