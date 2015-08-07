using System;
using System.Linq;
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
    public partial class Register : Page
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);
        protected void CreatePassenger(object sender, EventArgs e)
        {
            String name = nameBox.Text;
            String passport = passportBox.Text;
            String nationality = nationalityBox.Text;
            String age = ageBox.Text;
            String email = emailBox.Text;
            String password = passportBox.Text;
            String gender = femaleRB.Selected ? femaleRB.Value : maleRB.Value;
            String sql;
            SqlCommand cmd;

            try
            {
                sql = "SELECT passport, email FROM Passenger WHERE Passport = @passport OR Email = @email";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@passport", passport);
                cmd.Parameters.AddWithValue("@email", email);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    if (passport.Equals(rdr["Passport"].ToString()) || email.Equals(rdr["Email"].ToString()))
                    {
                        notification.Text = "You already have an account. Please login in using your existing credentials.";
                        return;
                    }
                }
                conn.Close();

                sql = "INSERT INTO Passenger (Name, Passport, Nationality, Age, Gender, Email, Password) " +
                            "VALUES (@name, @passport, @nationality, @age, @gender, @email, @password)";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@passport", passport);
                cmd.Parameters.AddWithValue("@nationality", nationality);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                Session["Registered"] = "Y";
                Response.Redirect("~/Account/Register.aspx");
            }
            catch (Exception err)
            {
                notification.Text = "Error: Please try again. " + err.Message;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ("Y".Equals((String)Session["Registered"]))
            {
                notification.Text = "Your registration is successful.";
                Session["Registered"] = "";
            }
        }
    }
}