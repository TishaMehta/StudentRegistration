using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using StudentRegistration.Areas.LOC_State.Models;
using StudentRegistration.Areas.LOC_Country.Models;

namespace StudentRegistration.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    public class StateController : Controller
    {
        private IConfiguration Configuration;
        private object searchCountry;

        public int StateID { get; private set; }

        public StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StateList(SearchStateModel srs)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_State_SerchByStateCodeOrStateName";
            objcmd.Parameters.AddWithValue("@StateName", srs.StateName);
            objcmd.Parameters.AddWithValue("@StateCode", srs.StateCode);
            objcmd.Parameters.AddWithValue("@CountryName", srs.CountryName);

            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);
            return View("StateList", dt);
        }
        public IActionResult DeleteState(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_State_DeleteByPK ";
            objcmd.Parameters.AddWithValue("@StateID", StateID);
            objcmd.ExecuteReader();
            conn.Close();
            return RedirectToAction("StateList");
        }


        public IActionResult AddState(int StateID)

        {
            StateDropdown(StateID);

            if (StateID != 0)
            {
                ViewBag.Data = StateID;
                try
                {

                    string str = this.Configuration.GetConnectionString("myConnectionString");
                    DataTable dt = new DataTable();
                    SqlConnection conn = new SqlConnection(str);
                    conn.Open();
                    SqlCommand objcmd = conn.CreateCommand();
                    objcmd.CommandType = CommandType.StoredProcedure;
                    objcmd.CommandText = "PR_STATE_SELECTBYPK";
                    objcmd.Parameters.AddWithValue("@StateID", StateID);
                    SqlDataReader objDataReader = objcmd.ExecuteReader();
                    dt.Load(objDataReader);
                    conn.Close();
                    LOC_StateModel LC = new LOC_StateModel
                    {
                        StateID = (int)dt.Rows[0]["StateID"],
                        StateName = (string)dt.Rows[0]["StateName"],
                        StateCode = (string)dt.Rows[0]["StateCode"],
                        CountryID = (int)dt.Rows[0]["CountryID"]
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
        public IActionResult Save(LOC_StateModel StateModel)
        {
            try
            {
                String connectionStr = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;
                if (StateModel.StateID != 0)
                {
                    objcmd.CommandText = "PR_state_UpdateByPK";
                    objcmd.Parameters.AddWithValue("@StateID", StateModel.StateID);
                }
                else
                {
                    objcmd.CommandText = "PR_State_Insert";
                }
                objcmd.Parameters.AddWithValue("@StateName", StateModel.StateName);
                objcmd.Parameters.AddWithValue("@StateCode", StateModel.StateCode);
                objcmd.Parameters.AddWithValue("@CountryID", StateModel.CountryID);
                objcmd.ExecuteReader();
                conn.Close();
                return RedirectToAction("StateList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("StateList");
            }
        }
        public IActionResult StateDropdown(int  StateID)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_STATE_SELECTBYPK";
            cmd.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            LOC_StateModel stateModel  = new LOC_StateModel();
            foreach(DataRow row in dt.Rows)
            {
                stateModel.StateID = int.Parse(row["StateID"].ToString());
                stateModel.StateName = row["StateName"].ToString();
                stateModel.StateCode = row["StateCOde"].ToString();
                stateModel.CountryID = int.Parse(row["CountryID"].ToString());
            }
            SqlCommand sqlCommand2 = sqlConnection.CreateCommand();
            sqlCommand2.CommandType = CommandType.StoredProcedure;
            sqlCommand2.CommandText = "PR_COUNTRY_SELECTALL";
            SqlDataReader sqlDataReader = sqlCommand2.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(sqlDataReader);
            
            List<LOC_CountryModel> li = new List<LOC_CountryModel>();
            foreach(DataRow dr in dt1.Rows)
            {
                LOC_CountryModel lOC_CountryModel = new LOC_CountryModel();
                lOC_CountryModel.CountryID = int.Parse(dr["CountryID"].ToString());
                lOC_CountryModel.CountryName = dr["CountryName"].ToString();
                li.Add(lOC_CountryModel);
            }
            ViewBag.LI = li;
            sqlConnection.Close();
            return View(stateModel);
        }

    }
}
