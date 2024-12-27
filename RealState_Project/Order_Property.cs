using RealState_Project.Data_Access_Point;
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
    public partial class Order_Property : Form
    {
        private Property_Conn _conn;
        private int _user_id, _property_id;
        
        public Order_Property(int user_id, int property_id)
        {
            InitializeComponent();
            _conn = new Property_Conn();
            _user_id = user_id;
            _property_id = property_id;
            propartyblocks();
        }

        private void propartyblocks()
        {
            DataRow row = _conn.GetSingleRow(_property_id);

            flowLayoutPanel1.Controls.Clear();

            
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
}
