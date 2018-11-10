using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStore.Services.Core
{
    public interface IEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }

        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}
