namespace BookAPI
{
    public class Book
    {
        public int id {  get; set; }
        public string title { get; set; } = string.Empty;

        public string author { get; set; } = string.Empty;

        public string year { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public string cover { get; set; } = string.Empty;

        public string pages { get; set; } = string.Empty;
    }
}
