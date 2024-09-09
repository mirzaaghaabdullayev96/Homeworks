namespace Api_intro_hw_33.Entities
{
    public class Book :BaseEntity
    {
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int GenreId { get; set; }

        //relational
        public Genre Genre { get; set; }

    }
}
