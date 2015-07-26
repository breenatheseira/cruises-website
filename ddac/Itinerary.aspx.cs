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
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT I.*, ShipName FROM Itinerary I, Ship S WHERE I.ShipID = S.ShipID ORDER BY ItineraryID", conn);
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