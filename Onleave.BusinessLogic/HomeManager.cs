namespace Onleave.BusinessLogic
{
    using BusinessEntities;
    using Common;
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
    }
}
