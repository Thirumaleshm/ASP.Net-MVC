using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using ASP.Net_MVC.Models;

namespace CRUDOPMVC.Models
{
    public class CompanyDBContext
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString);

        //Get All List of Employees
        public List<Employee> GetEmployees()
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                string query = "select * from Employee";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Employee emp = new Employee();
                        emp.Eno = Convert.ToInt32(dr["Eno"]);
                        emp.Ename = Convert.ToString(dr["Ename"]);
                        emp.Job = Convert.ToString(dr["Job"]);
                        emp.Salary = Convert.ToDouble(dr["Salary"]);
                        emp.Dname = Convert.ToString(dr["Dname"]);
                        employees.Add(emp);
                    }
                    return employees;
                }
                else
                {
                    return employees;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        //Get Employees based on Name
        public IEnumerable<Employee> GetEmployeesByName(string ename)
        {
            List<Employee> emps = GetEmployees();
            if (emps != null && emps.Count > 0)
            {
                //var employees = from emp in emps where emp.Ename == "ename" select emp;
                var employees = emps.Where(x => x.Ename == ename).ToList();
                return employees;
            }
            return new List<Employee>();
        }
        //Get Single Record based on Eno
        public Employee GetEmployee(int eno)
        {
            try
            {
                Employee emp = new Employee();
                string query = "select * from Employee where Eno=" + eno;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        emp.Eno = Convert.ToInt32(dr["Eno"]);
                        emp.Ename = Convert.ToString(dr["Ename"]);
                        emp.Job = Convert.ToString(dr["Job"]);
                        emp.Salary = Convert.ToDouble(dr["Salary"]);
                        emp.Dname = Convert.ToString(dr["Dname"]);
                    }
                    return emp;
                }
                return emp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        private void DBOperation(string query, Employee emp)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@eno", emp.Eno);
            cmd.Parameters.AddWithValue("@ename", emp.Ename);
            cmd.Parameters.AddWithValue("@job", emp.Job);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@dname", emp.Dname);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Insert Employee
        public void InsertEmp(Employee emp)
        {
            DBOperation("sp_insertEmp", emp);
        }
        //Update Employee
        public void UpdateEmp(Employee emp)
        {
            DBOperation("sp_Updatemp", emp);
        }
        //Delete Employee
        public void DeleteEmp(int eno)
        {
            //SqlCommand cmd = new SqlCommand("delete from Employee where Eno=" + eno, con);
            //SqlCommand cmd = new SqlCommand("sp_DeleteEmp" + eno, con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            Employee emp = GetEmployee(eno);
            DBOperation("deleteEmp", emp);
        }
    }
}