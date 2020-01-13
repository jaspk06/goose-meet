using System.Collections.Generic;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }
    }
}