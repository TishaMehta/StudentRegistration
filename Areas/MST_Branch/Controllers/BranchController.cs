using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using StudentRegistration.Areas.MST_Branch.Models;
using StudentRegistration.Areas.LOC_Country.Models;

namespace StudentRegistration.Areas.MST_Branch.Controllers
{
    [Area("MST_Branch")]
    public class BranchController : Controller
    {
        private IConfiguration Configuration;
        public BranchController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BranchList(SearchBranchModel searchBranch)

        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Branch_SerchByBranchCodeOrBranchName";
            objcmd.Parameters.AddWithValue("@BranchName", searchBranch.BranchName);
            objcmd.Parameters.AddWithValue("@BranchCode", searchBranch.BranchCode);
            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);
            return View("BranchList", dt);
        }
        public IActionResult DeleteBranch(int BranchID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Branch_DeleteByPK ";
            objcmd.Parameters.AddWithValue("@BranchID", BranchID);
            objcmd.ExecuteReader();
            conn.Close();
            return RedirectToAction("BranchList");
        }

        public IActionResult AddBranch(int BranchID)

        {
            if (BranchID != 0)
            {
                ViewBag.Data = BranchID;
                try
                {

                    string str = this.Configuration.GetConnectionString("myConnectionString");
                    DataTable dt = new DataTable();
                    SqlConnection conn = new SqlConnection(str);
                    conn.Open();
                    SqlCommand objcmd = conn.CreateCommand();
                    objcmd.CommandType = CommandType.StoredProcedure;
                    objcmd.CommandText = "PR_Branch_SELECTByPK";
                    objcmd.Parameters.AddWithValue("@BranchID", BranchID);
                    SqlDataReader objDataReader = objcmd.ExecuteReader();
                    dt.Load(objDataReader);
                    conn.Close();
                    MST_BranchModel LC = new MST_BranchModel
                    {
                        BranchID = (int)dt.Rows[0]["BranchID"],
                        BranchName = (string)dt.Rows[0]["BranchName"],
                        BranchCode = (string)dt.Rows[0]["BranchCode"]
                    };
                    return View(LC);
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        public IActionResult Save(MST_BranchModel BranchModel)
        {
            try
            {
                String connectionStr = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;
                if (BranchModel.BranchID != 0)
                {
                    objcmd.CommandText = "PR_Branch_UpdateBy_PK";
                    objcmd.Parameters.AddWithValue("@BranchID", BranchModel.BranchID);
                }
                else
                {
                    objcmd.CommandText = "PR_Branch_Insert";
                }
                objcmd.Parameters.AddWithValue("@BranchName", BranchModel.BranchName);
                objcmd.Parameters.AddWithValue("@BranchCode",BranchModel.BranchCode);
                objcmd.ExecuteReader();
                conn.Close();
                return RedirectToAction("BranchList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BranchList");
            }
        }


    }
}
