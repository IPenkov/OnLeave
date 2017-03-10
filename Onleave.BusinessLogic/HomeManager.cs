namespace Onleave.BusinessLogic
{
    using OnLeave.Database;
    using System.Data.Entity;
    using System.Linq;

    public static class HomeManager
    {
        public static UtilityBuilding[] TopOffers()
        {
            using (var db = new OnLeave())
            {
                // take  9 buildings with photos
                var buildings = db.UtilityBuildings                    
                    .Include(p => p.UtilityBuildingPhotoDetails)
                    .Include(b => b.UtilityBuildingLocales)
                    .Where(b => b.UtilityBuildingPhotoDetails.Count > 0)
                    .OrderByDescending(b => b.SearchRating)
                    .ThenByDescending(b => b.UtilityBuildingId)
                    .Take(9)                    
                    .ToArray();

                return buildings;
            }            
        }
    }
}
