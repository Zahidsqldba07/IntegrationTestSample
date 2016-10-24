using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace GPSNET.IntegrationTest
{
    [TestClass]
    public class BaseIntegrationTest
    {
        private TransactionScope scope;
                
        [TestInitialize]
        public void TestInit()
        {
            this.scope = new TransactionScope();
            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.scope.Dispose();
        }


        

    }
}
