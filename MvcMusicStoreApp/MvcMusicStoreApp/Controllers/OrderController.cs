using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMusicStoreApp.Models;
using System.Web.Security;

namespace MvcMusicStoreApp.Controllers
{
    public class OrderController : Controller
    {
        private MusicStoreDbContext musicStoreDb = new MusicStoreDbContext();

        // GET: Order
        public ActionResult Index()
        {
            return View(musicStoreDb.Orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = musicStoreDb.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,OrderDate,UserName,FirstName,LastName,Address,City,State,PostalCode,Country,Phone,Email,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                musicStoreDb.Orders.Add(order);
                musicStoreDb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        public JsonResult CheckUserName(string userName)
        {
            var result = Membership.FindUsersByName(userName).Count == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = musicStoreDb.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderDate,UserName,FirstName,LastName,Address,City,State,PostalCode,Country,Phone,Email,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                musicStoreDb.Entry(order).State = EntityState.Modified;
                musicStoreDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = musicStoreDb.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = musicStoreDb.Orders.Find(id);
            musicStoreDb.Orders.Remove(order);
            musicStoreDb.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddressAndPayment(Order order)
        {
            if (ModelState.IsValid)
            {
                order.UserName = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                musicStoreDb.Orders.Add(order);
                musicStoreDb.SaveChanges();
                // Process the order
                
                var cart = ShoppingCart.GetCart(this);
                cart.CreateOrder(order);
                return RedirectToAction("Complete", new { id = order.OrderId });
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddressAndPayment(FormCollection collection)
        {
            var order = new Order();
            UpdateModel(order);
            if (ModelState.IsValid)
            {
                order.UserName = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                musicStoreDb.Orders.Add(order);
                musicStoreDb.SaveChanges();
                // Process the order
                var cart = ShoppingCart.GetCart(this);
                cart.CreateOrder(order);
                return RedirectToAction("Complete", new { id = order.OrderId });
            }
            // Invalid -- redisplay with errors
            return View(order);
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
