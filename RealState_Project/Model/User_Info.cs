using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState_Project.Model
{
    public class User_Info
    {
        public int user_id { get; set; }

        public string username { get; set; }

        public string password_hash { get; set; }

        public string email { get; set; }

        public int role_id { get; set; }

        public DateTime last_login { get; set; } = DateTime.Now;

        public DateTime created_at { get; set; } = DateTime.Now;


    }
}
