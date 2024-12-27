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
    public partial class Login_Page : Form
    {
       

        Client_Conn _conn;
        string _email;
        string _password;
       User_Info _info;

        public Login_Page()
        {
            InitializeComponent();
            _conn = new Client_Conn();
           _info = new User_Info();
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            _email = GetEmailFromTextBox();
            _password = GetPasswordFromTextBox();

            

            if (!string.IsNullOrWhiteSpace(_email) && !string.IsNullOrWhiteSpace(_password))
            {
                DataTable dt = _conn.CheckLogin(_email, _password);
                if (dt.GetType() == typeof(DataTable) && dt != null && dt.Rows.Count > 0)
                {
                    // Process the result
                    _info.user_id = Convert.ToInt32(dt.Rows[0]["user_id"]);
                    _info.username = Convert.ToString(dt.Rows[0]["username"]);
                    _info.email = Convert.ToString(dt.Rows[0]["email"]);
                    _info.role_id = Convert.ToInt32(dt.Rows[0]["role_id"]);
                    _info.password_hash = Convert.ToString(dt.Rows[0]["password_hash"]);
                    
                    
                    Main_Property_Page main_Property_Page = new Main_Property_Page(_info);
               
                    main_Property_Page.Show();
                    this.Close();

                   
      
                   

                }
                else
                {
                    // Handle no result or error
                }
            }
            else
            {
                // Handle invalid input
                this.Close();
                Main_Property_Page main_Property_Page = new Main_Property_Page();
                main_Property_Page.Show();


            }
        }
        private string GetEmailFromTextBox()
        {
            return textBox1.Text.Trim();
        }

        private string GetPasswordFromTextBox()
        {
            return textBox2.Text.Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Main_Property_Page main_Property_Page = new Main_Property_Page();
            main_Property_Page.Show();
            
        }
    }
}
