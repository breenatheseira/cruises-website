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
        String sql;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ilbind();
            }
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {

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
                    "ON I.ItineraryID = ID.ItineraryID WHERE Price < " + (Int32)Session["price"] + " ";

				if (!string.IsNullOrEmpty((String)Session["region"]))
                    sql += "AND Region = '" + (String)Session["region"] + "' ";

				if (Session["dateTo"] != null && Session["dateFrom"] != null)
                    sql += "AND JourneyDate BETWEEN '" + (String)Session["dateFrom"] + "' AND '" +
                        (String)Session["dateTo"] + "'";

				sql += ") IT LEFT JOIN (SELECT B.ItineraryScheduleID, (C.Capacity*C.TotalInShip - COUNT(BookingID)) AS TotalRemainingHead FROM Booking B, Cabin C " +
					   "WHERE C.CabinID = B.CabinID GROUP BY B.ItineraryScheduleID, C.Capacity, C.TotalInShip) B ON B.ItineraryScheduleID = IT.ItineraryScheduleID";

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
    }
}