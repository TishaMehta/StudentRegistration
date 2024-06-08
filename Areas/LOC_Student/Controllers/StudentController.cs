using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using StudentRegistration.Areas.LOC_Student.Models;
using Microsoft.CodeAnalysis.Operations;
using StudentRegistration.Areas.LOC_Country.Controllers;
using StudentRegistration.Areas.MST_Branch.Models;
using StudentRegistration.Areas.LOC_City.Models;
using NuGet.Protocol;

namespace StudentRegistration.Areas.LOC_Student.Controllers
{
    [Area("LOC_Student")]
    public class StudentController : Controller
    {
        private IConfiguration Configuration;
        public StudentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StudentList(SearchStudentModel searchStudent)
        {

            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
           objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_State_SerchByStudentNAmeOrBranchNameOrCityName";
            //objcmd.Parameters.AddWithValue("@StudentName", searchStudent.StudentName);
            //objcmd.Parameters.AddWithValue("@BranchName", searchStudent.BranchName);
            //objcmd.Parameters.AddWithValue("@CityName", searchStudent.CityName);
            //objcmd.Parameters.AddWithValue("@MobileNoStudent", searchStudent.MobileNoStudent);
            //objcmd.Parameters.AddWithValue("MobileNoFather", searchStudent.MobileNoFather);
            //objcmd.Parameters.AddWithValue("@Email", searchStudent.Email);
            //objcmd.Parameters.AddWithValue("@Age", searchStudent.Age);
            //objcmd.Parameters.AddWithValue("@Address", searchStudent.Address);
            //objcmd.Parameters.AddWithValue("@BirthDate", searchStudent.BirthDate);
            //objcmd.Parameters.AddWithValue("@Gender", searchStudent.Gender);
            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);
            return View("StudentList", dt);
        }
        public IActionResult DeleteStudent(int StudentID)

        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Student_DeleteByPK ";
            objcmd.Parameters.AddWithValue("@StudentID", StudentID);
            objcmd.ExecuteReader();
            conn.Close();
            return RedirectToAction("StudentList");
        }
        public IActionResult AddStudent(int StudentID)
        {
            StudentDropdown1(StudentID);
            StudentDropDown2(StudentID);
            if (StudentID != 0)
            {
                ViewBag.Data = StudentID;
                string str = this.Configuration.GetConnectionString("myConnectionString");
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;
                objcmd.CommandText = "PR_Student_SelectByPK";
                objcmd.Parameters.AddWithValue("@StudentID", StudentID);
                SqlDataReader objDataReader = objcmd.ExecuteReader();
                dt.Load(objDataReader);
                conn.Close();
                LOC_StudentModel LC = new LOC_StudentModel
                {
                    BranchID = (int)dt.Rows[0]["BranchID"],
                    CityID = (int)dt.Rows[0]["CityID"],
                    StudentID = (int)dt.Rows[0]["StudentID"],
                    StudentName = (string)dt.Rows[0]["StudentName"],
                    MobileNoStudent = (string)dt.Rows[0]["MobileNoStudent"],
                    Email = (string)dt.Rows[0]["Email"],
                    MobileNoFather = (string)dt.Rows[0]["MobileNoFather"],
                    Address = (string)dt.Rows[0]["Address"],
                    BirthDate = (DateTime)dt.Rows[0]["BirthDate"],
                    Age = (int)dt.Rows[0]["Age"],
                    IsActive = (bool)dt.Rows[0]["IsActive"],
                    Gender = (string)dt.Rows[0]["Gender"],
                    Password = (string)dt.Rows[0]["Password"]
                   
                };
                return View(LC);
            }
            else
            {
                return View();
            }
            

        }
        public IActionResult Save(LOC_StudentModel StudentModel)
        {
            
            
                String connectionStr = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;
                if (StudentModel.StudentID != 0)
                {
                    objcmd.CommandText = "PR_Student_UpdateByPK";
                    objcmd.Parameters.AddWithValue("@StudentID", StudentModel.StudentID);
                }
                else
                {
                    objcmd.CommandText = "PR_Student_Insert";
                }
            objcmd.Parameters.AddWithValue("@BranchID", StudentModel.BranchID);
            objcmd.Parameters.AddWithValue("@CityID", StudentModel.CityID);
            objcmd.Parameters.AddWithValue("@StudentName", StudentModel.StudentName);
            objcmd.Parameters.AddWithValue("@MobileNoStudent", StudentModel.MobileNoStudent);
            objcmd.Parameters.AddWithValue("@Email", StudentModel.Email);
            objcmd.Parameters.AddWithValue("@MobileNOFather", StudentModel.MobileNoFather);
            objcmd.Parameters.AddWithValue("@Address", StudentModel.Address);
            objcmd.Parameters.AddWithValue("@Birthdate", StudentModel.BirthDate);
            objcmd.Parameters.AddWithValue("@Age", StudentModel.Age);
            objcmd.Parameters.AddWithValue("@IsActive", StudentModel.IsActive);
            objcmd.Parameters.AddWithValue("@Gender", StudentModel.Gender);
            objcmd.Parameters.AddWithValue("@Password", StudentModel.Password);
          

            objcmd.ExecuteReader();
                conn.Close();
                return RedirectToAction("StudentList");
            
           }
        public IActionResult StudentDropdown1(int? StudentID)
        {
            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt3 = new DataTable();
            SqlConnection conn3 = new SqlConnection(connectionstr1);
            conn3.Open();
            SqlCommand cmd4 = conn3.CreateCommand();
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.CommandText = "PR_MST_BRANCH_DROPDOWN";
            SqlDataReader objrdr1 = cmd4.ExecuteReader();
            dt3.Load(objrdr1);

            List<MST_BranchDropDownModel> branchDropDownModels = new List<MST_BranchDropDownModel>();
            foreach (DataRow dr in dt3.Rows)
            {
                MST_BranchDropDownModel DD = new MST_BranchDropDownModel();
                DD.BranchID = Convert.ToInt32(dr["BranchID"].ToString());
                DD.BranchName = dr["BranchName"].ToString();
                branchDropDownModels.Add(DD);
            }
            ViewBag.BranchList = branchDropDownModels;
            conn3.Close();
            return View();
        }
        public IActionResult StudentDropDown2(int? StudentId)
        {
            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt3 = new DataTable();
            SqlConnection conn3 = new SqlConnection(connectionstr1);
            conn3.Open();
            SqlCommand cmd4 = conn3.CreateCommand();
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.CommandText = "PR_lOC_CITY_DROPDOWN";
            SqlDataReader objrdr1 = cmd4.ExecuteReader();
            dt3.Load(objrdr1);

            List<LOC_CityDropDownModel> cityDropDownModels = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr in dt3.Rows)
            {
                LOC_CityDropDownModel DD = new LOC_CityDropDownModel();
                DD.CityID = Convert.ToInt32(dr["CityID"].ToString());
                DD.CityName = dr["CityName"].ToString();
                cityDropDownModels.Add(DD);
            }
            ViewBag.CityList = cityDropDownModels;
            conn3.Close();
            return View();
        }


    }
}
