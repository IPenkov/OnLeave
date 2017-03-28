namespace BusinessEntities
{
    public class SearchBuilding
    {
        public string Name { get; set; }
        public int[] TopFacilities { get; set; }
        public int? CityId { get; set; }
        public int? UtilityBuildingTypeId { get; set; }
        public byte? Rating { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
    }
}
