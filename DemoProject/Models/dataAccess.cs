using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Models
{
    public class dataAccess
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constr);

        }
        public IEnumerable<MyAdvertiseModel> GetAllProductList()
        {
            connection();
            List<MyAdvertiseModel> lstadv = new List<MyAdvertiseModel>();
            SqlCommand cmd = new SqlCommand("GetSellerData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                MyAdvertiseModel product = new MyAdvertiseModel();

                product.advertiseId = Convert.ToInt32(rdr["advertiseId"]);
                product.productSubCategoryName = Convert.ToString(rdr["productSubCategoryName"]);
                product.advertiseTitle = rdr["advertiseTitle"].ToString();
                product.advertiseDescription = rdr["advertiseDescription"].ToString();
                product.advertisePrice = Convert.ToDecimal(rdr["advertisePrice"]);
                product.areaName = Convert.ToString(rdr["areaName"]);
                product.advertiseStatus = Convert.ToBoolean(rdr["advertiseStatus"]);
                product.UserId = Convert.ToInt32(rdr["UserId"]);
                product.advertiseapproved = Convert.ToBoolean(rdr["advertiseapproved"]);
                product.createdOn = Convert.ToDateTime(rdr["createdOn"]);
                product.updatedOn = Convert.ToDateTime(rdr["updatedOn"]);
                //product.imageData = (byte[])(rdr["imageData"]);
                lstadv.Add(product);
            }
            con.Close();
            return lstadv;
        }
       
        public bool UpdateAdvertisement(MyAdvertiseModel advertisement)
        {
            connection();
                SqlCommand cmd = new SqlCommand("spUpdatetbl_MyAdvertise", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@advertiseId", advertisement.advertiseId);
                cmd.Parameters.AddWithValue("@productSubCategoryName", advertisement.productSubCategoryName);
                cmd.Parameters.AddWithValue("@advertiseTitle", advertisement.advertiseTitle);
                cmd.Parameters.AddWithValue("@advertiseDescription", advertisement.advertiseDescription);
                cmd.Parameters.AddWithValue("@advertisePrice", advertisement.advertisePrice);
                cmd.Parameters.AddWithValue("@areaName", advertisement.areaName);
                cmd.Parameters.AddWithValue("@advertiseStatus", advertisement.advertiseStatus);
                cmd.Parameters.AddWithValue("@firstName", advertisement.firstName);
                cmd.Parameters.AddWithValue("@advertiseapproved", advertisement.advertiseapproved);
                cmd.Parameters.AddWithValue("@createdOn", advertisement.createdOn);
                cmd.Parameters.AddWithValue("@updatedOn", advertisement.updatedOn);
                cmd.Parameters.AddWithValue("@imageData", advertisement.imageData);
                // Add other parameters as needed

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            
        }
        public MyAdvertiseModel GetAdvertiseById(int? advertiseId)
        {
            connection();
            MyAdvertiseModel product = null;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetAdvertiseDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the SqlCommand
                cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    product = new MyAdvertiseModel(); // Instantiate the product object

                    product.advertiseId = Convert.ToInt32(rdr["advertiseId"]);
                    product.productSubCategoryName = Convert.ToString(rdr["productSubCategoryName"]);
                    product.advertiseTitle = rdr["advertiseTitle"].ToString();
                    product.advertiseDescription = rdr["advertiseDescription"].ToString();
                    product.advertisePrice = Convert.ToDecimal(rdr["advertisePrice"]);
                    product.areaName = Convert.ToString(rdr["areaName"]);
                    product.firstName = Convert.ToString(rdr["firstName"]);
                    product.createdOn = Convert.ToDateTime(rdr["createdOn"]);
                    product.updatedOn = Convert.ToDateTime(rdr["updatedOn"]);
                    //product.imageData = (byte[])(rdr["imageData"]);
                    // Add other fields as needed
                }

                rdr.Close();
            }

            return product;
        }

        public bool DeleteAdvertise(int? advertiseId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteAdvertise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@advertiseId", advertiseId);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            return rowsAffected > 0;
        }
    }
}

