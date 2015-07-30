﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SendGrid;
using System.Net;
using System.Net.Mail;

namespace ddac
{
    public partial class Booking : System.Web.UI.Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);
        SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);
        int ShipID;
        String sql;
        String itineraryID;

        protected void Page_Load(object sender, EventArgs e)
        {
            String PassengerID = (String)Session["PassengerID"];
            if (string.IsNullOrEmpty(PassengerID))
            {
                Session["FromBooking"] = (String)Request.Params.Get("ItineraryID");
                Response.Redirect("/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                String itineraryID = (String)Request.Params.Get("ItineraryID");
                if (!string.IsNullOrEmpty(itineraryID))
                {
                    ItineraryIDLabel.Text = itineraryID;

                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT I.*, ShipName FROM Itinerary I, Ship S WHERE I.ShipID = S.ShipID AND ItineraryID = '" + ItineraryIDLabel.Text + "'", conn);
                        SqlDataReader dr = cmd.ExecuteReader();
    
                        if (dr.Read())
                        {
                            RegionLabel.Text = (String)dr["Region"];
                            SourceLabel.Text = (String)dr["Source"];
                            PriceLabel.Text = dr["Price"].ToString();
                            Session["ShipID"] = Convert.ToInt32(dr["ShipID"]);
                            ShipID = Convert.ToInt32(dr["ShipID"]);
                            ShipNameLabel.Text = (String)dr["ShipName"];
                            ImageButton1.ImageUrl = (String)dr["ItineraryDetails"];
                        }
                        else
                        {
                            notification.Text = "ItineraryNo: #" + ItineraryIDLabel.Text + " values could not be found.";
                            notification.ForeColor = System.Drawing.Color.Red;
                        }
                        conn.Close(); 
                    }
                    catch (Exception err)
                    {
                        conn.Close();
                        notification.Text = err.Message;
                        notification.ForeColor = System.Drawing.Color.Red;
                    }
                    jdlbind();
                    DateTime date = Convert.ToDateTime(dateDDL.SelectedValue);
                    String sqlDate = date.ToString("yyyy-MM-dd");
                    Session["dateDDL"] = sqlDate;
                    cabinlbind();
                    clbind();
                }
                else
                {
                    Response.Redirect("./Itinerary.aspx");
                }               
            }
        }

        protected void clbind()
        {
            try
            {
                conn.Open();
                String sql = "SELECT B.ItineraryScheduleID, B.TotalInShip, B.CabinID, B.Capacity, B.CabinName, B.CabinPrice, B.Available FROM " +
                        "(SELECT B.ItineraryScheduleID, C.TotalInShip, C.CabinID, C.Capacity, C.CabinName, C.CabinPrice, (C.TotalInShip - COUNT(B.CabinID)) AS Available " +
                        "FROM Cabin C LEFT JOIN Booking B ON C.CabinID = B.CabinID WHERE C.ShipID = " + (Int32)Session["ShipID"] + " " +
                        "GROUP BY B.ItineraryScheduleID, C.Capacity, C.CabinID, C.TotalInShip, C.Capacity, C.CabinName, C.CabinPrice) B INNER JOIN ItinerarySchedule I " +
                        "ON I.ItineraryScheduleID = B.ItineraryScheduleID AND B.ItineraryScheduleID = (SELECT ItineraryScheduleID FROM ItinerarySchedule WHERE " +
                        "JourneyDate = '" + (String)Session["dateDDL"] + "' AND ItineraryID = " + (String)Request.Params.Get("ItineraryID") + ")";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CabinList.DataSource = ds;
                CabinList.DataBind();
                conn.Close();
            }
            catch (Exception err)
            {
                conn.Close();
                notification.ForeColor = System.Drawing.Color.Red;
                notification.Text = err.Message;
            }
        }

        protected void jdlbind()
        {
            String sql = "SELECT JourneyDate FROM ItinerarySchedule WHERE ItineraryID = " + (String)Request.Params.Get("ItineraryID") + " AND JourneyDate > GETDATE() ORDER BY JourneyDate ";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dateDDL.DataSource = ds;
                dateDDL.DataTextField = "JourneyDate";
                dateDDL.DataValueField = "JourneyDate";
                dateDDL.DataTextFormatString = "{0:D}";
                dateDDL.DataBind();
                conn.Close();
            }
            catch (Exception err)
            {
                conn.Close();
                notification.ForeColor = System.Drawing.Color.Red;
                notification.Text = err.Message;
            }
        }
        protected void dateDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(dateDDL.SelectedValue);
            String sqlDate = date.ToString("yyyy-MM-dd");
            Session["dateDDL"] = sqlDate;
            clbind();
        }

        protected void cabinlbind()
        {
            String sql = "SELECT CabinID, CabinName FROM CABIN WHERE SHIPID = (SELECT ShipID FROM Itinerary WHERE ItineraryID = (SELECT ItineraryID FROM ItinerarySchedule WHERE ItineraryScheduleID = " +
                         "(SELECT ItineraryScheduleID FROM ItinerarySchedule WHERE JourneyDate = '" + (String)Session["dateDDL"] + "' AND ItineraryID = " + (String)Request.Params.Get("ItineraryID") + ")))";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cabinDDL.DataSource = ds;
                cabinDDL.DataTextField = "CabinName";
                cabinDDL.DataValueField = "CabinID";
                cabinDDL.DataBind();
                conn.Close();
            }
            catch (Exception err)
            {
                conn.Close();
                notification.ForeColor = System.Drawing.Color.Red;
                notification.Text = err.Message;
            }
        }

        protected void BookButton_Click(object sender, EventArgs e)
        {
            String PassengerID = (String)Session["PassengerID"];
            sql = "SELECT Name, Email FROM Passenger WHERE PassengerID = '" + PassengerID + "'";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    String PassengerName = rdr["Name"].ToString();
                    String PassengerEmail = rdr["Email"].ToString();
                    String dateDLL = (String)Session["dateDDL"];
                    conn.Close();
                    try
                    {
                        String sql1 = "INSERT INTO Booking ([PassengerID],[ItineraryScheduleID],[CabinID],[BookingDate],[BookingStatus],[PaymentStatus]) " +
                                    "OUTPUT INSERTED.BookingID VALUES (@pass_id, dbo.fx_getItineraryScheduleID(@itid, @journeydate), @cabin_id, GETDATE(), 'B', 'N')";
                        SqlCommand insertCommand = new SqlCommand(sql1, conn1);
                        insertCommand.Parameters.AddWithValue("@pass_id", PassengerID);
                        insertCommand.Parameters.AddWithValue("@itid", ItineraryIDLabel.Text);
                        insertCommand.Parameters.AddWithValue("@journeydate", dateDLL);
                        insertCommand.Parameters.AddWithValue("@cabin_id", cabinDDL.SelectedValue);

                        conn1.Open();
                        Int32 BookingId = Convert.ToInt32(insertCommand.ExecuteScalar().ToString());
                        conn1.Close();

                        sql = "SELECT CabinPrice FROM Cabin WHERE CabinID = " + cabinDDL.SelectedValue;
                        cmd = new SqlCommand(sql, conn);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        Decimal cPrice = 0;
                        if (dr.Read())
                        {
                            cPrice = Convert.ToDecimal(dr["CabinPrice"].ToString());
                            Decimal Total = Convert.ToDecimal(PriceLabel.Text) + cPrice;
                            conn.Close();
                            Response.Redirect("./Payment.aspx?bookingID=" + BookingId + "&item_name=" + RegionLabel.Text + "&cabin=" + cabinDDL.SelectedItem.ToString() + "&total=" + Total);
                        }
                        else
                        {
                            conn.Close();
                            notification.Text = "Error: Cabin does not exist!";
                            notification.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch (Exception err)
                    {
                        conn1.Close();
                        notification.Text = "Error: " + err.Message;
                        notification.ForeColor = System.Drawing.Color.Red;
                    }
                    //Send(PassengerName, PassengerEmail);
                }
            }
            catch (Exception err)
            {
                conn.Close();
                notification.ForeColor = System.Drawing.Color.Red;
                notification.Text = err.Message;
            }
        }

        public void Send(string ToName, string ToEmail)
        {
            // SendGrid credentials
            var credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["emailServiceUserName"],
                ConfigurationManager.AppSettings["emailServicePassword"]);


            // Create the email object first, then add the properties.
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(ToEmail);
            myMessage.From = new MailAddress("Admin@carnivalcorporation.com", "Carnival Corporation");
            myMessage.Subject = "Successful Purchase of Cruise Ticket. Booking #1";
            myMessage.Text = "You have successfully purchased ... ticket to ..., which is scheduled to leave on .... Your BookingID is ...";
            //myMessage.AddAttachment(@"https://ddac.blob.core.windows.net/itinerarydetails/ALASKA%20CRUISES%20FROM%20ANCHORAGE%20(WHITTIER).jpg");

            // Create an Web transport for sending email, using credential
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.DeliverAsync(myMessage);
        }
    }
}