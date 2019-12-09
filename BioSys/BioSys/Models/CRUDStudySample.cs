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
    public class CRUDStudySample
    {

        //******************** Set Connection Properties ******************//
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DbConn_BioSys"].ToString();
            con = new SqlConnection(constring);
        }

        //******************* Add Urine Sample **************************//
        public bool AddStudySample(AddStudySample smodel)
        {
            connection();

            SqlCommand cmd = new SqlCommand("Biobank.CollectSamples", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PSCID",smodel.PSCID);
            cmd.Parameters.AddWithValue("@StudyID", smodel.StudyID);
            cmd.Parameters.AddWithValue("@Barcode", smodel.SampleBarcode);
            cmd.Parameters.AddWithValue("@SampleType", smodel.SampleType);
            //cmd.Parameters.AddWithValue("@SampleSetName", smodel.SampleSetName);
            cmd.Parameters.AddWithValue("@DateOfCollection", smodel.DateOfCollection);
            cmd.Parameters.AddWithValue("@TimeOfCollection", smodel.TimeOfCollection);
            cmd.Parameters.AddWithValue("@GelCollected", smodel.GelCollected);
            cmd.Parameters.AddWithValue("@EdtaCollected", smodel.EDTACollected);
            cmd.Parameters.AddWithValue("@LitHepCollected", smodel.LitHepCollected);
            cmd.Parameters.AddWithValue("@OxalateCollected", smodel.OxalateCollected);
            cmd.Parameters.AddWithValue("@BuccalCollected", smodel.BuccalCollected);
            cmd.Parameters.AddWithValue("@UrineCollected", smodel.UrineCollected);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;


        }
    }
}