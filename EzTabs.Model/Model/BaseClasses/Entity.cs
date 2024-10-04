using System.ComponentModel.DataAnnotations;


namespace EzTabs.Model.Model.BaseClasses
{
    public class Entity : Dated
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
