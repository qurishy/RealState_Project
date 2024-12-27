using RealState_Project.Data_Access_Point;
using RealState_Project.Model;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace RealState_Project
{
    public partial class Main_Property_Page : Form
    {
        private Property_Conn  _conn;
        private User_Info _user;
        private bool logedIN = false;
 
        
        //it is going to run when the user logged in and check if the user has the info
        public Main_Property_Page(User_Info user = null)
        {

            InitializeComponent();

            _conn = new Property_Conn();

            _user = user;

            comboBox2.Items.Add("Available");
            comboBox2.Items.Add("Pending");
            comboBox2.Items.Add("Sold");
            comboBox2.Items.Add("Rented");

            comboBox1.Items.Add("Commercial");
            comboBox1.Items.Add("Land");
            comboBox1.Items.Add("Residential");

            if (_user.user_id != null)
            {
                button1.Visible = false;
                button2.Visible = false;

                button6.Visible = true;
                label2.Visible = true;
                button7.Visible = true;

                logedIN = true;

                label2.Text = _user.username;


                DataTable result = _conn.GetProperty();

                comboBox1.Items.Clear();

                propartyblocks(result);

            }
           

        }

        //it is just the defualt run
        public Main_Property_Page( )
        {
           

            InitializeComponent();
            _conn = new Property_Conn();

            comboBox1.Items.Add("Commercial");
            comboBox1.Items.Add("Land");
            comboBox1.Items.Add("Residential") ;

            comboBox2.Items.Add("Available");
            comboBox2.Items.Add("Pending");
            comboBox2.Items.Add("Sold");
            comboBox2.Items.Add("Rented");


            button6.Visible = false;
            label2.Visible = false;
            button7.Visible = false;


            DataTable result = _conn.GetProperty();

            propartyblocks(result);

        }

        //when we login from login page we get user info

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

                PictureBox pictureBox = new PictureBox
                {
                    //put your img here
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(100, 120),
                    Location = new Point(10, 10)
                };

                Label labeltype = new Label
                {
                    Text = row["property_type"].ToString(),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Location = new Point(10, 140),
                    AutoSize = true
                };

                Label labelprice = new Label
                {
                    Text = $"Price: ${row["list_price"]}",
                    Location = new Point(10, 165),
                    AutoSize = true
                };
                Label labelstatus = new Label
                {
                    Text = row["status"].ToString(),
                    Font = (row["status"].ToString() == "Available") ? new Font("Arial", 9, FontStyle.Bold) : new Font("Arial", 9, FontStyle.Regular),
                    ForeColor = (row["status"].ToString() == "Available") ? Color.Green : (row["status"].ToString() == "Sold") ? Color.Red : Color.Orange,
                    Location = new Point(10, 185),
                    AutoSize = true
                };

                Label labeldate = new Label
                {
                    Text = row["list_date"].ToString(),
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    Location = new Point(5, 215),
                    AutoSize = true 
                };

                Button selectbutton = new Button
                {
                    Text = "Select",
                    Tag = row["property_id"],
                    Location = new Point(10, 240),
                    Size = new Size(150, 35)
                };
                selectbutton.Click += selectbutton_Click;

                flowLayoutPanel1.AutoScroll = true;


                panel.Controls.Add(pictureBox);
                panel.Controls.Add(labeltype);
                panel.Controls.Add(labelprice);
                panel.Controls.Add(labelstatus);
                panel.Controls.Add(labeldate);
                panel.Controls.Add(selectbutton);

                flowLayoutPanel1.Controls.Add(panel);
            }
        }

        //It is the buttun that will get you to the order page and you have to login first
        private void selectbutton_Click(object sender, EventArgs e)
        {
            int propertyId = Convert.ToInt32((sender as Button).Tag);

            if (logedIN==true)
            {
                Order_Property order  = new Order_Property(_user, propertyId);

                order.Show();
                this.Close();
              
            }
            else { 
                
                Login_Page loginPage = new Login_Page();
                loginPage.Show();
                this.Hide();
            }
            
        }



        //When clicked the button will show the property Type of choosen option of combobox
        private void button3_Click(object sender, EventArgs e)
        {
            string selectedOption = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : null;

            DataTable result = null;
         
            switch (selectedOption)
            {
                case "Commercial":
                    result = _conn.GetTypeProperty("Commercial");
                    propartyblocks(result);
                    break;
                case "Land":
                    result = _conn.GetTypeProperty("Land");
                    propartyblocks(result);
                    break;
                case "Residential":
                    result = _conn.GetTypeProperty("Residential");
                    propartyblocks(result);
                    break;
                default:
                    result = _conn.GetProperty();
                    propartyblocks(result);
                    break;
            }

        }


        //When clicked the button it will get you to the login page
        private void button1_Click(object sender, EventArgs e)
        {
            Login_Page loginPage = new Login_Page();
            this.Hide();
            loginPage.Show();
            
        }

        //When clicked the button it will get you to the register page
        private void button2_Click(object sender, EventArgs e)
        {
            Register_Page registerPage = new Register_Page();
            this.Hide();
            registerPage.Show();
        }



        //When clicked the button will show the property status of choosen option of combobox
        private void button5_Click(object sender, EventArgs e)
        {
            string selectedOption = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : null;

            DataTable result = null;

            switch (selectedOption)
            {
                case "Available":
                    result = _conn.GetStatuesProperty("Available");
                    propartyblocks(result);
                    break;
                case "Pending":
                    result = _conn.GetStatuesProperty("Pending");
                    propartyblocks(result);
                    break;
                case "Sold":
                    result = _conn.GetStatuesProperty("Sold");
                    propartyblocks(result);
                    break;

                case "Rented":
                    result = _conn.GetStatuesProperty("Rented");
                    propartyblocks(result);
                    break;
                    
                default:
                    result = _conn.GetProperty();
                    propartyblocks(result);
                    break;


            }

        }

        //When clicked the button will show the top expensive property
        private void button4_Click(object sender, EventArgs e)
        {
            
            DataTable result = _conn.GetTopProperty();

            propartyblocks(result);
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }


        //when clicked the button will open the CRUD page and you have to login first
        private void button7_Click(object sender, EventArgs e)
        {
            Propperty_CRUD property_CRUD = new Propperty_CRUD(_user);
            property_CRUD.Show();
            this.Hide();
        }
    }
}
