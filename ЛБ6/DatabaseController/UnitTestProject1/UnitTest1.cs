using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseController;
namespace UnitTestProject1
{


    
    public class TestDatabase
    {
        private string sqlstr = @"Data Source=adclg1;Initial Catalog=LB_5_Гойсултанов;Integrated Security=true;";
        private Database database;
        public TestDatabase()
        {
            database = Database.GetInstance(sqlstr);
        }
        [TestMethod]
        public void TestExecuteSqlScript()
        {
            object result = database.GetScalar("SELECT 124");
            Assert.AreEqual(result.ToString(), "124");
        }
        [TestMethod]
        public void ExecuteCommandWithParameters()
        {
            string query = "INSERT INTO users (login, password) VALUES (@login, @password)";
            Parameter[] par ={ new Parameter("@login", "user1"),
                               new Parameter("@password", "12345")};
            int rows = database.Execute(query, par);
            Assert.AreEqual(rows, 1);

        }
        [TestMethod]
        public void GetScalarFromExistingTable()
        {
            string query = "SELECT COUNT(*) FROM users";
            object result = database.GetScalar(query);
            Assert.IsNotNull(
            result,
            "Не удалось получить число записей."
            );
        }
        [TestMethod]
        public void GetRowsFromDatabase()
        {
            string query = "SELECT id, login FROM users";
            object[][] result = database.GetRowsData(query);
            CollectionAssert.AllItemsAreNotNull(
            result,
            "Не удалось получить записи из таблицы."
            );
        }

    }
}
