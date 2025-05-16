using System;

namespace DTOs.Buffalo.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string System { get; set; }

        public bool Sex { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DayOfBirth { get; set; }

        public string UserImageURL { get; set; }
    }
}
