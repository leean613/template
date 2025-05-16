using System;

namespace DTOs.Buffalo.User
{
    public class CreateUserDto
    {
        public string UserId { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string System { get; set; }

        public DateTime DayOfBirth { get; set; }

        public bool Sex { get; set; }

        public string PhoneNumber { get; set; }
    }
}
