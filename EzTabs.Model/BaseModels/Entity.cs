using System.ComponentModel.DataAnnotations;


namespace EzTabs.Model.BaseModels
{
    public abstract class Entity : Dated
    {
        [Key]
        public Guid Id { get; private set; }
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
