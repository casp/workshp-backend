using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkShop.Data;
using WorkShop.Models;

namespace WorkShop.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<User>>> Get([FromServices]DataContext context)
    {
      var users = await context.Users.AsNoTracking().ToListAsync();
      return Ok(users);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<User>> GetById(int id, [FromServices]DataContext context)
    {
      var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
      return Ok(user);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<User>> Post([FromBody]User user, [FromServices]DataContext context)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return Ok(user);
      }
      catch (Exception e)
      {
        return BadRequest(new { message = e.Message});
      }
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult<User>> Update(int id, [FromBody] User user, [FromServices] DataContext context) 
    {
      if (user.Id != id)
      {
        return NotFound(new { message = "Not Found!!!" });
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        context.Entry<User>(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return Ok(user);
      }
      catch (DbUpdateConcurrencyException)
      {
        return BadRequest(new { message = "Already Modified !!!" });
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<List<User>>> Delete(int id, [FromServices]DataContext context)
    {
      var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
      if (user == null)
      {
        return NotFound(new { message = "Not Found!!!"});
      }

      try
      {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return Ok(user);
      }
      catch
      {
        return BadRequest(new { message = "Not Found!!!"});
      }
    }
  }
}
