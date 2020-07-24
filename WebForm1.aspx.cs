using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace StormWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == true) {
                Label3.Text = ("Great job! Data was inserted");
            }
        }


        protected void populateTagsDatabase() {
            //declaring a list of strings to store the tags 
            List<string> tagsList = new List<string>();

            //defining the persons attributes
            string fullname = TextBox1.Text;
            int age = Int32.Parse(TextBox2.Text);
            string maritalStatus = DropDownList1.Text;
            string gender = DropDownList2.Text;
            string occupation = TextBox3.Text;

            //set age tags
            if (age >= 65)
            {
                tagsList.Add("Elderly");
            }
            else if (age >= 35)
            {
                tagsList.Add("Adult");
            }
            else if (age >= 21)
            {
                tagsList.Add("Young Adult");
            }
            else if (age >= 16)
            {
                tagsList.Add("Teenager");
            }
            else {
                tagsList.Add("Child");
            }

            //set marital status tags
            tagsList.Add(maritalStatus);

            //set gender tags
            tagsList.Add(gender);

            //set occupation tags
            tagsList.Add(occupation);

            //send tags to database
            if (tagsList.Count > 0) {
                for (int i = 0; i < (tagsList.Count); i++) {
                    SqlConnection sqlConn = new SqlConnection("Server=tcp:cpftestingserver.database.windows.net,1433;Initial Catalog=StormWebDB;Persist Security Info=False;User ID=cpfadmin;Password=p@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                    {
                        SqlCommand insert = new SqlCommand("EXEC dbo.InsertTags @Fullname, @tag", sqlConn);
                        insert.Parameters.AddWithValue("@Fullname", TextBox1.Text);
                        insert.Parameters.AddWithValue("@tag", tagsList[i]);

                        sqlConn.Open();
                        insert.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
            }
        }

        protected void populatePersonDatabase() {
            SqlConnection sqlConn = new SqlConnection("Server=tcp:cpftestingserver.database.windows.net,1433;Initial Catalog=StormWebDB;Persist Security Info=False;User ID=cpfadmin;Password=p@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            {
                SqlCommand insert = new SqlCommand("EXEC dbo.InsertPerson @Fullname, @Age, @Maritalstatus, @Gender, @Employment", sqlConn);
                insert.Parameters.AddWithValue("@Fullname", TextBox1.Text);
                insert.Parameters.AddWithValue("@Age", TextBox2.Text);
                insert.Parameters.AddWithValue("@Maritalstatus", DropDownList1.Text);
                insert.Parameters.AddWithValue("@Gender", DropDownList2.Text);
                insert.Parameters.AddWithValue("@Employment", TextBox3.Text);

                sqlConn.Open();
                insert.ExecuteNonQuery();
                sqlConn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            populatePersonDatabase();
            populateTagsDatabase();
            if (IsPostBack)
            {
                Response.Redirect("WebForm2.aspx");
            }
        }
    }
}