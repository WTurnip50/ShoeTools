using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppUsersController : ControllerBase
{
   private readonly IUserService _userService;
   
   public  AppUsersController(IUserService userService)
   {
      _userService = userService;
   }
   
   [HttpGet]
   public async Task <ActionResult<Response<List<AppUsersDto>>>> GetAllAsync()
   {
       var response = new Response<List<AppUsersDto>>()
       {
           Data = await _userService.GetAllUsers()
       };
       return Ok(response);
   }

   [HttpGet]
   [Route("{id}")]
   public async Task<ActionResult<Response<AppUsersDto>>> GetById(int id)
   {
       var response = new Response<AppUsersDto>();
       if (!await _userService.UserExists(id))
       {
           response.Errors.Add("User not found");
           return NotFound(response);
       }
       response.Data = await _userService.GetById(id);
       return Ok(response);
   }

   [HttpPost]
   [Route("login")]
   public async Task<ActionResult<Response<AppUsersDto>>> Login([FromBody] AppUsersDto User)
   {
       var response = new Response<AppUsersDto>();
       var userDto = await _userService.LogIn(new AppUsers
       {
           username = User.username , 
           password = User.password
       });
       response.Data = userDto;
       response.Message = "Login Successful";
       return Ok(response);
   }
   
   [HttpPost]
   public async Task<ActionResult<Response<AppUsersDto>>> Post([FromBody] AppUsersDto appUsers)
   {
       var response = new Response<AppUsersDto>
       {
           Data = await _userService.SaveAsync(appUsers),
           Errors = new List<string>()
       };
       return Created($"/api/[controller]/{appUsers.Id}",response);
   }

   [HttpPut]
   public async Task<ActionResult<Response<AppUsersDto>>> Update([FromBody] AppUsersDto appUsers)
   {
       var response = new Response<AppUsersDto>();
       if (!await _userService.UserExists(appUsers.Id))
       {
           response.Errors.Add("User not found");
           return NotFound(response);
       }

       response.Data = await _userService.UpdateAsync(appUsers);
       return Ok(response);
   }

   [HttpDelete]
   [Route("{id}")]
   public async Task<ActionResult<Response<bool>>> Delete(int id)
   {
       var response = new Response<bool>();
       var result = await _userService.DeleteAsync(id);
       response.Data = result;
       return Ok(response);
   }
   
}



/*[HttpPost]
[Route("AppUsers")]
public async Task<ActionResult<Response<AppUsers>>> Login([FromBody] AppUsers user)
{
    var foundUser = await _userRepository.Login(user);
    var response = new Response<AppUsers>();

    if (foundUser == null)
    {
        response.Message = "Credenciales inválidas";
        return Unauthorized(response);
    }

    response.Data = foundUser;
    response.Message = "Login exitoso";
    return Ok(response);
}*/