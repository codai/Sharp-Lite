using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Domain.Entities;
using WebApp.Web.Models;
using SharpLite.Domain.DataInterfaces;
using WebApp.Web.Areas.Admin.ViewModels;
using AutoMapper;
using WebApp.Common.Mappers;

namespace WebApp.Web.Areas.Admin.Controllers
{
    public class FileTypeController : Controller
    {
        private readonly IRepository<FileType> fileTypeRepository;

        public FileTypeController(IRepository<FileType> fileTypeRepository)
        {
            this.fileTypeRepository = fileTypeRepository;
        }

        // GET: /Admin/FileType/
        public ActionResult Index()
        {
            var fileTypes = fileTypeRepository.GetAll();

            return View(Mapper.Map<IEnumerable<FileType>, IEnumerable<EditFileTypeViewModel>>(fileTypes));
        }

        // GET: /Admin/FileType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fileType = fileTypeRepository.Get(id.Value);
            if (fileType == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<EditFileTypeViewModel>(fileType));
        }

        // GET: /Admin/FileType/Create
        public ActionResult Create()
        {
            return View("Edit", new EditFileTypeViewModel());
        }

        // GET: /Admin/FileType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fileType = fileTypeRepository.Get(id.Value);
            if (fileType == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<EditFileTypeViewModel>(fileType));
        }

        // POST: /Admin/FileType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Label,DescriptionKey")] EditFileTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                FileType entity;

                if (viewModel.Id == 0)
                {
                    entity = new FileType();
                }
                else
                {
                    entity = fileTypeRepository.Get(viewModel.Id);
                }

                Mapper.Map(viewModel, entity);

                fileTypeRepository.SaveOrUpdate(entity);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /Admin/FileType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fileType = fileTypeRepository.Get(id.Value);
            if (fileType == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<EditFileTypeViewModel>(fileType));
        }

        // POST: /Admin/FileType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileType deliverytype = fileTypeRepository.Get(id);
            if (deliverytype == null)
            {
                return HttpNotFound();
            }

            fileTypeRepository.Delete(deliverytype);

            return RedirectToAction("Index");
        }
    }
}
