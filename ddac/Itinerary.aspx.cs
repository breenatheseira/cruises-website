﻿using System;
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
        public SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DDACConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ilbind();
            }
        }

        protected void ilbind()
        {
            try
            {            
				if (Session["price"] == null)
					Session["price"] = (Int32)1000000;

                String ITcolumns = "I.ItineraryDetails, I.ItineraryID, I.Region, I.Source, I.Price, I.ShipID ";
				//String Icolumns = "I.ItineraryDetails, I.ItineraryID, I.Region, I.Source, I.Price, ID.ItineraryScheduleID, ID.JourneyDate, I.ShipID ";
                String sql = "SELECT DISTINCT " + ITcolumns + " FROM Itinerary I, ItinerarySchedule ID " +
                    "WHERE I.ItineraryID = ID.ItineraryID AND I.Price < " + (Int32)Session["price"] + " AND JourneyDate > GETDATE() ";

				if (!string.IsNullOrEmpty((String)Session["region"]))
                    sql += "AND Region = '" + (String)Session["region"] + "' ";

				if (Session["dateTo"] != null && Session["dateFrom"] != null)
                    sql += "AND JourneyDate BETWEEN '" + (String)Session["dateFrom"] + "' AND '" +
                        (String)Session["dateTo"] + "'";

                sql += " AND ID.ItineraryScheduleID IN " +
                        "(SELECT R.ItineraryScheduleID FROM (SELECT B.ItineraryScheduleID, SUM(TotalInShip) - SUM(TotalCabinBooked) AS TotalCabinRemaining FROM (" +
                        " SELECT B.ItineraryScheduleID, C.TotalInShip, COUNT(B.CabinID) AS TotalCabinBooked FROM Booking B RIGHT JOIN Cabin C ON C.CabinID = B.CabinID" +
                        " GROUP BY B.ItineraryScheduleID, B.CabinID, C.TotalInShip) B GROUP BY B.ItineraryScheduleID) R WHERE R.TotalCabinRemaining > 0) ORDER BY I.ItineraryID";

				SqlCommand cmd = new SqlCommand(sql, conn);
				conn.Open();
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

        protected DataTable GetJourneyDate(int itid)
        {
            DataTable DT = default(DataTable);
            try
            {
                String sqlString = "SELECT ID.JourneyDate AS JD FROM ItinerarySchedule ID INNER JOIN " +
                    "(SELECT R.ItineraryScheduleID FROM (SELECT B.ItineraryScheduleID, SUM(TotalInShip) - SUM(TotalCabinBooked) AS TotalCabinRemaining FROM ( " +
                    "SELECT B.ItineraryScheduleID, C.TotalInShip, COUNT(B.CabinID) AS TotalCabinBooked FROM Booking B RIGHT JOIN Cabin C ON C.CabinID = B.CabinID " +
                    "GROUP BY B.ItineraryScheduleID, B.CabinID, C.TotalInShip) B GROUP BY B.ItineraryScheduleID) R WHERE R.TotalCabinRemaining > 0) Q " +
                    "ON ID.ItineraryScheduleID = Q.ItineraryScheduleID WHERE ID.ItineraryID = " + itid + " ORDER BY ID.JourneyDate";

                SqlCommand cmd1 = new SqlCommand(sqlString, conn1);
                conn1.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                da.Fill(ds, "Results");
                DT = ds.Tables["Results"];
                conn1.Close();
            }
            catch (Exception err)
            {
                conn1.Close();
                notification.ForeColor = System.Drawing.Color.Red;
                notification.Text = err.Message;
            }
            return DT;
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
            ilbind();
        }
    }
}