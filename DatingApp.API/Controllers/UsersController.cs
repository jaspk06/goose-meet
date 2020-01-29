using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize] //require authorization to proceed
    [Route("api/[controller]")] //redirects to the appropriate contollers e.g api/Users
    [ApiController] //makes use of the ApiController
    public class UsersController : ControllerBase
    {
        //DatingRepository holds the functions for managing the users
        private readonly IDatingRepository _repo;
        //Mapper is used to connect the appropriate DTOs to the class
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            //gather all the information about the users
            var users = await _repo.GetUsers();

            //filters out the variables that need to be hidden using
            //UserForListDto which is mapped using mapper
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            //check to see if the user has the correct token and is trying to do a put request on that id
            //if the id does not match the parsed id found in the token, it is unauthorized
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            //if cannot save to database throw exception
            throw new Exception($"Updating user {id} failed on save");
        }
    }
}