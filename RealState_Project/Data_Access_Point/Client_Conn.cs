﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState_Project.Data_Access_Point
{
    public class Client_Conn
    {
       private SqlConnect_point _conn;

        public Client_Conn()
        {
            _conn = new SqlConnect_point();
        }

        //It checks if the username and password are correct
        public DataTable CheckLogin(string username, string password)
        {
            if (_conn == null)
            {
                throw new InvalidOperationException("Connection is not initialized");
            }

            string query = "select * from [USER] where [username]=@username and [password_hash]=@password";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@username", username),
            new SqlParameter("@password", password)
            };

            DataTable result = _conn.ExecuteSelectQuery(query, parameters);

            if(result != null)
            {
                return result;
            }
            else
            { 
                return result;
            }
        }

        //It registers a new user to the database
        public bool RegisterUser_Client(ref string FirstName,ref string LastName,ref string username,
            ref string password, ref string email, ref string phone,ref string type)
        {
            int user_ids = 0;
            if (_conn == null)
            {
                throw new InvalidOperationException("Connection is not initialized");
            }
         
            string query  = "INSERT INTO [USER] (username, password_hash, email, role_id) VALUES  (@username, @password, @email, 2)";
           
            SqlParameter[] parameters = new SqlParameter[]
            {
           
            new SqlParameter("@username", username),
            new SqlParameter("@password", password),
            new SqlParameter("@email", email),
         
            };

            int rowsAffected =  _conn.ExecuteNonQuery(query, parameters);

            if(rowsAffected != 0)
            { 
                string query1 = "SELECT user_id FROM [USER] WHERE username = @username";
                SqlParameter[] parameters1 = new SqlParameter[]
          
                {
                    new SqlParameter("@username", username),
       
                };

                user_ids = _conn.ExecuteSelectQuery(query1, parameters1).Rows[0].Field<int>("user_id");
            }

           
            query = "INSERT INTO CLIENT (user_id,first_name,last_name,phone_number,Client_type) VALUES  (@user_id,@FirstName,@LastName,@phone,@role)";


            SqlParameter[] param = new SqlParameter[]   
            {
            new SqlParameter("@user_id", user_ids),

            new SqlParameter("@phone", phone),
            new SqlParameter("@role", type),
           
            new SqlParameter("@FirstName", FirstName),
            new SqlParameter("@LastName", LastName)
            };

            _conn.ExecuteNonQuery(query, param);


            return true;
        }



        //it gets all the property of the client
        public DataTable GetProperty_Client(int user_id)
        
        {
            string sqlPropertyInfo = @"SELECT 
            property_id,
            property_type,
            list_price,
            status,
            list_date
        FROM [Project_RealState].[dbo].[PROPERTY] WHERE   [Owner_id] = @user_id;";

            SqlParameter[] parameters = new SqlParameter[] 
            {
                new SqlParameter("@user_id", user_id)
            };

            DataTable result = _conn.ExecuteSelectQuery(sqlPropertyInfo, parameters);

            return result;

           
        }


        //it Deletes the the property of the specific owner
        public void DeleteProperty(int propertyId, int ownerId)
        {
            // Prepare parameters for the stored procedure
            SqlParameter[] parameters =
            {
            
                new SqlParameter("@PropertyId", SqlDbType.Int) {Value = propertyId},
            
                new SqlParameter("@OwnerId", SqlDbType.Int) {Value = ownerId}
      
            };

            // Call the stored procedure
           
            _conn.CallStoredProcedure("DeletePropertyByIdAndOwnerId", parameters);
            
        }







    }
}