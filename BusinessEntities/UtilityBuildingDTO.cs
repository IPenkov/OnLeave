using System.Collections.Generic;

namespace BusinessEntities
{
    public class UtilityBuildingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlAddress { get; set; }
        public byte Rating { get; set; }
        public int SystemTypeId { get; set; }
        public int? Size { get; set; }
        public int[] PhotoIds { get; set; }        
    }
}
