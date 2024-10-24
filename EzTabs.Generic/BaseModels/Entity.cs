using System.ComponentModel.DataAnnotations;


namespace EzTabs.Generic.BaseModels
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}
