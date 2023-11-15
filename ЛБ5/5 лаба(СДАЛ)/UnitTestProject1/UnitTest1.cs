using Microsoft.VisualStudio.TestTools.UnitTesting; 
using ClassLibrary1; 
using System.Data.SqlClient;

// Объявление пространства имен UnitTestProject
namespace UnitTestProject
{
    // Объявление класса UnitTests, который будет содержать тесты
    [TestClass]
    public class UnitTests
    {
        // Объявление переменной класса Class1
        Class1 class1;

        // Атрибут [TestInitialize] указывает, что данный метод будет выполнен перед каждым тестом
        [TestInitialize]
        public void TestInitialize()
        {
            // Создание экземпляра класса Class1 и присваивание его переменной class1
            class1 = new Class1();
        }

        // Атрибут [TestCleanup] указывает, что данный метод будет выполнен после каждого теста
        [TestCleanup]
        public void TestCleanup()
        {
            // Закрытие соединения, вызов метода closeConnection() на экземпляре класса Class1
            class1.closeConnection();
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestOpenConnection()
        {
            // Вызов метода openConnection() на экземпляре класса Class1
            class1.openConnection();

            // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
            SqlConnection connection = class1.getConnection();

            // Проверка, что состояние соединения равно открыто
            Assert.AreEqual(System.Data.ConnectionState.Open, connection.State);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestCloseConnection()
        {
            // Открытие соединения, вызов метода openConnection() на экземпляре класса Class1
            class1.openConnection();

            // Закрытие соединения, вызов метода closeConnection() на экземпляре класса Class1
            class1.closeConnection();

            // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
            SqlConnection connection = class1.getConnection();

            // Проверка, что состояние соединения равно закрыто
            Assert.AreEqual(System.Data.ConnectionState.Closed, connection.State);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestGetConnection()
        {
            // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
            SqlConnection connection = class1.getConnection();

            // Проверка, что соединение не является null
            Assert.IsNotNull(connection);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestOpenConnectionTwice()
        {
            // Открытие соединения два раза
            class1.openConnection();
            class1.openConnection();

            // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
            SqlConnection connection = class1.getConnection();

            // Проверка, что состояние соединения равно открыто
            Assert.AreEqual(System.Data.ConnectionState.Open, connection.State);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestCloseConnectionWithoutOpen()
        {
            // Закрытие соединения без предварительного открытия
            class1.closeConnection();

            // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
            SqlConnection connection = class1.getConnection();

            // Проверка, что состояние соединения равно закрыто
            Assert.AreEqual(System.Data.ConnectionState.Closed, connection.State);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestDifferentConnections()
        {
            // Создание нового экземпляра класса Class1
            Class1 class2 = new Class1();

            // Открытие соединений на двух разных экземплярах класса Class1
            class1.openConnection();
            class2.openConnection();

            // Получение соединений, вызов метода getConnection() на экземплярах класса Class1
            SqlConnection connection1 = class1.getConnection();
            SqlConnection connection2 = class2.getConnection();

            // Проверка, что соединения не являются одинаковыми
            Assert.AreNotSame(connection1, connection2);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestInvalidConnectionString()
        {
            // Создание нового экземпляра класса Class1
            Class1 class2 = new Class1();

            // Открытие соединения на неправильной строке подключения
            class2.openConnection();

            // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
            SqlConnection connection2 = class2.getConnection();

            // Проверка, что состояние соединения равно закрыто
            Assert.AreEqual(System.Data.ConnectionState.Closed, connection2.State);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestInvalidCommand()
        {
            // Открытие соединения, вызов метода openConnection() на экземпляре класса Class1
            class1.openConnection();

            // Создание неверной SQL-команды
            SqlCommand command = new SqlCommand("INVALID COMMAND", class1.getConnection());

            // Выполнение команды на чтение данных
            SqlDataReader reader = command.ExecuteReader();

            // Проверка, что у команды нет строк с данными
            Assert.IsFalse(reader.HasRows);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestNullConnection()
        {
            // Закрытие соединения, вызов метода closeConnection() на экземпляре класса Class1
            class1.closeConnection();

            // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
            SqlConnection connection = class1.getConnection();

            // Проверка, что соединение является null
            Assert.IsNull(connection);
        }
        // Метод [TestMethod] указывает, что данный метод является тестом
        [TestMethod]
        public void TestValidCommand()
        {
            // Открытие соединения, вызов метода openConnection() на экземпляре класса Class1
            class1.openConnection();

            // Создание верной SQL-команды
            SqlCommand command = new SqlCommand("Select * from register", class1.getConnection());

            // Выполнение команды на чтение данных
            SqlDataReader reader = command.ExecuteReader();

            // Проверка, что у команды есть строки с данными
            Assert.IsTrue(reader.HasRows);
        }

        // Метод [TestMethod] указывает, что данный метод является тестом
        //[TestMethod]
        //public void TestConnectionTimeout()
        //{
        //    // Открытие соединения на некорректной строке подключения с неправильно настроенным таймаутом
        //    class1.openConnection();

        //    // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
        //    SqlConnection connection = class1.getConnection();

        //    // Проверка, что состояние соединения равно закрыто
        //    Assert.AreEqual(System.Data.ConnectionState.Closed, connection.State);
        //}

        // Метод [TestMethod] указывает, что данный метод является тестом
        //[TestMethod]
        //public void TestUnsupportedDatabase()
        //{
        //    // Открытие соединения на несуществующей базе данных
        //    class1.openConnection();

        //    // Получение соединения, вызов метода getConnection() на экземпляре класса Class1
        //    SqlConnection connection = class1.getConnection();

        //    // Проверка, что состояние соединения равно закрыто
        //    Assert.AreEqual(System.Data.ConnectionState.Closed, connection.State);
        //}
    }
}