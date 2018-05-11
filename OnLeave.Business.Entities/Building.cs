namespace OnLeave.Business.Entities
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SystemTypeId { get; set; }
        public string UrlAddress { get; set; }
        public string PhotoIds { get; set; }
    }
}
