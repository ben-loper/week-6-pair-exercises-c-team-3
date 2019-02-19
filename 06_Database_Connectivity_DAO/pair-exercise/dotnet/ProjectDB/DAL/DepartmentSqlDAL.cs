using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class DepartmentSqlDAL
    {
        private string connectionString;
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        // Single Parameter Constructor
        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetDepartments()
        {
           string SQLGetDepartmentNames = " Select * From Department";
            List<Department> result = new List<Department>();

            try

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLGetDepartmentNames, connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = Convert.ToString(reader["name"]);
                        Department department = new Department();
                        int id = Convert.ToInt32(reader["department_id"]);
                        department.Name = name;
                        department.Id = id;
                        result.Add(department);
                    }
                }
            }
            catch(SqlException ex)
            { }
            return result;
        }

        public Department GetSinlgeDepartment(int id)
        {
            string SQLGetDepartmentNames = "Select * From Department WHERE department.department_id = @departmentId";

            Department result = new Department();

            try

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLGetDepartmentNames, connection);

                    cmd.Parameters.AddWithValue("@departmentId", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = Convert.ToString(reader["name"]);
                        //int id = Convert.ToInt32(reader["department_id"]);
                        result.Name = name;
                        result.Id = id;                        
                    }

                    if(result.Name == "" || result.Name == null)
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
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            int result = 0;
            string SQLCreateNewDepartment = $"INSERT INTO Department VALUES (@name);" + _getLastIdSQL;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLCreateNewDepartment, connection);

                    cmd.Parameters.AddWithValue("@name", newDepartment.Name);

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
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            bool result = false;
            string SQLUpdateDepartment = $"UPDATE Department SET name = (@name) WHERE department_id = (@id);";


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLUpdateDepartment, connection);

                    cmd.Parameters.AddWithValue("@name", updatedDepartment.Name);
                    cmd.Parameters.AddWithValue("@id", updatedDepartment.Id);
                    int numOfRows = cmd.ExecuteNonQuery();
                    if (numOfRows>0)
                    {
                        result = true;
                    }
                    

                }
            }
            catch (SqlException ex)
            {
            
            }
            catch (Exception)
            {
            }



                return result;
        }

        /// <summary>
        /// Removes a deparment from the deparment table using the given deparment ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveDepartment(int id)
        {
            bool result = false;

            string SQLCreateNewDepartment = $"DELETE FROM  department WHERE department_id = @deparmentId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLCreateNewDepartment, connection);

                    cmd.Parameters.AddWithValue("@deparmentId", id);

                    int numRowsEffected = cmd.ExecuteNonQuery();
                    if (numRowsEffected == 0)
                    {
                        result = false;
                    }
                }
            }
            catch (SqlException ex)
            { 

            }
            catch (Exception)
            {

            }

            return result;
        }

    }
}
