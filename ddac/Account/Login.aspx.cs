using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ddac.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ddac.Account
{
    public partial class Login : Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);
        String sql;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }

            if (!string.IsNullOrEmpty((String)Session["PleaseLogin"]))
            {
                ErrorMessage.Visible = true;
                FailureText.Text = (String)Session["PleaseLogin"] + "\n";
                Session["PleaseLogin"] = "";
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            ValidatePassengers(UserName.Text, Password.Text);
            //if (IsValid)
            //{
            // Validate the user password
            //var manager = new UserManager();
            //ApplicationUser user = manager.Find(UserName.Text, Password.Text);
            //if (user != null)
            //{
            //    IdentityHelper.SignIn(manager, user, RememberMe.Checked);
            //    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //}
            //else
            //{
            //    FailureText.Text = "Invalid username or password.";
            //    ErrorMessage.Visible = true;
            //}
            //}
        }

        protected bool ValidatePassengers(String email, String pwd)
        {
            bool result = false;
            sql = "SELECT PassengerID, Name FROM Passenger WHERE Email = @email AND Password = @pwd";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pwd", pwd);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                Session["PassengerID"] = rdr["PassengerID"].ToString();
                Session["PassengerName"] = rdr["Name"].ToString();

                String FromBooking = (String)Session["FromBooking"];
                if (!string.IsNullOrEmpty(FromBooking))
                {
                    Response.Redirect("../Booking.aspx?ItineraryID=" + FromBooking);
                }
                Response.Redirect("../Default.aspx");
            }
            else
            {
                FailureText.Text = "Invalid username or password.";
                ErrorMessage.Visible = true;
            }
            conn.Close();
            return result;
        }
    }
}