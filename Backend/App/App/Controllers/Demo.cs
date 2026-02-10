using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public EmployeeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }




    //=======================================****INSERT****=================================================//


    [HttpPost("insert")]
    public IActionResult InsertEmployee(EmployeeDto employee)
    {
        string connStr = _configuration.GetConnectionString("DefaultConnection");

        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@PhoneNo", employee.PhoneNo);
                cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                cmd.Parameters.AddWithValue("@JobTitle", employee.JobTitle);
                cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Status", employee.Status);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return Ok("Employee inserted successfully");
        }
        catch (SqlException ex)
        {
            // 🔴 SQL errors (duplicate email, FK, etc.)
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // 🔴 Any other unexpected error
            return StatusCode(500, "Internal server error");
        }
    }



    //=======================================****UPDATE****=================================================//



    //[HttpPut("update/{id}")]
    //public IActionResult UpdateEmployee(int id,EmployeeDto employee)
    //{
    //    string connStr = _configuration.GetConnectionString("DefaultConnection");

    //    using (SqlConnection con = new SqlConnection(connStr))
    //    {
    //        SqlCommand cmd = new SqlCommand("UpdateEmployee", con);

    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
    //        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
    //        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
    //        cmd.Parameters.AddWithValue("@Email", employee.Email);
    //        cmd.Parameters.AddWithValue("@PhoneNo", employee.PhoneNo);
    //        cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
    //        cmd.Parameters.AddWithValue("@JobTitle", employee.JobTitle);
    //        cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
    //        cmd.Parameters.AddWithValue("@Salary", employee.Salary);
    //        cmd.Parameters.AddWithValue("@Status", employee.Status);

    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //    }

    //    return Ok("Employee updated successfully ");
    //}


    // C#
    [HttpPut("update/{id}")]
    public IActionResult UpdateEmployee(int id, EmployeeDto employee)
    {
        if (employee == null) return BadRequest("Request body is empty.");
        if (id <= 0) return BadRequest("Invalid route id.");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Prefer route id as the authoritative identifier
        employee.EmployeeId = id;

        string connStr = _configuration.GetConnectionString("DefaultConnection");

        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("UpdateEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Use explicit SqlParameter types (avoid AddWithValue)
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employee.EmployeeId });

                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = (object)employee.FirstName ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = (object)employee.LastName ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 256) { Value = (object)employee.Email ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", SqlDbType.NVarChar, 50) { Value = (object)employee.PhoneNo ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int) { Value = employee.DepartmentId });
                cmd.Parameters.Add(new SqlParameter("@JobTitle", SqlDbType.NVarChar, 150) { Value = (object)employee.JobTitle ?? DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@DateOfJoining", SqlDbType.DateTime2) { Value = employee.DateOfJoining });
                var salaryParam = new SqlParameter("@Salary", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = employee.Salary };
                cmd.Parameters.Add(salaryParam);
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50) { Value = (object)employee.Status ?? DBNull.Value });

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (SqlException ex)
        {
            // Stored procedure raised an error (e.g. validation, no rows affected, constraint violation).
            // Return a controlled error to the caller; log ex in real app.
            return StatusCode(500, "Database error occurred while updating employee.");
        }

        return Ok("Employee updated successfully");
    }




    //=======================================****DELETE****=================================================//


    [HttpDelete("delete/{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        string connStr = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeId", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        return Ok("Employee deleted successfully ");
    }



    //=======================================****READ****=================================================//


    [HttpGet("getall")]
    public IActionResult GetAllEmployees()
    {
        string connStr = _configuration.GetConnectionString("DefaultConnection");
        List<EmployeeDto> employees = new List<EmployeeDto>();

        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand("ReadEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(new EmployeeDto
                {
                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString(),
                    PhoneNo = reader["PhoneNo"].ToString(),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                    JobTitle = reader["JobTitle"].ToString(),
                    DateOfJoining = Convert.ToDateTime(reader["DateOfJoining"]),
                    Salary = Convert.ToDecimal(reader["Salary"]),
                    Status = reader["Status"].ToString()
                });
            }

            con.Close();
        }

        return Ok(employees);
    }

}


//*********************************************************************************************************************************//

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNo { get; set; }
    public int DepartmentId { get; set; }
    public string JobTitle { get; set; }
    public DateTime DateOfJoining { get; set; }
    public decimal Salary { get; set; }
    public string Status { get; set; }
}
