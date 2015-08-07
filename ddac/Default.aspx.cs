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
            Response.Redirect("/Itinerary.aspx");
        }
    }
}