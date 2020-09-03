using System;

namespace StarWars.Core.Exceptions
{
    [Serializable]
    public class ResourceExistException : BusinessRuleException
    {
        public string ResourceType { get; private set; }
        public string Resource { get; private set; }

        private static string BuildMessage(string resourceType, string resourceId)
        {
            return $"Given {resourceType} already exists and cannot be created: {resourceId}";
        }

        public ResourceExistException(string resourceType, string resourceId)
            : base(BuildMessage(resourceType, resourceId))
        {
            ResourceType = resourceType;
            Resource = resourceId;
        }

    }
}