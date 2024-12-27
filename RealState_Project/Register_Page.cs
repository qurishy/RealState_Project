using RealState_Project.Data_Access_Point;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealState_Project
{
    public partial class Register_Page : Form
    {
        Client_Conn _conn;

        public Register_Page()
        {
            InitializeComponent();
            _conn = new Client_Conn();
        }
        private string _Fname = string.Empty;
        private string _Lname = string.Empty;
        private string _phone = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _password2 = string.Empty;
        private string _type = "both";
        private string _username = string.Empty;
        
        

        private void button1_Click(object sender, EventArgs e)
        {
            _Fname = textBox1.Text.Trim();
            _Lname = textBox2.Text.Trim();
            _phone = textBox3.Text.Trim();
            _email = textBox4.Text.Trim();
            _password = textBox6.Text.Trim();
            _password2 = textBox7.Text.Trim();
            _type = comboBox1.SelectedItem.ToString();
            _username = textBox5.Text.Trim();

            if(_password != _password2)
            {
                MessageBox.Show("Password does not match");
            }
            else if (_Fname == null || _Lname == null || _email == null || _password == null|| _password2 == null || _username == null)
            {
                MessageBox.Show("All fields are required");
            }
            else
            {
              
               bool dt = _conn.RegisterUser_Client(ref _Fname,ref _Lname,ref _username,ref _password,ref _email,ref _phone,ref _type);
                if (dt)
                {
                    // Process the result
                    MessageBox.Show("Registration successful!");
                   
                   
                    this.Close();
    
                    Login_Page loginPage = new Login_Page();
                    loginPage.Show();
                }
                else
                {
                    // Handle no result or error
                    MessageBox.Show("Registration failed. Please try again.");
                }
            }

        }

        private void Register_Page_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Buyer");
            comboBox1.Items.Add("Seller");
            comboBox1.Items.Add("Both");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Main_Property_Page main_Property_Page = new Main_Property_Page();
            main_Property_Page.Show();

        }
    }
}
