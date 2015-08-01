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
    public partial class MyBooking : System.Web.UI.Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);
        int ShipID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clbind();
            }
        }

        protected void clbind()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT B.BookingID, I.ItineraryID, I.JourneyDate, B.PaymentStatus, B.BookingStatus, C.CabinName, " +
                                                    "(Iti.Price + C.CabinPrice) AS TotalPrice FROM " +
                                                    "Booking B, ItinerarySchedule I, Cabin C, Itinerary Iti " +
                                                    "WHERE I.ItineraryScheduleID = B.ItineraryScheduleID " +
                                                        "AND B.CabinID = C.CabinID " +
                                                        "AND Iti.ItineraryID = I.ItineraryID " +
                                                        "AND B.PassengerID =  '" + (String)Session["PassengerID"] + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MyBookingList.DataSource = ds;
                MyBookingList.DataBind();
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