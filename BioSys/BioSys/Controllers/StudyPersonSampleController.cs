using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioSys.Models;

namespace BioSys.Controllers
{
    public class StudyPersonSampleController : Controller
    {
        // GET: StudyPersonSample
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam ="None")]
        public ActionResult Index(string wasRedirected, string Message)
        {
            try
            {
                if(wasRedirected == "True")
                {
                    ViewBag.Message = Message;
                }
                StudyList_Bind();  //Supply look-up list to drop down box for study name
                //FormList_Bind();   //Supply look-up list to drop down box DataEntryFormName
                return View();
            }
            catch(Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "StudyPersonSampleModel", "Index"));
            }

        }


        // GET: StudyPersonSample/Details/5
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudyPersonSample/Create
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudyPersonSample/Create
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        //public ActionResult Create(FormCollection collection)
        public ActionResult Create(CRUDStudyPerson spmodel)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudyPersonSample/Edit/5
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudyPersonSample/Edit/5
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudyPersonSample/Delete/5
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudyPersonSample/Delete/5
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void StudyList_Bind()
        {
            CRUDModels cm = new Models.CRUDModels();

            DataSet ds = cm.GetStudyList();
            List<SelectListItem> studyList = new List<SelectListItem>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                studyList.Add(new SelectListItem { Text = dr["StudyName"].ToString(), Value = dr["StudyID"].ToString() });
            }

            ViewBag.StudyID = studyList;
            //ViewBag.StudyName = studyList;
            //ViewData["Studies"] = studyList;
        }

        public void StudyListToStudyID_Bind()
        {
            CRUDModels cm = new Models.CRUDModels();

            DataSet ds = cm.GetStudyList();
            List<SelectListItem> studyList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                studyList.Add(new SelectListItem { Text = dr["StudyName"].ToString(), Value = dr["StudyID"].ToString() });
            }

            ViewBag.StudyIdentifier = studyList;
        }

        public void GenderList_Bind()
        {
            CRUDStudyPerson cm = new Models.CRUDStudyPerson();

            DataSet ds = cm.GetGenderList();
            List<SelectListItem> genderList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                genderList.Add(new SelectListItem { Text = dr["Label"].ToString(), Value = dr["TextValue"].ToString() });
            }

            ViewBag.Sex = genderList;
        }

        /*** --- CASCADED DROP DOWN LIST FOR LAB FORMS --- ***/ 
        public ActionResult GetFormList(string studyId)
        {
            CRUDModels cm = new Models.CRUDModels();
            DataSet ds = cm.GetFormList(studyId);
            List<SelectListItem> formNames = new List<SelectListItem>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                //formNames.Add(new SelectListItem { Text = dr["FormName"].ToString(), Value = dr["FormID"].ToString() });
                formNames.Add(new SelectListItem { Text = dr["FormName"].ToString(), Value = dr["ViewName"].ToString() });
            }

            return Json(formNames, JsonRequestBehavior.AllowGet);            
        }

        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult GetStudyByFormID(string FormID)
        {
            //Gets the StudyID that relates to a Form Barcode ID 

            CRUDStudySamples cm = new CRUDStudySamples();
            string StudyID = cm.GetStudyByFormID(FormID);
            //return Json(StudyID, JsonRequestBehavior.AllowGet);
            return Json(StudyID);
        }

        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult GetStudyFormByID(string formBarcode, string pscId, string studyId)
        {

            //Check if PSCID is registered on Biobank system

            //
            CRUDStudyPerson csp = new CRUDStudyPerson();
            int Exists = csp.ExistsPersonByPSCIDAndStudy(pscId, studyId);
            if (Exists == 1)
            {
                CRUDModels cm = new CRUDModels();
                DataSet ds = cm.GetStudyFormByID(formBarcode);
                List<StudyForm> form = new List<StudyForm>();
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    form.Add(new StudyForm
                    {
                        FormID = dr["FormID"].ToString(),
                        ViewName = dr["ViewName"].ToString()
                    });
                }

                return Json(form, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Person not on database, then return view AddStudyPerson form

                List<StudyForm> form = new List<StudyForm>();
                form.Add(new StudyForm
                {
                    FormID = "",
                    ViewName = "AddStudyPerson"
                    
                });

                return Json(form, JsonRequestBehavior.AllowGet);
            }



        }

        public void GetParticipantTypeList_Bind()
        {
            CRUDModels cm = new Models.CRUDModels();
            DataSet ds = cm.GetParticipantTypeList();
            List<SelectListItem> participantTypeList = new List<SelectListItem>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                participantTypeList.Add(new SelectListItem { Text = dr["ParticipantType"].ToString(), Value = dr["ParticipantType"].ToString() });
            }

            ViewBag.ParticipantType = participantTypeList;

        }
        
        public ActionResult GetPersonByPSCID(string pscId, string studyId)
        {
            CRUDModels cm = new CRUDModels();
            DataSet ds = cm.GetPersonByPSCID(pscId, studyId);
            //List<StudyPersonSample> person = new List<StudyPersonSample>();
            List<StudyPerson> person = new List<StudyPerson>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                person.Add(new StudyPerson
                {
                    PSCID = dr["PSCID"].ToString(),
                    StudyID = dr["StudyID"].ToString(),
                    PersonResult = dr["PersonResult"].ToString(),
                    PersonExists = dr["PersonExists"].ToString()
                });
            }

            return Json(person, JsonRequestBehavior.AllowGet);
        }



        //GET: AddStudyPerson
        public ActionResult AddStudyPerson(string PSCID, string StudyID, string StudyName, string wasRedirected)
        {
            ViewBag.PSCID = PSCID;
            ViewBag.StudyID = StudyID;
            ViewBag.StudyName = StudyName;            
            StudyList_Bind();  //Supply look-up list to drop down box for study name
            GetParticipantTypeList_Bind(); //supply participant type look up list
            GenderList_Bind();
            if (wasRedirected == "True")
            {
                ViewBag.Message = "Person with the ID " + PSCID + " is not registered for the " + StudyName + " study.  Please add them.  Thank you.";
            }
            return View();
        }

        //POST: AddStudyPerson 
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult AddStudyPerson(AddStudyPerson smodel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    CRUDStudyPerson cm = new CRUDStudyPerson();
                    if(cm.AddStudyPerson(smodel))
                    {
                        ViewBag.Message = "Study Participant Added Successfully";
                        ModelState.Clear();                        
                    }


                }

                //ViewBag.PSCID = smodel.PSCID;
                //ViewBag.StudyID = smodel.StudyID;
                //ViewBag.Message = smodel.Message;
                //return View("EditStudyPerson");

                return Json(new
                {
                    redirecturl = Url.Action("EditStudyPerson", "StudyPersonSample", new { PSCID = smodel.PSCID, StudyID = smodel.StudyID, Message = smodel.Message })
                    //redirecturl = "/BioSys/StudyPersonSample/EditStudyPerson?PSCID=" + smodel.PSCID + "&StudyID=" + smodel.StudyID + "&Message=" + smodel.Message
                });
                //},
                //                JsonRequestBehavior.AllowGet);



                //return Json(new { redirecturl = "/StudyPersonSample/EditStudyPerson?PSCID=" + smodel.PSCID + "&StudyID=" + smodel.StudyID }, JsonRequestBehavior.AllowGet);
                //return View();
                //return RedirectToAction("EditStudyPerson", new
                //{
                //    PSCID = smodel.PSCID,
                //    StudyID = smodel.StudyID
                //}
                //                        );
                //return Redirect("/Home/Index");
                //return View("Index");
            }
            catch(Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "StudyPersonSample", "AddStudyPerson")); //create error view
            }
        }

        //Once Study Person is added return their record on the Study Person Form
        [HttpPost]
        public ActionResult StudyPersonForm(string PSCID, string StudyID)
        {
            CRUDStudyPerson crud = new CRUDStudyPerson();
            ModelState.Clear();
            return View(crud.GetStudyPerson(PSCID, StudyID));
        }


        //GET: EditStudyPerson
        public ActionResult EditStudyPerson(string PSCID, string StudyID, string Message)
        {
            CRUDStudyPerson crud = new CRUDStudyPerson();
            ViewBag.PSCID = PSCID;
            ViewBag.StudyID = StudyID;
            StudyList_Bind();  //Supply look-up list to drop down box for study name
            GetParticipantTypeList_Bind(); //supply participant type look up list     
            GenderList_Bind();
            if (Message != "")
            {
                ViewBag.Message = Message;
            }
            //return View(crud.GetStudyPerson(PSCID,StudyID));
            return View(crud.GetStudyPersonList(PSCID,StudyID).Find(smodel => (smodel.PSCID == PSCID) && (smodel.StudyID == StudyID)));            
        }

        //POST: EditStudyPerson 
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult EditStudyPerson(StudyPerson smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CRUDStudyPerson cm = new CRUDStudyPerson();
                    if (cm.UpdateStudyPerson(smodel))
                    {
                        ViewBag.Message = "Thank you. Details for study participant " + smodel.PSCID + "(PSCID) have been updated successfully.";
                        ModelState.Clear();
                    }
                }
                //ViewBag.Message = "Study Participant Added Successfully";
                //return RedirectToAction("StudyPersonForm", new {PSCID = smodel.PSCID, StudyID = smodel.StudyID });
                //return View();

                CRUDStudyPerson crud = new CRUDStudyPerson();
                StudyList_Bind();  //Supply look-up list to drop down box for study name
                GetParticipantTypeList_Bind(); //supply participant type look up list     
                GenderList_Bind(); //Supply gender type look up list
                //return View("EditStudyPerson");
                var PSCID = smodel.PSCID;
                var StudyID = smodel.StudyID;
                //ViewBag.StudyID = smodel.StudyID;
                return View(crud.GetStudyPersonList(PSCID, StudyID).Find(model => (smodel.PSCID == PSCID) && (smodel.StudyID == StudyID)));

            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "StudyPersonSample", "EditStudyPersonStudyPerson")); //create error view
            }
        }

        //POST AddUrineSample_GU
        [Authorize(Roles ="SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult AddUrineSample_GU(StudySamples smodel) //In StudySampleHandling.cs
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CRUDStudySamples cm = new CRUDStudySamples();
                    if (cm.AddUrineSamples(smodel))
                    {
                        ViewBag.Message = "Urine Sample Processing Update completed!";
                        //Amend: 
                        //ViewBag.Message = xml;  //in test mode, return the xml string to view it
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "StudyPersonSample", "AddUrineSample_GU")); //create error view
            }
        }


        //GET AddUrineSample
        public ActionResult AddForm(string PSCID, string StudyID, string ViewName, string StudyName, string SampleProcessing)
        {
            if (ViewName != "AddStudyPerson")
            {
                ViewBag.PSCID = PSCID;
                ViewBag.PSCIDBarcode = PSCID;
                ViewBag.StudyID = StudyID;
                ViewBag.StudyName = StudyName;
                ViewBag.SampleProcessing = SampleProcessing;
                return View(ViewName);
            }else
            {
                string wasRedirected;
                wasRedirected = "True";
                return RedirectToAction("AddStudyPerson", new { PSCID = PSCID, StudyID = StudyID, StudyName = StudyName, wasRedirected = wasRedirected });
            }

        }

        //POST: AddUrineSample 
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult AddForm(AddStudySample smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CRUDStudySample cm = new CRUDStudySample();
                    if (cm.AddStudySample(smodel))
                    {
                        ViewBag.Message = "Collection details of study sample added successfully";
                        ModelState.Clear();
                    }
                }

                return View();
                //return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "StudyPersonSample", "AddUrineSample")); //create error view
            }
        }


        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        //public ActionResult CollectSample(CollectSample smodel)  //will need to change this to accept xml construct if it can be done in jQuery
        public ActionResult CollectSample(StudySamples smodel)
        {

            if (ModelState.IsValid)
            {            
                CRUDStudySamples cm = new CRUDStudySamples();

                //int status;
                //int sampleID;
                int status = cm.CollectSample(smodel);     //WILL NEED TO BE XML IF IT WORKS!  Try XMLWriter
                                                           //sampleID = cm.CollectSample(smodel.result[2]);      
                                                           //status = cm.CollectSample(smodel.result[1]);
                                                           //sampleID = cm.CollectSample(smodel.result[2]);
                if (status == 0)
                {
                    ViewBag.Message = "This sample is already collected and aliquoted. Please add another sample to action.";
                    return View(smodel.ViewName);
                }
                else if (status == 999)
                {
                    ViewBag.ViewName = smodel.ViewName;
                    ViewBag.SampleProcessing = "0"; //THIS ENSURES THAT ONLY THE SAMPLE COLLECTION CONTROLS AND HTML DISPLAY
                    ViewBag.PSCID = smodel.PSCID;
                    ViewBag.StudyName = smodel.StudyName;
                    ViewBag.StudyID = smodel.StudyID;
                    //ViewBag.UrineCollected = smodel.UrineCollected;
                    //ViewBag.DateOfCollection = smodel.DateOfCollection;
                    //ViewBag.TimeOfCollection = smodel.TimeOfCollection;                
                    ViewBag.Message = "Collection details for this sample could not be saved at this time. Please contact your system's administrator";
                    return View(smodel.ViewName);
                }
                else
                {
                    ViewBag.SampleProcessing = "1";  //THIS ENSURES THAT ONLY SAMPLE PROCESSING CONTROLS AND HTML DISPLAYS
                    ViewBag.PSCID = smodel.PSCID;
                    ViewBag.StudyID = smodel.StudyID;
                    ViewBag.StudyName = smodel.StudyName;
                    ViewBag.ViewName = smodel.ViewName;
                    //if ((smodel.SampleType == "Urine") || (smodel.SampleType == "Buccal"))

                    //TODO: MIGHT NEED TO DO A CALL TO SAMPLES PROCESSED PROCEDURE TO GET SAMPLES PROCESSED AS AN INTERMEDIARY VIEW BETWEEEN COLLECTION AND PROCESSING - FOR VERSION (2)???
                    if (smodel.SampleSetType == "Bloods")
                    {
                        if(smodel.EDTASampleID > 0)
                        {
                            smodel.EDTAProcessed = 1;
                        }
                        else
                        {
                            smodel.EDTAProcessed = 0;
                        }
                        if (smodel.GelSampleID > 0)
                        {
                            smodel.GelSerumProcessed = 1;
                        }
                        else
                        {
                            smodel.GelSerumProcessed = 0;
                        }
                        if (smodel.LitHepSampleID > 0)
                        {
                            smodel.LitHepProcessed = 1;
                        }
                        else
                        {
                            smodel.LitHepProcessed = 0;
                        }
                        if (smodel.OxalateSampleID > 0)
                        {
                            smodel.OxalateProcessed = 1;
                        }
                        else
                        {
                            smodel.OxalateProcessed = 0;
                        }
                        if (smodel.BuccalSampleID > 0)
                        {
                            smodel.BuccalProcessed = 1;
                        }
                        else
                        {
                            smodel.BuccalProcessed = 0;
                        }
                        ViewBag.SampleID = smodel.CollectedSampleID; // This is the collective bloods sample ID to which the collected samples are related and need to be linked - not needed for Growing Up at the processing stage but this may be so in another context
                        ViewBag.EDTASampleID = smodel.EDTASampleID;
                        ViewBag.EDTAProcessed = smodel.EDTAProcessed;
                        ViewBag.GelSampleID = smodel.GelSampleID;
                        ViewBag.GelSerumProcessed = smodel.GelSerumProcessed;
                        ViewBag.LitHepSampleID = smodel.LitHepSampleID;
                        ViewBag.LitHepProcessed = smodel.LitHepProcessed;
                        ViewBag.OxalateSampleID = smodel.OxalateSampleID;
                        ViewBag.OxalateProcessed = smodel.OxalateProcessed;
                        ViewBag.BuccalSampleID = smodel.BuccalSampleID;
                        ViewBag.BuccalProcessed = smodel.BuccalProcessed;
                    }
                    else
                    {
                        ViewBag.SampleID = smodel.CollectedSampleID;  //This is the urine sample ID from which aliquots may be derived
                    }
                             
                    ViewBag.Message = "Result: The " + smodel.SampleSetName + " has been collected for " + smodel.PSCID  + " and may now be processed.";
                    return View(smodel.ViewName);

                }
                    //return Json(ds, JsonRequestBehavior.AllowGet);
                    //return null;// temporary placeholder return action
            }
            //if ModelState NOT IsValid!
            ViewBag.ViewName = smodel.ViewName;
            ViewBag.SampleProcessing = "0"; //THIS ENSURES THAT ONLY THE SAMPLE COLLECTION CONTROLS AND HTML DISPLAY
            ViewBag.PSCID = smodel.PSCID;
            ViewBag.StudyName = smodel.StudyName;
            ViewBag.StudyID = smodel.StudyID;          
           
            return View(smodel.ViewName);
        }

        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult ProcessSample (StudySamples smodel)
        {

            if (ModelState.IsValid)
            {

                CRUDStudySamples cm = new CRUDStudySamples();
                int status = cm.ProcessSample(smodel);

                if (status == 0)
                {
                    ViewBag.Message = "Sample processing information was not saved. Please try again. If the problem persists contact the data team.";
                    return null;
                }
                else if (status == 999)
                {
                    if (smodel.Message != "")
                    {
                        ViewBag.Message = smodel.Message;

                    }
                    else
                    {
                        ViewBag.Message = "The system was not able to save your data. Please contact the data team.";
                    }
                    ViewBag.SampleProcessing = "1"; 
                    ViewBag.PSCID = smodel.PSCID;
                    ViewBag.StudyName = smodel.StudyName;
                    ViewBag.StudyID = smodel.StudyID;
                    return View(smodel.ViewName);
                }
                else
                {
                    //TODO: smodel.StudyName is not populating - need to bring this over from the model!
                    ViewBag.Message = "The " + smodel.SampleSetType + " sample processing data for " + smodel.PSCID + " on the " + smodel.StudyName + " study has been saved.";                
                }

                string wasRedirected;
                wasRedirected = "True";
                    //return View ("Index"); //Return to the /StudyPersonSample/Index view so further samples can be added.
                return RedirectToAction("Index", new { Message = ViewBag.Message, wasRedirected = wasRedirected });
                //return RedirectToAction("actionresult name", new { PSCID = PSCID, StudyID = StudyID, StudyName = StudyName, wasRedirected = wasRedirected});
                
            }

            ViewBag.SampleProcessing = "1"; 
            ViewBag.PSCID = smodel.PSCID;
            ViewBag.StudyName = smodel.StudyName;
            ViewBag.StudyID = smodel.StudyID;

            if (smodel.CollectedSampleID > 0)
            {
                ViewBag.SampleID = smodel.CollectedSampleID;
            }
             // This is the collective bloods sample ID to which the collected samples are related and need to be linked - not needed for Growing Up at the processing stage but this may be so in another context
            if (smodel.EDTASampleID > 0)
            {
                ViewBag.EDTASampleID = smodel.EDTASampleID;
            }
            
            if (smodel.EDTAProcessed > 0)
            {
                ViewBag.EDTAProcessed = smodel.EDTAProcessed;
            }
            
            if (smodel.GelSampleID > 0)
            {
                ViewBag.GelSampleID = smodel.GelSampleID;
            }

            if (smodel.GelSerumProcessed > 0)
            {
                ViewBag.GelSerumProcessed = smodel.GelSerumProcessed;
            }

            if (smodel.LitHepSampleID > 0)
            {
                ViewBag.LitHepSampleID = smodel.LitHepSampleID;
            }
            
            if (smodel.LitHepProcessed > 0)
            {
                ViewBag.LitHepProcessed = smodel.LitHepProcessed;
            }
            
            if (smodel.OxalateSampleID > 0)
            {
                ViewBag.OxalateSampleID = smodel.OxalateSampleID;
            }

            if (smodel.OxalateProcessed > 0)
            {
                ViewBag.OxalateProcessed = smodel.OxalateProcessed;
            }
            if (smodel.BuccalSampleID > 0)
            {
                ViewBag.BuccalSampleID = smodel.BuccalSampleID;
            }

            if (smodel.BuccalProcessed > 0)
            {
                ViewBag.BuccalProcessed = smodel.BuccalProcessed;
            }

            return View(smodel.ViewName);
        }


        //GET AddUrineSample
        public ActionResult AddUrineSample(string PSCID, string StudyID, string ViewName)
        {
            ViewBag.PSCID = PSCID;
            ViewBag.StudyID = StudyID;
            return View(ViewName);
        }

        //POST: AddUrineSample 
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult AddUrineSample(AddStudySample smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CRUDStudySample cm = new CRUDStudySample();

                    if (cm.AddStudySample(smodel))
                    {
                        ViewBag.Message = "Collection details of study sample added successfully";
                        ModelState.Clear();
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "StudyPersonSample", "AddUrineSample")); //create error view
            }
        }


        //GET AddStudySample
        public ActionResult AddBloodSample(string PSCID, string StudyID, string ViewName)
        {
            ViewBag.PSCID = PSCID;
            ViewBag.StudyID = StudyID;
            return View(ViewName);
        }

        //POST: AddStudySample 
        [Authorize(Roles = "SuperAdmin, BioSysUser")]
        [HttpPost]
        public ActionResult AddBloodSample(AddStudySample smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CRUDStudySample cm = new CRUDStudySample();
                    if (cm.AddStudySample(smodel))
                    {
                        ViewBag.Message = "Collection details of study sample added successfully";
                        ModelState.Clear();
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "StudyPersonSample", "AddBloodSample")); //create error view
            }
        }

        //public ActionResult ExistsParticipantByPSCID(string pscId)
        //{
        //    CRUDModels cm = new Models.CRUDModels();
        //    DataSet ds = cm.ExistsParticipantByPSCID(pscId);
        //    //string personExists = cm.ExistsParticipantByPSCID(pscId);

        //    return Json(ds, JsonRequestBehavior.AllowGet);
        //    //return Json(personExists, JsonRequestBehavior.AllowGet);
        //}

        //public void FormList_Bind()
        //{
        //    CRUDModels cm = new Models.CRUDModels();

        //    DataSet ds = cm.GetFormList();
        //    List<SelectListItem> formList = new List<SelectListItem>();
        //    foreach(DataRow dr in ds.Tables[0].Rows)
        //    {
        //        formList.Add(new SelectListItem { Text = dr["FormName"].ToString(), Value = dr["FormName"].ToString() });
        //    }
        //    ViewBag.DataEntryFormName = formList;
        //}
    }
}
