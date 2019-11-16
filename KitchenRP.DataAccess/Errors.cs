using System;

namespace KitchenRP.DataAccess
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string entityName, string search = "getAll")
            : base($"{entityName} not found: search was {search}")
        {
        }
    }
}