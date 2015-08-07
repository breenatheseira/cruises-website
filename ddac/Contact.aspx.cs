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
using SendGrid;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace ddac
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Submit_Click(object sender, EventArgs e)
        {
            String contact = "";

            if (!string.IsNullOrWhiteSpace(mobile.Text))
                contact = "Contact number: " + mobile.Text;

            // SendGrid credentials
            var credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["SendGridUserId"],
                ConfigurationManager.AppSettings["SendGridPassword"]);

            // Create the email object first, then add the properties.
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo("carnival.corporation@mail.com");
            myMessage.From = new MailAddress(email.Text, name.Text);
            myMessage.Subject = "Feedback: " + subject.Text;

            myMessage.Html = contact + "<p>" + message.Text + "</p>";

            // Create an Web transport for sending email, using credential
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.DeliverAsync(myMessage);

            email.Text = "";
            name.Text = "";
            mobile.Text = "";
            subject.Text = "";
            message.Text = "";
        }
    }
}