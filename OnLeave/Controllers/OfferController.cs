using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OnLeave.Models;

namespace OnLeave.Controllers
{
    public class OfferController : Controller
    {
        private OnLeaveContext db = new OnLeaveContext();

        // GET: /Offer/
        public ActionResult Index()
        {
            var offers = db.Offers.Include(o => o.OfferType).Include(o => o.UtilityBuilding);
            return View(offers.ToList());
        }

        // GET: /Offer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET: /Offer/Create
        public ActionResult Create()
        {
            ViewBag.OfferTypeId = new SelectList(db.OfferTypes, "OfferTypeId", "KeyWords");
            ViewBag.UtilityBuildingId = new SelectList(db.UtilityBuildings, "UtilityBuildingId", "KeyWords");
            return View();
        }

        // POST: /Offer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OfferId,KeyWords,Description,OfferTypeId,UtilityBuildingId")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OfferTypeId = new SelectList(db.OfferTypes, "OfferTypeId", "KeyWords", offer.OfferTypeId);
            ViewBag.UtilityBuildingId = new SelectList(db.UtilityBuildings, "UtilityBuildingId", "KeyWords", offer.UtilityBuildingId);
            return View(offer);
        }

        // GET: /Offer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferTypeId = new SelectList(db.OfferTypes, "OfferTypeId", "KeyWords", offer.OfferTypeId);
            ViewBag.UtilityBuildingId = new SelectList(db.UtilityBuildings, "UtilityBuildingId", "KeyWords", offer.UtilityBuildingId);
            return View(offer);
        }

        // POST: /Offer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OfferId,KeyWords,Description,OfferTypeId,UtilityBuildingId")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OfferTypeId = new SelectList(db.OfferTypes, "OfferTypeId", "KeyWords", offer.OfferTypeId);
            ViewBag.UtilityBuildingId = new SelectList(db.UtilityBuildings, "UtilityBuildingId", "KeyWords", offer.UtilityBuildingId);
            return View(offer);
        }

        // GET: /Offer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // POST: /Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
