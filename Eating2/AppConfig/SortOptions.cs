namespace Eating2.AppConfig
{
    public enum SortDirections
    {
        Asc,
        Desc
    }

    public class SortOptions
    {
        public SortOptions()
        {
            Field = string.Empty;
            Direction = SortDirections.Asc;
        }
        public string Field { get; set; }
        public SortDirections Direction { get; set; }
    }
}
