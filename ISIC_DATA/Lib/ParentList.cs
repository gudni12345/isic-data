using System;
using System.Collections.Generic;

namespace ISIC_DATA.Lib
{

    // List of dogs in pedigree for Inbreeding Testmate.
    public class ParentList
    {
        public ParentList()
        {
        }

        public ParentList(string name, string reg, string hd)
        {
            this.Name = name; this.Reg = reg; this.HD = hd; 
        }

        public ParentList(string name, string reg, bool commonAncestor, string hd)
        {
            this.Name = name; this.Reg = reg; this.CommonAncestor = commonAncestor; this.HD = hd;
        }
       
        public string Name
        {
            get;
            set;
        }

        public string Reg
        {
            get;
            set;
        }

        public bool CommonAncestor
        {
            get;
            set;
        }
        public string HD
        {
            get;
            set;
        }
    }

}