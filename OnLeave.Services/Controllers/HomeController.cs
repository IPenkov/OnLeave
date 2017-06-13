using Onleave.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OnLeave.Services.Controllers
{
    public class HomeController : ApiController
    {
        // GET api/<controller>
        [System.Web.Http.Route("api/home/offers")]
        [System.Web.Http.HttpGet]
        public object TopOffers()
        {
            if (!User.Identity.IsAuthenticated) return new { }; 

            var buildings = HomeManager.TopOffers();

            return buildings.Select(b => new
            {
                Id = b.UtilityBuildingId,
                Name = string.Join(" / ", b.UtilityBuildingLocales.Select(l => l.Name)),
                Description = string.Join(System.Environment.NewLine, b.UtilityBuildingLocales.Select(l => l.Description)),
                SystemTypeId = b.SystemTypeId,
                UrlAddress = b.ExternalUrl,
                PhotoIds = b.UtilityBuildingPhotoDetails.Select(p => p.PhotoId).ToList()
            });
        }
    }
}
