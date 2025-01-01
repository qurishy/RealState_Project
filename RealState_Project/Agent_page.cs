using RealState_Project.Data_Access_Point;
using RealState_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealState_Project
{
    public partial class Agent_page : Form
    {
        
        Agent_Conn _Agent_Conn;
        User_Info _user;
        Agent_Data _agent_info;
        DataTable order_table = new DataTable();
        public Agent_page(User_Info user_Info)
        {
            InitializeComponent();
            _user = user_Info;
            _Agent_Conn = new Agent_Conn();
            _agent_info = new Agent_Data();
            label3.Text = _user.username.ToString();
            
            LoadAgentInfo();//we are uploading the user info into the agent model
            label2.Text = "Commission Rate:" +_agent_info.first_name.ToString();

            DataTable result = _Agent_Conn.Get_OrderList();
            propartyblocks(result);
            

        }


        #region Geting Agent Info



        //In this method the user info will be loaded in to the agent model (Agent_Data)
        private void LoadAgentInfo()
        {
            try
            {
                DataRow dr = _Agent_Conn.GetSingleAgent(_user.user_id);
                if (dr != null)
                {
                    _agent_info.Agent_ID = GetIntValue(dr["agent_id"]);
                    _agent_info.user_id = GetIntValue(dr["user_id"]);
                    _agent_info.first_name = GetStringValue(dr["first_name"]);
                    _agent_info.last_name = GetStringValue(dr["last_name"]);
                    _agent_info.phone_number = GetStringValue(dr["phone_number"]);
                    _agent_info.commission_rate = GetDecimalValue(dr["commission_rate"]);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
        }

        private int GetIntValue(object value)
        {
            return value != DBNull.Value ? Convert.ToInt32(value) : 0;
        }

        private string GetStringValue(object value)
        {
            return value != DBNull.Value ? Convert.ToString(value) : string.Empty;
        }

        private decimal GetDecimalValue(object value)
        {
            return value != DBNull.Value ? Convert.ToDecimal(value) : 0m;
        }

        #endregion




        private void Agent_page_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //just list the orders
        private void propartyblocks(DataTable result = null)
        {


            flowLayoutPanel1.Controls.Clear();

            foreach (DataRow row in result.Rows)
            {
                Panel panel = new Panel
                {
                    Size = new Size(170, 280),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                Label labelBuyer = new Label
                {
                    Text = row["buyer_id"].ToString(),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Location = new Point(10, 140),
                    AutoSize = true
                };

                Label labelSeller = new Label
                {
                    Text = row["seller_id"].ToString(),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Location = new Point(10, 140),
                    AutoSize = true
                };


                Label labelprice = new Label
                {
                    Text = $"Price: ${row["sale_price"]}",
                    Location = new Point(10, 165),
                    AutoSize = true
                };
                Label labelstatus = new Label
                {
                    Text = row["transaction_type"].ToString(),
                    Font = (row["transaction_type"].ToString() == "Available") ? new Font("Arial", 9, FontStyle.Bold) : new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = (row["transaction_type"].ToString() == "Available") ? Color.Green : (row["transaction_type"].ToString() == "Sold") ? Color.Red : Color.Orange,
                    Location = new Point(10, 185),
                    AutoSize = true
                };

                Label labeldate = new Label
                {
                    Text = row["Order_date"].ToString(),
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    Location = new Point(5, 215),
                    AutoSize = true
                };

                Button selectbutton = new Button
                {
                    Text = "Select",
                    Tag = row["Order_id"],
                    Location = new Point(10, 240),
                    Size = new Size(150, 35)
                };
                selectbutton.Click += selectbutton_Click;

                flowLayoutPanel1.AutoScroll = true;

                panel.Controls.Add(labelBuyer);
                panel.Controls.Add(labelSeller);
                panel.Controls.Add(labelprice);
                panel.Controls.Add(labelstatus);
                panel.Controls.Add(labeldate);
                panel.Controls.Add(selectbutton);

                flowLayoutPanel1.Controls.Add(panel);
            }
        }


        //this buttun will process the selected property by the agent
        private void selectbutton_Click(object sender, EventArgs e)
        {
            int order_id = Convert.ToInt32((sender as Button).Tag);

            _Agent_Conn.AcceptOrder(order_id, _agent_info.Agent_ID);
            

        }



        //this is the button that will get you out of the agent page
        private void button2_Click(object sender, EventArgs e)
        {
            Login_Page login_Page = new Login_Page();
            login_Page.Show();
            this.Close();
        }


        //this is the button that will get you back to the main page
        private void button1_Click(object sender, EventArgs e)
        {
            Main_Property_Page main_Property_Page = new Main_Property_Page(_user);
            main_Property_Page.Show();
            this.Hide();
        }
    }
}
