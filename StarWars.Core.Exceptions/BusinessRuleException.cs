using System;

namespace StarWars.Core.Exceptions
{
    public class BusinessRuleException : Exception //Marker class for catching business exceptions
    {
        public BusinessRuleException(string message) : base(message)
        {
        }
    }
}
