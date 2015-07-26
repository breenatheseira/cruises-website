using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ddac
{
    public partial class Itinerary : System.Web.UI.Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ilbind();
            }
        }

        protected void ilbind()
        {
            try
            {            
				if (Session["price"] == null)
					Session["price"] = (Int32)1000000;

				String ITcolumns = "IT.ItineraryDetails, IT.ItineraryID, IT.Region, IT.Source, IT.Price, IT.ItineraryScheduleID, IT.JourneyDate, dbo.fx_getShipName(IT.ShipID) AS ShipName";
				String Icolumns = "I.ItineraryDetails, I.ItineraryID, I.Region, I.Source, I.Price, ID.ItineraryScheduleID, ID.JourneyDate, I.ShipID ";
				String sql = "SELECT DISTINCT " + ITcolumns + " FROM (SELECT " + Icolumns + " FROM Itinerary I LEFT JOIN ItinerarySchedule ID " +
                    "ON I.ItineraryID = ID.ItineraryID WHERE Price < " + (Int32)Session["price"] + " AND JourneyDate > GETDATE() ";

				if (!string.IsNullOrEmpty((String)Session["region"]))
                    sql += "AND Region = '" + (String)Session["region"] + "' ";

				if (Session["dateTo"] != null && Session["dateFrom"] != null)
                    sql += "AND JourneyDate BETWEEN '" + (String)Session["dateFrom"] + "' AND '" +
                        (String)Session["dateTo"] + "'";

                sql += ") IT LEFT JOIN (SELECT B.ItineraryScheduleID, SUM(TotalInShip) AS TotalInShip, SUM(TotalCabinBooked) AS TotalCabinBooked FROM (" +
                        "SELECT B.ItineraryScheduleID, C.TotalInShip, COUNT(B.CabinID) AS TotalCabinBooked FROM Booking B RIGHT JOIN Cabin C ON C.CabinID = B.CabinID " +
                        "GROUP BY B.ItineraryScheduleID, B.CabinID, C.TotalInShip) B GROUP BY B.ItineraryScheduleID, B.TotalInShip, B.TotalCabinBooked " +
                        "HAVING B.TotalInShip > B.TotalCabinBooked) B ON B.ItineraryScheduleID = IT.ItineraryScheduleID";

				SqlCommand cmd = new SqlCommand(sql, conn);
				conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ItineraryList.DataSource = ds;
                ItineraryList.DataBind();
                conn.Close();
            }
            catch (Exception err)
            {
                conn.Close();
                notification.ForeColor = System.Drawing.Color.Red;
                notification.Text = err.Message;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            DateTime fromDate = Convert.ToDateTime("1900-01-01");
            DateTime toDate = Convert.ToDateTime("9999-12-31");
            Session["region"] = RegionDropDown.SelectedValue.ToString();
            Decimal d = Convert.ToDecimal((String)Request.Form["price"]);
            Session["price"] = Convert.ToInt32((String)Request.Form["price"]);
    
            if (!"DD/MM/YYYY".Equals((String)Request["fDate"]) && !"DD/MM/YYYY".Equals((String)Request["tDate"]))
            {
                fromDate = Convert.ToDateTime((String)Request.Form["fDate"]);
                toDate = Convert.ToDateTime((String)Request.Form["tDate"]);
            }
    
            var fromDateFormat = fromDate.Date.ToString("yyyy-MM-dd");
            var toDateFormat = toDate.Date.ToString("yyyy-MM-dd");
            Session["dateTo"] = toDateFormat;
            Session["dateFrom"] = fromDateFormat;
            ilbind();
        }
    }
}