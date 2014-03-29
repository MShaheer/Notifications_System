using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pral_work_1
{
    public partial class user_notification : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["notifications_db_cs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)  //if not logged in redirect to login page
            {
                Response.Redirect("login.aspx", true);
            }
            else     //if logged in show the page
            {
                /* Populating the page with dynamic checkboxes after getting them from Notification_Types table */

                string strQuery = "SELECT * FROM [Notification_Types]";    // Fetch rows of Notification type from database
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand sqlCmd = new SqlCommand(strQuery, connection);
                DataTable dt = new DataTable();                            //DataTable which will be used as datasource 
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                sqlDa.Fill(dt);
                CheckBoxList CheckBoxList1 = new CheckBoxList();           //Create a dynamic CheckBoxList
                CheckBoxList1.ID = "CheckBoxList1";
                CheckBoxList1.DataSource = dt;                              //Set the datasource of CheckBoxList1
                CheckBoxList1.DataTextField = "Notification_Description";  //Set the text of checkbox
                CheckBoxList1.DataValueField = "Notification_Type_ID";     //Set the value of textbox
                CheckBoxList1.DataBind();                                  //Bind the data to CheckBoxList1
                CheckBoxList1.Visible = true;
                PlaceHolder1.Controls.Add(CheckBoxList1);                  //Place the created CheckBoxList1 inside the placeholder 
                connection.Close();
            }

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            
            /* Iterate through the CheckboxList and perform INSERT OR UPDATE queries based on the existing entries in the database */
            CheckBoxList CheckBoxList1 = (CheckBoxList)PlaceHolder1.FindControl("CheckBoxList1");   //Find the dynamic CheckBoxList1 which was placed inside PlaeHolder1
            foreach(ListItem li in CheckBoxList1.Items)
            {
                int count = CheckBoxList1.Items.Count;
                int value = Convert.ToInt32(li.Value);
                string text = li.Text;
                bool ischecked = li.Selected;
                updatenotifications(value, text, ischecked);    //Call the method which will perform the task
            }
             Response.Redirect("index.aspx", true);
        }


        /* This is the main method which is performing Insert or update database queries */
        private void updatenotifications(int value, string text, bool ischecked)
        {
            //throw new NotImplementedException();
            string u_id=Request.Cookies["UserSettings"]["Name"];  //u_id is the user account id of the currently logged in user, this can be chenaged to any field of choice
            Console.Write(u_id);
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlCmd = new SqlCommand("SELECT * from Virtual_Player_Notifications WHERE User_Account_ID=@u_id AND Notification_Type_ID=@type_id", connection);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            try
            {
                sqlCmd.Parameters.AddWithValue("@u_id", u_id.ToString());
                sqlCmd.Parameters.AddWithValue("@type_id", value);

                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    /* Entry was already in the database of this user, Need an update */
                    string updateSQL;
                    updateSQL = "UPDATE Virtual_Player_Notifications SET ";
                    updateSQL += "User_Account_ID=@u_id, Notification_Type_ID=@type_id,isActive=@ischecked ";
                    updateSQL += "WHERE User_Account_ID=@u_id AND Notification_Type_ID=@type_id";
                 
                    SqlConnection con = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand(updateSQL, con);

                    cmd.Parameters.AddWithValue("@u_id", u_id);
                    cmd.Parameters.AddWithValue("@type_id", value);
                    cmd.Parameters.AddWithValue("@ischecked", ischecked);
                                      
                    int updated = 0;
                    try
                    {
                        con.Open();
                        updated = cmd.ExecuteNonQuery();
                        lblResults.Text = updated.ToString() + " record updated.";
                    }
                    catch (Exception err)
                    {
                        lblResults.Text = "Error updating. ";
                        lblResults.Text += err.Message;
                    }
                    finally
                    {
                        sqlDa.Dispose();
                        dt.Dispose();
                        con.Close();
                    }


                }  //end of if condition (when entry was already in database)
                else { 
                    /* New entry should be made for the user */
                    string insertSQL;
                    insertSQL = "INSERT INTO Virtual_Player_Notifications (";
                    insertSQL += "User_Account_ID,Notification_Type_ID,isActive) ";
                    insertSQL += "VALUES (";
                    insertSQL += "@u_id, @type_id, @ischecked)";
                 
                    SqlConnection con = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand(insertSQL, con);

                    cmd.Parameters.AddWithValue("@u_id", u_id);
                    cmd.Parameters.AddWithValue("@type_id", value);
                    cmd.Parameters.AddWithValue("@ischecked", ischecked);
                  
                    int added = 0;
                    try
                    {
                        con.Open();
                        added = cmd.ExecuteNonQuery();
                        lblResults.Text = added.ToString() + " record inserted.";
                    }
                    catch (Exception err)
                    {
                        lblResults.Text = "Error inserting record. ";
                        lblResults.Text += err.Message;
                    }
                    finally
                    {
                        sqlDa.Dispose();
                        dt.Dispose();
                        con.Close();
                    }


                }//end of else condition (when entry was not in the database)

            } //End of Try block
            catch (Exception err)
            {
                lblResults.Text = "Error accessing the database! Please enter the correct ID. ";
                lblResults.Text += err.Message;
            }
            finally
            {
                dt.Dispose();
                connection.Close();
            }


        }  //end of updatenotifications method

    }
}