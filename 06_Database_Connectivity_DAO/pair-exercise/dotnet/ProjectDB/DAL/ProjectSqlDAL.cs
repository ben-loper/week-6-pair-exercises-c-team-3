using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class ProjectSqlDAL
    {
        private string connectionString;
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        // Single Parameter Constructor
        public ProjectSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {

            string SQLGetProjects = " Select * From project";
            List<Project> result = new List<Project>();

            try

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLGetProjects, connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Project project = new Project();

                        project.ProjectId = Convert.ToInt32(reader["project_id"]);
                        project.Name = Convert.ToString(reader["name"]);
                        project.StartDate = Convert.ToDateTime(reader["from_date"]);
                        project.EndDate = Convert.ToDateTime(reader["to_date"]);

                        result.Add(project);
                    }
                }
            }
            catch (SqlException ex)
            { }
            return result;
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            bool result = true;

            string SQLCreateNewDepartment = $"INSERT INTO project_employee VALUES (@projectId, @employeeId);";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLCreateNewDepartment, connection);

                    cmd.Parameters.AddWithValue("@projectId", projectId);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                result = false;
            }
            catch (Exception)
            {

            }

            return result;
        }

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            bool result = true;

            string SQLCreateNewDepartment = $"DELETE FROM  project_employee WHERE project_id = @projectId AND employee_id =  @employeeId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLCreateNewDepartment, connection);

                    cmd.Parameters.AddWithValue("@projectId", projectId);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);

                    int numRowsEffected = cmd.ExecuteNonQuery();
                    if (numRowsEffected == 0 )
                    {
                        result = false;
                    }
                }
            }
            catch (SqlException ex)
            {
                result = false;
            }
            catch (Exception)
            {

            }

            return result;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            int result = 0;
            string SQLCreateNewProject = $"INSERT INTO project VALUES (@name,@fromdate,@todate);" + _getLastIdSQL;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQLCreateNewProject, connection);

                    cmd.Parameters.AddWithValue("@name", newProject.Name);
                    cmd.Parameters.AddWithValue("@fromdate", newProject.StartDate);
                    cmd.Parameters.AddWithValue("@todate", newProject.EndDate);
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

    }
}
