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
    public class CRUDStudyPerson
    {

        //******************** Set Connection Properties ******************//
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DbConn_BioSys"].ToString();
            con = new SqlConnection(constring);
        }

        public bool AddStudyPerson(AddStudyPerson smodel)
        {
            connection();

            string message;
            SqlCommand cmd = new SqlCommand("Recruitment.AddStudyPerson", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PSCID", smodel.PSCID);
            cmd.Parameters.AddWithValue("@StudyName", smodel.StudyName);
            cmd.Parameters.AddWithValue("@StudyID", smodel.StudyID);            
            cmd.Parameters.AddWithValue("@ParticipantType", smodel.ParticipantType);
            cmd.Parameters.AddWithValue("@DateOfBirth", smodel.DateOfBirth);
            if(smodel.Sex != null)
            {
                cmd.Parameters.AddWithValue("@Sex", smodel.Sex);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Sex", "");
            }
            
            cmd.Parameters.Add(new SqlParameter("@Message", SqlDbType.VarChar, 255, "message")).Direction = ParameterDirection.Output;
            if (smodel.StudyPersonID != null)
            {
                cmd.Parameters.AddWithValue("@StudyPersonID", smodel.StudyPersonID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@StudyPersonID", DBNull.Value);
            }
            //cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
            


            con.Open();
            //int i = cmd.ExecuteNonQuery();
            //con.Close();
            //if (i >= 1)
            //    return true;
            //else
            //    return false;

            int rows = 0;
            rows = cmd.ExecuteNonQuery();
            message = Convert.ToString(cmd.Parameters["@Message"].Value);
            con.Close();
            smodel.Message = message;
            if (rows > 0)
            {
                return true;
            } else
            {
                return false;
            }

            
            

        }

        public bool UpdateStudyPerson(StudyPerson smodel)
        {
            connection();

            SqlCommand cmd = new SqlCommand("Recruitment.UpdateStudyPerson", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PSCID", smodel.PSCID);
            cmd.Parameters.AddWithValue("@StudyID", smodel.StudyID);
            cmd.Parameters.AddWithValue("@ParticipantType", smodel.ParticipantType);
            cmd.Parameters.AddWithValue("@DateOfBirth", smodel.DateOfBirth);
            cmd.Parameters.AddWithValue("@Sex", smodel.Sex);
            if (smodel.StudyPersonID != null)
            {
                cmd.Parameters.AddWithValue("@StudyPersonID", smodel.StudyPersonID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@StudyPersonID", DBNull.Value);
            }

            con.Open();

            int rows = 0;
            rows = cmd.ExecuteNonQuery();

            if(rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int ExistsPersonByPSCIDAndStudy(string pscId, string studyId)
        {
            int Exists;
           // string StudyName;
            Exists = 0;
            //StudyName = "";
            connection();
            SqlCommand cm = new SqlCommand("Recruitment.ExistsPersonByPSCIDandStudyID", con);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add(new SqlParameter("@PSCID", pscId));
            cm.Parameters.Add(new SqlParameter("@StudyID", studyId));
            cm.Parameters.Add(new SqlParameter("@Exists", SqlDbType.Int)).Direction = ParameterDirection.Output;
            //cm.Parameters.Add(new SqlParameter("@StudyName", SqlDbType.VarChar, 50, "StudyName")).Direction = ParameterDirection.Output;

            con.Open();
            cm.ExecuteNonQuery();
            Exists = Convert.ToInt32(cm.Parameters["@Exists"].Value);
            //StudyName = Convert.ToString(cm.Parameters["@StudyName"].Value);
            con.Close();

            return Exists;
        }

        public DataSet StudyPersonExists(AddStudyPerson smodel)
        {
            connection();
            SqlCommand cm = new SqlCommand("Recruitment.ExistsPersonByPSCIDandStudy", con);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@PSCID", smodel.PSCID);
            cm.Parameters.AddWithValue("@StudyName", smodel.StudyName);
            SqlDataAdapter sd = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();

            con.Open();
            sd.Fill(ds);
            con.Close();

            return ds;

        }

        public DataSet GetStudyPerson (string PSCID, string StudyID)
        {
            connection();

            SqlCommand cmd = new SqlCommand("Person.GetPersonByPSCIDandStudy", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PSCID", PSCID));
            cmd.Parameters.Add(new SqlParameter("@StudyID", StudyID));
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            con.Open();
            sd.Fill(ds);
            con.Close();

            return ds;
        }

        public List<StudyPerson> GetStudyPersonList(string PSCID, string StudyID)
        {
            connection();
            List<StudyPerson> studyPersonList = new List<StudyPerson>();


            SqlCommand cmd = new SqlCommand("Person.GetPersonByPSCIDandStudy", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PSCID", PSCID));
            cmd.Parameters.Add(new SqlParameter("@StudyID", StudyID));
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach(DataRow dr in dt.Rows)
            {
                studyPersonList.Add(
                    new StudyPerson
                    {
                        PSCID = Convert.ToString(dr["PSCID"]),
                        StudyID = Convert.ToString(dr["StudyID"]),
                        StudyName = Convert.ToString(dr["StudyID"]),
                        ParticipantType = Convert.ToString(dr["ParticipantType"]),
                        DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                        Sex = Convert.ToString(dr["Sex"]),
                        StudyPersonID = Convert.ToString(dr["StudyPersonID"])

                    });
            }
            return studyPersonList;
        }


        public DataSet GetGenderList()
        {
            connection();

            SqlCommand cmd = new SqlCommand("Reference.GenderLookUp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            con.Open();
            sd.Fill(ds);
            con.Close();

            return ds;

        }

    }
}