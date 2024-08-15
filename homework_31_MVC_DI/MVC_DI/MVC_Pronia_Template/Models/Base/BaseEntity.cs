namespace MVC_Pronia_Template.Models.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreateAt { get; set; }= DateTime.Now;
    }
}
