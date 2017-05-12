using System;
using System.ComponentModel.DataAnnotations;

namespace HeeP.Models.BusinessModel
{
    public abstract class BaseEntity
    {
        public string AddedUser { get; set; }
        public DateTime AddedDate { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
