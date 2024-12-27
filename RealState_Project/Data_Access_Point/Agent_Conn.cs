using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState_Project.Data_Access_Point
{
    public class Agent_Conn
    {
        SqlConnect_point conn;

        public Agent_Conn()
        {
            conn = new SqlConnect_point();
        }

    }
}
