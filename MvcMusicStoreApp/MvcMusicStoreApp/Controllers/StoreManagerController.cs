using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMusicStoreApp.Models;
using Microsoft.Ajax.Utilities;
using System.Web.Mvc.Html;
using MvcMusicStoreApp.Utils.Enums;
using System.IO;

namespace MvcMusicStoreApp.Controllers
{
    public class StoreManagerController : Controller
    {
        private MusicStoreDbContext musicStoreDb = new MusicStoreDbContext();

        // GET: StoreManagers
        public ActionResult Index()
        {
            var albums = musicStoreDb.Albums.Include(album => album.Artist).Include(album => album.Genre);
            return View(albums.ToList());  
        }

        public ActionResult GetAlbumsByArtist(string artistName)
        {   IEnumerable<Album>albums = null;
            if (!string.IsNullOrWhiteSpace(artistName))
            {
                albums = musicStoreDb.Albums.Include(album => album.Artist).Where(album => album.Artist.Name.Contains(artistName)).ToList();
            }
            else
            {
                albums = musicStoreDb.Albums.Include(album => album.Artist).Include(album => album.Genre).ToList();
            }
            
            return View(albums); 
        }

        /*Render a partial View using @Html.Action
         * @Html.Action() allows to render a partial view from calling another action.
        */

        [ChildActionOnly]
        public ActionResult PartialViewUsingHtmlAction()
        {
            return PartialView("_PartialViewUsingHtmlAction");
        }


        // GET: StoreManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = musicStoreDb.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(musicStoreDb.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(musicStoreDb.Genres, "GenreId", "Name");
            return View();
        }

        // POST: StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                musicStoreDb.Albums.Add(album);
                musicStoreDb.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(musicStoreDb.Artists, "ArtistId", "ArtistId", album.ArtistId);
            ViewBag.GenreId = new SelectList(musicStoreDb.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = musicStoreDb.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.Price = 100;
            ViewBag.ArtistId = new SelectList(musicStoreDb.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(musicStoreDb.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            
            if (ModelState.IsValid)
            {
                
                musicStoreDb.Entry(album).State = EntityState.Modified;
                musicStoreDb.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = musicStoreDb.Artists.OrderBy(artist => artist.Name).AsEnumerable().Select(artist => new SelectListItem()
            {
                Text = artist.Name,
                Value = artist.ArtistId.ToString(),
                Selected = artist.ArtistId == album.ArtistId
            });
            ViewBag.ArtistId = new SelectList(musicStoreDb.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(musicStoreDb.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
            //UrlHelper is available in View and the controller;
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = musicStoreDb.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = musicStoreDb.Albums.Find(id);
            musicStoreDb.Albums.Remove(album);
            musicStoreDb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                musicStoreDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
