using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarWars.Core.Exceptions
{
    public class MissingResourceException : BusinessRuleException
    {
        public string ResourceType { get; private set; }
        public string ResourceName { get; private set; }
        public List<string> Resources { get; private set; }

        private static string BuildMessage(string resourceName, List<string> resources)
        {
            var sb = new StringBuilder();            
            sb.Append($"Given {resourceName}(s) are missing: ");
            foreach(var resource in resources)
            {
                sb.Append(resource);

                if (!resource.Equals(resources.Last()))
                    sb.Append(", ");
            }            
            return sb.Append(".").ToString();
        }

        public MissingResourceException(string resourceType, string resourceName, List<string> resources)
            :base(BuildMessage(resourceName, resources))
        {
            ResourceType = resourceType;
            ResourceName = resourceName;
            Resources = resources;
        }
    }
}
