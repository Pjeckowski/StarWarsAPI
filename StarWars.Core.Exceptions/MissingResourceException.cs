using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Core.Exceptions
{
    public class MissingResourceException : Exception
    {
        public string ResourceType { get; private set; }
        public string ResourceName { get; private set; }
        public List<string> Resources { get; private set; }

        private static string BuildMessage(string resourceName, List<string> resources)
        {
            var message = $"Given {resourceName}(s) are missing: ";
            foreach(var resource in resources)
            {
                message += resource;

                if (!resource.Equals(resources.Last()))
                    message += ", ";
            }

            return message + ".";
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
