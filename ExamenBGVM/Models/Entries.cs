namespace ExamenBGVM.Models
{
    public class Entries
    {
        public int count { get; set; }
        public List<ItemEntry> entries { get; set; }
    }

    public class ItemEntry
    {
        public string? API { get; set; }
        public string? Description { get; set; }
        public string? Auth { get; set; }
        public bool HTTPS { get; set; }
        public string? Cors { get; set; }
        public string? Link { get; set; }
        public string? Category { get; set; }
    }

    public class Categories
    {
        public int count { get; set; }
        public List<string?>? categories { get; set; }
    }

    public class SearchEntries
    {
        public string? api { get; set; }
        public string? description { get; set; }
        public string? auth { get; set; }
        public bool https { get; set; }
        public string? cors { get; set; }
        public string? link { get; set; }
        public string? category { get; set; }
    }
}
