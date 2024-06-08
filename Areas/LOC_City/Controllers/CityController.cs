using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using StudentRegistration.Areas.LOC_City.Models;
using StudentRegistration.Areas.LOC_Country.Controllers;
using StudentRegistration.Areas.LOC_Country.Models;
using StudentRegistration.Areas.LOC_State.Models;
using StudentRegistration.Areas.LOC_Student.Models;

namespace StudentRegistration.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    public class CityController : Controller
    {
        private IConfiguration Configuration;
        public CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
     
        public IActionResult CityList(LOC_SearchCityModel srach)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_State_SerchByCityCodeOrCityName";
            objcmd.Parameters.AddWithValue("@CityName", srach.CItyName);
            objcmd.Parameters.AddWithValue("@CityCode", srach.Citycode);
            objcmd.Parameters.AddWithValue("@CountryName", srach.CountryName);
            objcmd.Parameters.AddWithValue("@StateName", srach.StateName);
            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);
            return View("CityList", dt);
        }

        public IActionResult DeleteCity(int CityID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_City_DeleteByPK ";
            objcmd.Parameters.AddWithValue("@CityID", CityID);
            objcmd.ExecuteReader();
            conn.Close();
            return RedirectToAction("CityList");
        }

        public IActionResult AddCity(int CityID)
        {
            CityDropdown2(CityID);

            CityDropdown1(CityID);
            if (CityID != 0)
            {
                ViewBag.Data = CityID;
                

                    string str = this.Configuration.GetConnectionString("myConnectionString");
                    DataTable dt = new DataTable();
                    SqlConnection conn = new SqlConnection(str);
                    conn.Open();
                    SqlCommand objcmd = conn.CreateCommand();
                    objcmd.CommandType = CommandType.StoredProcedure;
                    objcmd.CommandText = "PR_CITY_SELECTBYPK";
                    objcmd.Parameters.AddWithValue("@CityID", CityID);
                    SqlDataReader objDataReader = objcmd.ExecuteReader();
                    dt.Load(objDataReader);
                    conn.Close();
                    LOC_CityModel LC = new LOC_CityModel
                    {
                        CityID = (int)dt.Rows[0]["CityID"],
                        CityName = (string)dt.Rows[0]["CityName"],
                        CityCode = (string)dt.Rows[0]["CityCode"],
                        CountryID = (int)dt.Rows[0]["CountryID"],
                        StateID = (int)dt.Rows[0]["StateID"],
                        CountryName = (string)dt.Rows[0]["CountryName"],
                        StateName = (string)dt.Rows[0]["StateName"]

                    };
                    return View(LC);
                
                
                   
                }
            
            else
            {
                return View();
            }
        }
        public IActionResult Save(LOC_CityModel CityModel)
        {
            
                String connectionStr = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;
                if (CityModel.CityID != 0)
                {
                    objcmd.CommandText = "PR_City_UpdateByPK";
                    objcmd.Parameters.AddWithValue("@CityID", CityModel.CityID);
                }
                else
                {
                    objcmd.CommandText = "PR_City_Insert";
                }
                objcmd.Parameters.AddWithValue("@CityName", CityModel.CityName);
                objcmd.Parameters.AddWithValue("@CityCode", CityModel.CityCode);
                objcmd.Parameters.AddWithValue("@CountryID", CityModel.CountryID);
                objcmd.Parameters.AddWithValue("@StateID", CityModel.StateID);


                objcmd.ExecuteReader();
                conn.Close();
                return RedirectToAction("CityList");
            
           
        }
        public IActionResult CityDropdown1(int? CityID)
        {
            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt2 = new DataTable();
            SqlConnection conn2 = new SqlConnection(connectionstr1);
              conn2.Open();
            SqlCommand cmd3 = conn2.CreateCommand();
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "PR_LOC_State_DropDown";
            SqlDataReader objrdr1 = cmd3.ExecuteReader();
            dt2.Load(objrdr1);

            List<LOC_StateDropDownModel> stateDropDownModels = new List<LOC_StateDropDownModel>();
            foreach(DataRow dr in dt2.Rows)
            {
                LOC_StateDropDownModel DD = new LOC_StateDropDownModel();
                DD.StateID = Convert.ToInt32(dr["StateID"].ToString());
                DD.StateName = dr["StateName"].ToString();
                stateDropDownModels.Add(DD);
            }
            ViewBag.StateList = stateDropDownModels;
            conn2.Close();
            return View();
        }

        public IActionResult CityDropdown2(int? CityID)
        {
            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt3 = new DataTable();
            SqlConnection conn3 = new SqlConnection(connectionstr1);
            conn3.Open();
            SqlCommand cmd4 = conn3.CreateCommand();
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.CommandText = "pr_LOC_Country_DropDown";
            SqlDataReader objrdr1 = cmd4.ExecuteReader();
            dt3.Load(objrdr1);

            List<LOC_CountryDropDownModel> countryDropDownModels = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt3.Rows)
            {
                LOC_CountryDropDownModel DD = new LOC_CountryDropDownModel();
                DD.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
                DD.CountryName = dr["CountryName"].ToString();
                countryDropDownModels.Add(DD);
            }
            ViewBag.CountryList = countryDropDownModels;
            conn3.Close();
            return View();
        }
        public dynamic Index(int CountryID)
        {
            SqlConnection connection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            //connection open
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_StateByCountry";
            command.Parameters.AddWithValue("CountryID", CountryID);



            SqlDataReader reader = command.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            connection.Close();

            List<LOC_StateModel> stateList = new List<LOC_StateModel>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_StateModel stateModel = new LOC_StateModel();
                stateModel.StateID = int.Parse(dr["StateID"].ToString());
                stateModel.StateName = dr["StateName"].ToString();
                stateList.Add(stateModel);
            }

            ViewBag.StateList = stateList;

            return Json(stateList);
        }
    }
}
