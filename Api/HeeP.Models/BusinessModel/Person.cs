using System;

namespace HeeP.Models.BusinessModel
{
    public abstract class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public virtual IdentificationType IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public bool Active { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}