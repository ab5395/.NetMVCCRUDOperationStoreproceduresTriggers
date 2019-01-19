using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUDStoreProcedureTrigger.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Marks1 { get; set; }
        public int Marks2 { get; set; }
        public int Marks3 { get; set; }
        public int Marks4 { get; set; }
        public int Total { get; set; }
        public float Percentage { get; set; }
    }

    public class CRUDModel
    {
        string connectionString = @"Data Source=AJAY\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        //To View all employees details    
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> lstemployee = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SpSelectEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeId = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Marks1 = Convert.ToInt32(rdr["Marks1"].ToString());
                    employee.Marks2 = Convert.ToInt32(rdr["Marks2"].ToString());
                    employee.Marks3 = Convert.ToInt32(rdr["Marks3"].ToString());
                    employee.Marks4 = Convert.ToInt32(rdr["Marks4"].ToString());
                    employee.Total = Convert.ToInt32(rdr["Total"].ToString());
                    employee.Percentage = float.Parse(rdr["Percentage"].ToString());
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        //To Add new employee record    
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SpAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Marks1", employee.Marks1);
                cmd.Parameters.AddWithValue("@Marks2", employee.Marks2);
                cmd.Parameters.AddWithValue("@Marks3", employee.Marks3);
                cmd.Parameters.AddWithValue("@Marks4", employee.Marks4);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a particluar employee  
        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SpUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Marks1", employee.Marks1);
                cmd.Parameters.AddWithValue("@Marks2", employee.Marks2);
                cmd.Parameters.AddWithValue("@Marks3", employee.Marks3);
                cmd.Parameters.AddWithValue("@Marks4", employee.Marks4);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular employee  
        public Employee GetEmployeeData(int? id)
        {
            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblEmployee WHERE EmployeeID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Marks1 = Convert.ToInt32(rdr["Marks1"].ToString());
                    employee.Marks2 = Convert.ToInt32(rdr["Marks2"].ToString());
                    employee.Marks3 = Convert.ToInt32(rdr["Marks3"].ToString());
                    employee.Marks4 = Convert.ToInt32(rdr["Marks4"].ToString());
                }
            }
            return employee;
        }

        //To Delete the record on a particular employee  
        public void DeleteEmployee(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SpDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}