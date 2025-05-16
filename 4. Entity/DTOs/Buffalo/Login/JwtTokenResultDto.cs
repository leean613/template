using System;

namespace DTOs.Buffalo
{
    public class JwtTokenResultDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserImageURL { get; set; }

        public string TokenType { get; set; }

        public string AccessToken { get; set; }

        //public int ExpiresInSeconds { get; set; }

        //public string System { get; set; }

        public bool IsAdmin { get; set; }

    }
}
