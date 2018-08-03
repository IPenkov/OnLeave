namespace OnLeave.Business.Entities
{
    using System.Collections.Generic;

    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SystemTypeId { get; set; }
        public string UrlAddress { get; set; }
        public List<int> PhotoIds { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public RoomPrice[] Prices { get; set; }
        public byte? Rating { get; set; }
    }
}
