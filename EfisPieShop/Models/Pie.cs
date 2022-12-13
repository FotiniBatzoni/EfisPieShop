namespace EfisPieShop.Models
{
    public class Pie
    {
        public int PieId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        public string? AllergyInformation { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public string? ImageThumbnailUrl { get; set; }

        public bool? IsPieOfTheWeek { get; set; }

        public bool? InStock { get; set; }

        public int CategoryId{ get; set; }

        //default! is the null forgiving operator
        //It's used in combination with nullable to declare that Category shouldn't be null
        public Category Category { get; set; } = default!;
    }
}
