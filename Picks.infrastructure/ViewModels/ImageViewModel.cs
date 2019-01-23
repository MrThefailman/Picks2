using Picks.core.Entities;
using Picks.infrastructure.ViewModels.Base;
using System;

namespace Picks.infrastructure.ViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public virtual int CategoryId { get; set; }
    }
}
