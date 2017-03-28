namespace Onleave.BusinessLogic
{
    using BusinessEntities;
    using Common;
    using Common.Constant;
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

        /// <summary>
        /// Gets a photo.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>        
        public static PhotoEntity GetPhoto(int photoId, int? width, int? height)
        {
            using (var db = new OnLeave())
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
                        return new PhotoEntity { Image = resizedImage.ToByteArray(), Name = photo.Name };
                    }

                    return new PhotoEntity { Image = photo.Image, Name = photo.Name };

                }
            }
            
            return null;
        }

        /// <summary>
        /// Searches buildings to match criterias.
        /// </summary>
        /// <param name="model">The Search model.</param>
        /// <returns>The result</returns>
        public static BusinessEntities.UtilityBuildingDTO[] SearchBuilding(SearchBuilding search)
        {
            //int[] facilitiesIds = model.Facilities.Concat(model.TopFacilities).Where(f => f.Selected)
            //    .Select(f => f.FacilityTypeId).ToArray();

            if (search.TopFacilities == null) search.TopFacilities = new int[] { };

            using (var db = new OnLeave())
            {
                var result = db.UtilityBuildings
                    .Include(b => b.UtilityBuildingPhotoDetails)
                    .Include(b => b.Periods)
                    .Include(b => b.Periods.Select(p => p.RoomAmounts))
                    .Include(b => b.UtilityBuildingLocales)
                    .Where(b =>
                        (search.Name == null || search.Name.Trim().Length == 0 || b.UtilityBuildingLocales.Any(l => l.Name.ToLower().Contains(search.Name.ToLower())))
                        && b.UtilityBuildingPhotoDetails.Count > 0)
                    .Where(b => !search.CityId.HasValue || b.CityId == search.CityId)
                    .Where(b => !search.UtilityBuildingTypeId.HasValue || b.UtilityBuildingTypeId == search.UtilityBuildingTypeId) // UtilityBuildingTypeId
                    .Where(b => search.TopFacilities.All(fID => b.UtilityBuidingFacilityDetails.Any(ft => ft.UtilityBuildingFacilityTypeId == fID)))
                    .Where(b => !search.Rating.HasValue || b.Rating == search.Rating)
                    .Where(b => (!search.MinAmount.HasValue && !search.MaxAmount.HasValue)
                        || b.Periods.SelectMany(p => p.RoomAmounts).Any(a => (!search.MinAmount.HasValue || search.MinAmount <= a.Amount) && (!search.MaxAmount.HasValue || search.MaxAmount >= a.Amount)))
                    .OrderByDescending(b => b.SearchRating)
                    .Take(51)
                    .ToArray();

                var buildings = result
                    .Select(b => new BusinessEntities.UtilityBuildingDTO
                    {
                        Id = b.UtilityBuildingId,
                        Name = b.UtilityBuildingLocales.Where(l => l.LocaleId == (int)LocaleTypes.BG).Select(l => l.Name).FirstOrDefault(),
                        Description = b.UtilityBuildingLocales.Where(l => l.LocaleId == (int)LocaleTypes.BG).Select(l => l.Description).FirstOrDefault(),
                        UrlAddress = b.ExternalUrl,
                        Rating = b.Rating ?? 0,
                        SystemTypeId = b.SystemTypeId,
                        Size = b.Size,
                        PhotoIds = new System.Collections.Generic.List<int>() { b.UtilityBuildingPhotoDetails.First().PhotoId }.ToArray(),
                        //Periods = b.Periods.OrderBy(p => p.RoomAmounts.Min(a => a.Amount)).Take(1).ToList()
                    }).ToArray();

                return buildings;
            }
        }
    }
}
