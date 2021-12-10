using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoredProceduresTask.Data;
using StoredProceduresTask.Models;

namespace StoredProceduresTask.Controllers
{
    public class EmployeeController : Controller
    {
        public ApplicationDbContext _context;
        public IConfiguration _config { get; }

        public EmployeeController(ApplicationDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetEmployees()
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");
            using SqlConnection con = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetEmployees";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            List<Employee> output = new List<Employee>();
            while (sdr.Read())
            {
                Employee newEmployee = new Employee
                {
                    Id = Convert.ToInt32(sdr["Id"]),
                    Name = sdr["Name"].ToString(),
                    Gender = sdr["Gender"].ToString(),
                    DepartmentId = Convert.ToInt32(sdr["DepartmentId"])
                };
                output.Add(newEmployee);

            }

            return Json(output);
        }

        public IActionResult GetEmployeesCount()
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");
            using SqlConnection con = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetEmployeeCount";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                var output = sdr["EmployeeCount"].ToString();
                con.Close();
                return Json(output);
            }

            throw new Exception("Error reading stored procedure or no data is present");
        }
        public IActionResult GetEmployeesCountByGender(string Gender)
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");
            using SqlConnection con = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetEmployeeCountByGender";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            
            SqlParameter param_g = new SqlParameter("@Gender", Gender);
            cmd.Parameters.Add(param_g);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                var output = sdr["EmployeeCount"].ToString();
                con.Close();
                return Json(output);
            }

            throw new Exception("Error reading stored procedure or no data is present");
        }
        public IActionResult GetNameById(int id)
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");
            using SqlConnection con = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetNameById";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter param_id = new SqlParameter("@Id", id);
            cmd.Parameters.Add(param_id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                var output = sdr["Name"].ToString();
                con.Close();
                return Json(output);
            }

            throw new Exception("Error reading stored procedure or no data is present");
        }
        public IActionResult GetEmployeesByGenderAndDepartmentId(string Gender, int DepartmentId)
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");
            using SqlConnection con = new SqlConnection(connectionStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "GetEmployeesByGenderAndDepartment";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            SqlParameter param_g = new SqlParameter("@Gender", Gender);
            cmd.Parameters.Add(param_g);

            SqlParameter param_d_id = new SqlParameter("@DepartmentId", DepartmentId);
            cmd.Parameters.Add(param_d_id);

            SqlDataReader sdr = cmd.ExecuteReader();
            List<Employee> output = new List<Employee>();
            while (sdr.Read())
            {
                Employee newEmployee = new Employee
                {
                    Id = Convert.ToInt32(sdr["Id"]),
                    Name = sdr["Name"].ToString(),
                    Gender = sdr["Gender"].ToString(),
                    DepartmentId = Convert.ToInt32(sdr["DepartmentId"])
                };
                output.Add(newEmployee);

            }

            return Json(output);
        }

    }
}
