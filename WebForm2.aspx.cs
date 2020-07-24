using StormWeb.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StormWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          

            //Establish connection to database, retrieve the latest entry, just for demonstratino purposes
            DBMaster dbm = new DBMaster();
            SqlDataReader reader = dbm.getReader("SELECT TOP 1 * FROM [dbo].[tblFullname] ORDER BY ID DESC");
            string userName = "";
            while (reader.Read()) {
                userName += reader["Fullname"].ToString();
            }
            dbm.closeConnection();
            //user Greeting
            userGreeting.Text += userName;



            //This retrives the user's tags from the database
            //string[] userTagArr = new string[100];
            List<string> userTagList = new List<string>();
            //int n = 0;
            SqlDataReader reader1 = dbm.getReader("SELECT * FROM tblPersontags WHERE Fullname='"+userName+"'");
            while (reader1.Read())
            {
                //userTagArr[n] = reader1["tag"].ToString();
                //n++;
                userTagList.Add(reader1["tag"].ToString().TrimEnd());
            }
            dbm.closeConnection();

            //This populates the URL dictionary
            Dictionary<string, string> siteURL =
            new Dictionary<string, string>();
            SqlDataReader reader2 = dbm.getReader("SELECT * FROM [dbo].[tblSiteurl] ORDER BY ID DESC");
            while (reader2.Read())
            {
                string sitenametemp = reader2["Sitename"].ToString();
                string sitelinktemp = reader2["Siteurl"].ToString();
                siteURL.Add(sitenametemp, sitelinktemp);
            }
            dbm.closeConnection();







            //This populates the tag dictionary
            string temp2 = "";
            Dictionary<string, int> siteCompatibilityIndex =
            new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> x in siteURL)
            {
                //string[] siteTagArr = new string[100];
                List<string> siteTagList = new List<string>();
                //int i = 0;
                SqlDataReader reader3 = dbm.getReader("SELECT * FROM tblSitetags WHERE Sitename='" + x.Key + "'");
                while (reader3.Read())
                {
                    siteTagList.Add(reader3["Sitetag"].ToString().TrimEnd());
                    //siteTagArr[i] = reader3["Sitetag"].ToString();
                    //i++;
                }
                dbm.closeConnection();
                TagHandler tagHandler = new TagHandler();
                
                int compatibilityIndex = userTagList.Intersect(siteTagList).Count();
                siteCompatibilityIndex.Add(x.Key.ToString(), compatibilityIndex);
                //temp2 += x.Key.ToString();
                //temp2 += compatibilityIndex;
                //temp2 += "</br>";


            }
            //lbl_test.Text = temp2;

            var top5 = siteCompatibilityIndex.OrderByDescending(pair => pair.Value).Take(5)
               .ToDictionary(pair => pair.Key, pair => pair.Value);


            //Populate the links with the best-fit sites
            int recommendedSiteCount = 0;
            string[] recommendedSites = new string[5];
            foreach (KeyValuePair<string, int> x in top5)
            {
                recommendedSites[recommendedSiteCount] = x.Key;
                recommendedSiteCount++;
            }
            HyperLink0.Text = recommendedSites[0];
            HyperLink0.NavigateUrl = siteURL[recommendedSites[0]];
            HyperLink1.Text = recommendedSites[1];
            HyperLink1.NavigateUrl = siteURL[recommendedSites[1]];
            HyperLink2.Text = recommendedSites[2];
            HyperLink2.NavigateUrl = siteURL[recommendedSites[2]];
            HyperLink3.Text = recommendedSites[3];
            HyperLink3.NavigateUrl = siteURL[recommendedSites[3]];
            HyperLink4.Text = recommendedSites[4];
            HyperLink4.NavigateUrl = siteURL[recommendedSites[4]];

        }


    }
}