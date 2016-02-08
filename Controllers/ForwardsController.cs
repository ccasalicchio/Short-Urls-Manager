using Shorten_Urls.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Shorten_Urls.Controllers
{
    public class ForwardsController : Controller
    {
        private UrlRepository _repo;
        private UserRepository _usersRepo;
        private ApplicationUser user;

        public ForwardsController()
        {
            _repo = new UrlRepository();
            _usersRepo = new UserRepository();
        }
        //
        // GET: /Forwards/

        public ActionResult SendToSrc(string src)
        {
            string error404 = "404.html";
            string redirect = "/Home/Index";

            Url url = _repo.GetBySrc(src);
            if (url != null)
            {

                if (!url.Redirect.Equals(string.Empty) && (url.Expires > DateTime.Now || url.Expires == null))
                    redirect = url.Redirect;
                else
                    redirect = error404;
            }
            return Redirect(redirect);
        }
        [Authorize]
        public ActionResult List()
        {

            List<Url> urls;
            if (User != null)
                user = _usersRepo.GetById(User.Identity.GetUserId());

            if (user.UserType == UserTypes.Administrator)
                urls = _repo.Urls;
            else urls = _repo.GetByUserId(user.Id);

            return View(urls);
        }

        //
        // GET: /Forwards/Details/5
        [Authorize]

        public ActionResult Details(int id)
        {
            if (User != null)
                user = _usersRepo.GetById(User.Identity.GetUserId());

            if (id == 0)
                Redirect("/Home/Index");
            Url url = _repo.GetById(id);
            return View(url);
        }

        //
        // GET: /Forwards/Create
        [Authorize]

        public ActionResult Create()
        {
            if (User != null)
                user = _usersRepo.GetById(User.Identity.GetUserId());

            Url url = new Url();
            string random = Helpers.GenerateRandomgUrl();
            while (_repo.Exists(random))
            {
                random = Helpers.GenerateRandomgUrl();
            }
            ViewBag.RandomUrl = random;
            return View(url);
        }

        [HttpGet]
        public string Available(string Src)
        {
            bool exists = _repo.Exists(Src);
            return new JavaScriptSerializer().Serialize(!exists);
        }
        //
        // POST: /Forwards/Create

        [Authorize]
        [HttpPost]
        public ActionResult Create(string Redirect, string Src, DateTime? Expires)
        {
            try
            {
                // TODO: Add insert logic here
                if (User != null)
                    user = _usersRepo.GetById(User.Identity.GetUserId());

                Url newUrl = new Url
                {
                    Redirect = Redirect,
                    Src = Src,
                    CreatedOn = DateTime.Now,
                    Expires = Expires,
                    UserId = user.Id,
                    Username = _usersRepo.GetUsername(user.Id)
                };

                if (_repo.needLastId)
                    newUrl.Id = _repo.LastId + 1;

                _repo.Add(newUrl);
                TempData["Success"] = "Short Url has been created";
                return RedirectToAction("List");
            }
            catch
            {
                TempData["Error"] = "There was an error saving your new Url";
                return View("List");
            }
        }

        //
        // GET: /Forwards/Edit/5
        [Authorize]

        public ActionResult Edit(int id)
        {
            if (User != null)
                user = _usersRepo.GetById(User.Identity.GetUserId());

            Url editUrl = _repo.GetById(id);
            return View(editUrl);
        }

        //
        // POST: /Forwards/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int Id, string Redirect, string Src, DateTime? Expires = null)
        {
            try
            {
                // TODO: Add update logic here
                if (User != null)
                    user = _usersRepo.GetById(User.Identity.GetUserId());

                Url editUrl = _repo.GetById(Id);
                editUrl.Redirect = Redirect;
                editUrl.Src = Src;
                editUrl.UserId = user.Id;
                editUrl.Expires = Expires;
                editUrl.Username = _usersRepo.GetUsername(user.Id);

                _repo.Update(editUrl);
                TempData["Success"] = "Short Url has been updated";
                return RedirectToAction("List");
            }
            catch
            {
                TempData["Error"] = "There was an error saving your Url";
                return View();
            }
        }

        //
        // GET: /Forwards/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (User != null)
                user = _usersRepo.GetById(User.Identity.GetUserId());
            Url url = _repo.GetById(id);
            return View(url);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, bool confirm)
        {
            try
            {
                if (User != null)
                    user = _usersRepo.GetById(User.Identity.GetUserId());

                _repo.Remove(id);
                TempData["Success"] = "Short Url has been deleted";
                return RedirectToAction("List");

            }
            catch
            {
                TempData["Error"] = "There was an error deleting your Url";
                Url url = _repo.GetById(id);
                return View(url);
            }
        }

    }
}
