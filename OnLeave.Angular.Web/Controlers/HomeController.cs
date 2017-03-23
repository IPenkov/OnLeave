using Onleave.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OnLeave.Angular.Web
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// Gets the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>        
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetPhoto(int photoId, int? width, int? height)
        {
            var photo = HomeManager.GetPhoto(photoId, width, height);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(photo.Image);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MimeMapping.GetMimeMapping(photo.Name));
            return result;
        }



        // GET api/<controller>
        [Route("api/home/offers")]
        [System.Web.Http.HttpGet]
        public object TopOffers()
        {
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

        // GET api/<controller>
        [Route("api/home/cities")]
        [System.Web.Http.HttpGet]
        public object GetCities()
        {
            return StaticDataProvider.Cities.Select(city => new
            {
                CityId = city.CityId,
                KeyWords = city.KeyWords,
                Name = city.Name,
                Description = city.Description,
                lat = city.lat,
                lon = city.lon
            });
        }

        // GET api/<controller>
        [Route("api/home/buildingtypes")]
        [System.Web.Http.HttpGet]
        public object GetUtilityBuildings()
        {
            return StaticDataProvider.UtilityBuildingTypes.Select(type => new
            {
                UtilityBuildingTypeId = type.UtilityBuildingTypeId,
                Description = type.Description
            });
        }

        // GET api/<controller>
        [Route("api/home/facilitytypes")]
        [System.Web.Http.HttpGet]
        public object GetFacilityTypes()
        {
            return StaticDataProvider.FacilityTypes.Select(type => new
            {
                UtilityBuildingFacilityTypeId = type.UtilityBuildingFacilityTypeId,
                Name = type.Name
            });
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}