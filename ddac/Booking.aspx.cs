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
    public partial class Booking : System.Web.UI.Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);
        int ShipID;

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
                    Session["dateDDL"] = dateDDL.SelectedValue;
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
            Session["dateDDL"] = dateDDL.SelectedValue;
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
    }
}