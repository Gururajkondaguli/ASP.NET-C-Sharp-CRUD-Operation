
////===============================================================================//

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using System.Data;

////===============================================================================//






//[ApiController]
//[Route("api/[controller]")]
//public class StudentController : ControllerBase
//{




//    //===============================================================================//
//    private readonly IConfiguration _configuration;

//    public StudentController(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }
//    //===============================================================================//



//    //===============================================================================//
//    [HttpPost("insert")]
//    public IActionResult InsertStudent(StudentDto student)
//    //===============================================================================//


//    {
//        string connectionString = _configuration.GetConnectionString("DefaultConnection");

//        using (SqlConnection conn = new SqlConnection(connectionString))
//        {
//            SqlCommand cmd = new SqlCommand("InsertStudent", conn);
//            cmd.CommandType = CommandType.StoredProcedure;
//            //===============================================================================//

//            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
//            cmd.Parameters.AddWithValue("@LastName", student.LastName);
//            cmd.Parameters.AddWithValue("@Email", student.Email);
//            //===============================================================================//
//            conn.Open();

//            cmd.ExecuteNonQuery();
//        }

//        return Ok("Student inserted successfully");
//    }


//    //===============================================================================//
//}



//public class StudentDto
//{
//    public string FirstName { get; set; }
//    public string LastName { get; set; }
//    public string Email { get; set; }
//}
