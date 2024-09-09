namespace Api_intro_hw_33.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        //relational
        public ICollection<Book> Books { get; set; }
    }
}
