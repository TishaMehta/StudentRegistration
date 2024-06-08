using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Areas.LOC_Country.Models;
using System.Data;
using System.Data.SqlClient;

namespace StudentRegistration.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]

    public class CountryController : Controller
    {

        private IConfiguration Configuration;  
      public CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CountryList(SearchCountryModel searchCountry)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_Country_Search";
            objcmd.Parameters.AddWithValue("@CountryName", searchCountry.CountryName);
            objcmd.Parameters.AddWithValue("@CountryCode", searchCountry.CountryCode);
            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);
            return View("CountryList",dt);
        }
        public IActionResult DeleteCountry(int CountryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_COUNTRY_Delete ";
            objcmd.Parameters.AddWithValue("@CountryID", CountryID);
            objcmd.ExecuteReader();
            conn.Close();
            return RedirectToAction("CountryList");
        }
        public IActionResult AddCountry(int CountryID)
        {
            if(CountryID != 0)
            {
                ViewBag.Data = CountryID;
                try
                {

                    string str = this.Configuration.GetConnectionString("myConnectionString");
                    DataTable dt = new DataTable();
                    SqlConnection conn = new SqlConnection(str);
                    conn.Open();
                    SqlCommand objcmd = conn.CreateCommand();
                    objcmd.CommandType = CommandType.StoredProcedure;
                    objcmd.CommandText = "PR_COUNTRY_SELECTByPK";
                    objcmd.Parameters.AddWithValue("@CountryID", CountryID);
                    SqlDataReader objDataReader = objcmd.ExecuteReader();
                    dt.Load(objDataReader);
                    conn.Close();
                    LOC_CountryModel LC = new LOC_CountryModel
                    {
                        CountryID = (int)dt.Rows[0]["CountryID"],
                        CountryName = (string)dt.Rows[0]["CountryName"],
                        CountryCode = (string)dt.Rows[0]["CountryCode"]
                    };
                    return View(LC);
                }
                catch(Exception ex)
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        public IActionResult Save(LOC_CountryModel CountryModel)
        {
            try
            {
                String connectionStr = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;
                if (CountryModel.CountryID != 0)
                {
                    objcmd.CommandText = "PR_Country_Update";
                    objcmd.Parameters.AddWithValue("@CountryID", CountryModel.CountryID);
                }
                else
                {
                    objcmd.CommandText = "PR_Country_Insert";
                }
                objcmd.Parameters.AddWithValue("@CountryName", CountryModel.CountryName);
                objcmd.Parameters.AddWithValue("@CountryCode", CountryModel.CountryCode);
                objcmd.ExecuteReader();
                conn.Close();
                return RedirectToAction("CountryList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("CountryList");
            }
        }
    }
}
