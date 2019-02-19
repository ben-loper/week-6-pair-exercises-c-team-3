using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDB.DAL;
using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ProjectDBIntegrationTests
{
    [TestClass]
    public class EmployeeSqlDALTests
    {
        private TransactionScope tran;      //<-- used to begin a transaction during initialize and rollback during cleanup
        private string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security = True";
        private Employee _employee;
        private EmployeeSqlDAL _employeeItem;
        private DepartmentSqlDAL _departmentItem;

        private Department _department;

        [TestInitialize]
        public void Initialize()
        {
            _departmentItem = new DepartmentSqlDAL(_connectionString);

            _department = new Department();

            _department.Name = "Test Department";

            _department.Id = _departmentItem.CreateDepartment(_department);

            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();

            _employeeItem = new EmployeeSqlDAL(_connectionString);

            _employee = new Employee();

            _employee.DepartmentId = _department.Id;
            _employee.FirstName = "John";
            _employee.LastName = "Smith";
            _employee.Gender = "M";
            _employee.JobTitle = "Developer";
            _employee.BirthDate = new DateTime(1990, 01, 01);
            _employee.HireDate = new DateTime(2000, 02, 03);
            
            _employee.EmployeeId = _employeeItem.CreateEmployee(_employee);

            
        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }


        [TestMethod]
        public void CreateEmployeeTest()
        {

        }

        [TestMethod]
        public void GetSingleEmployeeTest()
        {
            Employee getEmployee = _employeeItem.GetSingleEmployee(_employee.EmployeeId);

            Assert.AreEqual(_employee.EmployeeId, getEmployee.EmployeeId);
            Assert.AreEqual(_employee.FirstName, getEmployee.FirstName);
            Assert.AreEqual(_employee.LastName, getEmployee.LastName);
            Assert.AreEqual(_employee.JobTitle, getEmployee.JobTitle);
            Assert.AreEqual(_employee.BirthDate, getEmployee.BirthDate);
            Assert.AreEqual(_employee.Gender, getEmployee.Gender);
            Assert.AreEqual(_employee.HireDate, getEmployee.HireDate);
            Assert.AreEqual(_employee.DepartmentId, getEmployee.DepartmentId);

        }
    }
}
