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
    public partial class Propperty_CRUD : Form
    {

        private Client_Conn _conn;
        private User_Info _user;
        public Propperty_CRUD( User_Info user = null)
        {
            InitializeComponent();

            _conn = new Client_Conn();

            _user = user;

            comboBox2.Items.Add("Available");
            comboBox2.Items.Add("Pending");
            comboBox2.Items.Add("Sold");
            comboBox2.Items.Add("Rented");

            comboBox1.Items.Add("Commercial");
            comboBox1.Items.Add("Land");
            comboBox1.Items.Add("Residential");

            DataTable result = _conn.GetProperty_Client(5);
            propartyblocks(result);

        
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }



        private void propartyblocks(DataTable result = null)
        {


            flowLayoutPanel1.Controls.Clear();

            foreach (DataRow row in result.Rows)
            {
                Panel panel = new Panel
                {
                    Size = new Size(270, 60),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                PictureBox pictureBox = new PictureBox
                {
                    //put your img here
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(10, 60),
                    Location = new Point(55, 10)
                };

                Label labeltype = new Label
                {
                    Text = row["property_type"].ToString(),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Location = new Point(65, 10),
                    AutoSize = true
                };

                Label labelprice = new Label
                {
                    Text = $"Price: ${row["list_price"]}",
                    Location = new Point(65, 40),
                    AutoSize = true
                };
                Label labelstatus = new Label
                {
                    Text = row["status"].ToString(),
                    Font = (row["status"].ToString() == "Available") ? new Font("Arial", 9, FontStyle.Bold) : new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = (row["status"].ToString() == "Available") ? Color.Green : (row["status"].ToString() == "Sold") ? Color.Red : Color.Orange,
                    Location = new Point(130, 10),
                    AutoSize = true
                };

                //Label labeldate = new Label
                //{
                //    Text = row["list_date"].ToString(),
                //    Font = new Font("Arial", 9, FontStyle.Bold),
                //    Location = new Point(180, 10),
                //    AutoSize = true
                //};

                Button selectbutton = new Button
                {
                    Text = "Delete",
                    ForeColor = Color.Blue,
                    BackColor = Color.Red,
                    Tag = row["property_id"],
                    Location = new Point(195, 0),
                    Size = new Size(75, 58)
                };
                selectbutton.Click += selectbutton_Click;

                flowLayoutPanel1.AutoScroll = true;


                panel.Controls.Add(pictureBox);
                panel.Controls.Add(labeltype);
                panel.Controls.Add(labelprice);
                panel.Controls.Add(labelstatus);
               // panel.Controls.Add(labeldate);
                panel.Controls.Add(selectbutton);

                flowLayoutPanel1.Controls.Add(panel);
            }
        }

        private void selectbutton_Click(object sender, EventArgs e)
        {
            int propertyId = Convert.ToInt32((sender as Button).Tag);
            _conn.DeleteProperty( propertyId,5);

            if (true == true)
            {

                MessageBox.Show("Property deleted successfully!");

                //Main_Property_Page main_Property_Page = new Main_Property_Page(_user);
                //main_Property_Page.Show();
                //this.Close();
            }
            else
            {
                Login_Page loginPage = new Login_Page();
                loginPage.Show();
                this.Hide();
            }
        }
    }
   
}

