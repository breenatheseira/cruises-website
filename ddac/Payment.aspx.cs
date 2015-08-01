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
        String sql;
        SqlCommand cmd;
        SqlDataReader rdr;

        String email = "breenatheseira-facilitator@yahoo.com";
        String ReturnUrl = "http://carnivalcruise.azurewebsites.net/Payment.aspx";
        String CancelUrl = "http://carnivalcruise.azurewebsites.net/Payment.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            // to be removed once booking is done with redirecting params
            if (string.IsNullOrEmpty((String)Request.Params["bookingID"]))
                Response.Redirect("./Itinerary.aspx");

            String bookingID = (String)Request.Params["bookingID"];
            LoadValues(bookingID);

            if (!string.IsNullOrEmpty((String)Request.Params["tx"]))
            {
                if (!string.IsNullOrEmpty((String)Request.Params["st"]))
                {
                    if ("Completed".Equals((String)Request.Params["st"]))
                    {
                        Session["CancelledBooking"] = "";
                        UpdatePaymentStatus(bookingID);
                        PayButton.Visible = false;
                        notification.Text = "Thank you for your payment. Your transaction has been completed, and a receipt for your purchase has been emailed to you. You may log into your account at www.paypal.com to view details of this transaction.";
                        notification.ForeColor = System.Drawing.Color.Green;
                        setPaymentStatusLabel((String)Request.Params["PaymentStatus"], (String)Request.Params["amt"], bookingID);
                    }
                    else
                    {
                        Session["CancelledBooking"] = "";
                        paymentFailed();
                    }
                }
            }
            if (bookingID.ToString().Equals((String)Session["CancelledBooking"]))
            {
                Session["CancelledBooking"] = "";
                paymentFailed();
            }            
        }

        private void paymentFailed()
        {
            notification.Text = "Payment Failed. Please try again to confirm your booking to secure your room for the cruise.";
            notification.ForeColor = System.Drawing.Color.Red;
            PayButton.Visible = true;
        }

        private String dateToString (DateTime date){
            String sqlDate = date.ToString("dddd, MMMM d, yyyy");
            return sqlDate;
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            bool TestMode = true;
            string url = TestMode ?
               "https://www.sandbox.paypal.com/us/cgi-bin/webscr" :
               "https://www.paypal.com/us/cgi-bin/webscr";

            String booking = (String)Request.Params["BookingID"];

            String item_name = "BookingID: " + booking + " - Region: " + RegionLabel.Text + " using Cabin Type: " + CabinTypeLabel.Text;
            CancelUrl += "?BookingID=" + booking;
            ReturnUrl += "?BookingID=" + booking + "&PaymentStatus=P";

            var builder = new StringBuilder();
            builder.Append(url);
            builder.AppendFormat("?cmd=_xclick&business={0}", HttpUtility.UrlEncode(email));
            builder.Append("&lc=US&no_note=0&currency_code=USD");
            builder.AppendFormat("&item_name={0}", HttpUtility.UrlEncode(item_name));
            builder.AppendFormat("&invoice={0}", booking);
            builder.AppendFormat("&amount={0}", TotalPriceLabel.Text);
            builder.AppendFormat("&return={0}", HttpUtility.UrlEncode(ReturnUrl));
            builder.AppendFormat("&custom={0}", booking);
            builder.AppendFormat("&cancel_return={0}", CancelUrl);
            builder.AppendFormat("&quantity={0}", 1);

            Session["CancelledBooking"] = booking;

            Response.Redirect(builder.ToString());
        }

        protected void LoadValues(String bookingID)
        {
            sql = "SELECT B.BookingID, B.PassengerID, B.BookingDate, B.PaymentStatus, P.Name, " +
                         "I.ItineraryID, I.Region, I.ItineraryDetails, I.Source, I.Price, " +
                         "SI.JourneyDate, C.CabinName, C.CabinPrice, C.Capacity, " +
                         "S.ShipName, S.CruiseOperator FROM Booking B, Itinerary I, ItinerarySchedule SI, Cabin C, Ship S, Passenger P " +
                         "WHERE B.ItineraryScheduleID = SI.ItineraryScheduleID AND C.CabinID = B.CabinID AND P.PassengerID = B.PassengerID AND " +
                         "I.ItineraryID = SI.ItineraryID AND S.ShipID = C.ShipID AND I.ShipID = S.ShipID AND B.BookingID = " + bookingID;

            cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();

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
                    CabinCapacityLabel.Text = rdr["Capacity"].ToString();

                    Decimal total = Convert.ToDecimal(CabinPriceLabel.Text) + Convert.ToDecimal(PriceLabel.Text);

                    setPaymentStatusLabel(rdr["PaymentStatus"].ToString(), total.ToString(), bookingID);

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

        protected void UpdatePaymentStatus(String bookingID)
        {
            sql = "UPDATE Booking SET PaymentStatus = 'P' WHERE BookingID = " + bookingID;
            cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            } catch (Exception err)
            {
                conn.Close();
                notification.Text = "Error: " + err.Message;
                notification.ForeColor = System.Drawing.Color.Red;

            }
        }

        private void setPaymentStatusLabel(String status, String total, String bookingID)
        {
            if ("P".Equals(status))
            {
                PaymentStatusLabel.Text = "Paid";
                PaymentStatusLabel.ForeColor = System.Drawing.Color.Green;
                HeadingLabel.Text = "Details of Booking #" + bookingID;
                PayButton.Visible = false;
            }
            else
            {
                PaymentStatusLabel.Text = "Payment Pending. Please pay $" + total + " to confirm your booking.";
                PaymentStatusLabel.ForeColor = System.Drawing.Color.Red;
                HeadingLabel.Text = "Please Confirm Booking #" + bookingID + " :";
            }
        }
    }
}