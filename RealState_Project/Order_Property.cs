using RealState_Project.Data_Access_Point;
using RealState_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealState_Project
{
    public partial class Order_Property : Form
    {

        private Client_Conn _Client_Conn;
        private Property_Conn _conn;
        private int  _property_id;
        User_Info _user;
        
        public Order_Property(User_Info user, int property_id)
        {
            InitializeComponent();
            _conn = new Property_Conn();
            _Client_Conn = new Client_Conn();
            _user= user;
            _property_id = property_id;
            propartyblocks();
            comboBox1.Items.Add("Sale");
            comboBox1.Items.Add("Rent");
            label2.Text = _user.username.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main_Property_Page main_Property_Page = new Main_Property_Page(_user);
            main_Property_Page.Show();
            this.Close();
        }

        private void propartyblocks()
        {
            DataRow row = _conn.GetSingleRow(_property_id);

            flowLayoutPanel1.Controls.Clear();

            
                Panel panel = new Panel
                {
                    Size = new Size(550, 230),
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
               

                flowLayoutPanel1.AutoScroll = true;
            string imagePath = "C:\\Users\\erhad\\OneDrive\\Desktop\\Arshad_UNI_Matrials\\Semister_5\\Database Management\\c2\\RealState_Project\\RealState_Project\\Model\\villa.jpg";
            try
            {
                pictureBox.Image = Image.FromFile(imagePath);
            }
            catch (FileNotFoundException ex)
            {
                // Handle the case where the image file isn't found.
                MessageBox.Show($"Error loading image: {ex.Message}");
                pictureBox.Image = null; // Or set an error image if you want.
                return; // Or continue if you want to ignore the error
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred loading the image: {ex.Message}");
                pictureBox.Image = null;
                return; // Or handle the error
            }


            panel.Controls.Add(pictureBox);
                panel.Controls.Add(labeltype);
                panel.Controls.Add(labelprice);
                panel.Controls.Add(labelstatus);
                panel.Controls.Add(labeldate);
                panel.Controls.Add(selectbutton);

                flowLayoutPanel1.Controls.Add(panel);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow value = null;
            string type;
            int owner_id;
            decimal price;
            
            if(comboBox1 != null)
            {
                type = comboBox1.SelectedItem.ToString();


                value = _conn.GetSingleRow(_property_id);
                if (value != null)
                {
                    price = decimal.Parse(value["list_price"].ToString()); 
                    owner_id = int.Parse(value["Owner_id"].ToString());
                    
                   int rowsAffected = _Client_Conn.MakeOrder( _property_id, _user.user_id, price, type,  owner_id);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Order placed successfully.");
                        Main_Property_Page main_Property_Page = new Main_Property_Page(_user);
                        main_Property_Page.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Order placement failed.");
                    }


                }
                else
                {
                    MessageBox.Show("property not found");
                }
                

                
            }else
            { MessageBox.Show("please choose transaction type"); }
        }
    }
}
