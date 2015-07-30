using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ddac
{
    public partial class Payment : System.Web.UI.Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            // to be removed once booking is done with redirecting params
            if (string.IsNullOrEmpty((String)Request.Params["bookingID"]))
                Response.Redirect("./Itinerary.aspx");

            String bookingID = (String)Request.Params["bookingID"];

            String sql = "SELECT B.BookingID, B.PassengerID, B.BookingDate, P.Name, " +
                         "I.ItineraryID, I.Region, I.ItineraryDetails, I.Source, I.Price, " +
                         "SI.JourneyDate, C.CabinName, C.CabinPrice, C.Capacity, " +
                         "S.ShipName, S.CruiseOperator FROM Booking B, Itinerary I, ItinerarySchedule SI, Cabin C, Ship S, Passenger P " +
                         "WHERE B.ItineraryScheduleID = SI.ItineraryScheduleID AND C.CabinID = B.CabinID AND P.PassengerID = B.PassengerID AND " +
                         "I.ItineraryID = SI.ItineraryID AND S.ShipID = C.ShipID AND I.ShipID = S.ShipID AND B.BookingID = " + bookingID;

            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    PassengerIDLabel.Text = rdr["PassengerID"].ToString();
                    NameLabel.Text = rdr["Name"].ToString();

                    ItineraryIDLabel.Text = rdr["ItineraryID"].ToString();
                    RegionLabel.Text = (String)rdr["Region"];
                    ImageButton1.ImageUrl = (String)rdr["ItineraryDetails"];
                    SourceLabel.Text = (String)rdr["Source"];
                    PriceLabel.Text = rdr["Price"].ToString();
                    ShipNameLabel.Text = (String)rdr["ShipName"];

                    OperatorLabel.Text = (String)rdr["CruiseOperator"];

                    BDateLabel.Text = dateToString((DateTime)rdr["BookingDate"]);
                    JDateLabel.Text = dateToString((DateTime)rdr["JourneyDate"]);
                    CabinTypeLabel.Text = (String)rdr["CabinName"];
                    CabinPriceLabel.Text = rdr["CabinPrice"].ToString();
                    CabinCapacityLabel.Text = (String)rdr["Capacity"].ToString();

                    Decimal total = Convert.ToDecimal(CabinPriceLabel.Text) + Convert.ToDecimal(PriceLabel.Text);
                    TotalPriceLabel.Text = total.ToString();
                }
                conn.Close();
            }
            catch (Exception err)
            {
                conn.Close();
                notification.Text = "Error: " + err.Message;
                notification.ForeColor = System.Drawing.Color.Red;
            }
        }
        private String dateToString (DateTime date){
            String sqlDate = date.ToString("dddd, MMMM d, yyyy");
            return sqlDate;
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            String email = "breenatheseira-facilitator@yahoo.com";

            bool TestMode = true;
            string url = TestMode ?
               "https://www.sandbox.paypal.com/us/cgi-bin/webscr" :
               "https://www.paypal.com/us/cgi-bin/webscr";

            String ReturnUrl = "https://google.com";
            String CancelUrl = "https://youtube.com";
            String booking = (String)Request.Params["BookingID"];
            String item_name = "BookingID: " + booking + " - Region: " + RegionLabel.Text + " using Cabin Type: " + CabinTypeLabel.Text;

            var builder = new StringBuilder();
            builder.Append(url);
            builder.AppendFormat("?cmd=_xclick&business={0}", HttpUtility.UrlEncode(email));
            builder.Append("&lc=US&no_note=0&currency_code=USD");
            builder.AppendFormat("&item_name={0}", HttpUtility.UrlEncode(item_name));
            builder.AppendFormat("&invoice={0}", booking);
            builder.AppendFormat("&amount={0}", TotalPriceLabel.Text);
            builder.AppendFormat("&return={0}", HttpUtility.UrlEncode(ReturnUrl));
            builder.AppendFormat("&cancel_return={0}", HttpUtility.UrlEncode(CancelUrl));
            builder.AppendFormat("&quantity={0}", 1);

            Response.Redirect(builder.ToString());
        }
    }
}