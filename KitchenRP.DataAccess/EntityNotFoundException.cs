using System;
using System.Text;

namespace KitchenRP.DataAccess
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; }
        public string SpecialQuery { get; }

        public EntityNotFoundException(string entityName, string specialQuery = "")
        {
            EntityName = entityName;
            SpecialQuery = specialQuery;
        }


        public override string Message => $"Entity not found: {EntityName}"
                                          + (string.IsNullOrWhiteSpace(SpecialQuery)
                                              ? ""
                                              : $"  : While trying to fetch for {SpecialQuery}");
    }
}