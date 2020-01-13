using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    //DTO (data transfer objects) is a data container to transfer data between layers so that everything can be transferred at once
    //https://softwareengineering.stackexchange.com/questions/171457/what-is-the-point-of-using-dto-data-transfer-objects

    //The User DTOs all need to be somehow connected to the User class
    //this is done using the tool AutoMapper

    //DTO for registering purposes
    public class UserForRegisterDto
    {
        [Required]
        
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
    }
}