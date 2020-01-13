using System;

namespace DatingApp.API.Dtos
{
    //DTO for displaying to list on website purposes
    //does not transfer passwords
    //works because of how a DTO works (check UserForRegisterDto.cs)
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
    }
}