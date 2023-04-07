using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntitiesComparers
{
    public static class EntityCompararer
    {
        public static bool AreEntitiesPropertiesEqual<Entity>(Entity entity1, Entity entity2, params string[] ignoreProperties) where Entity : class
        {
            if (entity1 != null && entity2 != null)
            {
                Type type = typeof(Entity);
                List<string> ignoredProperiesList = new List<string>(ignoreProperties);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoredProperiesList.Contains(pi.Name))
                    {
                        object entity1PropertyValue = type.GetProperty(pi.Name).GetValue(entity1, null);
                        object entity2PropertyValue = type.GetProperty(pi.Name).GetValue(entity2, null);

                        if (entity1PropertyValue != entity2PropertyValue && (entity1PropertyValue == null || !entity1PropertyValue.Equals(entity2PropertyValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return entity1 == entity2;
        }
    }
}
