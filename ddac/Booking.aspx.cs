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
                ItineraryIDLabel.Text = (String)Request.Params.Get("ItineraryID");

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
                        ShipID = Convert.ToInt32(dr["ShipID"]);
                        ShipNameLabel.Text = (String)dr["ShipName"];
                        ImageButton1.ImageUrl = (String)dr["ItineraryDetails"];
                    }
                    else
                    {
                        notification.Text = "ItineraryNo: #" + ItineraryIDLabel.Text + " values could not be found.";
                        notification.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception err)
                {
                    conn.Close();
                    notification.ForeColor = System.Drawing.Color.Red;
                    notification.Text = err.Message;
                }
                conn.Close();
                clbind();
            }
        }

        protected void clbind()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cabin WHERE ShipID = '" + ShipID + "'", conn);
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
    }
}