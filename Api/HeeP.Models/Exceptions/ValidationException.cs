using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HeeP.Models.Application;

namespace HeeP.Models.Exceptions
{
    /// <summary>
    /// Represents a generic (usually POCO) validation error.
    /// Inspect Errors collection to check for more specific errors. 
    /// Throwers must add an entry with the invalid property name.
    /// </summary>
    public class ValidationException : Exception
    {
        private IDictionary<String, ISet<ValidationError>> errors = new Dictionary<String, ISet<ValidationError>>();

        /// <summary>
        /// Contains additional information about specific error, usually related to properties validation.
        /// </summary>
        public IReadOnlyDictionary<String, ISet<ValidationError>> Errors
        {
            get
            {
                return new ReadOnlyDictionary<String, ISet<ValidationError>>(errors);
            }
        }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception innerException) : base(message, innerException) { }

        public void AddErrors(string errorKey, ISet<ValidationError> errors)
        {
            foreach (var error in errors)
            {
                AddError(errorKey, error);
            }
        }

        /// <summary>
        /// Adds the given error under the errorKey. 
        /// </summary>
        /// <param name="errorKey">Usually a property name</param>
        /// <param name="error">Error type</param>
        public void AddError(String errorKey, ValidationError error)
        {
            if (errors.ContainsKey(errorKey) == false)
            {
                this.errors.Add(errorKey, new HashSet<ValidationError>() { error });
            }
            else
            {
                this.errors[errorKey].Add(error);
            }
        }
    }
}
