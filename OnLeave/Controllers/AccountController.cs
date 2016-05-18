using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using OnLeave.Models;
using Common;
using Owin;
using Microsoft.Owin.Security.DataProtection;
using System.Net.Mail;
using Microsoft.AspNet.Identity.Owin;
using Common.Constant;
using System.IO;
using System.Net.Mime;


namespace OnLeave.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<User>(new UserStore<User>(new UserDbContext())))
        {
        }

        public AccountController(UserManager<User> userManager)
        {
            UserManager = userManager;
            var userValidator = userManager.UserValidator as UserValidator<User, string>;
            
            // user email as user's name
            userValidator.AllowOnlyAlphanumericUserNames = false;

            if (Startup.DataProtectionProvider != null)
            {
                this.UserManager.UserTokenProvider = new DataProtectorTokenProvider<User>(Startup.DataProtectionProvider.Create("PasswordReset"));
            }
        }

        public UserManager<User> UserManager { get; private set; }

        // GET: Account/Account
        public ActionResult Account()
        {            
            return View();
        }

        [HttpGet]
        public ActionResult AddUtilityBuilding()
        {
            var utilityBuilding = new UtilityBuildingModel
            {
                UtilityBuildingTypeId = (int)UtilityBuildingTypes.Hotel,
                Periods = new List<Period>()
            };

            using (var db = new Models.OnLeaveContext())
            {
                utilityBuilding.Facilities = db.UtilityBuildingFacilityTypes
                    .Select(ft => new FacilityTypeModel
                    {
                        FacilityTypeId = ft.UtilityBuildingFacilityTypeId,
                        FacilityTypeName = ft.Name
                    })
                    .ToArray();

                // five periods per hotel
                while (utilityBuilding.Periods.Count < 5)
                {
                    utilityBuilding.Periods.Add(new Period
                    {
                        FromDate = DateTime.Now.Date,
                        ToDate = DateTime.Now.Date,
                        RoomAmounts = StaticDataProvider.RoomTypes.Select(rType => new RoomAmount
                        {
                            Amount = 0M,
                            RoomTypeId = rType.RoomTypeId,                            
                        }).ToList()
                    });
                }
            }

            ViewBag.Cities = StaticDataProvider.Cities.Select(c => new SelectListItem { Text = c.Name, Value = c.CityId.ToString() });
            ViewBag.RoomTypes = StaticDataProvider.RoomTypes;
            ViewBag.UtilityBuildingTypes = StaticDataProvider.UtilityBuildingTypes.Select(t => new SelectListItem
            {
                Text = t.Description,
                Value = t.UtilityBuildingTypeId.ToString()
            });

            return PartialView(utilityBuilding);
        }

        /// <summary>
        /// Adds the utility building and related images.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>View to be shown.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUtilityBuilding(UtilityBuildingModel model)
        {
            if (ModelState.IsValid)
            {  
                using (var db = new Models.OnLeaveContext())
                {
                    var city = StaticDataProvider.Cities.FirstOrDefault(c => c.CityId == model.CityId);
                    model.Longitude = city.lon;
                    model.Latitude = city.lat;

                    var building = new UtilityBuilding
                    {
                        KeyWords = model.Name,                        
                        UtilityBuildingTypeId = model.UtilityBuildingTypeId,
                        UserId = User.Identity.GetUserId(),
                        PhoneNumber = model.PhoneNumber,
                        CityId = city.CityId,
                        Rating = model.Rating,
                        lon = model.Longitude,
                        lat = model.Latitude,
                        Size = model.Size
                    };

                    building.UtilityBuildingLocales.Add(new UtilityBuildingLocale
                    {
                        LocaleId = (int)LocaleTypes.BG,
                        Name = model.Name,
                        Description = model.Description,
                        ContactPerson = model.ContactPerson,
                        Address = model.Address
                    });

                    if (!string.IsNullOrWhiteSpace(model.ContactPersonEN)
                        || !string.IsNullOrWhiteSpace(model.NameEN)
                        || !string.IsNullOrWhiteSpace(model.DescriptionEN)
                        || !string.IsNullOrWhiteSpace(model.Address))
                    {
                        building.UtilityBuildingLocales.Add(new UtilityBuildingLocale
                        {
                            LocaleId = (int)LocaleTypes.EN,
                            Name = model.NameEN,
                            Description = model.DescriptionEN,
                            ContactPerson = model.ContactPersonEN,
                            Address = model.AddressEN
                        });
                    }

                    var buildingDB = db.UtilityBuildings.Add(building);                    

                    // update facilities info
                    var facilitiesToRemove = buildingDB.UtilityBuidingFacilityDetails
                        .Where(fd => !model.Facilities.Any(f => f.FacilityTypeId == fd.UtilityBuildingFacilityTypeId && f.Selected));                    

                    db.UtilityBuidingFacilityDetails.RemoveRange(facilitiesToRemove);

                    model.Facilities.Where(f => f.Selected).ToList().ForEach(f =>
                    {
                        if (!buildingDB.UtilityBuidingFacilityDetails.Any(fd => fd.UtilityBuildingFacilityTypeId == f.FacilityTypeId))
                        {
                            buildingDB.UtilityBuidingFacilityDetails.Add(new UtilityBuidingFacilityDetail
                            {
                                UtilityBuildingFacilityTypeId = f.FacilityTypeId,
                            });
                        }
                    });

                    model.Periods.Where(p => p.RoomAmounts.Any(a => a.Amount != 0M)).ToList().ForEach(p =>
                    {
                        buildingDB.Periods.Add(p);
                        p.RoomAmounts.Where(a => a.Amount == 0M)
                            .ToList()
                            .ForEach(a => p.RoomAmounts.Remove(a));                        
                    });

                    db.SaveChanges();

                    model.Id = buildingDB.UtilityBuildingId;                   
                }
                
            }

            ViewBag.Cities = StaticDataProvider.Cities.Select(c => new SelectListItem { Text = c.Name, Value = c.CityId.ToString() });
            ViewBag.RoomTypes = StaticDataProvider.RoomTypes;
            ViewBag.UtilityBuildingTypes = StaticDataProvider.UtilityBuildingTypes.Select(t => new SelectListItem
            {
                Text = t.Description,
                Value = t.UtilityBuildingTypeId.ToString()
            });

            ModelState.Keys.Where(key => key.StartsWith("Periods")).ToList().ForEach(key => ModelState.Remove(key));
            return PartialView(model);
        }

        [HttpGet]

        public ActionResult UtilityBuildings()
        {
            string userId = User.Identity.GetUserId();
            using (var db = new Models.OnLeaveContext())
            {
                var result = db.UtilityBuildings
                    .Where(b => b.UserId == userId)
                    .Include(b => b.UtilityBuildingPhotoDetails)
                    .Include(b => b.UtilityBuildingLocales)
                    .ToArray();

                UtilityBuildingModel[] photoes = result
                    .Select(b => new UtilityBuildingModel
                    {
                        Id = b.UtilityBuildingId,
                        Name = string.Join(" / ", b.UtilityBuildingLocales.Where(l => !string.IsNullOrWhiteSpace(l.Name)).Select(l => l.Name).ToArray()),
                        Description = string.Join(System.Environment.NewLine, b.UtilityBuildingLocales.Where(l => !string.IsNullOrWhiteSpace(l.Description)).Select(l => l.Description).ToArray()),
                        PhotoIds = b.UtilityBuildingPhotoDetails.Select(photo => photo.PhotoId).ToList()                        
                    }).ToArray();

                return PartialView("UtilityBuildings", photoes);
            }
        }

        /// <summary>
        /// Buildings system search.
        /// </summary>
        /// <param name="model">The model.</param>                
        [HttpPost]
        [Authorize(Roles="Root")]
        [ValidateAntiForgeryToken]
        public ActionResult SearchBuildings(SearchSystemViewModel model)
        {
            using (var db = new Models.OnLeaveContext())
            {
                var result = db.UtilityBuildings
                    .Where(b =>  (model.Name == null || model.Name.Trim() == string.Empty || b.UtilityBuildingLocales.Any(l => l.Name.Trim().ToLower().Contains(model.Name.Trim().ToLower())))
                        && b.UtilityBuildingPhotoDetails.Count > 0)
                    .Where(b => !model.CityId.HasValue || b.CityId == model.CityId)
                    .Include(b => b.UtilityBuildingPhotoDetails)
                    .Include(b => b.UtilityBuildingLocales)
                    .OrderByDescending(b => b.SearchRating)
                    .ToArray();

                UtilityBuildingModel[] photoes = result
                    .Select(b => new UtilityBuildingModel
                    {
                        Id = b.UtilityBuildingId,
                        Name = string.Join(" / ", b.UtilityBuildingLocales.Where(l => !string.IsNullOrWhiteSpace(l.Name)).Select(l => l.Name).ToArray()),
                        Description = string.Join(System.Environment.NewLine, b.UtilityBuildingLocales.Where(l => !string.IsNullOrWhiteSpace(l.Description)).Select(l => l.Description).ToArray()),
                        PhotoIds = b.UtilityBuildingPhotoDetails.Select(photo => photo.PhotoId).ToList()                        
                    }).ToArray();

                return PartialView("UtilityBuildings", photoes);
            }           
        }

        /// <summary>
        /// Find building by id.
        /// </summary>
        /// <param name="buildingId">The building id.</param>
        /// <returns>Matched Building</returns>
        [HttpGet]
        public ActionResult EditBuilding(int? buildingId)
        {
            if (!buildingId.HasValue)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);                
            }

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

                var bg = buildingDB.UtilityBuildingLocales.FirstOrDefault(l => l.LocaleId == (int)LocaleTypes.BG);
                if (bg == null) bg = new UtilityBuildingLocale();
                var en = buildingDB.UtilityBuildingLocales.FirstOrDefault(l => l.LocaleId == (int)LocaleTypes.EN);
                if (en == null) en = new UtilityBuildingLocale();
                
                var model = new UtilityBuildingModel
                {
                    Id = buildingDB.UtilityBuildingId,
                    Name =  bg.Name,
                    NameEN = en.Name,
                    Description = bg.Description,
                    DescriptionEN = en.Description,
                    UtilityBuildingTypeId = buildingDB.UtilityBuildingTypeId,
                    Address = bg.Address,
                    AddressEN = en.Address,
                    ContactPerson = bg.ContactPerson,
                    ContactPersonEN = en.ContactPerson,
                    PhoneNumber = buildingDB.PhoneNumber,
                    CityId = buildingDB.CityId,
                    Rating = buildingDB.Rating ?? 0,
                    SearchRating = User.IsInRole("Root") ? buildingDB.SearchRating : null,
                    Latitude = buildingDB.lat ?? 0M,
                    Longitude = buildingDB.lon ?? 0M,                    
                    Size = buildingDB.Size,
                    PhotoIds = buildingDB.UtilityBuildingPhotoDetails.Select(ph => ph.PhotoId).ToList(),                    
                    Facilities = db.UtilityBuildingFacilityTypes.Select(ft =>
                        new FacilityTypeModel
                        {
                            FacilityTypeId = ft.UtilityBuildingFacilityTypeId,
                            FacilityTypeName = ft.Name
                        }).ToArray(),
                    Periods = buildingDB.Periods.Select(p => 
                    {
                        StaticDataProvider.RoomTypes.ToList().ForEach(rType =>
                        {
                            var roomAmount = p.RoomAmounts.FirstOrDefault(rAmountDB => rAmountDB.RoomTypeId == rType.RoomTypeId);
                            if (roomAmount == null)
                            {
                                // add default value for room type
                                p.RoomAmounts.Add(new RoomAmount
                                {
                                    PeriodId = p.PeriodId,
                                    RoomTypeId = rType.RoomTypeId,
                                    Amount = 0M,                                    
                                });
                            }
                        });

                        return p; 
                    }).ToList()
                };

                // get offers
                model.Offers = buildingDB.Offers.Select(o => new OnLeave.Models.BusinessEntities.Offer
                {
                    OfferId = o.OfferId,
                    Description = o.Description,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    Discount = o.Discount
                }).ToList();

                // 5 periods for each hotel
                while (model.Periods.Count < 5)
                {
                    model.Periods.Add(new Period
                    {
                        UtilityBuildingId = model.Id,
                        FromDate = DateTime.Now.Date,
                        ToDate = DateTime.Now.Date,
                        RoomAmounts = StaticDataProvider.RoomTypes.Select(rType => new RoomAmount
                        {
                            RoomTypeId = rType.RoomTypeId,                            
                            Amount = 0M,
                        }).ToList()
                    });
                }

                foreach (var f in model.Facilities)
                {
                    f.Selected = buildingDB.UtilityBuidingFacilityDetails.Any(ft => ft.UtilityBuildingFacilityTypeId == f.FacilityTypeId);
                }

                ViewBag.Cities = StaticDataProvider.Cities.Select(c => new SelectListItem { Text = c.Name, Value = c.CityId.ToString() });
                ViewBag.RoomTypes = StaticDataProvider.RoomTypes;
                ViewBag.UtilityBuildingTypes = StaticDataProvider.UtilityBuildingTypes.Select(t => new SelectListItem
                {
                    Text = t.Description,
                    Value = t.UtilityBuildingTypeId.ToString()
                });

                return PartialView(model);
            }            
        }

        /// <summary>
        /// Edits the building.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult EditBuilding(UtilityBuildingModel model)
        {
            
            if (ModelState.IsValid)
            {
                using (var db = new Models.OnLeaveContext())
                {
                    var buildingDB = db.UtilityBuildings
                        .Include(b => b.Periods)
                        .Include(b => b.Offers)
                        .Include(b => b.UtilityBuildingPhotoDetails)
                        .Include(b => b.UtilityBuildingLocales)
                        .FirstOrDefault(b => b.UtilityBuildingId == model.Id);
                    if (buildingDB == null || buildingDB.UserId != User.Identity.GetUserId())
                    {
                        new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                    }

                    buildingDB.UtilityBuildingTypeId = model.UtilityBuildingTypeId;                    
                    buildingDB.PhoneNumber = model.PhoneNumber;
                    buildingDB.CityId = model.CityId;
                    buildingDB.Rating = model.Rating;
                    buildingDB.Size = model.Size;

                    if (User.IsInRole("Root"))
                    {
                        buildingDB.SearchRating = model.SearchRating;
                    }

                    var buildingLocaleBG = buildingDB.UtilityBuildingLocales.FirstOrDefault(l => l.LocaleId == (int)LocaleTypes.BG);
                    if (buildingLocaleBG != null)
                    {
                        buildingLocaleBG.Name = model.Name;
                        buildingLocaleBG.Description = model.Description;
                        buildingLocaleBG.ContactPerson = model.ContactPerson;
                        buildingLocaleBG.Address = model.Address;
                    }
                    else
                    {
                        buildingDB.UtilityBuildingLocales.Add(new UtilityBuildingLocale
                        {
                            LocaleId = (int)LocaleTypes.BG,
                            Name = model.Name,
                            Description = model.Description,
                            ContactPerson = model.ContactPerson,
                            Address = model.Address
                        });
                    }

                    var buildingLocaleEN = buildingDB.UtilityBuildingLocales.FirstOrDefault(l => l.LocaleId == (int)LocaleTypes.EN);
                    if (buildingLocaleEN != null)
                    {
                         if (string.IsNullOrWhiteSpace(model.ContactPersonEN)
                        && string.IsNullOrWhiteSpace(model.NameEN)
                        && string.IsNullOrWhiteSpace(model.DescriptionEN)
                        && string.IsNullOrWhiteSpace(model.AddressEN))
                         {
                             // remove if no data                             
                             db.UtilityBuildingLocales.Remove(buildingLocaleEN);
                         }
                         else
                         {
                             buildingLocaleEN.Name = model.NameEN;
                             buildingLocaleEN.Description = model.DescriptionEN;
                             buildingLocaleEN.ContactPerson = model.ContactPersonEN;
                             buildingLocaleEN.Address = model.AddressEN;
                         }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(model.ContactPersonEN)
                       || !string.IsNullOrWhiteSpace(model.NameEN)
                       || !string.IsNullOrWhiteSpace(model.DescriptionEN)
                       || !string.IsNullOrWhiteSpace(model.Address))
                        {
                            buildingDB.UtilityBuildingLocales.Add(new UtilityBuildingLocale
                            {
                                LocaleId = (int)LocaleTypes.EN,
                                Name = model.NameEN,
                                Description = model.DescriptionEN,
                                ContactPerson = model.ContactPersonEN,
                                Address = model.AddressEN
                            });
                        }
                    }

                     // update facilities info
                    var facilitiesToRemove = buildingDB.UtilityBuidingFacilityDetails
                        .Where(fd => !model.Facilities.Any(f => f.FacilityTypeId == fd.UtilityBuildingFacilityTypeId && f.Selected));                    

                    db.UtilityBuidingFacilityDetails.RemoveRange(facilitiesToRemove);

                    model.Facilities.Where(f => f.Selected).ToList().ForEach(f =>
                    {
                        if (!buildingDB.UtilityBuidingFacilityDetails.Any(fd => fd.UtilityBuildingFacilityTypeId == f.FacilityTypeId))
                        {
                            buildingDB.UtilityBuidingFacilityDetails.Add(new UtilityBuidingFacilityDetail
                            {
                                UtilityBuildingFacilityTypeId = f.FacilityTypeId,
                                UtilityBuildingId = model.Id
                            });
                        }
                    });

                    // periods
                    model.Periods.Where(p => !p.StartDate.HasValue).ToList().ForEach(p => p.StartDate = DateTime.Now.Date);
                    model.Periods.Where(p => !p.EndDate.HasValue).ToList().ForEach(p => p.EndDate = DateTime.Now.Date);

                    var periodsToRemove = buildingDB.Periods
                        .Where(pDB => 
                            !model.Periods.Where(p => p.PeriodId != 0).Any(p => p.PeriodId == pDB.PeriodId && p.RoomAmounts.Any(a => a.Amount != 0M)))
                            .ToList();

                    var periodsToAdd = model.Periods.Where(p => p.PeriodId == 0 && p.RoomAmounts.Any(a => a.Amount != 0M))
                        .Select(p => new Period
                        {
                            FromDate = p.FromDate,
                            ToDate = p.ToDate,
                            RoomAmounts = p.RoomAmounts.Where(a => a.Amount != 0M).Select(a => new RoomAmount{
                                Amount = a.Amount,
                                RoomTypeId = a.RoomTypeId
                            }).ToList()
                        }).ToList();

                    db.RoomAmounts.RemoveRange(periodsToRemove.SelectMany(p => p.RoomAmounts));
                    db.Periods.RemoveRange(periodsToRemove);

                    buildingDB.Periods.ToList().ForEach(pDB =>
                    {
                        var period = model.Periods.First(p => p.PeriodId == pDB.PeriodId);
                        pDB.FromDate = period.FromDate;
                        pDB.ToDate = period.ToDate;

                        var amountsToDelete = pDB.RoomAmounts
                            .Where(aDB => !period.RoomAmounts.Any(a => a.RoomAmountId == aDB.RoomAmountId && a.Amount != 0M))
                            .ToList();

                        db.RoomAmounts.RemoveRange(amountsToDelete);

                        pDB.RoomAmounts
                            .Where(aDB => period.RoomAmounts.Any(a => a.RoomTypeId == aDB.RoomTypeId))
                            .ToList()
                            .ForEach(aDB => { aDB.Amount = period.RoomAmounts.First(a => a.RoomTypeId == aDB.RoomTypeId).Amount; });

                        period.RoomAmounts
                            .Where(a => a.RoomAmountId == 0 && a.Amount != 0M)
                            .ToList()
                            .ForEach(a => pDB.RoomAmounts.Add(new RoomAmount
                            {
                                PeriodId = pDB.PeriodId,
                                RoomTypeId = a.RoomTypeId,
                                Amount = a.Amount
                            }));
                    });

                    periodsToAdd.ForEach(p => buildingDB.Periods.Add(p));
                    
                    // Offers
                    var offersToDelete = buildingDB.Offers
                        .Where(o => model.Offers == null ||  !model.Offers.Any(offer => offer.OfferId == o.OfferId));
                    db.Offers.RemoveRange(offersToDelete);
                    model.Offers.Where(o => o.OfferId > 0).ToList().ForEach(o =>
                    {
                        var offerDB = buildingDB.Offers.FirstOrDefault(oDB => oDB.OfferId == o.OfferId);
                        if (offerDB != null)
                        {
                            offerDB.StartDate = o.StartDate.Value;
                            offerDB.EndDate = o.EndDate;
                            offerDB.Discount = o.Discount;
                            offerDB.Description = o.Description;
                        }
                    });

                    model.Offers.Where(o => o.OfferId == -1).ToList().ForEach(o =>
                    {
                        buildingDB.Offers.Add(new Offer
                        {
                            StartDate = o.StartDate.Value,
                            EndDate = o.EndDate,
                            Discount = o.Discount,
                            Description = o.Description,
                            OfferTypeId = (int)OfferTypes.Discount
                        });
                    });
                    
                    db.SaveChanges();

                    model.Latitude = buildingDB.lat ?? 0M;
                    model.Longitude = buildingDB.lon ?? 0M;
                    model.Periods = buildingDB.Periods.ToList();
                    model.Periods.ForEach(p =>
                    {
                        StaticDataProvider.RoomTypes.ToList().ForEach(rType =>
                        {
                            if (!p.RoomAmounts.Any(a => a.RoomTypeId == rType.RoomTypeId))
                            {
                                p.RoomAmounts.Add(new RoomAmount
                                {
                                    RoomTypeId = rType.RoomTypeId,
                                    Amount = 0M,
                                    PeriodId = p.PeriodId
                                });
                            }
                        });
                    });

                    // 5 periods for each hotel
                    while (model.Periods.Count < 5)
                    {
                        model.Periods.Add(new Period
                        {
                            UtilityBuildingId = model.Id,
                            FromDate = DateTime.Now.Date,
                            ToDate = DateTime.Now.Date,
                            RoomAmounts = StaticDataProvider.RoomTypes.Select(rType => new RoomAmount
                            {
                                RoomTypeId = rType.RoomTypeId,
                                Amount = 0M,
                            }).ToList()
                        });
                    }

                    model.PhotoIds = buildingDB.UtilityBuildingPhotoDetails.Select(p => p.PhotoId).ToList();
                }
            }
            else
            {
                using (var db = new Models.OnLeaveContext())
                {
                    var buildingDB = db.UtilityBuildings
                        .Include(b => b.UtilityBuildingPhotoDetails)
                        .FirstOrDefault(b => b.UtilityBuildingId == model.Id);

                    if (buildingDB != null)
                    {
                        model.Latitude = buildingDB.lat ?? 0M;
                        model.Longitude = buildingDB.lon ?? 0M;
                        model.PhotoIds = buildingDB.UtilityBuildingPhotoDetails.Select(p => p.PhotoId).ToList();
                    }
                }
            }

            var roomTypesIDs = StaticDataProvider.RoomTypes.OrderBy(t => t.RoomTypeId).Select(t => t.RoomTypeId).ToList();
            model.Periods.ForEach(p =>
            {
                // sort room's amount per room's type
                var roomAmouns = p.RoomAmounts.OrderBy(a => a.RoomTypeId, Comparer<int>.Create((a, b) =>
                    {
                        return roomTypesIDs.IndexOf(a).CompareTo(roomTypesIDs.IndexOf(b));
                    })).ToList();
                p.RoomAmounts.Clear();
                roomAmouns.ForEach(a => p.RoomAmounts.Add(a));
            });

            ViewBag.Cities = StaticDataProvider.Cities.Select(c => new SelectListItem { Text = c.Name, Value = c.CityId.ToString() });
            ViewBag.RoomTypes = StaticDataProvider.RoomTypes.OrderBy(t => t.RoomTypeId).ToArray();
            ViewBag.UtilityBuildingTypes = StaticDataProvider.UtilityBuildingTypes.Select(t => new SelectListItem
            {
                Text = t.Description,
                Value = t.UtilityBuildingTypeId.ToString()
            });

            ModelState.Keys.Where(key => key.StartsWith("Periods")).ToList().ForEach(key => ModelState.Remove(key));            
            return PartialView(model);
        }


        /// <summary>
        /// Saves the building location.
        /// </summary>
        /// <param name="buildingId">The building id.</param>
        /// <param name="Latitude">The latitude.</param>
        /// <param name="Longitude">The longitude.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBuildingLocation(int buildingId, decimal latitude, decimal longitude)
        {
            using (var db = new OnLeaveContext())
            {
                var buildingDB = db.UtilityBuildings.FirstOrDefault(b => b.UtilityBuildingId == buildingId);
                if (buildingDB == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
                }

                if (buildingDB.UserId != User.Identity.GetUserId())
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
                }

                buildingDB.lat = latitude;
                buildingDB.lon = longitude;

                db.SaveChanges();
            }
            
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Removes a building.
        /// </summary>
        /// <param name="buildingId">The building id to be removed.</param>
        /// <returns>True if successfull, otherwise false.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBuilding(int? buildingId)
        {
            if (!buildingId.HasValue)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            using (var db = new Models.OnLeaveContext())
            {
                var building = db.UtilityBuildings
                    .Include(b => b.UtilityBuildingLocales)
                    .Include(b => b.UtilityBuildingPhotoDetails)
                    .Include(b => b.UtilityBuildingPhotoDetails.Select(pd => pd.Photo))
                    .Include(b => b.UtilityBuidingFacilityDetails)
                    .Include(b => b.Rooms)
                    .Include(b => b.Periods.Select(p => p.RoomAmounts))
                    .Include(b => b.Offers)
                    .FirstOrDefault(b => b.UtilityBuildingId == buildingId);
                System.Diagnostics.Debug.Assert(building != null, "building not found");

                if (building.UserId != User.Identity.GetUserId() && !User.IsInRole("Root"))
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("avoid to delete building, which doesn't belong to user"));                    
                    // avoid to delete building, which doesn't belong to user
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);                    
                }

                db.UtilityBuildingLocales.RemoveRange(building.UtilityBuildingLocales);                
                db.UtilityBuildingPhotoDetails.RemoveRange(building.UtilityBuildingPhotoDetails);
                db.Photos.RemoveRange(building.UtilityBuildingPhotoDetails.Select(pd => pd.Photo));
                db.UtilityBuidingFacilityDetails.RemoveRange(building.UtilityBuidingFacilityDetails);
                db.RoomAmounts.RemoveRange(building.Periods.SelectMany(p => p.RoomAmounts));
                db.Rooms.RemoveRange(building.Rooms);
                db.Periods.RemoveRange(building.Periods);
                db.Offers.RemoveRange(building.Offers);
                db.UtilityBuildings.Remove(building);

                db.SaveChanges();
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [HttpGet]
        [Authorize(Roles = "Root")]
        public ActionResult Cities()
        {
            using (var db = new OnLeaveContext())
            {
                var cities = db.Cities.ToArray();
                return PartialView(cities);
            }
        }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <param name="cityId">The city id.</param>        
        [Authorize(Roles = "Root")]
        public ActionResult GetCity(int cityId)
        {
            if (cityId == -1)
            {
                ModelState.Clear();
                return PartialView("City", new CityModel());
            }
            
            using (var db = new OnLeaveContext())
            {
                var city = db.Cities.FirstOrDefault(c => c.CityId == cityId);
                if (city == null)
                {
                    ModelState.AddModelError(string.Empty, "Крад липсва");
                    return RedirectToAction("Cities");                    
                }

                return PartialView("City",
                    new CityModel
                    {
                        Name = city.Name,
                        longitude = city.lon,
                        latitude = city.lat
                    });
            }
        }

        /// <summary>
        /// Deletes the city.
        /// </summary>
        /// <param name="cityId">The city id.</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Root")]
        public ActionResult DeleteCity(int cityId)
        {
            if (cityId < 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            using (var db = new OnLeaveContext())
            {
                var city = db.Cities.FirstOrDefault(c => c.CityId == cityId);
                if (city == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);

                if (db.UtilityBuildings.Any(b => b.CityId == city.CityId))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden, "Съществуващ хотел от града");
                }

                db.Cities.Remove(city);
                db.SaveChanges();
                StaticDataProvider.updateCity = true;
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }


        /// <summary>
        /// Manages City.
        /// </summary>
        /// <param name="city">The city.</param>        
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Root")]
        [HttpPost]
        public ActionResult City(CityModel city)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(city);
            }
            
            using (var db = new OnLeaveContext())
            {
                if (city.CityId == 0)
                {
                    if (db.Cities.Any(c => c.Name.ToLower() == city.Name.ToLower()))
                    {
                        ModelState.AddModelError(city.GetPropertyName(() => city.Name), "Дублиран град");
                        return PartialView(city);
                    }

                    // add city
                    db.Cities.Add(new City
                        {
                            Name = city.Name,
                            KeyWords = city.Name,
                            lat = city.latitude,
                            lon = city.longitude
                        });
                }
                else if (city.CityId > 0)
                {
                    var c = db.Cities.FirstOrDefault(cityDB => cityDB.CityId == city.CityId);
                    if (c == null)
                    {
                        ModelState.AddModelError(string.Empty, "Град липсва");                        
                    }

                    if (db.Cities.Any(cityDB => cityDB.CityId != city.CityId && cityDB.Name.ToLower() == city.Name.ToLower()))
                    {
                        ModelState.AddModelError(city.GetPropertyName(() => city.Name), "Дублиран град");
                    }

                    if (!ModelState.IsValid)
                    {
                        return PartialView(city);
                    }

                    c.Name = city.Name;
                    c.KeyWords = city.Name;
                    c.lon = city.longitude;
                    c.lat = city.latitude;
                }

                db.SaveChanges();
                StaticDataProvider.updateCity = true;
            }

            return RedirectToAction("Cities");
        }

        [ValidateAntiForgeryToken] 
        public ActionResult AddPicture()
        {
            return PartialView("_BuildingPhotoes", new UtilityBuildingModel());
        }

        /// <summary>
        /// Adds the picture of the hotel.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>View to be display.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<ActionResult> AddPicture(UtilityBuildingModel model)
        {
            //ModelState.Clear();
            if (model.Id == 0)
            {
                ModelState.AddModelError(model.GetPropertyName(() => model.PhotoFile), "hotel missing");
            }

            if (model.PhotoFile == null)
            {
                ModelState.AddModelError(model.GetPropertyName(() => model.PhotoFile), "image missing");
            }

            // take just PhotoFile validation
            var errors = ModelState.Where(s => s.Key != model.GetPropertyName(() => model.PhotoFile));
            errors.ToList().ForEach(e => ModelState.Remove(e.Key));

            if (!ModelState.IsValid)
            {
                return PartialView("_BuildingPhotoes", model);
            }

            using (var db = new Models.OnLeaveContext())
            {
                // add pictures
                                
                //var resizedImage = ImageResizer.ImageBuilder.Current.Build(buffer, rSetting);
                //resizedImage.Save(@"C:\\Temp\" + Path.GetFileName(model.PhotoFile.FileName));
                
                using (var ms = new MemoryStream())
                {
                    var job = new ImageResizer.ImageJob(
                        model.PhotoFile,
                        ms,
                        new ImageResizer.Instructions("mode=crop;format=jpg;width=600;height=472;"));
                    
                    await Task.Run(() =>  job.Build());

                    var imageDb = new Photo
                    {
                        KeyWords = null,
                        Name = Path.GetFileName(model.PhotoFile.FileName),
                        Image = ms.ToArray()
                    };                    
                    db.Photos.Add(imageDb);

                    var imageDetail = new UtilityBuildingPhotoDetail
                    {
                        UtilityBuildingId = model.Id,
                        KeyWords = null,
                        PhotoId = imageDb.PhotoId
                    };

                    db.UtilityBuildingPhotoDetails.Add(imageDetail);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex);
                    }

                    // get images
                    model.PhotoIds = db.UtilityBuildingPhotoDetails
                        .Where(pDetail => pDetail.UtilityBuildingId == model.Id)
                        .Select(pDetail => pDetail.PhotoId)
                        .ToList();
                }
            }

            return PartialView("_BuildingPhotoes", model);
        }

        /// <summary>
        /// Removes the photo.
        /// </summary>
        /// <param name="id">The Photo's id.</param>
        /// <returns>Operation result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemovePhoto(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            using (var db = new Models.OnLeaveContext())
            {
                var photoDB = db.Photos
                    .Include(p => p.UtilityBuildingPhotoDetails)
                    .Include(p => p.UtilityBuildingPhotoDetails.Select(pDetail => pDetail.UtilityBuilding))
                    .FirstOrDefault(p => p.PhotoId == id);
                if (photoDB == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                if (photoDB.UtilityBuildingPhotoDetails.Select(pDetail => pDetail.UtilityBuilding).Any(building => building.UserId != User.Identity.GetUserId()))
                {
                    // avoid deleting photoes, which doesn't belongs to User's buildings
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
                }

               
                db.UtilityBuildingPhotoDetails.RemoveRange(photoDB.UtilityBuildingPhotoDetails);
                db.Photos.Remove(photoDB);
                db.SaveChanges();
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Gets the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public FileResult GetPhoto(int photoId, int? width, int? height)
        {
            using (var db = new Models.OnLeaveContext())
            {
                var photo = db.Photos.FirstOrDefault(p => p.PhotoId == photoId);
                if (photo != null)
                {
                    if (width.HasValue && height.HasValue)
                    {
                        // resize image if required
                        var rSetting = new ImageResizer.ResizeSettings(width.Value, height.Value, ImageResizer.FitMode.Stretch, null);
                        rSetting.Anchor = System.Drawing.ContentAlignment.MiddleCenter;
                        rSetting.Scale = ImageResizer.ScaleMode.UpscaleCanvas;                        
                        rSetting.BackgroundColor = System.Drawing.Color.FromArgb(51, 221, 255);                        
                        var resizedImage = ImageResizer.ImageBuilder.Current.Build(photo.Image, rSetting);
                        return File(resizedImage.ToByteArray(), MimeMapping.GetMimeMapping(photo.Name));
                    }
                    else
                    {
                        return File(photo.Image, MimeMapping.GetMimeMapping(photo.Name));
                    }
                }
            }
            
            return null;
        }

        /// <summary>
        /// Instantiates the resize settings.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private ImageResizer.ResizeSettings instantiateResizeSettings(int width, int height)
        {
            var queryString = string.Format("maxwidth={0}&maxheight={1}&quality=90", width, height);
            return new ImageResizer.ResizeSettings(queryString);
        }

        /// <summary>
        /// Edit account action.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EditAccount()
        {
            var user = await UserManager.FindByNameAsync(@User.Identity.GetUserName());
            System.Diagnostics.Debug.Assert(user != null, "user not found");

            var model = new EditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Telephone = user.Telephone
            };
            
            return PartialView(model);
        }

        /// <summary>
        /// Edits the user's account.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAccount(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.GetUserName());
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Telephone = model.Telephone;

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Login");
                }
                else
                {
                    result.Errors.ToList().ForEach(err => ModelState.AddModelError(string.Empty, err));
                }
            }

            return PartialView(model);
        }

        /// <summary>
        /// Get change password view.
        /// </summary>
        /// <returns>View to change password.</returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.GetUserName());                
                System.Diagnostics.Debug.Assert(user != null, "user not found");

                await UserManager.RemovePasswordAsync(user.Id);
                var result = await UserManager.AddPasswordAsync(user.Id, model.Password);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Login");
                }
                else
                {
                    result.Errors.ToList().ForEach(err => ModelState.AddModelError(string.Empty, err));
                }
            }

            return View(model);
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (AuthenticationManager.User.Identity.IsAuthenticated)
            {
                //ViewBag.Cities = StaticDataProvider.Cities.Select(c => new SelectListItem { Text = c.Name, Value = c.CityId.ToString() });
                /*ViewBag.UtilityBuildingTypes = StaticDataProvider.UtilityBuildingTypes.Select(t => new SelectListItem
                {
                    Text = t.Description,
                    Value = t.UtilityBuildingTypeId.ToString()
                });*/

                return View("Account");
            }

            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        //
        // GET: /Account/Login
        //[AllowAnonymous]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Невалиден E-mail или парола.");                    
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user != null)
            {

                // reset password and send to email
                // Generae password token that will be used in the email link to authenticate user
                var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                // Generate the html link sent via email
                string resetLink = "<a href='"
                + Url.Action("ResetPassword", "Account", new { rt = token, email = model.Email }, "http")
                + "'>Reset Password Link</a>";

                // Email stuff
                string subject = "Reset your password for asdf.com";
                string body = "You link: " + resetLink;
                string from = "donotreply@asdf.com";

                MailMessage message = new MailMessage(from, model.Email);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();

                // Attempt to send the email
                try
                {
                    client.Send(message);                    
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Issue sending email: " + e.Message);
                }
            }
            else
            {
                //ModelState.AddModelError("", "The E-Mail Address was not found in our records, please try again!");

                //return View("ResetPasswordEmail");
            }

            return View("ResetPasswordEmail", model);                       
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string rt, string email)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.ReturnToken = rt;
            model.Email = email;
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ViewBag.Message = "User not found";
                    return View(model);
                }

                var resetResponse =  await UserManager.ResetPasswordAsync(
                    userId: user.Id,
                    token: model.ReturnToken,
                    newPassword: model.Password); 
                if (resetResponse.Succeeded)
                {
                    ViewBag.Message = "Successfully Changed";
                }
                else
                {
                    ViewBag.Message = "Something went horribly wrong!";
                }
            }

            return RedirectToAction("Login");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    Telephone = model.Telephone
                };

                var userExist = await UserManager.FindByNameAsync(user.UserName);
                if (userExist != null)
                {
                    ModelState.AddModelError(
                        model.GetPropertyName(() => model.Email),
                        "Email already exists");

                    return View(model);
                }

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Success", "Account");
                }
                else
                {  
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new User() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}