using RealState_Project.Data_Access_Point;
using RealState_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
            comboBox2.Items.Add("Sold");
            comboBox2.Items.Add("Rented");

            comboBox1.Items.Add("Commercial");
            comboBox1.Items.Add("Land");
            comboBox1.Items.Add("Residential");

            DataTable result = _conn.GetProperty_Client(5);
            propartyblocks(result);

        
        }

        //THis button is used to add new property to the database
        private void button1_Click(object sender, EventArgs e)
        {
            string statues, type, city, neighberhood,state,zipcode, address = "";

            int bedroom, bathrooms = 1;

            double price, square = 0;

            if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (double.TryParse(textBox1.Text, out double parsedPrice)&& double.TryParse(textBox2.Text,  out double parsesquare))
                {
                    price = parsedPrice;
                    statues = comboBox2.SelectedItem.ToString();
                    type = comboBox1.SelectedItem.ToString();
                    city = comboBox2.SelectedItem.ToString();
                    square = parsesquare;
                    bedroom = int.Parse(textBox3.Text);
                    bathrooms = int.Parse(textBox4.Text);
                    zipcode = textBox8.Text.Trim();
                    city = textBox6.Text.Trim();
                    neighberhood = textBox9.Text.Trim();
                    address = textBox5.Text.Trim();
                    state = textBox7.Text.Trim();

                    // int   owner_id  = int.Parse(_user.user_id.ToString());
                    int owner_id = 2;

                    //we are passing the values to our Propperty_conn to insert propperty in datatable

                   _conn.InsertProperty(ref statues, ref type, ref city, ref neighberhood, ref address, ref bedroom, ref bathrooms, ref zipcode, ref price, ref square, ref state, owner_id);

                }
                else
                {
                    // The text in textBox1 cannot be converted to a double
                }
            }
            else
            {
                // Either comboBox1 or comboBox2 is empty, or textBox1 is empty
            }
            //in this part we are clearing the textboxes and comboboxes
            textBox8.Text = string.Empty; textBox9.Text = string.Empty; textBox5.Text = string.Empty; textBox4.Text = string.Empty; textBox3.Text= string.Empty; textBox2.Text = string.Empty; textBox1.Text = string.Empty; textBox7.Text = string.Empty; textBox6.Text = string.Empty;

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
        }


        //this method is used to show the details of the property that we have 
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

        //this buttonn is used to delete the property which we chooose in UI
        private void selectbutton_Click(object sender, EventArgs e)
        {
            int propertyId = Convert.ToInt32((sender as Button).Tag);
           bool check =  _conn.DeleteProperty( propertyId, _user.user_id);

            if (check == true)
            {

                MessageBox.Show("Property deleted successfully!");

                Main_Property_Page main_Property_Page = new Main_Property_Page(_user);
                main_Property_Page.Show();
                this.Close();
            }
            else
            {

                MessageBox.Show("Property did not deleted succesfully!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main_Property_Page main_page = new Main_Property_Page(_user);
            main_page.Show();
            this.Close();
        }
    }
   
}

