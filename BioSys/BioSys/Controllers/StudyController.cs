using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioSys.Models;

namespace BioSys.Controllers
{
    public class StudyController : Controller
    {
        // GET: Study
        public ActionResult Index()
        {
            CRUDModels cm = new CRUDModels();
            ModelState.Clear();
            return View(cm.GetStudyList());
        }

        // GET: Study/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Study/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Study/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: Study/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Study/Edit/5
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

        // GET: Study/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Study/Delete/5
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
    }
}
