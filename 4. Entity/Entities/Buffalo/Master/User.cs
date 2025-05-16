using Entities.Interfaces;
using System;

namespace Entities.Buffalo
{
    public class User : IFullEntity
    {
        public Guid Id { get; set; }

        public bool IsAlive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime DayOfBirth { get; set; }

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string UserImageURL { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }
    }
}
