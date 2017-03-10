using Onleave.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnLeave.Angular.Web
{
    public class HomeController : ApiController
    {
        // GET api/<controller>
        [Route("api/home/offers")]
        [HttpGet]
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