using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class EmployeeSqlDAL
    {
        private string connectionString;
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";


        // Single Parameter Constructor
        public EmployeeSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public int CreateEmployee(Employee newEmployee)
        {
            int result = 0;
            string SQLCreateNewDepartment = $"  INSERT INTO employee(department_id, first_name, last_name, job_title, birth_date, gender, hire_date)" +
                                            $"  VALUES (@departmentId, @firstName, @lastName, @jobTitle, @birthDate, @gender, @hireDate);" + _getLastIdSQL;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLCreateNewDepartment, connection);

                    cmd.Parameters.AddWithValue("@departmentId", newEmployee.DepartmentId);
                    cmd.Parameters.AddWithValue("@firstName", newEmployee.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", newEmployee.LastName);
                    cmd.Parameters.AddWithValue("@jobTitle", newEmployee.JobTitle);
                    cmd.Parameters.AddWithValue("@birthDate", newEmployee.BirthDate);
                    cmd.Parameters.AddWithValue("@gender", newEmployee.Gender);
                    cmd.Parameters.AddWithValue("@hireDate", newEmployee.HireDate);               

                    result = (int)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception)
            {

            }

            return result;
        }
        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public IList<Employee> GetAllEmployees()
        {
            string SQLGetEmployeesNames = " Select * From employee";
            List<Employee> result = new List<Employee>();

            try

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLGetEmployeesNames, connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);
                        
                        result.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            { }
            return result;
        }

        /// <summary>
        /// Searches the system for an employee by first name or last name.
        /// </summary>
        /// <remarks>The search performed is a wildcard search.</remarks>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>A list of employees that match the search.</returns> 
        public IList<Employee> Search(string firstname, string lastname)
        {
            string SQLSearchEmployeesNames = " Select * From employee WHERE First_Name Like @firstname AND Last_Name like @lastname";
            List<Employee> result = new List<Employee>();

            try

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLSearchEmployeesNames, connection);
                    cmd.Parameters.AddWithValue("@firstname", "%" + firstname + "%");
                    cmd.Parameters.AddWithValue("@lastname", "%" + lastname + "%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        result.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            { }
            return result;
        }

        public Employee GetSingleEmployee(int id)
        {
            string SQLGetDepartmentNames = "Select * From employee WHERE employee.employee_id = @employeeId";

            Employee result = new Employee();

            try

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLGetDepartmentNames, connection);

                    cmd.Parameters.AddWithValue("@employeeId", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);
                    }

                    if (result.FirstName == "" || result.FirstName == null)
                    {
                        Exception ex = new Exception();
                        throw ex;
                    }
                }
            }
            catch (SqlException ex)
            { }

            return result;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployeesWithoutProjects()
        {
            string SQLSearchEmployeesWithoutProjects = @" SELECT *
                                                        FROM employee
                                                        WHERE employee.employee_id NOT IN ( 
                                                            SELECT project_employee.employee_id
									                        FROM project_employee
									                        JOIN project ON project_employee.project_id = project.project_id
									                        WHERE CURRENT_TIMESTAMP BETWEEN project.from_date AND project.to_date
									                        ) ";

            List<Employee> result = new List<Employee>();

            try

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLSearchEmployeesWithoutProjects, connection);
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        result.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            { }
            return result;
        }
    }
}
