using Microsoft.AspNetCore.Mvc;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;
using ShoeTools.Core.Http;

namespace ShoeTools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppUsersController : ControllerBase
{
   private readonly IUserRepository _userRepository;
   
   public  AppUsersController(IUserRepository userRepository)
   {
      _userRepository = userRepository;
   }
   
   [HttpGet]
   public async Task <ActionResult<Response<List<AppUsers>>>> GetAllAsync()
   {
       var users = await _userRepository.GetAllAsync();
       var response = new Response<List<AppUsers>>();
       response.Data = users;
       return Ok(response);
   }

   [HttpGet]
   [Route("{id}")]
   public async Task<ActionResult<Response<AppUsers>>> GetById(int id)
   {
       var user = await _userRepository.GetUserById(id);
       var response = new Response<AppUsers>();
       response.Data = user;
       if (user == null)
       {
           response.Message = "AppUsers not found";
           return NotFound(response);
       }
       return Ok(response);
   }

   [HttpPost]
   public async Task<ActionResult<Response<AppUsers>>> Post([FromBody] AppUsers appUsers)
   {
       appUsers = await _userRepository.SaveAsync(appUsers);
       var response = new Response<AppUsers>();
       response.Data = appUsers;
        
       return Created($"/api/[controller]/{appUsers.Id}",response);
   }

   [HttpPut]
   public async Task<ActionResult<Response<AppUsers>>> Update([FromBody] AppUsers appUsers)
   {
       var result = await _userRepository.UpdateAsync(appUsers);
       var response = new Response<AppUsers> { Data = result };
       return Ok(response);
   }

   [HttpDelete]
   [Route("{id}")]
   public async Task<ActionResult<Response<bool>>> Delete(int id)
   {
       var response = new Response<bool>();
       var result = await _userRepository.DeleteAsync(id);
       response.Data = result;
       if (result == null)
       {
           response.Message = "AppUsers not found";
           return NotFound(response);
       }
       return Ok(response);
   }
}