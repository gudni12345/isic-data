using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISIC_DATA.Models;
using ISIC_DATA.Controllers;

namespace UnitTest_ISIC_DATA
{
    


    [TestClass]
    public class UnitTest1
    {
        public void createFakeData()
        {
            Dog grandFather1 = new Dog();


        }


        [TestMethod]
        public void TestMethod1()
        {
            createFakeData();
            
        }
    }
}
