using Picks.core.Base;

namespace Picks.core.Entities
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public virtual int CategoryId { get; set; }
    }
}
