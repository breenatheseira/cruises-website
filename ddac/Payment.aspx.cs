using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ddac
{
    public partial class Payment : System.Web.UI.Page
    {
        public String item_name {get; set;}
        public String total_price { get; set; }
        public String email { get; set; }
        public String website { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            // to be removed once booking is done with redirecting params
            if (string.IsNullOrEmpty((String)Request.Params["bookingID"])) 
                Response.Redirect("Payment.aspx?bookingID=1&item_name=Asia Pacific&cabin=Oceanview&total=5000");

            String booking = (String)Request.Params["bookingID"];
            String item = (String)Request.Params["item_name"];
            String cabin = (String)Request.Params["cabin"];
            String total = (String)Request.Params["total"];

            item_name = "BookingID: " + booking + " - Region: " + item + " using Cabin Type: " + cabin;
            total_price = total;
            email = "breenatheseira-facilitator@yahoo.com";
            website = "https://www.sandbox.paypal.com/cgi-bin/webscr";
        }
    }
}