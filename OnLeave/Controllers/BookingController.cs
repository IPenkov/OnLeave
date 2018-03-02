using OnLeave.Models;
using OnLeave.Business.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnLeave.Controllers
{
    [RoutePrefix("api/booking")]
    public class BookingController : ApiController
    {
        [HttpGet]
        public List<string> Echo()
        {
            return new List<string>
            {
                "Amazon",
                "Google",
                "Microsoft"
            };
        }

        [HttpPost]
        public List<string> EchoPost()
        {
            return new List<string>
            {
                "Amazon",
                "Google",
                "Microsoft"
            };
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
                            UrlAddress = b.ExternalUrl,
                            PhotoIds = b.UtilityBuildingPhotoDetails.Select(p => p.PhotoId).ToArray()
                        })
                        .ToArray();

                return buildings;
            }            
        }
    }
}
