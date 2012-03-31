using Microsoft.VisualStudio.TestTools.UnitTesting;
using MitraTech.Core;
using MitraTech.Core.Mapper;
using MitraTech.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace ServiceTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class StudentServiceTest
    {
        private enum mode
        {
            SetUp,
            TearDown
        }
       
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestDoSave()        
        {
            try
            {
                List<Student> test = new List<Student>();

                test.Add(new Student
                {
                    StudentName = "StudentName1",
                    StudentID = "S00001",
                    BirthDate = DateTime.Parse("#01/01/2000#"),
                    BirthPlace = "Denpasar",
                    Class = "Class1",
                    School = "ST. Yosef",
                    SexType = Student.Sex.Female,
                    RowAction = RowActions.New
                });

                test.Add(new Student
                {
                    StudentName = "StudentName2",
                    StudentID = "S00002",
                    BirthDate = DateTime.Parse("#01/02/2002#"),
                    BirthPlace = "Denpasar",
                    Class = "Class3",
                    School = "ST. Yosef",
                    SexType = Student.Sex.Male,
                    RowAction = RowActions.New
                });

                test.Add(new Student
                {
                    StudentName = "StudentName3",
                    StudentID = "S00003",
                    BirthDate = DateTime.Parse("#01/03/2001#"),
                    BirthPlace = "Yogyakarta",
                    Class = "Class2",
                    School = "ST. Yosef",
                    SexType = Student.Sex.Female,
                    RowAction = RowActions.New
                });

                test.Add(new Student
                {
                    StudentName = "StudentName4",
                    StudentID = "S00004",
                    BirthDate = DateTime.Parse("#04/01/2002#"),
                    BirthPlace = "Boston",
                    Class = "Class2",
                    School = "ST. Yosef",
                    SexType = Student.Sex.Male,
                    RowAction = RowActions.New
                });

                StudentService.StudentService service = new StudentService.StudentService();
                List<Student> result = service.DoSave(test);

                Assert.IsFalse(service.Errors.IsError(),  "There is unexpected error: " + string.Join(Environment.NewLine, service.Errors.Message.ToArray()));
                CollectionAssert.AreEquivalent(test, result, "result not equal");
            }

            finally
            {
                SetupEnvironment(mode.TearDown);
            }
        }

        private void SetupEnvironment(mode setupMode)
        {
            if (setupMode == mode.TearDown)            
            {
                string query = "DELETE FROM STUDENT";
                DBConnectionObject con = new DBConnectionObject();
                con.ExecuteNonQuery(new QueryParameter { commandText = query, parameters = new List<SqlParameter>() });
            }
        }
    }
}
