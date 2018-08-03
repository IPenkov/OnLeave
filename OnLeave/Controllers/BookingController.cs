using OnLeave.Models;
using OnLeave.Business.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Constant;

namespace OnLeave.Controllers
{
    [RoutePrefix("api/booking")]
    public class BookingController : ApiController
    {
        [Route("buildingtypes"), HttpGet]
        public BuildingType[] GetBuildingTypes()
        {
            using (var db = new OnLeaveContext())
            {
                var data = db.UtilityBuildingTypes.Select(t => new BuildingType
                {
                    Id= t.UtilityBuildingTypeId,
                    Name = t.Description
                }).ToArray();

                return data;
            }
        }

        [Route("facilitytypes"), HttpGet]
        public Object[] GetFacilityTypes()
        {
            using (var db = new OnLeaveContext())
            {
                var data = db.UtilityBuildingFacilityTypes.Select(t => new
                {
                    Id = t.UtilityBuildingFacilityTypeId,
                    t.Name
                }).ToArray();

                return data;
            }
        }


        [Route("topoffers"), HttpPost]
        public Building[] TopOffers()
        {
            using (var db = new OnLeaveContext())
            {
                // take  9 buildings with photos
                var buildings = db.UtilityBuildings
                        .Include(p => p.UtilityBuildingPhotoDetails)
                        .Include(b => b.UtilityBuildingLocales)
                        .Where(b => b.UtilityBuildingPhotoDetails.Count > 0)
                        .OrderByDescending(b => b.SearchRating)
                        .ThenByDescending(b => b.UtilityBuildingId)
                        .Take(9)
                        .ToArray()
                        .Select(b => new Building
                        {
                            Id = b.UtilityBuildingId,
                            Name = string.Join(" / ", b.UtilityBuildingLocales.Select(l => l.Name)),
                            Description = string.Join(System.Environment.NewLine, b.UtilityBuildingLocales.Select(l => l.Description)),
                            SystemTypeId = b.SystemTypeId,
                            UrlAddress = b.UtilityBuildingPhotoDetails.First().Photo.Image.ToString()
                        })
                        .ToArray();

                return buildings;
            }
        }

        /// <summary>
        /// Searches buildings to match criterias.
        /// </summary>
        /// <param name="model">The Search model.</param>
        /// <returns>The result</returns>
        [Route("search"), HttpPost]
        public Building[] SearchBuilding([FromBody]string name)
        {
            System.Diagnostics.Debug.WriteLine(name);

            using (var db = new Models.OnLeaveContext())
            {
                var result = db.UtilityBuildings
                    .Include(b => b.UtilityBuildingPhotoDetails)
                    .Include(b => b.Periods)
                    .Include(b => b.Periods.Select(p => p.RoomAmounts))
                    .Include(b => b.UtilityBuildingLocales)
                    .Where(b => (string.IsNullOrEmpty(name) || b.UtilityBuildingLocales.Any(l => l.Name.ToLower().Contains(name.ToLower()))) && b.UtilityBuildingPhotoDetails.Count > 0)
                    .OrderByDescending(b => b.SearchRating)
                    .Take(51)
                    .ToArray();

                var buildings = result
                    .Select(b => new Building
                    {
                        Id = b.UtilityBuildingId,
                        Name = b.UtilityBuildingLocales.Where(l => l.LocaleId == (int)LocaleTypes.BG).Select(l => l.Name).FirstOrDefault(),
                        Description = b.UtilityBuildingLocales.Where(l => l.LocaleId == (int)LocaleTypes.BG).Select(l => l.Description).FirstOrDefault(),
                        SystemTypeId = b.SystemTypeId,
                        PhotoIds = b.UtilityBuildingPhotoDetails.Select(p => p.PhotoId).ToList(),
                        Longitude = b.lon,
                        Latitude = b.lat,
                        Rating = b.Rating,
                        Prices = new RoomPrice[] { }
                    }).ToArray();

                return buildings;
            }
        }

        [HttpPost, Route("building/{id:int}")]
        public Building GetBuilding([FromUri] int id)
        {
            System.Diagnostics.Debug.WriteLine($"there are coming my friend - {id}");
            Building building = new Building();
            using (var db = new Models.OnLeaveContext())
            {
                var result = db.UtilityBuildings
                    .Include(b => b.Periods)
                    .Include(b => b.Periods.Select( r => r.RoomAmounts))
                    .FirstOrDefault(b => b.UtilityBuildingId == id);
                if (result != null)
                {
                    building.Id = result.UtilityBuildingId;                    

                    // get prices 
                    building.Prices = result.Periods.SelectMany(p => p.RoomAmounts.Select(a => new RoomPrice
                    {
                        PeriodStartDate = p.StartDate ?? DateTime.MinValue,
                        PeriodEndDate = p.EndDate ?? DateTime.MinValue,
                        Amount = a.Amount,
                        RoomTypeId = a.RoomTypeId
                    }).ToArray()).ToArray();
                }
            }

            return building;
        }
    }
}
