using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;
using System.Web;
using System.Web.Mvc; //Needed for the selected item in an instance of the SelectList class

namespace BioSys.Models
{
    public class StudyPersonSample
    {

        public string StudyID { get; set; }

        public SelectList StudyList { get; set; }
        [Display(Name = "StudyName")]
        public string StudyName { get; set; }

        [Required]
        [Display(Name = "PSCID")]
        [MaxLength(7)]
        public string PSCID { get; set; }

        public string NHSNumber { get; set; }
        public string HospitalNumber { get; set; }

        public SelectList ParticipantList { get; set; }
        [Display(Name = "ParticipantType")]
        public string ParticipantType { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Sex { get; set; }
        public string StudyPersonID { get; set; }
        public string PersonSnippet { get; set; }
        public string PersonResult { get; set; }

        [Display(Name = "SampleFormList")]
        //public SelectList FormList { get; set; }
        public IList<SelectListItem> FormName { get; set; }
        public string DataEntryFormName { get; set; }
        public string FormIDBarcode { get; set; }

    }

    public class StudyPerson
    {
        internal string PersonResult;

        public string PSCID { get; set; }
        public string StudyID { get; set; }
        public IEnumerable<SelectListItem> Study { get; set; }
        public IEnumerable<SelectListItem> StudyIdentifier { get; set; }
        public List<SelectListItem> Studies { get; set; }
        public SelectList StudyList { get; set; }
        [Display(Name = "Study Name")]
        public string StudyName { get; set; }
        public string NHSNumber { get; set; }
        public string HospitalNumber { get; set; }

        //public IList<SelectListItem> ParticipantType { get; set; }
        //public SelectList ParticipantList { get; set; }
        //[Display(Name = "Participant Type")]
        //public IList<SelectListItem> ParticipantType { get; set; }
        [Display(Name = "Participant Type")]
        public string ParticipantType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:DD/MM/YYYY}", ApplyFormatInEditMode = true)]
        //public DateTime DateOfBirth { get; set; }
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        public SelectList GenderList { get; set; }  
        public string Sex { get; set; }
        [Display(Name = "Study Person ID")]
        public string StudyPersonID { get; set; }
        public string PersonExists { get; internal set; }
        public string Message { get; set; }
    }

    public class StudyForm
    {
        public string FormID { get; set; }
        public string ViewName { get; set; }
    }

    public class PersonExists
    {
        public string StudyPersonExists { get; set; }
    }

    public class AddStudyPerson
    {
        public string PSCID { get; set; }
        public string StudyID { get; set; }
        public SelectList StudyList { get; set; }
        //[Display(Name = "StudyName")]
        public string StudyName { get; set; }
        public string NHSNumber { get; set; }
        public string HospitalNumber { get; set; }
        public string wasRedirected { get; set; }

        //public IList<SelectListItem> ParticipantType { get; set; }
        //public SelectList ParticipantList { get; set; }
        //[Display(Name = "Participant Type")]
        //public IList<SelectListItem> ParticipantType { get; set; }
        public string ParticipantType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:DD/MM/YYYY}", ApplyFormatInEditMode = true)]
        //public DateTime DateOfBirth { get; set; }
        public string DateOfBirth { get; set; }

        //public sexType Sex {get; set; }  // i.e. enum sexType
        //public string Sex { get; set; }
        public SelectList GenderList { get; set; }
        public string Sex { get; set; }
        [Display(Name = "Study Person ID")]
        public string StudyPersonID { get; set; }
        public string Message { get; set; }
    }


    public enum yesNo
    {
        [Display(Name = @"-- Select --")]
        Select = 0,
        [Display(Name = @"Yes")]
        Yes = 1,
        [Display(Name = @"No")]
        No = 2
    }

    public enum sexType
    {
        [Display(Name = @"-- Select --")]
        Select = 0,
        [Display(Name = @"Female")]
        Female = 1,
        [Display(Name = @"Male")]
        Male = 2
    }

    public class AddStudySample
    {
        public string PSCID { get; set; }
        public string PSCIDBarcode { get; set; }
        public string LabBarcode { get; set; }
        public string StudyID { get; set; }
        public string StudyName { get; set; }
        public string SampleBarcode { get; set; }
        public string SalivaBarcode { get; set; }
        public string CordBloodBarcode { get; set; }
        public string BuccalBarcode { get; set; }
        public string SampleSetName { get; set; }

        public string SampleType { get; set; }

        public string notes { get; set; }

        public string Fasting { get; set; }

        public string BloodCollected { get; set; }
        public string CordBloodCollected { get; set; }
        public string CordBloodEDTACollected { get; set; }
        public string CordBloodSerumCollected { get; set; }
        public string GelCollected { get; set; }
        public string GelNumber { get; set; }
        public string EDTACollected { get; set; }
        public string EDTANumber { get; set; }
        public string LitHepCollected { get; set; }
        public string LitHepNumber { get; set; }
        public string OxalateCollected { get; set; }
        public string OxalateLastDate { get; set; }
        public string OxalateLastTime { get; set; }
        public string BuccalCollected { get; set; }
        public string BuccalNumber { get; set; }
        public string Serum1Collected { get; set; }
        public string Serum2Collected { get; set; }
        public string SalivaCollected { get; set; }
        public string UrineCollected { get; set; }
        public string SampleCollectedYes { get; set; }
        public string SampleCollectedNo { get; set; }
        public string HairCollected { get; set; }

        //public IList<SelectListItem> ConsentGenetics { get; set; }
        public yesNo YesNo { get; set; }
        public String ConsentGenetics { get; set; }
        public string ConsentNonGenetic { get; set; }
        public string ddlRenalStudyYN { get; set; }

        /**************** DATE & TIME ********************/
        public string DateBuccalProcessed { get; set; }
        public string DateFrozen { get; set; }
        public string DateOfCollection { get; set; }
        public string DateProcessed { get; set; }

        public string TimeBuccalFrozen { get; set; }
        public string TimeBuccalProcessed { get; set; }
        public string TimeFrozen { get; set; }
        public string TimeOfCollection { get; set; }
        public string TimeProcessed { get; set; }


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
        public string BuffyDraw { get; set; }
        public string Drawer { get; set; }
        public string EDTAPlasmaDraw { get; set; }
        public string LitHepPlasmaDraw { get; set; }
        public string OxalatePlasmaDraw { get; set; }
        public string RBCDraw { get; set; }
        public string SerumAliquotDraw { get; set; }
        public string UrineDraw { get; set; }
        public string WBDraw { get; set; }

        /**************** BOX ********************/
        public string Box { get; set; }
        public string BuccalBox { get; set; }
        public string BuffyBox { get; set; }
        public string EDTAPlasmaBox { get; set; }
        public string LitHepPlasmaBox { get; set; }
        public string OxalatePlasmaBox { get; set; }
        public string RBCBox { get; set; }
        public string SerumAliquotBox { get; set; }
        public string UrineBox { get; set; }
        public string WBBox { get; set; }


        /**************** POSIITION ********************/

        public string BuccalPosition { get; set; }

        public string BuffyAliquot1Position { get; set; }
        public string BuffyAliquot2Position { get; set; }
        public string BuffyAliquot3Position { get; set; }

        public string EDTAPlasmaAliquot1Position { get; set; }
        public string EDTAPlasmaAliquot2Position { get; set; }
        public string EDTAPlasmaAliquot3Position { get; set; }

        public string LitHepPlasmaAliquot1Position { get; set; }
        public string LitHepPlasmaAliquot2Position { get; set; }
        public string LitHepPlasmaAliquot3Position { get; set; }
        public string LitHepPlasmaAliquot4Position { get; set; }

        public string OxalatePlasmaAliquot1Position { get; set; }
        public string OxalatePlasmaAliquot2Position { get; set; }
        public string OxalatePlasmaAliquot3Position { get; set; }
        public string OxalatePlasmaAliquot4Position { get; set; }

        public string RBCAliquot1Position { get; set; }
        public string RBCAliquot2Position { get; set; }
        public string RBCAliquot3Position { get; set; }

        public string SerumAliquot1Position { get; set; }
        public string SerumAliquot2Position { get; set; }
        public string SerumAliquot3Position { get; set; }
        public string SerumAliquot4Position { get; set; }

        public string UrineAliquot1Position { get; set; }
        public string UrineAliquot2Position { get; set; }
        public string UrineAliquot3Position { get; set; }
        public string UrineAliquot4Position { get; set; }
        public string UrineAliquot5Position { get; set; }

        public string WBAliquot1Position { get; set; }
        public string WBAliquot2Position { get; set; }
        public string WBAliquot3Position { get; set; }


        /**************** ALIQUOTS ********************/

        public string BuffyAliquot1 { get; set; }
        public string BuffyAliquot2 { get; set; }
        public string BuffyAliquot3 { get; set; }

        public string EDTAPlasmaAliquot1 { get; set; }
        public string EDTAPlasmaAliquot2 { get; set; }
        public string EDTAPlasmaAliquot3 { get; set; }

        public string LitHepPlasmaAliquot1 { get; set; }
        public string LitHepPlasmaAliquot2 { get; set; }
        public string LitHepPlasmaAliquot3 { get; set; }
        public string LitHepPlasmaAliquot4 { get; set; }

        public string OxalatePlasmaAliquot1 { get; set; }
        public string OxalatePlasmaAliquot2 { get; set; }
        public string OxalatePlasmaAliquot3 { get; set; }
        public string OxalatePlasmaAliquot4 { get; set; }
        public string RBCAliquot1 { get; set; }
        public string RBCAliquot2 { get; set; }
        public string RBCAliquot3 { get; set; }

        public string SerumAliquot1 { get; set; }
        public string SerumAliquot2 { get; set; }
        public string SerumAliquot3 { get; set; }
        public string SerumAliquot4 { get; set; }

        public string UrineAliquot1 { get; set; }
        public string UrineAliquot2 { get; set; }
        public string UrineAliquot3 { get; set; }
        public string UrineAliquot4 { get; set; }
        public string UrineAliquot5 { get; set; }

        public string WBAliquot1 { get; set; }
        public string WBAliquot2 { get; set; }
        public string WBAliquot3 { get; set; }


        /**************** VOLUME *******************/

        public string UrineAliquot1Vol { get; set; }
        public string UrineAliquot2Vol { get; set; }
        public string UrineAliquot3Vol { get; set; }
        public string UrineAliquot4Vol { get; set; }
        public string UrineAliquot5Vol { get; set; }

    }
}