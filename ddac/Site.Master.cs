using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ddac
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String PID = (String)Session["PassengerID"];
            if (string.IsNullOrEmpty(PID))
            {
                RegisterLink.Visible = true;
                LoginLink.Visible = true;
                WelcomeText.Visible = false;
                LogOffLink.Visible = false;
                RegisterLink1.Visible = true;
                LoginLink1.Visible = true;
                WelcomeText1.Visible = false;
                LogOffLink1.Visible = false;

                String myBookingPage = "/MyBooking";
                String paymentPage = "/Payment";
                String bookingPage = "/Booking";

                String currentPage = Request.CurrentExecutionFilePath;

                if (currentPage.Equals(myBookingPage) || currentPage.Equals(bookingPage) || currentPage.Equals(paymentPage))
                {
                    Session["PleaseLogin"] = "Please login to view the requested page";
                    Response.Redirect("./Account/Login.aspx");
                }
            }
            else
            {
                WelcomeText.Text = "Hello, " + (String)Session["PassengerName"];
                RegisterLink.Visible = false;
                LoginLink.Visible = false;
                WelcomeText.Visible = true;
                LogOffLink.Visible = true;
                RegisterLink1.Visible = false;
                LoginLink1.Visible = false;
                WelcomeText1.Visible = true;
                LogOffLink1.Visible = true;
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }
    }

}