using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ddac
{
    public partial class _Default : Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);
        String sql;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            //String region = RegionDropDown.SelectedValue.ToString();
            //DateTime dateFrom = Convert.ToDateTime(Request["datepicker"].ToString());
            //DateTime dateTo = Convert.ToDateTime(Request["datepicker1"].ToString());
            //Decimal price = Convert.ToDecimal(Request["price"].ToString());

            Response.Redirect("/Itinerary.aspx");
        }
    }
}