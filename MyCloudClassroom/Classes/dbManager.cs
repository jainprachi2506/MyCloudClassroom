using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyCloudClassroom.Classes
{
    public class dbManager {
	 
     //Database parameters
     MySqlConnection conn;
     MySqlCommand command;
     string connectionString;
     string sql;

     public dbManager(){
         connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MCCdbConnectionString"].ConnectionString;
    	 try{
    
             conn =new MySqlConnection(connectionString);
             conn.Open();
             
         // Open a connection
             command = new MySqlCommand(null, conn);
         
         
    	 }catch(Exception se){
	         //Handle errors for JDBC
	        //HttpResponse res se.Message();
          //}catch(Exception e){
          //   //Handle errors for Class.forName
          //   e.printStackTrace();
          }
     }
     
    public int executeDDL(String sql, MySqlParameter[] param)
 	{
    	 int iRows=0;
    	 try{

             command.CommandText =  sql.ToLower();
             
             for(int i=0;i<param.Length;i++)
             {
                 command.Parameters.Add(param[i]);
             }

             command.Prepare();
             iRows = command.ExecuteNonQuery();

	        
	         
 	     	}catch(Exception se){
 	         //Handle errors for JDBC
 	         //se.printStackTrace();
 	     	//}catch(Exception e){
 	    	 //Handle errors for Class.forName
 	         //e.printStackTrace();
 	     	}finally{
 	         //finally block used to close resources
 	          try{
 	            //if(command!=null)
 	             //  command.close();
 	          	//}catch(SQLException se2){
 	          		//se2.printStackTrace();
 	          	//}
 	          //try{
                  if (conn != null)
                      conn.Close();
                    
 	          	  }catch(Exception se){
 	          		  //se.printStackTrace();
 	          	  }
 	     	} 
    	 return iRows;
 	}

    public DataTable fetchRows(String sql, MySqlParameter[] param)
    {

        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DataTable ds = new DataTable();
        try{
	    	
            command.CommandText = sql.ToLower();
             
             for(int i=0;i<param.Length;i++)
             {
                 command.Parameters.Add(param[i]);
             }

            command.Prepare();
            adapter.SelectCommand = command;
            adapter.Fill(ds);

            return ds;
        }
        catch (Exception se)
        {
            //Handle errors for JDBC
            //se.printStackTrace();
        }
        //catch (Exception e)
        //{
            //Handle errors for Class.forName
        //    e.printStackTrace();
        //}
        finally
        {
            //finally block used to close resources
            try
            {
            //    if (command != null)
            //        command.close();
            //}
            //catch (SQLException se2)
            //{
            //    se2.printStackTrace();
            //}
            //try
            //{
                if (conn != null)
                    conn.Close();
            }
            catch (Exception se)
            {
                //se.printStackTrace();
            }
        }
        return ds;
    }
    }
}