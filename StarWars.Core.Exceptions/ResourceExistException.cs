using System;
using System.Runtime.Serialization;

namespace StarWars.Core.RuleValidators
{
    [Serializable]
    public class ResourceExistException : Exception
    {
        public string ResourceType { get; private set; }
        public string Resource { get; private set; }

        private static string BuildMessage(string resourceType, string resourceId)
        {
            return $"Given {resourceType} already exists: {resourceId}.";
        }

        public ResourceExistException(string resourceType, string resourceId)
            : base(BuildMessage(resourceType, resourceId))
        {
            ResourceType = resourceType;
            Resource = resourceId;
        }

    }
}