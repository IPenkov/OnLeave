using OnLeave.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Postal;
using System.Configuration;
using Common.Constant;

namespace OnLeave.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private OnLeaveContext db = new OnLeaveContext();

        public ActionResult Index()
        {
            //var offers = db.Offers.Include(o => o.OfferType).Include(o => o.UtilityBuilding).ToList();

            // take  9 buildings with photos
            var buildings = db.UtilityBuildings
                .Include(p => p.UtilityBuildingPhotoDetails)
                .Include(b => b.UtilityBuildingLocales)
                .Where(b => b.UtilityBuildingPhotoDetails.Count > 0)
                .OrderByDescending(b => b.SearchRating)
                .ThenByDescending(b => b.UtilityBuildingId)
                .Take(9)
                .ToArray()
                .Select(b => new UtilityBuildingModel
                {
                    Id = b.UtilityBuildingId,
                    Name = string.Join(" / ", b.UtilityBuildingLocales.Select(l => l.Name)),
                    Description = string.Join(System.Environment.NewLine, b.UtilityBuildingLocales.Select(l => l.Description)),
                    SystemTypeId = b.SystemTypeId,
                    UrlAddress = b.ExternalUrl,
                    PhotoIds = b.UtilityBuildingPhotoDetails.Select(p => p.PhotoId).ToList()
                })
                .ToArray();
            
            return View(buildings);            
        }

        public ActionResult TopOffers()
        {
            ViewBag.Message = "Your top offers";
            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult GetUtilityBuilding(int buildingId)
        {
            using (var db = new Models.OnLeaveContext())
            {
                var buildingDB = db.UtilityBuildings
                    .Include(b => b.UtilityBuildingPhotoDetails)
                    .Include(b => b.UtilityBuidingFacilityDetails)
                    .Include(b => b.Periods)
                    .Include(b => b.Periods.Select(p => p.RoomAmounts))
                    .Include(b => b.UtilityBuildingLocales)
                    .Include(b => b.Offers)
                    .FirstOrDefault(b => b.UtilityBuildingId == buildingId);
                if (buildingDB == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                var model = new UtilityBuildingModel
                {
                    Id = buildingDB.UtilityBuildingId,
                    Name = string.Join(" / ", buildingDB.UtilityBuildingLocales.Select(l => l.Name).ToArray()),
                    Description = string.Join(" <br /><br /> ", buildingDB.UtilityBuildingLocales.Select(l => l.Description).ToArray()),
                    Address = string.Join(" / ", buildingDB.UtilityBuildingLocales.Select(l => l.Address).ToArray()),
                    ContactPerson = string.Join(" / ", buildingDB.UtilityBuildingLocales.Select(l => l.ContactPerson).ToArray()),
                    PhoneNumber = buildingDB.PhoneNumber,                    
                    PhotoIds = buildingDB.UtilityBuildingPhotoDetails.Select(ph => ph.PhotoId).ToList(),
                    CityId = buildingDB.CityId,
                    Rating = buildingDB.Rating ?? 0,
                    Latitude = buildingDB.lat ?? 0M,
                    Longitude = buildingDB.lon ?? 0M,
                    Size = buildingDB.Size,
                    Periods = buildingDB.Periods.ToList(),
                    Facilities = db.UtilityBuildingFacilityTypes.Select(ft =>
                        new FacilityTypeModel
                        {
                            FacilityTypeId = ft.UtilityBuildingFacilityTypeId,
                            FacilityTypeName = ft.Name
                        }).ToArray(),
                };

                // sort by room types
                model.Periods.ForEach(p =>
                {
                    p.RoomAmounts = p.RoomAmounts
                        .Join(StaticDataProvider.RoomTypes, r => r.RoomTypeId, t => t.RoomTypeId, (r, t) => new
                    {
                        Room = r,
                        Order = t.Order
                    })
                    .OrderBy(r => r.Order)
                    .Select(r => r.Room)
                    .ToList();
                });

                foreach (var f in model.Facilities)
                {
                    f.Selected = buildingDB.UtilityBuidingFacilityDetails.Any(ft => ft.UtilityBuildingFacilityTypeId == f.FacilityTypeId);
                }

                model.Offers = buildingDB.Offers.Select(o => new OnLeave.Models.BusinessEntities.Offer
                {
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    Discount = o.Discount,
                    Description = o.Description
                }).ToList();

                ViewBag.RoomTypes = StaticDataProvider.RoomTypes;
                ViewBag.Cities = StaticDataProvider.Cities;
                //return View(model);
                return PartialView("GetUtilityBuilding", model); 
            }
        }

        /// <summary>
        /// Searches buildings to match criterias.
        /// </summary>
        /// <param name="model">The Search model.</param>
        /// <returns>The result</returns>
        public ActionResult SearchBuilding(SearchViewModel model)
        {
            System.Diagnostics.Debug.WriteLine(model.Name);

            int[] facilitiesIds = model.Facilities.Concat(model.TopFacilities).Where(f => f.Selected)
                .Select(f => f.FacilityTypeId).ToArray();

            using (var db = new Models.OnLeaveContext())
            {
                var result = db.UtilityBuildings
                    .Include(b => b.UtilityBuildingPhotoDetails)
                    .Include(b => b.Periods)
                    .Include(b => b.Periods.Select(p => p.RoomAmounts))
                    .Include(b => b.UtilityBuildingLocales)
                    .Where(b =>
                        (model.Name == null || model.Name.Trim().Length == 0 || b.UtilityBuildingLocales.Any(l => l.Name.ToLower().Contains(model.Name.ToLower())))
                        && b.UtilityBuildingPhotoDetails.Count > 0)
                    .Where(b => !model.CityId.HasValue || b.CityId == model.CityId)
                    .Where(b => !model.UtilityBuildingTypeId.HasValue || b.UtilityBuildingTypeId == model.UtilityBuildingTypeId) // UtilityBuildingTypeId
                    .Where(b => facilitiesIds.All(fID => b.UtilityBuidingFacilityDetails.Any(ft => ft.UtilityBuildingFacilityTypeId == fID)))
                    .Where(b => !model.Rating.HasValue || b.Rating == model.Rating)
                    .Where(b => (!model.MinAmount.HasValue && !model.MaxAmount.HasValue) 
                        || b.Periods.SelectMany(p => p.RoomAmounts).Any(a => (!model.MinAmount.HasValue || model.MinAmount <= a.Amount) && (!model.MaxAmount.HasValue || model.MaxAmount >= a.Amount)))                   
                    .OrderByDescending(b => b.SearchRating)
                    .ToArray();

                var buildings = result
                    .Select(b => new UtilityBuildingModel
                    {
                        Id = b.UtilityBuildingId,
                        Name = b.UtilityBuildingLocales.Where(l => l.LocaleId == (int)LocaleTypes.BG).Select(l => l.Name).FirstOrDefault(),
                        Description = b.UtilityBuildingLocales.Where(l => l.LocaleId == (int)LocaleTypes.BG).Select(l => l.Description).FirstOrDefault(),
                        UrlAddress = b.ExternalUrl,
                        Rating = b.Rating ?? 0,
                        Size = b.Size,
                        PhotoIds =  new System.Collections.Generic.List<int>(){  b.UtilityBuildingPhotoDetails.First().PhotoId },
                        Periods = b.Periods.OrderBy(p => p.RoomAmounts.Min(a => a.Amount)).Take(1).ToList()
                    }).ToArray();

                

                return PartialView("_SearchResult", buildings.ToArray());
            }            
        }

        /// <summary>
        /// Sends the reservation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The model.</returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")] 
        [ValidateAntiForgeryToken]
        public ActionResult SendReservation(SendReservationModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_SendReservation",  model);
            }

            UtilityBuilding building = null;
            using (var db = new OnLeaveContext())
            {
                building = db.UtilityBuildings
                    .Include(b => b.UtilityBuildingLocales)                    
                    .FirstOrDefault(b => b.UtilityBuildingId == model.UtilityBuildingId);
            }

            if (building == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "building not found");

            User user = null;
            using (var db = new UserDbContext())
            {
                user = db.Users.FirstOrDefault(u => u.Id == building.UserId);
            }

            if (user == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "user not found");

            dynamic email = new Email("_Reservation");
            
            email.To = ConfigurationManager.AppSettings["email"];
            email.From = ConfigurationManager.AppSettings["email"];
            email.Subject = "Резервация от Отпускарче";
            email.ClientName = model.ClientName;
            email.Email = model.Email;                    
            email.BuildingEmail = user.Email;
            email.StartDate = model.StartDate;
            email.EndDate = model.EndDate;
            email.ReservationDescription = model.ReservationDescription;
            email.BuildingId = building.UtilityBuildingId;
            email.Name = string.Join(" / ", building.UtilityBuildingLocales.Select(l => l.Name).ToArray());
            email.Send();            

            ModelState.Clear();            
            return PartialView("_SendReservation", new SendReservationModel());
        }

        /// <summary>
        /// Renders Search Section.
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            var searchModel = new SearchViewModel();
            var topFacilities = new int[] 
            {
                (int)Common.Constant.FacilityTypes.WI_FI,
                (int)Common.Constant.FacilityTypes.SWIMMING_POOL,
                (int)Common.Constant.FacilityTypes.SPA,
                (int)Common.Constant.FacilityTypes.BREAKFASET
            };

            using (var db = new Models.OnLeaveContext())
            {
                searchModel.TopFacilities = db.UtilityBuildingFacilityTypes
                    .Where(f => topFacilities.Any(tf => f.UtilityBuildingFacilityTypeId == tf))
                    .Select(ft => new FacilityTypeModel
                    {
                        FacilityTypeId = ft.UtilityBuildingFacilityTypeId,
                        FacilityTypeName = ft.Name
                    }).ToArray();
                
                searchModel.Facilities = db.UtilityBuildingFacilityTypes
                    .Where(f => !topFacilities.Any(tf => f.UtilityBuildingFacilityTypeId == tf))
                    .Select(ft => new FacilityTypeModel
                    {
                        FacilityTypeId = ft.UtilityBuildingFacilityTypeId,
                        FacilityTypeName = ft.Name                        
                    }).ToArray();
            }

            ViewBag.Cities = StaticDataProvider.Cities.Select(c => new SelectListItem { Text = c.Name, Value = c.CityId.ToString() });
            ViewBag.UtilityBuildingTypes = StaticDataProvider.UtilityBuildingTypes.Select(t => new SelectListItem
            {
                Text = t.Description,
                Value = t.UtilityBuildingTypeId.ToString()
            });

            return PartialView("_SearchSection", searchModel);
        }
    }
}