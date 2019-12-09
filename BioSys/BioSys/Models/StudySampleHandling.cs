using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace BioSys.Models
{
    [Serializable]
    public class StudySampleHandling
    {
        //we need a default constructor for serialization!
        public StudySampleHandling()
        {

        }

        //[XmlRoot(Namespace = "", IsNullable = true)]
        //public class SampleProcessingDetailsToXML
        //{
        //    public string StudyID { get; set; }
        //    public string PSCID { get; set; }
        //    public string SampleSetName { get; set; }
        //    public string BoxBarcode { get; set; }
        //    public string BoxName { get; set; }
        //    public string DateCollected { get; set; }
        //    public string TimeCollected { get; set;}
        //    public string DateProcessed { get; set; }
        //    public string TimeProcessed { get; set; }
        //    public string DateFrozen { get; set; }
        //    public string TimeFrozen { get; set; }

        //    [XmlArray("Samples")]
        //    [XmlArrayItem("Sample")]
        //    public List<StudySamples> samples { get; set; }

        //    public string Serialize()
        //    {
        //        var studysamples = new StudySamples { SampleID = "SampleID", Barcode = "Barcode", DerivedFrom = "DerivedFrom", SampleType = "SampleType", Volume = "Volume", Position = "Position", notes = "Notes" };
        //        this.samples = new List<StudySamples> { studysamples, studysamples, studysamples, studysamples, studysamples, studysamples, studysamples };
        //        XmlSerializer s = new XmlSerializer(this.GetType());
        //        StringBuilder sb = new StringBuilder();
        //        TextWriter w = new StringWriter(sb);  //should this be StringWriter w = new StringWriter(sb);????
        //        s.Serialize(w, this);
        //        w.Flush();
        //        return sb.ToString();
        //    }
        //}
    }

    public class CollectSample 
    {
        public string PSCID { get; set; }
        public string PSCIDBarcode { get; set; }
        public string StudyName { get; set; }
        public string ViewName { get; set; }
        //public string PSCIDBarcode { get; set; }
        public string SampleType { get; set; }
        public string LabBarcode { get; set; }
        public string StudyID { get; set; }
        public string DateOfCollection { get; set; }
        public string TimeOfCollection { get; set; }
        public string PreviouslyCollected { get; set; }
        public string PreviouslyProcessed { get; set; }
        public string SampleSetType { get; set; }
        public string SampleSetName { get; set; }
        public string CollectedBy { get; set; }
        public string Fasting { get; set; }
        public string FastingDate { get; set; }
        public string FastingTime { get; set; }

        public string ConsentGenetics { get; set; }
        public string ConsentNonGenetic { get; set; }
        public string ConsentRenalStudy { get; set; }

        public string GeneticsConsent { get; set; }
        public string NonGeneticsConsent { get; set; }
        public string RenalStudyConsent { get; set; }

        [XmlIgnore]
        public int CollectedSampleID { get; set; }
        [XmlIgnore]
        public int BuccalSampleID { get; set; }
        [XmlIgnore]
        public int EDTASampleID { get; set; }
        [XmlIgnore]
        public int GelSampleID { get; set; }
        [XmlIgnore]
        public int LitHepSampleID { get; set; }
        [XmlIgnore]
        public int OxalateSampleID { get; set; }
        [XmlIgnore]
        public int UrineSampleID { get; set; }

        public string CordBloodBarcode { get; set; }
        public string BloodBarcode { get; set; }
        public string SalivaBarcode { get; set; }

        public string BuccalCollected { get; set; }
        public string CordBloodCollected { get; set; }
        public string CordBloodEDTACollected { get; set; }
        public string CordBloodSerumCollected { get; set; }
        public string EDTACollected { get; set; }
        public string GelCollected { get; set; }
        public string HairCollected { get; set; }
        public string LitHepCollected { get; set; }
        public string OxalateCollected { get; set; }
        public string SalivaCollected { get; set; }
        public string Serum1Collected { get; set; }
        public string Serum2Collected { get; set; }
        public string UrineCollected { get; set; }

        public string BuccalNumber { get; set; }
        public string EDTANumber { get; set; }
        public string GelNumber { get; set; }
        public string LitHepNumber { get; set; }
                
        public string BirthType { get; set; }
        public string MultipleBirthOrder { get; set; }
        public string StorageLocationNumber { get; set; }
        public string BoxNumber { get; set; }
        public string Position { get; set; }
        public string ReasonNotCollected { get; set; }
        public string ReasonNotCollectedOther { get; set; }
    }
    /*
    *  The following blog article helped formulate thinking about the need for two classes - maffelu.net/c-xml-serializing/ - 
    *  One class of properties is required for the nested nth recurring elements (i.e. Sample: this contains the properties of the nested elements)
    *  and the second for the top level elements (i.e. SampleProcessing class providing root), this will incorporate 
    */
    [XmlType("Sample")]
    public class Sample
    {
        [XmlElement("SampleID")]
        public int? SampleID { get; set; }
        [XmlElement("Barcode")]
        public string Barcode { get; set; }
        [XmlElement("DerivedFrom")]
        public int? DerivedFrom { get; set; }
        [XmlElement("SampleType")]
        public string SampleType { get; set; }
        [XmlElement("Volume")]
        public string Volume { get; set; }
        [XmlElement("Position")]
        public string Position { get; set; }
        [XmlElement("Notes")]
        public string Notes { get; set; }
    }

    [XmlRoot("SampleProcessingDetails")]
    public class SampleProcessingDetails
    {
        [XmlElement("PSCID")]
        public string PSCID { get; set; }
        [XmlElement("StudyID")]
        public string StudyID { get; set; }

        [XmlIgnore]
        public string StudyName { get; set; }

        [XmlElement("SampleID")]
        public string SampleID { get; set; }
        [XmlElement("SampleSetType")]
        public string SampleSetType { get; set; }
        [XmlElement("SampleSetName")]
        public string SampleSetName { get; set; }
        [XmlElement("DateFrozen")]
        public string DateFrozen { get; set; }
        [XmlElement("DateProcessed")]
        public string DateProcessed { get; set; }
        [XmlElement("TimeFrozen")]
        public string TimeFrozen { get; set; }
        [XmlElement("TimeProcessed")]
        public string TimeProcessed { get; set; }

        [XmlElement("BuccalBarcode")]
        public string BuccalBarcode { get; set; }

        /*************** GENERICS *********************/
        [XmlElement("Freezer")]
        public string Freezer { get; set; }
        [XmlElement("Rack")]
        public string Rack { get; set; }
        [XmlElement("Drawer")]
        public string Drawer { get; set; }
        [XmlElement("Box")]
        public string Box { get; set; }

        /**************** FREEZER ********************/
        /************ SAMPLE-SPECIFIC ****************/
        //[Required]
        //[Display(Name ="(f)")]

        [XmlElement("BuccalFreezer")]
        public string BuccalFreezer { get; set; }
        [XmlElement("BuffyFreezer")]
        public string BuffyFreezer { get; set; }
        [XmlElement("EDTAPlasmaFreezer")]
        public string EDTAPlasmaFreezer { get; set; }
        [XmlElement("LitHepPlasmaFreezer")]
        public string LitHepPlasmaFreezer { get; set; }
        [XmlElement("OxalatePlasmaFreezer")]
        public string OxalatePlasmaFreezer { get; set; }
        [XmlElement("RBCFreezer")]
        public string RBCFreezer { get; set; }
        [XmlElement("SerumAliquotFreezer")]
        public string SerumAliquotFreezer { get; set; }
        [XmlElement("UrineFreezer")]
        public string UrineFreezer { get; set; }
        [XmlElement("WBFreezer")]
        public string WBFreezer { get; set; }

        /**************** RACK ********************/
        /************ SAMPLE-SPECIFIC ****************/
        [XmlElement("BuffyRack")]
        public string BuffyRack { get; set; }
        [XmlElement("EDTAPlasmaRack")]
        public string EDTAPlasmaRack { get; set; }
        [XmlElement("LitHepPlasmaRack")]
        public string LitHepPlasmaRack { get; set; }
        [XmlElement("OxalatePlasmaRack")]
        public string OxalatePlasmaRack { get; set; }
        [XmlElement("RBCRack")]
        public string RBCRack { get; set; }
        [XmlElement("SerumAliquotRack")]
        public string SerumAliquotRack { get; set; }
        [XmlElement("UrineRack")]
        public string UrineRack { get; set; }
        [XmlElement("WBRack")]
        public string WBRack { get; set; }

        /**************** DRAW ********************/
        /************ SAMPLE-SPECIFIC ****************/
        [XmlElement("BuffyDrawer")]
        public string BuffyDrawer { get; set; }
        [XmlElement("EDTAPlasmaDrawer")]
        public string EDTAPlasmaDrawer { get; set; }
        [XmlElement("LitHepPlasmaDrawer")]
        public string LitHepPlasmaDrawer { get; set; }
        [XmlElement("OxalatePlasmaDrawer")]
        public string OxalatePlasmaDrawer { get; set; }
        [XmlElement("RBCDrawer")]
        public string RBCDrawer { get; set; }
        [XmlElement("SerumAliquotDrawer")]
        public string SerumAliquotDrawer { get; set; }
        [XmlElement("UrineDrawer")]
        public string UrineDrawer { get; set; }
        [XmlElement("WBDrawer")]
        public string WBDrawer { get; set; }

        /**************** BOX ********************/
        /************ SAMPLE-SPECIFIC ****************/

        [XmlElement("BuccalBox")]
        public string BuccalBox { get; set; }
        [XmlElement("BuffyBox")]
        public string BuffyBox { get; set; }
        [XmlElement("EDTAPlasmaBox")]
        public string EDTAPlasmaBox { get; set; }
        [XmlElement("LitHepPlasmaBox")]
        public string LitHepPlasmaBox { get; set; }
        [XmlElement("OxalatePlasmaBox")]
        public string OxalatePlasmaBox { get; set; }
        [XmlElement("RBCBox")]
        public string RBCBox { get; set; }
        [XmlElement("SerumAliquotBox")]
        public string SerumAliquotBox { get; set; }
        [XmlElement("UrineBox")]
        public string UrineBox { get; set; }
        [XmlElement("WBBox")]
        public string WBBox { get; set; }

        [XmlElement("BoxBarcode")]
        public string BoxBarcode { get; set; }
        [XmlElement("BoxName")]
        public string BoxName { get; set; }

        [XmlElement("EDTAGrosslyHaemolysed")]
        public string EDTAGrosslyHaemolysed { get; set; }
        [XmlElement("GelGrosslyHaemolysed")]
        public string GelGrosslyHaemolysed { get; set; }
        [XmlElement("OxalateGrosslyHaemolysed")]
        public string OxalateGrosslyHaemolysed { get; set; }
        [XmlElement("LitHepGrosslyHaemolysed")]
        public string LitHepGrosslyHaemolysed { get; set; }

        [XmlElement("EDTAInsufficient")]
        public string EDTAInsufficient { get; set; }
        [XmlElement("EDTAClotted")]
        public string EDTAClotted { get; set; }
        [XmlElement("EDTAHaemolysed")]
        public string EDTAHaemolysed { get; set; }
        [XmlElement("SerumInsufficient")]
        public string SerumInsufficient { get; set; }
        [XmlElement("SerumClotted")]
        public string SerumClotted { get; set; }
        [XmlElement("SerumHaemolysed")]
        public string SerumHaemolysed { get; set; }
        [XmlElement("GelInsufficient")]
        public string GelInsufficient { get; set; }
        [XmlElement("LitHepInsufficient")]
        public string LitHepInsufficient { get; set; }
        [XmlElement("OxalateInsufficient")]
        public string OxalateInsufficient { get; set; }
        [XmlElement("UrineInsufficient")]
        public string UrineInsufficient { get; set; }

        //TODO: The following properties will be used to derive the nested xml tags for each of the sample aliquots from the sample class
        //This helped formulat idea of two classes - maffelu.net/c-xml-serializing/ - sample processing as root and sub class of of XmlArray Samples, of which sample is an XMLArray item
        //THIS MIGHT PROVE USEFUL: visualstudiomagazine.com/articles/2004/02/01/serialize-arrays-and-arraylists-to-xml.aspx

        [XmlArray("Samples")]
        [XmlArrayItem("Sample")]
        public List<Sample> Samples { get; set; }     
         //public Array
        public SampleProcessingDetails()
        {
            Samples = new List<Sample>();            
        }

    }

    public class StudySamples : IValidatableObject
    {
        [MaxLength(14, ErrorMessage = "PSCID/TestID is 14 characters max. You've have exceeded the character limit")]
        public string PSCID { get; set; }
        [MaxLength(14, ErrorMessage = "PSCID/TestID is 14 characters max. You've have exceeded the character limit")]
        public string PSCIDBarcode { get; set; }
        public string LabBarcode { get; set; }
        public string StudyID { get; set; }
        public string StudyName { get; set; }
        public string ViewName { get; set; }
        public string SampleSetType { get; set; }
        public string SampleSetName { get; set; }
        public string CollectedBy { get; set; }

        public int CollectedSampleID { get; set; }
        public int BuccalSampleID { get; set; }
        public int EDTASampleID { get; set; }
        public int GelSampleID { get; set; }        
        public int LitHepSampleID { get; set; }  
        public int OxalateSampleID { get; set; }     
        public int SampleID { get; set; }
        public int SerumSampleID { get; set; }            
        public int SourceSampleID { get; set; }
        public int UrineSampleID { get; set; }

        public string DerivedFrom { get; set; }
        public string SampleType { get; set; }

        public int BloodsProcessed { get; set; }
        public int BuccalProcessed { get; set; }
        public int BuffyProcessed { get; set; }
        public int EDTAProcessed { get; set; }
        public int EDTAPlasmaProcessed { get; set; }
        public int EDTABuffyProcessed { get; set; }
        public int EDTARBCProcessed { get; set; }
        public int EDTAWBProcessed { get; set; }
        public int GelSerumProcessed { get; set; }
        public int LitHepProcessed { get; set; }
        public int OxalateProcessed { get; set; }
        public int SerumProcessed { get; set; }
        public int UrineProcessed { get; set; }

        public string BirthType { get; set; }
        public string MultipleBirthOrder { get; set; }
        public string StorageLocationNumber { get; set; }
        public string BoxNumber { get; set; }
        public string ReasonNotCollected { get; set; }
        public string ReasonNotCollectedOther { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Volume { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string Position { get; set; }          
        public string notes { get; set; }    
        public string Message { get; set; }   

        public string Fasting { get; set; }

        public int AliquotCount { get; set; }
        public int BuffyAliquotCount { get; set; }
        public int EDTAAliquotCount { get; set; }
        public int EDTAPlasmaAliquotCount { get; set; }        
        public int LitHepPlasmaAliquotCount { get; set; }
        public int OxalatePlasmaAliquotCount { get; set; }
        public int RBCAliquotCount { get; set; }
        public int GelSerumAliquotCount { get; set; }
        public int SerumAliquotCount { get; set; }
        public int WBAliquotCount { get; set; }

        /*
         ************* SAMPLE STATE
         ************* PROPERTIES
        */
        public bool EDTAInsufficient { get; set; }
        public bool EDTAClotted { get; set; }
        public bool EDTAHaemolysed { get; set; }
        public bool SerumInsufficient { get; set; }
        public bool SerumClotted { get; set; }
        public bool SerumHaemolysed { get; set; }
        public bool GelInsufficient { get; set; }
        public bool LitHepInsufficient { get; set; }
        public bool OxalateInsufficient { get; set; }
        public bool UrineInsufficient { get; set; }


        /**************** Barcode ***********************
         * N.B. FOR ALIQUOTS, THE BARCODES ARE NAMED 
         *      SIMPLY BY A COMBINATION OF ALIQUOT+NUMBER
         *      SEE ALIQUOT BARCODES SECTION BELOW
         * **/
        public string Barcode { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string BuccalBarcode { get; set; }
        public string BloodBarcode { get; set; }
        public string CordBloodBarcode { get; set; }
        public string SalivaBarcode { get; set; }
        public string SampleBarcode { get; set; }

        /* 
         * *********** COLLECTED ****************
         * ********** PROPERTIES ****************
         */
        public string BloodCollected { get; set; }
        public bool BuccalCollected { get; set; }
        public string BuccalNumber { get; set; }
        public string CordBloodCollected { get; set; }
        public string CordBloodEDTACollected { get; set; }
        public string CordBloodEDTANumber { get; set; }
        public string CordBloodSerumCollected { get; set; }
        public string CordBloodSerumNumber { get; set; }
        public bool EDTACollected { get; set; }
        public string EDTANumber { get; set; }
        public bool GelCollected { get; set; }
        public string GelNumber { get; set; }
        public string HairCollected { get; set; }
        public bool LitHepCollected { get; set; }
        public string LitHepNumber { get; set; }
        public bool OxalateCollected { get; set; }
        public string OxalateLastDate { get; set; }
        public string OxalateLastTime { get; set; }
        public string SampleCollectedYes { get; set; }
        public string SampleCollectedNo { get; set; }
        public string SalivaCollected { get; set; }
        public string Serum1Collected { get; set; }
        public string Serum2Collected { get; set; }
        public string UrineCollected { get; set; }

        public bool EDTAGrosslyHaemolysed { get; set; }
        public bool GelGrosslyHaemolysed { get; set; }
        public bool OxalateGrosslyHaemolysed { get; set; }
        public bool LitHepGrosslyHaemolysed { get; set; }

        /* COLLECT PROPERTIES WHEN USING CHECKBOX CONTROLS  */
        public bool BloodCollectedCheckBox { get; set; }
        public bool BuccalCollectedCheckBox { get; set; } 
        public bool CordBloodCollectedCheckBox { get; set; }
        public bool CordBloodEDTACollectedCheckBox { get; set; }
        public bool CordBloodSerumCollectedCheckBox { get; set; }
        public bool EDTACollectedCheckBox { get; set; }
        public bool GelCollectedCheckBox { get; set; }
        public bool HairCollectedCheckBox { get; set; }
        public bool LitHepCollectedCheckBox { get; set; }
        public bool OxalateCollectedCheckBox { get; set; }
        public bool OxalateLastDateCheckBox { get; set; }
        public bool OxalateLastTimeCheckBox { get; set; }
        public bool SampleCollectedCheckBox { get; set; }
        public bool SalivaCollectedCheckBox { get; set; }
        public bool Serum1CollectedCheckBox { get; set; }
        public bool Serum2CollectedCheckBox { get; set; }
        public bool UrineCollectedCheckBox { get; set; }



        /* 
         * *************** CONSENT *******************
         * ************** PROPERTIES *****************
         */
        //public IList<SelectListItem> ConsentGenetics { get; set; }
        public yesNo YesNo { get; set; }
        public bool ConsentGenetics { get; set; }
        public bool ConsentNonGenetic { get; set; }
        public bool ConsentRenalStudy { get; set; }
        public string ddlRenalStudyYN { get; set; }
        public string GeneticsConsent { get; set; }
        public string NonGeneticsConsent { get; set; }
        public string RenalStudyConsent { get; set; }


        /*
         * *************** DATE & TIME *******************
         * *************** PROPERTIES  *******************
         */
        public string DateBuccalProcessed { get; set; }
        public string DateFrozen { get; set; }
        public string DateOfCollection { get; set; }
        public string DateProcessed { get; set; }

        public string TimeBuccalFrozen { get; set; }
        public string TimeBuccalProcessed { get; set; }
        public string TimeFrozen { get; set; }
        public string TimeOfCollection { get; set; }
        public string TimeProcessed { get; set; }

        public string FastingDate { get; set; }
        public string FastingTime { get; set; }

        /*
         ****************LOCATION *********************
         *************** PROPERTIES *******************
         ************** BY CATEGORY *******************        
        */

        /**************** FREEZER ********************/
        //[Required]
        //[Display(Name ="(f)")]
        public string Freezer { get; set; }
        public string BuccalFreezer { get; set; }
        public string BuffyFreezer { get; set; }
        public string EDTAPlasmaFreezer { get; set; }
        public string LitHepPlasmaFreezer { get; set; }
        public string OxalatePlasmaFreezer { get; set; }
        public string RBCFreezer { get; set; }
        public string SerumAliquotFreezer { get; set; }
        public string UrineFreezer { get; set; }
        public string WBFreezer { get; set; }

        /**************** RACK ********************/
        public string BuffyRack { get; set; }
        public string EDTAPlasmaRack { get; set; }
        public string LitHepPlasmaRack { get; set; }
        public string OxalatePlasmaRack { get; set; }
        public string Rack { get; set; }
        public string RBCRack { get; set; }
        public string SerumAliquotRack { get; set; }
        public string UrineRack { get; set; }
        public string WBRack { get; set; }

        /**************** DRAW ********************/
        public string BuffyDrawer { get; set; }
        public string Drawer { get; set; }
        public string EDTAPlasmaDrawer { get; set; }
        public string LitHepPlasmaDrawer { get; set; }
        public string OxalatePlasmaDrawer { get; set; }
        public string RBCDrawer { get; set; }
        public string SerumAliquotDrawer { get; set; }
        public string UrineDrawer { get; set; }
        public string WBDrawer { get; set; }

        /**************** BOX ********************/
        public string Box { get; set; }
        public string BoxBarcode { get; set; }
        public string BoxName { get; set; }
        public string BuccalBox { get; set; }
        public string BuffyBox { get; set; }
        public string EDTAPlasmaBox { get; set; }
        public string LitHepPlasmaBox { get; set; }
        public string OxalatePlasmaBox { get; set; }
        public string RBCBox { get; set; }
        public string SerumAliquotBox { get; set; }
        public string UrineBox { get; set; }
        public string WBBox { get; set; }


        /***************** POSITION ********************/

        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string BuccalPosition { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string BuffyAliquotPosition1 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string BuffyAliquotPosition2 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string BuffyAliquotPosition3 { get; set; }

        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string EDTAPlasmaAliquotPosition1 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string EDTAPlasmaAliquotPosition2 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string EDTAPlasmaAliquotPosition3 { get; set; }

        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string LitHepPlasmaAliquotPosition1 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string LitHepPlasmaAliquotPosition2 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string LitHepPlasmaAliquotPosition3 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string LitHepPlasmaAliquotPosition4 { get; set; }

        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string OxalatePlasmaAliquotPosition1 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string OxalatePlasmaAliquotPosition2 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string OxalatePlasmaAliquotPosition3 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string OxalatePlasmaAliquotPosition4 { get; set; }

        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string RBCAliquotPosition1 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string RBCAliquotPosition2 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string RBCAliquotPosition3 { get; set; }

        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string SerumAliquotPosition1 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string SerumAliquotPosition2 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string SerumAliquotPosition3 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string SerumAliquotPosition4 { get; set; }

        //[MaxLength(3) ]
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string UrineAliquotPosition1 { get; set; }
        //[MaxLength(3)]
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string UrineAliquotPosition2 { get; set; }
        //[MaxLength(3)]
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string UrineAliquotPosition3 { get; set; }
        //[MaxLength(3)]
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string UrineAliquotPosition4 { get; set; }
        //[MaxLength(3)]
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string UrineAliquotPosition5 { get; set; }

        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string WBAliquotPosition1 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string WBAliquotPosition2 { get; set; }
        [RegularExpression("^[A-J]([0][1-9]|10)$|^[1-9]$|^[1-9][0-9]$|100", ErrorMessage = "Please enter a valid position value: EITHER a letter (A-J) followed by 2 digits (e.g. A01 to J10 etc.) OR an integer between 1 and 100")]
        public string WBAliquotPosition3 { get; set; }


        /*
         * ************  ALIQUOT BARCODE ***************
         * *************** PROPERTIES ******************
         */
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string BuffyAliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string BuffyAliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string BuffyAliquot3 { get; set; }

        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string EDTAPlasmaAliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string EDTAPlasmaAliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string EDTAPlasmaAliquot3 { get; set; }

        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string LitHepPlasmaAliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string LitHepPlasmaAliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string LitHepPlasmaAliquot3 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string LitHepPlasmaAliquot4 { get; set; }

        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string OxalatePlasmaAliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string OxalatePlasmaAliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string OxalatePlasmaAliquot3 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string OxalatePlasmaAliquot4 { get; set; }

        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string RBCAliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string RBCAliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string RBCAliquot3 { get; set; }

        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string SerumAliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string SerumAliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string SerumAliquot3 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string SerumAliquot4 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string Serum1Aliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string Serum1Aliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string Serum1Aliquot3 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string Serum1Aliquot4 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string Serum2Aliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string Serum2Aliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string Serum2Aliquot3 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string Serum2Aliquot4 { get; set; }

        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string UrineAliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string UrineAliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string UrineAliquot3 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string UrineAliquot4 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string UrineAliquot5 { get; set; }

        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string WBAliquot1 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string WBAliquot2 { get; set; }
        [MaxLength(7, ErrorMessage = "Barcode longer then the permitted 7 characters!")]
        public string WBAliquot3 { get; set; }


        /*
         * *************** VOLUME ******************
         * ************* PROPERTIES ****************
         */
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string BuffyAliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string BuffyAliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string BuffyAliquotVol3 { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string EDTAPlasmaAliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string EDTAPlasmaAliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string EDTAPlasmaAliquotVol3 { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string LitHepPlasmaAliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string LitHepPlasmaAliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string LitHepPlasmaAliquotVol3 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string LitHepPlasmaAliquotVol4 { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string OxalatePlasmaAliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string OxalatePlasmaAliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string OxalatePlasmaAliquotVol3 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string OxalatePlasmaAliquotVol4 { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string RBCAliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string RBCAliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string RBCAliquotVol3 { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string SerumAliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string SerumAliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string SerumAliquotVol3 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string SerumAliquotVol4 { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Serum1AliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Serum1AliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Serum1AliquotVol3 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Serum1AliquotVol4 { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Serum2AliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Serum2AliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Serum2AliquotVol3 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string Serum2AliquotVol4 { get; set; }

        [Range(0.0, 1.90, ErrorMessage = "Volume must be in the range of 0.0 to 1.9 inclusive")]
        public string UrineAliquotVol1 { get; set; }
        [Range(0.0, 1.90, ErrorMessage = "Volume must be in the range of 0.0 to 1.9 inclusive")]
        public string UrineAliquotVol2 { get; set; }
        [Range(0.0, 1.90, ErrorMessage = "Volume must be in the range of 0.0 to 1.9 inclusive")]
        public string UrineAliquotVol3 { get; set; }
        [Range(0.0, 1.90, ErrorMessage = "Volume must be in the range of 0.0 to 1.9 inclusive")]
        public string UrineAliquotVol4 { get; set; }
        [Range(0.0, 1.90, ErrorMessage = "Volume must be in the range of 0.0 to 1.9 inclusive")]
        public string UrineAliquotVol5 { get; set; }

        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string WBAliquotVol1 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string WBAliquotVol2 { get; set; }
        [Range(0.0, 2.0, ErrorMessage = "Volume must be in the range of 0.0 to 2.0 inclusive")]
        public string WBAliquotVol3 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var vFreezer = new[] { "Freezer" };
            if(BuffyAliquot1 != null && Freezer == null || EDTAPlasmaAliquot1 != null && Freezer == null || 
                LitHepPlasmaAliquot1 != null && Freezer == null || OxalatePlasmaAliquot1 != null && Freezer == null ||
                RBCAliquot1 != null && Freezer == null || SerumAliquot1 != null && Freezer == null || 
                UrineAliquot1 != null && Freezer == null || WBAliquot1 != null && Freezer == null)
            {
                yield return new ValidationResult("Please specify freezer!", vFreezer);
            }

            var vRack = new[] { "Rack" };
            if (BuffyAliquot1 != null && Rack == null || EDTAPlasmaAliquot1 != null && Rack == null ||
                LitHepPlasmaAliquot1 != null && Rack == null || OxalatePlasmaAliquot1 != null && Rack == null ||
                RBCAliquot1 != null && Rack == null || SerumAliquot1 != null && Rack == null ||
                UrineAliquot1 != null && Rack == null || WBAliquot1 != null && Rack == null)
            {
                yield return new ValidationResult("Please specify a rack!", vRack);
            }

            var vDraw = new[] { "Drawer" };
            if (BuffyAliquot1 != null && Drawer == null || EDTAPlasmaAliquot1 != null && Drawer == null ||
                LitHepPlasmaAliquot1 != null && Drawer == null || OxalatePlasmaAliquot1 != null && Drawer == null ||
                RBCAliquot1 != null && Drawer == null || SerumAliquot1 != null && Drawer == null ||
                UrineAliquot1 != null && Drawer == null || WBAliquot1 != null && Drawer == null)
            {
                yield return new ValidationResult("Please specify a drawer!", vDraw);
            }

            var vBox = new[] { "Box" };
            if (BuffyAliquot1 != null && Box == null || EDTAPlasmaAliquot1 != null && Box == null ||
                LitHepPlasmaAliquot1 != null && Box == null || OxalatePlasmaAliquot1 != null && Box == null ||
                RBCAliquot1 != null && Box == null || SerumAliquot1 != null && Box == null ||
                UrineAliquot1 != null && Box == null || WBAliquot1 != null && Box == null)
            {
                yield return new ValidationResult("Please specify a box!", vBox);
            }

            /* 
             * Missing Volumes 
             */
            var vBuffyVol = new[] { "BuffyAliquotVol1" };
            if (BuffyAliquot1 != null && BuffyAliquotVol1 == null || BuffyAliquot2 != null && BuffyAliquotVol2 == null || BuffyAliquot3 != null && BuffyAliquotVol3 == null)
            {
                yield return new ValidationResult("Please provide the missing volume!", vBuffyVol);
            }

            var vEDTAPlamaVol = new[] { "EDTAPlasmaAliquotVol1" };
            if (EDTAPlasmaAliquot1 != null && EDTAPlasmaAliquotVol1 == null || EDTAPlasmaAliquot2 != null && EDTAPlasmaAliquotVol2 == null || EDTAPlasmaAliquot3 != null && EDTAPlasmaAliquotVol3 == null)
            {
                yield return new ValidationResult("Please provide the missing volume!", vEDTAPlamaVol);
            }

            var vLitHepPlasmaVol = new[] { "LitHepPlasmaAliquotVol1" };
            if (LitHepPlasmaAliquot1 != null && LitHepPlasmaAliquotVol1 == null || LitHepPlasmaAliquot2 != null && LitHepPlasmaAliquotVol2 == null || 
                 LitHepPlasmaAliquot3 != null && LitHepPlasmaAliquotVol3 == null || LitHepPlasmaAliquot4 != null && LitHepPlasmaAliquotVol4 == null)
            {
                yield return new ValidationResult("Please provide the missing volume!", vLitHepPlasmaVol);
            }
                                                
            var vOxalatePlasmaVol = new[] { "OxalatePlasmaAliquotVol1" };
            if (OxalatePlasmaAliquot1 != null && OxalatePlasmaAliquotVol1 == null || OxalatePlasmaAliquot2 != null && OxalatePlasmaAliquotVol2 == null ||
                OxalatePlasmaAliquot3 != null && OxalatePlasmaAliquotVol3 == null || OxalatePlasmaAliquot4 != null && OxalatePlasmaAliquotVol4 == null)
            {
                yield return new ValidationResult("Please provide the missing volume!", vOxalatePlasmaVol);
            }

            var vRBCAliquotVol = new[] { "RBCAliquotVol1" };
            if (RBCAliquot1 != null && RBCAliquotVol1 == null || RBCAliquot2 != null && RBCAliquotVol2 == null || RBCAliquot3 != null && RBCAliquotVol3 == null)
            {
                yield return new ValidationResult("Please provide the missing volume!", vRBCAliquotVol);
            }

            var vSerumAliquotVol = new[] { "SerumAliquotVol1" };
            if (SerumAliquot1 != null && SerumAliquotVol1 == null || SerumAliquot2 != null && SerumAliquotVol2 == null ||
                SerumAliquot3 != null && SerumAliquotVol3 == null || SerumAliquot4 != null && SerumAliquotVol4 == null)
            {
                yield return new ValidationResult("Please provide the missing volume!", vSerumAliquotVol);
            }

            var vUrineAliquotVol = new[] { "UrineAliquotVol1" };
            if (UrineAliquot1 != null && UrineAliquotVol1 == null || UrineAliquot2 != null && UrineAliquotVol2 == null ||
                UrineAliquot3 != null && UrineAliquotVol3 == null || UrineAliquot4 != null && UrineAliquotVol4 == null ||
                UrineAliquot5 != null && UrineAliquotVol5 == null)
            {
                yield return new ValidationResult("Please provide the missing volume!", vUrineAliquotVol);
            }

            var vWBAliquotVol = new[] { "WBAliquotVol1" };
            if (WBAliquot1 != null && WBAliquotVol1 == null || WBAliquot2 != null && WBAliquotVol2 == null ||WBAliquot3 != null && WBAliquotVol3 == null)
            {
                yield return new ValidationResult("Please provide the missing volume!", vWBAliquotVol);
            }

            /* 
             * Missing Box Positions  
            */
            var vBuffyAliquotPosition = new[] { "BuffyAliquotPosition1" };
            if (BuffyAliquot1 != null && BuffyAliquotPosition1 == null || BuffyAliquot2 != null && BuffyAliquotPosition2 == null || BuffyAliquot3 != null && BuffyAliquotPosition3 == null)
            {
                yield return new ValidationResult("Please provide the missing box position!", vBuffyAliquotPosition);
            }

            var vEDTAPlasmaAliquotPosition = new[] { "EDTAPlasmaAliquotPosition1" };
            if (EDTAPlasmaAliquot1 != null && EDTAPlasmaAliquotPosition1 == null || EDTAPlasmaAliquot2 != null && EDTAPlasmaAliquotPosition2 == null || EDTAPlasmaAliquot3 != null && EDTAPlasmaAliquotPosition3 == null)
            {
                yield return new ValidationResult("Please provide the missing box position!", vEDTAPlasmaAliquotPosition);
            }

            var vLitHepPlasmaAliquotPosition = new[] { "LitHepPlasmaAliquotPosition1" };
            if (LitHepPlasmaAliquot1 != null && LitHepPlasmaAliquotPosition1 == null || LitHepPlasmaAliquot2 != null && LitHepPlasmaAliquotPosition2 == null ||
                LitHepPlasmaAliquot3 != null && LitHepPlasmaAliquotPosition3 == null || LitHepPlasmaAliquot4 != null && LitHepPlasmaAliquotPosition4 == null)
            {
                yield return new ValidationResult("Please provide the missing box position!", vLitHepPlasmaAliquotPosition);
            }
            var vOxalatePlasmaAliquotPosition = new[] { "OxalatePlasmaAliquotPosition1" };
            if (OxalatePlasmaAliquot1 != null && OxalatePlasmaAliquotPosition1 == null || OxalatePlasmaAliquot2 != null && OxalatePlasmaAliquotPosition2 == null ||
                OxalatePlasmaAliquot3 != null && OxalatePlasmaAliquotPosition3 == null || OxalatePlasmaAliquot4 != null && OxalatePlasmaAliquotPosition4 == null)
            {
                yield return new ValidationResult("Please provide the missing box position!", vOxalatePlasmaAliquotPosition);
            }

            var vRBCAliquotPosition = new[] { "RBCAliquotPosition1" };
            if (RBCAliquot1 != null && RBCAliquotPosition1 == null || RBCAliquot2 != null && RBCAliquotPosition2 == null || RBCAliquot3 != null && RBCAliquotPosition3 == null)
            {
                yield return new ValidationResult("Please provide the missing box position!", vRBCAliquotPosition);
            }

            var vSerumAliquotPosition = new[] { "SerumAliquotPosition1" };
            if (SerumAliquot1 != null && SerumAliquotPosition1 == null || SerumAliquot2 != null && SerumAliquotPosition2 == null ||
                SerumAliquot3 != null && SerumAliquotPosition3 == null || SerumAliquot4 != null && SerumAliquotPosition4 == null)
            {
                yield return new ValidationResult("Please provide the missing box position!", vSerumAliquotPosition);
            }

            var vUrineAliquotPosition = new[] { "UrineAliquotPosition1" };
            if (UrineAliquot1 != null && UrineAliquotPosition1 == null || UrineAliquot2 != null && UrineAliquotPosition2 == null ||
                UrineAliquot3 != null && UrineAliquotPosition3 == null || UrineAliquot4 != null && UrineAliquotPosition4 == null ||
                UrineAliquot5 != null && UrineAliquotPosition5 == null)
            {
                yield return new ValidationResult("Please provide the missing box position!", vUrineAliquotPosition);
            }

            var vWBAliquotPosition = new[] { "WBAliquotPosition1" };
            if (WBAliquot1 != null && WBAliquotPosition1 == null || WBAliquot2 != null && WBAliquotPosition2 == null || WBAliquot3 != null && WBAliquotPosition3 == null)
            {
                yield return new ValidationResult("Please provide the missing box position!", vWBAliquotPosition);
            }

            /*
             * Buccal Validation
            */

            var vBuccalBox = new[] { "BuccalBox" };
            if (BuccalBarcode != null && BuccalBox == null)
            {
                yield return new ValidationResult("Please provide box number!", vBuccalBox);
            }

            var vBuccalFreezer = new[] { "BuccalFreezer" };
            if (BuccalBarcode != null && BuccalFreezer == null)
            {
                yield return new ValidationResult("Please provide Freezer number!", vBuccalFreezer);
            }

            var vBuccalPosition = new[] { "BuccalPosition" };
            if (BuccalBarcode != null && BuccalPosition == null)
            {
                yield return new ValidationResult("Please provide box position!", vBuccalPosition);
            }

            var vBuccalBarcode = new[] { "BuccalBarcode" };
            if (BuccalProcessed == 1 && BuccalBarcode == null)
            {
                yield return new ValidationResult("Please scan in the buccal barcode, as a buccal is registered as collected with this blood sample!", vBuccalBarcode);
            }

            /*
             * Time and Date Validation
             */

            var vTimeFrozen = new[] { "TimeFrozen" };
            if (BuffyAliquot1 != null && TimeFrozen == null || BuffyAliquot2 != null && TimeFrozen == null || BuffyAliquot3 != null && TimeFrozen == null ||
                EDTAPlasmaAliquot1 != null && TimeFrozen == null || EDTAPlasmaAliquot2 != null && TimeFrozen == null ||
                EDTAPlasmaAliquot3 != null && TimeFrozen == null || LitHepPlasmaAliquot1 != null && TimeFrozen == null ||
                LitHepPlasmaAliquot2 != null && TimeFrozen == null || LitHepPlasmaAliquot3 != null && TimeFrozen == null ||
                LitHepPlasmaAliquot4 != null && TimeFrozen == null || OxalatePlasmaAliquot1 != null && TimeFrozen == null ||
                OxalatePlasmaAliquot2 != null && TimeFrozen == null || OxalatePlasmaAliquot3 != null && TimeFrozen == null ||
                OxalatePlasmaAliquot4 != null && TimeFrozen == null || RBCAliquot1 != null && TimeFrozen == null ||
                RBCAliquot2 != null && TimeFrozen == null || RBCAliquot3 != null && TimeFrozen == null ||
                SerumAliquot1 != null && TimeFrozen == null || SerumAliquot2 != null && TimeFrozen == null ||
                SerumAliquot3 != null && TimeFrozen == null || SerumAliquot4 != null && TimeFrozen == null ||
                UrineAliquot1 != null && TimeFrozen == null || UrineAliquot2 != null && TimeFrozen == null ||
                UrineAliquot3 != null && TimeFrozen == null || UrineAliquot4 != null && TimeFrozen == null ||
                UrineAliquot5 != null && TimeFrozen == null || WBAliquot1 != null && TimeFrozen == null ||
                WBAliquot2 != null && TimeFrozen == null || WBAliquot3 != null && TimeFrozen == null)
            {
                yield return new ValidationResult("Please provide the time frozen!", vTimeFrozen);
            }

            var vTimeProcessed = new[] { "TimeProcessed" };
            if (BuffyAliquot1 != null && TimeProcessed == null || BuffyAliquot2 != null && TimeProcessed == null || BuffyAliquot3 != null && TimeProcessed == null ||
                EDTAPlasmaAliquot1 != null && TimeProcessed == null || EDTAPlasmaAliquot2 != null && TimeProcessed == null ||
                EDTAPlasmaAliquot3 != null && TimeProcessed == null || LitHepPlasmaAliquot1 != null && TimeProcessed == null ||
                LitHepPlasmaAliquot2 != null && TimeProcessed == null || LitHepPlasmaAliquot3 != null && TimeProcessed == null ||
                LitHepPlasmaAliquot4 != null && TimeProcessed == null || OxalatePlasmaAliquot1 != null && TimeProcessed == null ||
                OxalatePlasmaAliquot2 != null && TimeProcessed == null || OxalatePlasmaAliquot3 != null && TimeProcessed == null ||
                OxalatePlasmaAliquot4 != null && TimeProcessed == null || RBCAliquot1 != null && TimeProcessed == null ||
                RBCAliquot2 != null && TimeProcessed == null || RBCAliquot3 != null && TimeProcessed == null ||
                SerumAliquot1 != null && TimeProcessed == null || SerumAliquot2 != null && TimeProcessed == null ||
                SerumAliquot3 != null && TimeProcessed == null || SerumAliquot4 != null && TimeProcessed == null ||
                WBAliquot1 != null && TimeProcessed == null ||
                WBAliquot2 != null && TimeProcessed == null || WBAliquot3 != null && TimeProcessed == null)
            {
                yield return new ValidationResult("Please provide the time processed!", vTimeProcessed);
            }

            var vDateProcessed = new[] { "DateProcessed" };
            if (BuffyAliquot1 != null && DateProcessed == null || BuffyAliquot2 != null && DateProcessed == null || BuffyAliquot3 != null && DateProcessed == null ||
                EDTAPlasmaAliquot1 != null && DateProcessed == null || EDTAPlasmaAliquot2 != null && DateProcessed == null ||
                EDTAPlasmaAliquot3 != null && DateProcessed == null || LitHepPlasmaAliquot1 != null && DateProcessed == null ||
                LitHepPlasmaAliquot2 != null && DateProcessed == null || LitHepPlasmaAliquot3 != null && DateProcessed == null ||
                LitHepPlasmaAliquot4 != null && DateProcessed == null || OxalatePlasmaAliquot1 != null && DateProcessed == null ||
                OxalatePlasmaAliquot2 != null && DateProcessed == null || OxalatePlasmaAliquot3 != null && DateProcessed == null ||
                OxalatePlasmaAliquot4 != null && DateProcessed == null || RBCAliquot1 != null && DateProcessed == null ||
                RBCAliquot2 != null && DateProcessed == null || RBCAliquot3 != null && DateProcessed == null ||
                SerumAliquot1 != null && DateProcessed == null || SerumAliquot2 != null && DateProcessed == null ||
                SerumAliquot3 != null && DateProcessed == null || SerumAliquot4 != null && DateProcessed == null ||
                WBAliquot1 != null && DateProcessed == null || WBAliquot2 != null && DateProcessed == null || 
                WBAliquot3 != null && DateProcessed == null)
            {
                yield return new ValidationResult("Please provide the date processed!", vDateProcessed);
            }

            var vBloodBarcode = new[] { "BloodBarcode" };
            if (EDTACollected == true && BloodBarcode == null ||
                EDTANumber != null && BloodBarcode == null ||
                GelCollected == true && BloodBarcode == null ||
                GelNumber != null && BloodBarcode == null ||
                LitHepCollected == true && BloodBarcode == null ||
                LitHepNumber != null && BloodBarcode == null ||
                OxalateCollected == true && BloodBarcode == null ||
                FastingTime != null && BloodBarcode == null ||
                FastingDate !=null  && BloodBarcode == null)
            {
                yield return new ValidationResult("Please provide barcode to register the blood sample!", vBloodBarcode);
            }

            var vCollectedDate = new[] { "DateOfCollection" };
            if (BuccalCollected == true && DateOfCollection == null ||
                EDTACollected == true && DateOfCollection == null ||
                GelCollected == true && DateOfCollection == null ||
                LitHepCollected == true && DateOfCollection == null ||
                OxalateCollected == true && DateOfCollection == null ||
                UrineCollected == "Yes" && DateOfCollection == null)
            {
                yield return new ValidationResult("Please provide a date of collection!", vCollectedDate);
            }

            var vCollectedTime = new[] { "TimeOfCollection" };
            if (BuccalCollected == true && TimeOfCollection == null ||
                EDTACollected == true && TimeOfCollection == null ||
                GelCollected == true && TimeOfCollection == null ||
                LitHepCollected == true && TimeOfCollection == null ||
                OxalateCollected == true && TimeOfCollection == null ||
                UrineCollected == "Yes" && TimeOfCollection == null)
            {
                yield return new ValidationResult("Please provide a time of collection!", vCollectedTime);
            }

            var vPSCID = new[] { "PSCID" };
            if (BuccalCollected == true && PSCID == null ||
                EDTACollected == true && PSCID == null ||
                GelCollected == true && PSCID == null ||
                LitHepCollected == true && PSCID == null ||
                OxalateCollected == true && PSCID == null)
            {
                yield return new ValidationResult("Please provide the missing barcode label!", vPSCID);
            }

        }

    }

}