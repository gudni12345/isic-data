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

        public ParentList(string name, string reg)
        {
            this.Name = name; this.Reg = reg; 
        }

        public ParentList(string name, string reg, bool commonAncestor)
        {
            this.Name = name; this.Reg = reg; this.CommonAncestor = commonAncestor;
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
    }

}