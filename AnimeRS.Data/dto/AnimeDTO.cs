namespace AnimeRS.Data.dto
{
    public class AnimeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int Episodes { get; set; }
        public string Status { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
