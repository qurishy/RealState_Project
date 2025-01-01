using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState_Project.Data_Access_Point
{
    public class Agent_Conn
    {
        SqlConnect_point _conn;

        public Agent_Conn()
        {
            _conn = new SqlConnect_point();
        }

        //Gets all the order list
        public DataTable Get_OrderList()
        {

            DataTable dt_result = new DataTable();
            dt_result = _conn.ExecuteSelectQuery("select * from [Project_RealState].[dbo].[OrderList] Where agent_id is null");
            
            if(dt_result != null)
            {
                return dt_result;
            }
            else
            {

                return dt_result=null;
            }


        }


        //this method is getting us the single agent info
        public DataRow GetSingleAgent(int user_id)
        {
            
            string query = "SELECT agent_id,  user_id, first_name,   last_name,   phone_number, commission_rate FROM [Project_RealState].[dbo].[AGENT] WHERE user_id = @user_id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user_id", user_id)
               
            };

           return _conn.GetSingleRow(query, parameters);
        }



        //this method is going to get the clent iD
        public DataRow GetAgentId(int user_id)
        {
            string query = "SELECt * FROM [Project_RealState].[dbo].[CLIENT] WHERE user_id = @user_id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user_id", user_id)
            };

            return _conn.GetSingleRow(query, parameters);

        }

        //this method is accepting the order and going to call the stored procedure
        public void AcceptOrder(int order_id, int agent_id)
        {
            string query = "UPDATE [Project_RealState].[dbo].[OrderList] SET [agent_id] = @agent_id WHERE [Order_id] = @order_id; ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Order_id", order_id),
                new SqlParameter("@agent_id", agent_id)
            };

            _conn.ExecuteNonQuery(query, parameters);
        }




    }
}
