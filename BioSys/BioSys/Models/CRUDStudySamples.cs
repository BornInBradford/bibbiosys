using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BioSys.Models
{
    public class CRUDStudySamples
    {

        /************** Method to Set Connection Properties ***********************************
         **
         ** Requires namespaces System.Configuration and System.Data.SqlClient
         **            
       */
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DbConn_BioSys"].ToString();
            con = new SqlConnection(constring);
        }

        public string GetStudyByFormID(string FormID)
        {

            connection();

            string StudyID;

            StudyID = "";

            SqlCommand cmd = new SqlCommand("Study.GetStudyByFormID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FormID", FormID));
            cmd.Parameters.Add(new SqlParameter("@StudyID", SqlDbType.VarChar, 7, "StudyID")).Direction = ParameterDirection.Output;

            con.Open();
            cmd.ExecuteNonQuery();
            StudyID = Convert.ToString(cmd.Parameters["@StudyID"].Value);
            con.Close();

            return StudyID;
        }


        //public int CollectSample(CollectSample smodel) //(string sampleCollectionDetails) // could this be an xml variable created by jQuery e.g. (sampleCollectionDetails)
        //public int CollectSample(CollectSample smodel)
        public int CollectSample(StudySamples smodel)
        {

            //Check sample not already collected - here?
            //CollectSample cs = new Models.CollectSample();
            //connection();
            int collected;
            int collectedSampleID;
            string sampleSetType;
            int buccalSampleID;
            int edtaSampleID;
            int gelSampleID;
            int litHepSampleID;
            int oxalateSampleID;
            //int urineSampleID;
            int processed;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConn_BioSys"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Biobank.IsSampleCollectedAndOrProcessed", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PSCID", smodel.PSCID));
                cmd.Parameters.Add(new SqlParameter("@StudyID", smodel.StudyID));
                cmd.Parameters.Add(new SqlParameter("@DateOfCollection", smodel.DateOfCollection));
                cmd.Parameters.Add(new SqlParameter("@TimeOfCollection", smodel.TimeOfCollection));
                cmd.Parameters.Add(new SqlParameter("@SampleSetType", smodel.SampleSetType));
                cmd.Parameters.Add(new SqlParameter("@Collected", SqlDbType.Int)).Direction = ParameterDirection.Output;                
                cmd.Parameters.Add(new SqlParameter("@Processed", SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@CollectedSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@EDTASampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@GelSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@LitHepSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@OxalateSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new SqlParameter("@BuccalSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                con.Open();
                //sda.Fill(ds);
                cmd.ExecuteNonQuery();
                collected = Convert.ToInt32(cmd.Parameters["@Collected"].Value);
                processed = Convert.ToInt32(cmd.Parameters["@Processed"].Value);
                collectedSampleID = Convert.ToInt32(cmd.Parameters["@CollectedSampleID"].Value);
                buccalSampleID = Convert.ToInt32(cmd.Parameters["@BuccalSampleID"].Value);
                edtaSampleID = Convert.ToInt32(cmd.Parameters["@EDTASampleID"].Value);
                gelSampleID = Convert.ToInt32(cmd.Parameters["@GelSampleID"].Value);
                litHepSampleID = Convert.ToInt32(cmd.Parameters["@LitHepSampleID"].Value);
                oxalateSampleID = Convert.ToInt32(cmd.Parameters["@OxalateSampleID"].Value);
                con.Close();
        }

            int status = -1;
            
            //int []result = new int[2];

            /**/
            if (collected == 1 && processed == 1)
            {
                status = 0; // 0 = nothing further to do; return user to the controller to redirect them to /StudyPersonSample/Index form with a confirmation message
                            //result[0] = status;
                            //result[1] = SampleID;
                smodel.CollectedSampleID = collectedSampleID;
                return status;
                //return result;                
            }
            else if (collected == 1 && processed == 0)
            {
                status = 1; // i.e. activate sample processing
                            //result[0] = status;
                            //result[1] = SampleID;
                smodel.CollectedSampleID = collectedSampleID;
                smodel.BuccalSampleID = buccalSampleID;
                smodel.EDTASampleID = edtaSampleID;
                smodel.GelSampleID = gelSampleID;
                smodel.LitHepSampleID = litHepSampleID;
                smodel.OxalateSampleID = oxalateSampleID;

                return status;
                //return result;  
            }
            else if (collected == 0 && processed == 1)
            {
                status = 0; // 0 = nothing further to do - return user to the controller to redirect them to /StudyPersonSample/Index form with a confirmation message
                //result[0] = status;
                //result[1] = SampleID;
                smodel.CollectedSampleID = collectedSampleID;
                return status;
                //return result;  
            }
            else 
            {

                //TRANSFORM DATA INTO WELL-FORMED XML
                CollectSample cs = new Models.CollectSample();

                if(smodel.PSCIDBarcode != null)
                {
                    cs.PSCID = smodel.PSCIDBarcode;
                }
                else
                {
                    cs.PSCID = smodel.PSCID;
                }
                
                cs.StudyID = smodel.StudyID;
                cs.DateOfCollection = smodel.DateOfCollection;
                cs.TimeOfCollection = smodel.TimeOfCollection;
                cs.SampleSetType = smodel.SampleSetType;
                cs.SampleSetName = smodel.SampleSetName;
               // cs.SampleType = smodel.SampleType;

                if (smodel.CollectedBy != null)
                {
                    cs.CollectedBy = smodel.CollectedBy;
                }
                else
                {
                    cs.CollectedBy = "";
                }

                if (smodel.Fasting != null)
                {
                    cs.Fasting = smodel.Fasting;
                }
                else
                {
                    cs.Fasting = "";
                }

                
                if (smodel.BuccalCollected == true)
                {
                    cs.BuccalCollected = "1";
                }
                else
                {
                    cs.BuccalCollected = "";
                }

                if (smodel.CordBloodCollected == "Yes")
                {
                    cs.CordBloodCollected = "1";
                }
                else
                {
                    cs.CordBloodCollected = "";
                }

                if (smodel.CordBloodEDTACollected == "Yes")
                {
                    cs.CordBloodEDTACollected = "1";
                }
                else
                {
                    cs.CordBloodEDTACollected = "";
                }

                if (smodel.CordBloodSerumCollected == "Yes")
                {
                    cs.CordBloodSerumCollected = "1";
                }
                else
                {
                    cs.CordBloodSerumCollected = "";
                }

                if (smodel.EDTACollected == true)
                {
                    cs.EDTACollected = "1";
                }
                else
                {
                    cs.EDTACollected = "";
                }

                if (smodel.GelCollected == true)
                {
                    cs.GelCollected = "1";
                }
                else
                {
                    cs.GelCollected = "";
                }

                if (smodel.HairCollected == "Yes")
                {
                    cs.HairCollected = "1";
                }
                else
                {
                    cs.HairCollected = "";
                }

                if (smodel.LitHepCollected == true)
                {
                    cs.LitHepCollected = "1";
                }
                else
                {
                    cs.LitHepCollected = "";
                }

                if (smodel.OxalateCollected == true)
                {
                    cs.OxalateCollected = "1";
                }
                else
                {
                    cs.OxalateCollected = "";
                }

                // NEW - HOWEVER: Do set oxalate collected if fasting date is not null
                if (smodel.FastingDate != null)
                {
                    cs.FastingDate = smodel.FastingDate;
                    cs.OxalateCollected = "1";
                }
                else
                {
                    cs.FastingDate = "";
                }

                // NEW - HOWEVER: Do set oxalate collected if fasting time is not null 
                if (smodel.FastingTime != null)
                {
                    cs.FastingTime = smodel.FastingTime;
                    cs.OxalateCollected = "1";
                }
                else
                {
                    cs.FastingTime = "";
                }
                //NEW IF ONLY FASTING TIME IS GIVEN THEN SET FASTING DATE FROM DATE OF COLLECTION
                if (smodel.FastingTime != null && smodel.FastingDate == null)
                {
                    cs.FastingDate = smodel.DateOfCollection;                    
                }


                if (smodel.SalivaCollected == "Yes")
                {
                    cs.SalivaCollected = "1";
                }
                else
                {
                    cs.SalivaCollected = "";
                }


                if (smodel.Serum1Collected == "Yes")
                {
                    cs.Serum1Collected = "1";
                }
                else
                {
                    cs.Serum1Collected = "";
                }


                if (smodel.Serum2Collected == "Yes")
                {
                    cs.Serum2Collected = "1";
                }
                else
                {
                    cs.Serum2Collected = "";
                }


                if (smodel.UrineCollected == "Yes")
                {
                    cs.UrineCollected = "1";
                }
                else
                {
                    cs.UrineCollected = "";
                }

                if (smodel.SalivaBarcode != null)
                {
                    cs.SalivaBarcode = smodel.SalivaBarcode;
                }
                else
                {
                    cs.SalivaBarcode = "";
                }

                if (smodel.BloodBarcode != null)
                {
                    cs.BloodBarcode = smodel.BloodBarcode;
                }
                else
                {
                    cs.BloodBarcode = "";
                }

                if (smodel.CordBloodBarcode != null)
                {
                    cs.CordBloodBarcode = smodel.CordBloodBarcode;
                }
                else
                {
                    cs.CordBloodBarcode = "";
                }

                if (smodel.BirthType != null)
                {
                    cs.BirthType = smodel.BirthType;
                }
                else
                {
                    cs.BirthType = "";
                }

                if (smodel.MultipleBirthOrder != null)
                {
                    cs.MultipleBirthOrder = smodel.MultipleBirthOrder;
                }
                else
                {
                    cs.MultipleBirthOrder = "";
                }

                if (smodel.StorageLocationNumber != null)
                {
                    cs.StorageLocationNumber = smodel.StorageLocationNumber;
                }
                else
                {
                    cs.StorageLocationNumber = "";
                }

                if (smodel.BoxNumber != null)
                {
                    cs.BoxNumber = smodel.BoxNumber;
                }
                else
                {
                    cs.BoxNumber = "";
                }

                if (smodel.Position != null)
                {
                    cs.Position = smodel.Position;
                }
                else
                {
                    cs.Position = "";
                }

                if (smodel.ReasonNotCollected != null)
                {
                    cs.ReasonNotCollected = smodel.ReasonNotCollected;
                }else
                {
                    cs.ReasonNotCollected = "";
                }

                if (smodel.ReasonNotCollectedOther != null)
                {
                    cs.ReasonNotCollectedOther = smodel.ReasonNotCollectedOther;
                }else
                {
                    cs.ReasonNotCollectedOther = "";
                }

                /*** NEW FROM HERE **/
                if (smodel.BuccalNumber != null)
                {
                    cs.BuccalNumber = smodel.BuccalNumber;
                    cs.BuccalCollected = "1";
                }
                else
                {
                    cs.BuccalNumber = "";
                }

                if (smodel.EDTANumber != null)
                {
                    cs.EDTANumber = smodel.EDTANumber;
                    cs.EDTACollected = "1";
                }
                else
                {
                    cs.EDTANumber = "";
                }

                if (smodel.GelNumber != null)
                {
                    cs.GelNumber = smodel.GelNumber;
                    cs.GelCollected = "1";
                }
                else
                {
                    cs.GelNumber = "";
                }

                if (smodel.LitHepNumber != null)
                {
                    cs.LitHepNumber = smodel.LitHepNumber;
                    cs.LitHepCollected = "1";
                }
                else
                {
                    cs.LitHepNumber = "";
                }


                if (smodel.GeneticsConsent != null)
                {
                    cs.GeneticsConsent = smodel.GeneticsConsent;
                }
                else
                {
                    cs.GeneticsConsent = "";
                }
  

                if (smodel.NonGeneticsConsent != null)
                {
                    cs.NonGeneticsConsent = smodel.NonGeneticsConsent;
                }
                else
                {
                    cs.NonGeneticsConsent = "";
                }


                if (smodel.RenalStudyConsent != null)
                {
                    cs.RenalStudyConsent = smodel.RenalStudyConsent;
                }
                else
                {
                    cs.RenalStudyConsent = "";
                }




                string xml = Utils.GetXMLFromObject(cs);  // THIS WORKS 

                //string successMessage;

                //TODO: COMMENTED OUT WHILE TESTING
                //connection();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConn_BioSys"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Biobank.CollectSamples", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    ////insert.Parameters.Add(new SqlParameter("@PSCID", smodel.PSCID));
                    cmd.Parameters.Add(new SqlParameter("@SampleCollectionDetails", xml));  //i.e. xml construct - can it be done??
                    cmd.Parameters.Add(new SqlParameter("@BuccalSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CollectedSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@EDTASampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@GelSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@LitHepSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@OxalateSampleID", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@SampleSet", SqlDbType.VarChar, 50, "SampleSet")).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    //TODO: NEED ALSO TO BRING BACK SampleID FOR PROVISION OF DERIVEDFROM IDs FOR ALIQUOTS
                    con.Open();
                    cmd.ExecuteNonQuery();                    
                    buccalSampleID = Convert.ToInt32(cmd.Parameters["@BuccalSampleID"].Value);
                    collectedSampleID = Convert.ToInt32(cmd.Parameters["@CollectedSampleID"].Value);
                    edtaSampleID = Convert.ToInt32(cmd.Parameters["@EDTASampleID"].Value);
                    gelSampleID = Convert.ToInt32(cmd.Parameters["@GelSampleID"].Value);
                    litHepSampleID = Convert.ToInt32(cmd.Parameters["@LitHepSampleID"].Value);
                    oxalateSampleID = Convert.ToInt32(cmd.Parameters["@OxalateSampleID"].Value);                    
                    sampleSetType = Convert.ToString(cmd.Parameters["@SampleSet"].Value);
                    status = Convert.ToInt32(cmd.Parameters["@Status"].Value);
                    con.Close();
                }

                smodel.CollectedSampleID = collectedSampleID;  // for use with sample sets containing a single collected sample (e.g. like urine)
                smodel.SampleSetType = sampleSetType;
                smodel.BuccalSampleID = buccalSampleID;
                smodel.EDTASampleID = edtaSampleID;
                smodel.GelSampleID = gelSampleID;
                smodel.LitHepSampleID = litHepSampleID;
                smodel.OxalateSampleID = oxalateSampleID;

                return status;
            }
        }

        public int ProcessSample(StudySamples smodel)
        {
            
            int status = -1;
            int loopTotal = 0;
            string studyName = "";
            string messageText = "";      

           SampleProcessingDetails spd = new Models.SampleProcessingDetails();

            spd.PSCID = smodel.PSCID;
            spd.StudyID = smodel.StudyID; 
            spd.SampleSetType = smodel.SampleSetType;          
            spd.SampleSetName = smodel.SampleSetName;
            if (smodel.DateProcessed != null)
            {
                spd.DateProcessed = smodel.DateProcessed;
            }
            else if (smodel.DateProcessed == null && smodel.DateFrozen != null)
            {
                spd.DateProcessed = smodel.DateFrozen;
            }else
            {
                spd.DateProcessed = "";
            }

            if (smodel.TimeProcessed != null)
            {
                spd.TimeProcessed = smodel.TimeProcessed;
            }
            else if (smodel.TimeProcessed == null && smodel.DateFrozen != null)
            {
                spd.TimeProcessed = smodel.TimeFrozen;
            }
            else
            {
                spd.TimeProcessed = "";
            }

            if(smodel.DateFrozen != null)
            {
                spd.DateFrozen = smodel.DateFrozen;
            }else
            {
                spd.DateFrozen = "";
            }

            if (smodel.TimeFrozen != null)
            {
                spd.TimeFrozen = smodel.TimeFrozen;
            }else
            {
                spd.TimeFrozen = "";
            }
            if (smodel.Freezer != null)
            {
                spd.Freezer = smodel.Freezer;
            } else
            {
                spd.Freezer = "";
            }

            if (smodel.Rack != null)
            {
                spd.Rack = smodel.Rack;
            }
            else
            {
                spd.Rack = "";
            }

            if (smodel.Drawer != null)
            {
                spd.Drawer = smodel.Drawer;
            }
            else
            {
                spd.Drawer = "";
            }

            if (smodel.Box != null)
            {
                spd.Box = smodel.Box;
            }
            else
            {
                spd.Box = "";
            }
            
            if (smodel.EDTAGrosslyHaemolysed == true)
            {
                spd.EDTAGrosslyHaemolysed = "1";
            }else
            {
                spd.EDTAGrosslyHaemolysed = "";
            }

            if (smodel.GelGrosslyHaemolysed == true)
            {
                spd.GelGrosslyHaemolysed = "1";
            }
            else
            {
                spd.GelGrosslyHaemolysed = "";
            }

            if (smodel.OxalateGrosslyHaemolysed == true)
            {
                spd.OxalateGrosslyHaemolysed = "1";
            }
            else
            {
                spd.OxalateGrosslyHaemolysed = "";
            }

            if (smodel.LitHepGrosslyHaemolysed == true)
            {
                spd.LitHepGrosslyHaemolysed = "1";
            }
            else
            {
                spd.LitHepGrosslyHaemolysed = "";
            }



            if (smodel.EDTAInsufficient == true)
            {
                spd.EDTAInsufficient = "1";
            }
            else
            {
                spd.EDTAInsufficient = "";
            }


            if (smodel.GelInsufficient == true)
            {
                spd.GelInsufficient = "1";
            }
            else
            {
                spd.GelInsufficient = "";
            }

            if (smodel.LitHepInsufficient == true)
            {
                spd.LitHepInsufficient = "1";
            }
            else
            {
                spd.LitHepInsufficient = "";
            }

            if (smodel.OxalateInsufficient == true)
            {
                spd.OxalateInsufficient = "1";
            }
            else
            {
                spd.OxalateInsufficient = "";
            }

            if (smodel.SerumInsufficient == true)
            {
                spd.SerumInsufficient = "1";
            }
            else
            {
                spd.SerumInsufficient = "";
            }

            if (smodel.UrineInsufficient == true)
            {
                spd.UrineInsufficient = "1";
            }
            else
            {
                spd.UrineInsufficient = "";
            }           
            

            /* 
             * SAMPLE-SPECIFIC IF BLOCKS FOR F/D/R/B FOR ALL KNOWN SAMPLE TYPES TO CREATE EMPTY OR POPULATED XML ELEMENTS
             * THERE IS POTENTIAL GIVEN SOME FORM DESIGNS (E.G. GROWING UP) FOR F/R/D/B DETAILS TO DIFFER ACROSS TUBE ALIQUOT 
             * TYPES (N.B. EDTA WB SOLICITS BOX INFO SEPARATELY FROM EDTA PLASMA ETC.), SO THIS IS CATERED FOR HERE.
            */
            if (smodel.BuccalProcessed == 1)
            {
                spd.BuccalFreezer = smodel.BuccalFreezer;
                spd.BuccalBox = smodel.BuccalBox;
                spd.BuccalBarcode = smodel.BuccalBarcode;

                //ALLOCATE BOX NAME - THIS WILL THEN BE ASSIGNED TO THE SPD MODEL BOX NAME PROPERTY BELOW
                smodel.BoxName = "Growing Up Buccal Box " + spd.BuccalBox;
            }
            else
            {
                spd.BuccalFreezer = "";
                spd.BuccalBox = "";
                spd.BuccalBarcode = "";
            }


            //if (smodel.BuffyProcessed == 1)
            //{
            //    spd.BuffyFreezer = smodel.BuffyFreezer;
            //    spd.BuffyRack = smodel.BuffyRack;
            //    spd.BuffyDrawer = smodel.BuffyDrawer;
            //    spd.BuffyBox = smodel.BuffyBox;
            //}
            //else
            //{
            //    spd.BuffyFreezer = "";
            //    spd.BuffyRack = "";
            //    spd.BuffyDrawer = "";
            //    spd.BuffyBox = "";
            //}

            //if (smodel.EDTAPlasmaProcessed == 1)
            //{
            //    spd.EDTAPlasmaFreezer = smodel.EDTAPlasmaFreezer;
            //    spd.EDTAPlasmaRack = smodel.EDTAPlasmaRack;
            //    spd.EDTAPlasmaDrawer = smodel.EDTAPlasmaDrawer;
            //    spd.EDTAPlasmaBox = smodel.EDTAPlasmaBox;
            //}
            //else
            //{
            //    spd.EDTAPlasmaFreezer = "";
            //    spd.EDTAPlasmaRack = "";
            //    spd.EDTAPlasmaDrawer = "";
            //    spd.EDTAPlasmaBox = "";
            //}

            //if (smodel.EDTARBCProcessed == 1)
            //{
            //    spd.RBCFreezer = smodel.RBCFreezer;
            //    spd.RBCRack = smodel.RBCRack;
            //    spd.RBCDrawer = smodel.RBCDrawer;
            //    spd.RBCBox = smodel.RBCBox;
            //}
            //else
            //{
            //    spd.RBCFreezer = "";
            //    spd.RBCRack = "";
            //    spd.RBCDrawer = "";
            //    spd.RBCBox = "";
            //}

            //if (smodel.EDTAWBProcessed == 1)
            //{
            //    spd.WBFreezer = smodel.WBFreezer;
            //    spd.WBRack = smodel.WBRack;
            //    spd.WBDrawer = smodel.WBDrawer;
            //    spd.WBBox = smodel.WBBox;
            //}
            //else
            //{
            //    spd.WBFreezer = "";
            //    spd.WBRack = "";
            //    spd.WBDrawer = "";
            //    spd.WBBox = "";
            //}

            //if (smodel.GelSerumProcessed == 1)
            //{
            //    spd.SerumAliquotFreezer = smodel.SerumAliquotFreezer;
            //    spd.SerumAliquotRack = smodel.SerumAliquotRack;
            //    spd.SerumAliquotDrawer = smodel.SerumAliquotDrawer;
            //    spd.SerumAliquotBox = smodel.SerumAliquotBox;
            //}
            //else
            //{
            //    spd.SerumAliquotFreezer = "";
            //    spd.SerumAliquotRack = "";
            //    spd.SerumAliquotDrawer = "";
            //    spd.SerumAliquotBox = "";
            //}


            //if (smodel.LitHepProcessed == 1)
            //{
            //    spd.LitHepPlasmaFreezer = smodel.LitHepPlasmaFreezer;
            //    spd.LitHepPlasmaRack = smodel.LitHepPlasmaRack;
            //    spd.LitHepPlasmaDrawer = smodel.LitHepPlasmaDrawer;
            //    spd.LitHepPlasmaBox = smodel.LitHepPlasmaBox;
            //}
            //else
            //{
            //    spd.LitHepPlasmaFreezer = "";
            //    spd.LitHepPlasmaRack = "";
            //    spd.LitHepPlasmaDrawer = "";
            //    spd.LitHepPlasmaBox = "";
            //}

            //if (smodel.OxalateProcessed == 1)
            //{
            //    spd.OxalatePlasmaFreezer = smodel.OxalatePlasmaFreezer;
            //    spd.OxalatePlasmaRack = smodel.OxalatePlasmaRack;
            //    spd.OxalatePlasmaDrawer = smodel.OxalatePlasmaDrawer;
            //    spd.OxalatePlasmaBox = smodel.OxalatePlasmaBox;
            //}
            //else
            //{
            //    spd.OxalatePlasmaFreezer = "";
            //    spd.OxalatePlasmaRack = "";
            //    spd.OxalatePlasmaDrawer = "";
            //    spd.OxalatePlasmaBox = "";
            //}



            //if (smodel.SerumProcessed == 1)
            //{
            //    spd.SerumAliquotFreezer = smodel.SerumAliquotFreezer;
            //    spd.SerumAliquotRack = smodel.SerumAliquotRack;
            //    spd.SerumAliquotDrawer = smodel.SerumAliquotDrawer;
            //    spd.SerumAliquotBox = smodel.SerumAliquotBox;
            //}
            //else
            //{
            //    spd.SerumAliquotFreezer = "";
            //    spd.SerumAliquotRack = "";
            //    spd.SerumAliquotDrawer = "";
            //    spd.SerumAliquotBox = "";
            //}

            //if (smodel.UrineProcessed == 1)
            //{
            //    spd.UrineFreezer = smodel.UrineFreezer;
            //    spd.UrineRack = smodel.UrineRack;
            //    spd.UrineDrawer = smodel.UrineDrawer;
            //    spd.UrineBox = smodel.UrineBox;
            //}else
            //{
            //    spd.UrineFreezer = "";
            //    spd.UrineRack = "";
            //    spd.UrineDrawer = "";
            //    spd.UrineBox = "";
            //}

            if (smodel.BoxBarcode != null)
            {
                spd.BoxBarcode = smodel.BoxBarcode;
            }else
            {
                spd.BoxBarcode = "";
            }

            if(smodel.BoxName != null)
            {
                spd.BoxName = smodel.BoxName;
            }else
            {
                spd.BoxName = "";
            }
            

            /* *****************************************************
             *  NOW: GATHER SAMPLE PROCESSING DATA FOR EACH ALIQUOT
             * *****************************************************
             */
            Sample smp = new Models.Sample();
            

            /* Instantiate Loop Total - i.e. to define the maximum number of loops */

            /* Instantiate an empty list variable */
            var aliquotList = new List<Sample>();
            //var aliquot4List = new List<Sample>();

            /* Moved outside the sample specific scope to enable > 1 type of sample set to be added to the nested sample XML */
            List<Sample> alq4List = new List<Sample>();

            /**** BLOOD SAMPLES ****/
            if (smodel.BloodsProcessed == 1)
            {
                //List<Sample> alq4List = new List<Sample>();

                //To obtain the information for an indeterminate number of aliquots we'll use a WHILE Loop
                int i;
                i = 1;
                if (smodel.EDTASampleID > 0)
                {
                    smp.SampleID = smodel.EDTASampleID;
                    loopTotal = smodel.EDTAAliquotCount;

                    if (smodel.WBAliquot1 != "")
                    {
                        while (i <= loopTotal)
                        {
                            string iString = Convert.ToString(i);
                            string aliquotnum = "WBAliquot" + iString;
                            string getAliquotNumVal = Utils.GetPropertyValue(smodel, aliquotnum);

                            //if one of the anticipated aliquots has not been provided, stop looping
                            if (getAliquotNumVal == "")
                            {
                                break;
                            }
                            else
                            {
                                string aliquotPos = "WBAliquotPosition" + iString;
                                string getAliquotPos = Utils.GetPropertyValue(smodel, aliquotPos);
                                string aliquotVol = "WBAliquotVol" + iString;
                                string getAliquotVol = Utils.GetPropertyValue(smodel, aliquotVol);

                                alq4List.Add(
                                        new Sample
                                        {
                                            Barcode = getAliquotNumVal,
                                            SampleID = smp.SampleID, //smodel.SampleID,
                                            DerivedFrom = smp.SampleID, //smodel.SampleID,
                                            SampleType = "WB",
                                            Position = getAliquotPos,
                                            Volume = getAliquotVol
                                        });

                                //Increment the WHILE index
                                i = i + 1;
                            }
                        }

                        //Reset WHILE index to 1 as may be required for a subsequent loop
                        i = 1;
                    }

                    if (smodel.EDTAPlasmaAliquot1 != "")
                    {
                        while (i <= loopTotal)
                        {
                            string iString = Convert.ToString(i);
                            string aliquotnum = "EDTAPlasmaAliquot" + iString;
                            string getAliquotNumVal = Utils.GetPropertyValue(smodel, aliquotnum);

                            //if one of the anticipated aliquots has not been provided, stop looping
                            if (getAliquotNumVal == "")
                            {
                                break;
                            }
                            else
                            {
                                string aliquotPos = "EDTAPlasmaAliquotPosition" + iString;
                                string getAliquotPos = Utils.GetPropertyValue(smodel, aliquotPos);
                                string aliquotVol = "EDTAPlasmaAliquotVol" + iString;
                                string getAliquotVol = Utils.GetPropertyValue(smodel, aliquotVol);

                                alq4List.Add(
                                        new Sample
                                        {
                                            Barcode = getAliquotNumVal,
                                            SampleID = smp.SampleID, //smodel.SampleID,
                                            DerivedFrom = smp.SampleID, //smodel.SampleID,
                                            SampleType = "Plasma",
                                            Position = getAliquotPos,
                                            Volume = getAliquotVol
                                        });

                                //Increment the WHILE index
                                i = i + 1;
                            }
                        }

                        //Reset WHILE index to 1 as may be required for a subsequent loop
                        i = 1;
                    }

                    if (smodel.BuffyAliquot1 != "")
                    {
                        while (i <= loopTotal)
                        {
                            string iString = Convert.ToString(i);
                            string aliquotnum = "BuffyAliquot" + iString;
                            string getAliquotNumVal = Utils.GetPropertyValue(smodel, aliquotnum);

                            //if one of the anticipated aliquots has not been provided, stop looping
                            if (getAliquotNumVal == "")
                            {
                                break;
                            }
                            else
                            {
                                string aliquotPos = "BuffyAliquotPosition" + iString;
                                string getAliquotPos = Utils.GetPropertyValue(smodel, aliquotPos);
                                string aliquotVol = "BuffyAliquotVol" + iString;
                                string getAliquotVol = Utils.GetPropertyValue(smodel, aliquotVol);

                                alq4List.Add(
                                        new Sample
                                        {
                                            Barcode = getAliquotNumVal,
                                            SampleID = smp.SampleID, //smodel.SampleID,
                                            DerivedFrom = smp.SampleID, //smodel.SampleID,
                                            SampleType = "Buffy",
                                            Position = getAliquotPos,
                                            Volume = getAliquotVol
                                        });

                                //Increment the WHILE index
                                i = i + 1;
                            }
                        }

                        //Reset WHILE index to 1 as may be required for a subsequent loop
                        i = 1;
                    }

                    if (smodel.RBCAliquot1 != "")
                    {
                        while (i <= loopTotal)
                        {
                            string iString = Convert.ToString(i);
                            string aliquotnum = "RBCAliquot" + iString;
                            string getAliquotNumVal = Utils.GetPropertyValue(smodel, aliquotnum);

                            //if one of the anticipated aliquots has not been provided, stop looping
                            if (getAliquotNumVal == "")
                            {
                                break;
                            }
                            else
                            {
                                string aliquotPos = "RBCAliquotPosition" + iString;
                                string getAliquotPos = Utils.GetPropertyValue(smodel, aliquotPos);
                                string aliquotVol = "RBCAliquotVol" + iString;
                                string getAliquotVol = Utils.GetPropertyValue(smodel, aliquotVol);

                                alq4List.Add(
                                        new Sample
                                        {
                                            Barcode = getAliquotNumVal,
                                            SampleID = smp.SampleID, //smodel.SampleID,
                                            DerivedFrom = smp.SampleID, //smodel.SampleID,
                                            SampleType = "RBC",
                                            Position = getAliquotPos,
                                            Volume = getAliquotVol
                                        });

                                //Increment the WHILE index
                                i = i + 1;
                            }
                        }

                        //Reset WHILE index to 1 as may be required for a subsequent loop
                        i = 1;
                    }

                } //END OF EDTA

                if (smodel.GelSampleID > 0)
                {
                    smp.SampleID = smodel.GelSampleID;
                    loopTotal = smodel.GelSerumAliquotCount;

                    while (i <= loopTotal)
                    {
                        string iString = Convert.ToString(i);
                        string aliquotnum = "SerumAliquot" + iString;
                        string getAliquotNumVal = Utils.GetPropertyValue(smodel, aliquotnum);

                        //if one of the anticipated aliquots has not been provided, stop looping
                        if (getAliquotNumVal == "")
                        {
                            break;
                        }
                        else
                        {
                            string aliquotPos = "SerumAliquotPosition" + iString;
                            string getAliquotPos = Utils.GetPropertyValue(smodel, aliquotPos);
                            string aliquotVol = "SerumAliquotVol" + iString;
                            string getAliquotVol = Utils.GetPropertyValue(smodel, aliquotVol);

                            alq4List.Add(
                                    new Sample
                                    {
                                        Barcode = getAliquotNumVal,
                                        SampleID = smp.SampleID, //smodel.SampleID,
                                        DerivedFrom = smp.SampleID, //smodel.SampleID,
                                        SampleType = "Serum",
                                        Position = getAliquotPos,
                                        Volume = getAliquotVol
                                    });

                            //Increment the WHILE index
                            i = i + 1;
                        }
                    }

                    //Reset WHILE index to 1 as may be required for a subsequent loop
                    i = 1;
                }

                if (smodel.OxalateSampleID > 0)
                {
                    smp.SampleID = smodel.OxalateSampleID;
                    loopTotal = smodel.OxalatePlasmaAliquotCount;

                    while (i <= loopTotal)
                    {
                        string iString = Convert.ToString(i);
                        string aliquotnum = "OxalatePlasmaAliquot" + iString;
                        string getAliquotNumVal = Utils.GetPropertyValue(smodel, aliquotnum);

                        //if one of the anticipated aliquots has not been provided, stop looping
                        if (getAliquotNumVal == "")
                        {
                            break;
                        }
                        else
                        {
                            string aliquotPos = "OxalatePlasmaAliquotPosition" + iString;
                            string getAliquotPos = Utils.GetPropertyValue(smodel, aliquotPos);
                            string aliquotVol = "OxalatePlasmaAliquotVol" + iString;
                            string getAliquotVol = Utils.GetPropertyValue(smodel, aliquotVol);

                            alq4List.Add(
                                    new Sample
                                    {
                                        Barcode = getAliquotNumVal,
                                        SampleID = smp.SampleID, //smodel.SampleID,
                                        DerivedFrom = smp.SampleID, //smodel.SampleID,
                                        SampleType = "Plasma",
                                        Position = getAliquotPos,
                                        Volume = getAliquotVol
                                    });

                            //Increment the WHILE index
                            i = i + 1;
                        }
                    }

                    //Reset WHILE index to 1 as may be required for a subsequent loop
                    i = 1;
                }

                if (smodel.LitHepSampleID > 0)
                {
                    smp.SampleID = smodel.LitHepSampleID;
                    loopTotal = smodel.LitHepPlasmaAliquotCount;

                    while (i <= loopTotal)
                    {
                        string iString = Convert.ToString(i);
                        string aliquotnum = "LitHepPlasmaAliquot" + iString;
                        string getAliquotNumVal = Utils.GetPropertyValue(smodel, aliquotnum);

                        //if one of the anticipated aliquots has not been provided, stop looping
                        if (getAliquotNumVal == "")
                        {
                            break;
                        }
                        else
                        {
                            string aliquotPos = "LitHepPlasmaAliquotPosition" + iString;
                            string getAliquotPos = Utils.GetPropertyValue(smodel, aliquotPos);
                            string aliquotVol = "LitHepPlasmaAliquotVol" + iString;
                            string getAliquotVol = Utils.GetPropertyValue(smodel, aliquotVol);

                            alq4List.Add(
                                    new Sample
                                    {
                                        Barcode = getAliquotNumVal,
                                        SampleID = smp.SampleID, //smodel.SampleID,
                                        DerivedFrom = smp.SampleID, //smodel.SampleID,
                                        SampleType = "Plasma",
                                        Position = getAliquotPos,
                                        Volume = getAliquotVol
                                    });

                            //Increment the WHILE index
                            i = i + 1;
                        }
                    }

                    //Reset WHILE index to 1 as may be required for a subsequent loop
                    i = 1;
                }

                if (smodel.BuccalSampleID > 0)
                {
                    smp.SampleID = smodel.BuccalSampleID;
                    loopTotal = 1; // JUST ONE ITERATION REQUIRED FOR BUCCAL SAMPLES

                    while (i <= loopTotal)
                    {

                            alq4List.Add(
                                    new Sample
                                    {
                                        Barcode = spd.BuccalBarcode,
                                        //SampleID = null, //smodel.SampleID,
                                        //DerivedFrom = null, //smodel.SampleID,
                                        SampleID = smp.SampleID,
                                        DerivedFrom = smp.SampleID, //smodel.SampleID,
                                        SampleType = "Buccal",
                                        Position = smodel.BuccalPosition,
                                        Volume = ""
                                    });

                            //Increment the WHILE index
                            i = i + 1;                       
                    }

                    //Reset WHILE index to 1 as may be required for a subsequent loop
                    i = 1;
                }


                /* COMMENTED OUT - THIS IS NOW DONE OUTSIDE THE SCOPE OF GENERATING THE SAMPLE LIST
               /*Populate the list variable */
                //aliquotList = alq4List;

            } /**** END OF BLOOD SAMPLES ****/

            /**** URINE SAMPLES ****/
            if (smodel.UrineProcessed == 1)
            {
                smp.SampleID = smodel.SampleID;   //Single Sample ID to work with for urine samples -- need to set to EDTA for EDTA etc. - this is used to update volume to 0.0 and to create Aliquoted action record on the database
                //spd.SampleID = smodel.SampleID;  //Single Sample ID to work with for urine samples

                loopTotal = smodel.AliquotCount;          
                
                //List<Sample> alq4List = new List<Sample>();

                //To obtain the information for an indeterminate number of aliquots we'll use a WHILE Loop
                    int i;
                    i = 1;
                    while (i <= loopTotal)
                    {
                        string iString = Convert.ToString(i);
                        string aliquotnum = "UrineAliquot" + iString;
                        string getAliquotNumVal = Utils.GetPropertyValue(smodel, aliquotnum);

                        //if one of the anticipated aliquots has not been provided, stop looping
                        if (getAliquotNumVal == "")
                        {
                            break;
                        }
                        else
                        {
                            string aliquotPos = "UrineAliquotPosition" + iString;
                            string getAliquotPos    = Utils.GetPropertyValue(smodel, aliquotPos);
                            string aliquotVol = "UrineAliquotVol" + iString;
                            string getAliquotVol = Utils.GetPropertyValue(smodel, aliquotVol);

                            alq4List.Add(
                                    new Sample {
                                        Barcode = getAliquotNumVal,
                                        SampleID = smp.SampleID, //smodel.SampleID,
                                        DerivedFrom = smp.SampleID, //smodel.SampleID,
                                        SampleType = smodel.SampleType,
                                        Position = getAliquotPos,
                                        Volume = getAliquotVol
                                    });

                             //Increment the WHILE index
                             i = i + 1;
                        }

                    }

                /*Populate the list variable */
               // aliquotList = alq4List;

            }

            //consolidate aliquot list arrays as one list
            aliquotList = alq4List;

            //Next: set the Samples model properties using the list of list array of aliquot details
            spd.Samples = aliquotList;

            //Next: add spd object to XMLSerializer 
            string xml = Utils.GetXMLFromObject(spd);


            //NEXT STEP POST XML TO DATABASE AND SET STATUS FLAG AS RESULT OF DATABASE POST

            connection();
            SqlCommand cmd = new SqlCommand("Biobank.SaveSampleProcessing", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SampleProcessingDetails", xml));
            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int)).Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(new SqlParameter("@SampleSetType, SqlDbType.VarChar, 50, "SampleSetType")).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new SqlParameter("@StudyName", SqlDbType.VarChar, 50, "StudyName")).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new SqlParameter("@Error", SqlDbType.VarChar, 2000, "Message")).Direction = ParameterDirection.Output;
            con.Open();
            /* Comment Back In When You're Ready to Test the DB Procedure */
            cmd.ExecuteNonQuery();
            status = Convert.ToInt32(cmd.Parameters["@Status"].Value);
            studyName = Convert.ToString(cmd.Parameters["@StudyName"].Value);
            con.Close();

            smodel.StudyName = studyName;
            
            if (Convert.ToString(cmd.Parameters["@Error"].Value) != "")
            {
                messageText = Convert.ToString(cmd.Parameters["@Error"].Value);
                Regex regex = new Regex(@"(\r\n|\r|\n)+");
                //smodel.Message = Convert.ToString(cmd.Parameters["@Error"].Value);
                smodel.Message = regex.Replace(messageText, "<br />");
            }

            //PASS AS STATUS FLAG BACK TO THE CONTROLLER
            return status; // Temporary return value!
        }


        public String AddStudySample(AddStudySample smodel)
        {
            AddStudySample sSample = new Models.AddStudySample();
            sSample.PSCID = smodel.PSCID;
            sSample.StudyID = smodel.StudyID;
            sSample.DateFrozen = smodel.DateFrozen;
            sSample.TimeFrozen = smodel.TimeFrozen;
            sSample.DateProcessed = smodel.DateProcessed;
            sSample.TimeProcessed = smodel.TimeProcessed;
            sSample.UrineAliquot1 = smodel.UrineAliquot1;
            sSample.UrineAliquot1 = smodel.UrineAliquot1Position;
            sSample.UrineAliquot1Vol = smodel.UrineAliquot1Vol;

            string xml = Utils.GetXMLFromObject(sSample);

            return xml;

        }

        public bool AddUrineSamples(StudySamples smodel)
        {
            //StudySampleHandling.SampleProcessingDetailsToXML sSample = new Models.StudySampleHandling.SampleProcessingDetailsToXML();
            StudySamples sSample = new Models.StudySamples();
            sSample.PSCID = smodel.PSCID;
            sSample.StudyID = smodel.StudyID;            
            sSample.DateFrozen = smodel.DateFrozen;
            sSample.TimeFrozen = smodel.TimeFrozen;
            sSample.DateProcessed = smodel.DateProcessed;
            sSample.TimeProcessed = smodel.TimeProcessed; 

            /** The following variables need to be turned into a list to provide the nested sample-specific tags in the XML Schema **/
            sSample.SampleID = smodel.SampleID;  // i.e. this is the source sample that is databased at the time of collection which will be populated used to create the Aliquoted action for the source sample
            //sSample.Barcode = smodel.Barcode;   //  i.e. this is barcode number allocated for each aliquot produced 
            sSample.DerivedFrom = smodel.DerivedFrom; // i.e. this will be the sample ID of the source sample
            sSample.SampleType = smodel.SampleType; // i.e. the sample type of each aliquot

            sSample.UrineAliquot1 = smodel.UrineAliquot1; //i.e. this is barcode number allocated for the aliquot produced
            sSample.UrineAliquot2 = smodel.UrineAliquot2;
            sSample.UrineAliquot3 = smodel.UrineAliquot3;
            sSample.UrineAliquot4 = smodel.UrineAliquot4;
            sSample.UrineAliquot5 = smodel.UrineAliquot5;

            sSample.UrineAliquotPosition1 = smodel.UrineAliquotPosition1;
            sSample.UrineAliquotPosition2 = smodel.UrineAliquotPosition2;
            sSample.UrineAliquotPosition3 = smodel.UrineAliquotPosition3;
            sSample.UrineAliquotPosition4 = smodel.UrineAliquotPosition4;
            sSample.UrineAliquotPosition5 = smodel.UrineAliquotPosition5;

            sSample.UrineAliquotVol1 = smodel.UrineAliquotVol1;
            sSample.UrineAliquotVol2 = smodel.UrineAliquotVol2;
            sSample.UrineAliquotVol3 = smodel.UrineAliquotVol3;
            sSample.UrineAliquotVol4 = smodel.UrineAliquotVol4;
            sSample.UrineAliquotVol5 = smodel.UrineAliquotVol5;

            //TO DO: CREATE LIST OF ALIQUOTS TO PASS WITH SINGLE ITEM VARIABLES TO THE SERIALIZER METHOD

            //TODO: MAKE SERIAILIZER METHOD StudySampleHandling.SampleProcessingDetailsToXML(); AN INVOCABLE METHOD
            //string xml = Utils.GetXMLFromObject(sSample);
            //string xml = StudySampleHandling.SampleProcessingDetailsToXML();


            //connection();

            //SqlCommand cmd = new SqlCommand("Biobank.SaveXMLString", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@XML", xml);

            //con.Open();
            //int rows = 0;
            //rows = cmd.ExecuteNonQuery();
            //con.Close();
            //if (rows >0)
            //{
            return true;
            //}
            //else
            //{
            //    return false;
            //}            

        }

        internal int CollectSample(int v)
        {
            throw new NotImplementedException();
        }
    } //EOCRUDStudySamples
} //EONamespace