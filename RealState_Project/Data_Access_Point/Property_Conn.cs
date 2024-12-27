using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealState_Project.Data_Access_Point
{
    public class Property_Conn
    {
        private SqlConnect_point _conn;

        public Property_Conn()
        {
            _conn = new SqlConnect_point();
        }


        //gets all the property
        public DataTable GetProperty()
        {
            string sqlPropertyInfo = @"
                SELECT 
                    property_id,
                    property_type,
                    list_price,
                    status,
                    list_date
                FROM [Project_RealState].[dbo].[PROPERTY]";



            return _conn.ExecuteSelectQuery(sqlPropertyInfo);
        }


        //get a row of property
        public DataRow GetSingleRow(int property_id)
        {
            string sqlPropertyInfo = @"SELECT
pd.property_id,
    pd.square_footage,
    pd.bedrooms,
    pd.bathrooms,
    pd.address,
    pd.city,
    pd.state,
    pd.zip_code,
    pd.neighborhood,
    p.property_type,
    p.list_price,
    p.status,
    p.list_date,
    p.Owner_id
FROM
    [Project_RealState].[dbo].[PROPERTY_DETAILS] pd
JOIN
    [Project_RealState].[dbo].[PROPERTY] p ON pd.property_id = p.property_id WHERE pd.property_id = @property_id ;";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@property_id", property_id)
            };

            object result = _conn.GetSingleRow(sqlPropertyInfo,parameters);

            if (result != null)
            {
 
                return (DataRow)result;
            }
            else
            {
                Console.WriteLine("No value found or error occurred.");
            }

            return null;




        }

        //get all the property of a specific type
        public DataTable GetTypeProperty( string property_type)
        { 
        

            string sqlPropertyInfo = @"
                SELECT 
                    property_id,
                    property_type,
                    list_price,
                    status,
                    list_date
                FROM [Project_RealState].[dbo].[PROPERTY] WHERE   [property_type] = @property_type;";


            SqlParameter[] parameters = new SqlParameter[]
       
                {
                
                    new SqlParameter("@property_type", property_type)
        
                };

            DataTable result = _conn.ExecuteSelectQuery(sqlPropertyInfo, parameters);

            return result;


        }


        //get all the property of a specific Statues
        public DataTable GetStatuesProperty(string property_Status)
        {


            string sqlPropertyInfo = @"
                SELECT 
                    property_id,
                    property_type,
                    list_price,
                    status,
                    list_date
                FROM [Project_RealState].[dbo].[PROPERTY] WHERE   [status] = @property_Status";


            SqlParameter[] parameters = new SqlParameter[]

                {

                    new SqlParameter("@property_Status", property_Status)

                };

            DataTable result = _conn.ExecuteSelectQuery(sqlPropertyInfo, parameters);

            return result;


        }


        public DataTable GetTopProperty()
        {
            string sqlPropertyInfo = @"SELECT TOP (10)
    [property_id],
    [property_type],
    [list_price],
    [status],
    [list_date],
    [Owner_id]
FROM 
    [Project_RealState].[dbo].[PROPERTY] ORDER BY  [list_price] DESC;";

            return _conn.ExecuteSelectQuery(sqlPropertyInfo);


        }

    


    }
}
