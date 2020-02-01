using System;
using System.Collections.Generic;
using System.Text;

namespace KitchenRP.Domain.Commands
{
    public class UpdateResourceCommand : AddResourceCommand
    {
        public long id { get; set; }
    }
}
