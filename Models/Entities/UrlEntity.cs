namespace SubUrl.Models.Entities
{
    public class UrlEntity : Entity
    {
        public string LongValue { get; set; } = null!;

        public string ShortValue { get; set; } = null!;

        public DateTime DateCreated { get; set; }
        
        public int FollowCount { get; set; }
    }
}