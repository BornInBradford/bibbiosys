using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BioSys.Models
{
    public class CRUDModels
    {
        //******************** Set Connection Properties ******************//
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DbConn_BioSys"].ToString();
            con = new SqlConnection(constring);
        }


        //******************** Get Study List ********************//
        public DataSet GetStudyList()
        {
            connection();            
            SqlCommand cmd = new SqlCommand("Study.GetStudyList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            sd.Fill(ds);
            con.Close();
           return ds;
        }

        //******************** Get the Study-specific Form List for Data Capture ******************//
        public DataSet GetFormList(string studyId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Study.GetFormListByStudyID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@StudyID", studyId));
            SqlDataAdapter sd = new  SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            sd.Fill(ds);
            con.Close();
            return ds;
        }

        public DataSet GetParticipantTypeList()
        {
            connection();
            SqlCommand cmd = new SqlCommand("Person.GetParticipantTypeList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            sd.Fill(ds);
            con.Close();
            return ds;

        }


        public DataSet GetPersonByPSCID(string pscId, string studyId)
        {
            connection();

            SqlCommand cmd = new SqlCommand("Recruitment.GetPersonByPSCID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PSCID", pscId));
            cmd.Parameters.Add(new SqlParameter("@StudyID", studyId));
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            con.Open();
            sd.Fill(ds);
            con.Close();

            return ds;
        }

        public DataSet GetStudyFormByID(string formBarcode)
        {
            connection();

            SqlCommand cmd = new SqlCommand("Study.GetStudyFormByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FormID", formBarcode));
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            con.Open();
            sd.Fill(ds);
            con.Close();

            return ds;

        }



        //public DataSet ExistsParticipantByPSCID(string pscId)
        //{
        //    connection();

        //    SqlCommand cmd = new SqlCommand("Recruitment.ExistsPersonByPSCID", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@PSCID", pscId));
        //    SqlDataAdapter sd = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();

        //    con.Open();
        //    sd.Fill(ds);
        //    con.Close();

        //    return ds;
        //}

        //public string ExistsParticipantByPSCID(string pscId)
        //{
        //    connection();

        //    string personExists = String.Empty;

        //    SqlCommand cmd = new SqlCommand("Recruitment.ExistsPersonByPSCID", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@PSCID", pscId));
        //    cmd.Parameters.Add(new SqlParameter("@Exists", SqlDbType.Bit)).Direction = ParameterDirection.Output;
        //    //cmd.Parameters.Add("@Exists", SqlDbType.Bit);
        //    //cmd.Parameters["@Exists"].Direction = ParameterDirection.Output;

        //    //SqlParameter outputParameter = new SqlParameter();
        //    //outputParameter.ParameterName = "@Exists";
        //    //outputParameter.SqlDbType = System.Data.SqlDbType.Bit;
        //    //outputParameter.Direction = System.Data.ParameterDirection.Output;
        //    //cmd.Parameters.Add(outputParameter);

        //    SqlDataAdapter sd = new SqlDataAdapter(cmd);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    //cmd.ExecuteReader();
        //    personExists = (string) cmd.Parameters["@Exists"].Value;
        //    con.Close();  

        //    return personExists;


        //}


        //********************  Get Study List - Alternative Method  ********************//
        public List<SelectListItem> GetStudies()
        {
            connection();
            List<SelectListItem> items = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("Study.GetStudyList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter sd = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();

            con.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    items.Add(new SelectListItem
                    {
                        Text = sdr["StudentType"].ToString(),
                        Value = sdr["StudentType"].ToString()
                    });
                }

            }
            con.Close();
            return items;
        }


        //****************** Get Participant's Details ******************************************//


        //****************** Get Details of Samples Collected for a Study Donor ***************//



        //*************************************************************************************//

    }
}