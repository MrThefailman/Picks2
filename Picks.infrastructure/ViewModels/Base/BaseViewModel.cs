using System;

namespace Picks.infrastructure.ViewModels.Base
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string DisplayDate
        {
            get
            {
                return Created.ToString("yyyy-MM-dd");
            }
        }
    }
}
