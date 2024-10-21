using System.ComponentModel.DataAnnotations;


namespace EzTabs.Model.BaseModels
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}
