using System;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDB.DAL;
using ProjectDB.Models;

namespace ProjectDBIntegrationTests
{
    [TestClass]
    public class DepartmentSqlDALTests
    {
        private TransactionScope tran;      //<-- used to begin a transaction during initialize and rollback during cleanup
        private string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security = True";
        private Department _department;
        private DepartmentSqlDAL _departmentItem;

        [TestInitialize]
        public void Initialize()
        {            

            
            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();

            _departmentItem = new DepartmentSqlDAL(_connectionString);

            _department = new Department();

            _department.Name = "A Department";

            _department.Id = _departmentItem.CreateDepartment(_department);

            
        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }

        [TestMethod]
        public void GetSingleDepartmentTest()
        {
            Department getDepartment = _departmentItem.GetSinlgeDepartment(_department.Id);

            Assert.AreEqual(_department.Name, getDepartment.Name);

            Assert.AreEqual(_department.Id, getDepartment.Id);
        }

        [TestMethod]
        public void UpdateDepartmentsTests()
        {
            _department.Name = "New Name";

            _departmentItem.UpdateDepartment(_department);

            Department getDepartment = _departmentItem.GetSinlgeDepartment(_department.Id);

            Assert.AreEqual(_department.Name, getDepartment.Name);

            Assert.AreEqual(_department.Id, getDepartment.Id);
        }
    }
}
