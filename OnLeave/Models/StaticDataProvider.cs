namespace OnLeave.Models
{
    using System.Linq;

    /// <summary>
    /// Static Data Provider
    /// </summary>
    public static class StaticDataProvider
    {
        /// <summary>
        /// Flag to update cities.
        /// </summary>
        public static bool updateCity = false;

        /// <summary>
        /// The cities
        /// </summary>
        private static City[] cities = new City[] { };

        /// <summary>
        /// The room types
        /// </summary>
        private static RoomType[] roomTypes = new RoomType[] { };

        /// <summary>
        /// System types
        /// </summary>
        private static SystemType[] systemTypes = new SystemType[] { };

        /// <summary>
        /// The utility building types
        /// </summary>
        private static UtilityBuildingType[] utilityBuildingTypes = new UtilityBuildingType[] { };

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <value>
        /// The cities.
        /// </value>
        public static City[] Cities
        {
            get
            {
                if (StaticDataProvider.cities.Length == 0 || StaticDataProvider.updateCity)
                {
                    using (var db = new OnLeaveContext())
                    {
                        StaticDataProvider.cities = db.Cities.OrderBy(c => c.Name).ToArray();                            
                    }
                }

                return StaticDataProvider.cities;
            }          
        }

        /// <summary>
        /// Gets the room types.
        /// </summary>
        /// <value>
        /// The room types.
        /// </value>
        public static RoomType[] RoomTypes
        {
            get
            {
                if (StaticDataProvider.roomTypes.Length == 0)
                {
                    using (var db = new OnLeaveContext())
                    {
                        StaticDataProvider.roomTypes = db.RoomTypes.OrderBy(r => r.Order).ToArray();
                    }
                }

                return StaticDataProvider.roomTypes;
            }
        }

        /// <summary>
        /// Gets system types
        /// </summary>
        public static SystemType[] SystemTypes
        {
            get
            {
                if (StaticDataProvider.systemTypes.Length == 0)
                {
                    using (var db = new OnLeaveContext())
                    {
                        StaticDataProvider.systemTypes = db.SystemTypes.OrderBy(s => s.SystemTypeId).ToArray();
                    }
                }

                return StaticDataProvider.systemTypes;
            }
        }

        /// <summary>
        /// Gets the utility building types.
        /// </summary>
        /// <value>
        /// The utility building types.
        /// </value>
        public static UtilityBuildingType[] UtilityBuildingTypes
        {
            get
            {
                if (StaticDataProvider.utilityBuildingTypes.Length == 0)
                {
                    using (var db = new OnLeaveContext())
                    {
                        StaticDataProvider.utilityBuildingTypes = db.UtilityBuildingTypes
                            .ToList()
                            .Select(t => new UtilityBuildingType
                            {
                                UtilityBuildingTypeId = t.UtilityBuildingTypeId,
                                Description = t.Description
                            }).ToArray();
                    }
                }

                return StaticDataProvider.utilityBuildingTypes;
            }            
        }
    
    }
}