using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;

namespace GPSNET.IntegrationTest
{
    [TestClass]
    public class BaseIntegrationTest
    {
        private TransactionScope scope;
        private string connectionString = @"Data Source = localhost;Initial Catalog = IntegrationTestDB; Integrated Security = true; Connection Timeout = 10;";

        [TestInitialize]
        public void TestInit()
        {
            this.scope = new TransactionScope();
            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.scope.Dispose();            
            SqlConnection conn = new SqlConnection(connectionString);
            var cmdText = @"exec sp_MSforeachtable @command1 = 'DBCC CHECKIDENT(''?'', RESEED,1)';
                            exec sp_MSforeachtable @command1 = 'DBCC CHECKIDENT (''?'', RESEED)';";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Dispose();
        }


        

    }
}
