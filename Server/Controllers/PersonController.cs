using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly Context _context;

        private bool _specialFeatureIsTurnOn = true;

        public PersonController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var people = await _context.People.Include(p => p.Role).ToListAsync();
            return Ok(people);
        }


        [HttpGet("LogIn")]
        public IActionResult LogIn(string name, string password)
        {
            Person person = _context.People.Include(p => p.Role).FirstOrDefault(p => p.Name == name && p.Password == password);

            if(person != null)
            {
                return Ok(person);  
            }
            else
            {
                return BadRequest(person);
            }
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            Person person = _context.People.Include(p => p.Role).FirstOrDefault(p => p.Id == changePasswordRequest.UserId && p.Password == changePasswordRequest.OldPassword);

            if(person != null)
            {
                if(person.Role.Name == "Admin")
                {
                    person.Password = changePasswordRequest.NewPassword;

                    _context.People.Update(person);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    return BadRequest("Password cannot be changed for a user who is not an administrator");
                }
            }
            else
            {
                return BadRequest("Invalid password");
            }
        }

        [HttpPost("BlockUser")]
        public IActionResult BlockUser(BlockUserRequest blockUserRequest)
        {
            Person admin = _context.People.FirstOrDefault(p => p.Id == blockUserRequest.AdminId);

            if (admin != null)
            {
                Person person = _context.People.FirstOrDefault(p => p.Id == blockUserRequest.UserId);

                if(person != null)
                {
                    person.IsBlocked = true;

                    _context.People.Update(person);

                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    return BadRequest("Person not found");
                }
            }
            else
            {
                return BadRequest("Admin not found");
            }
        }

        [HttpPost("RegisterWithPassword")]
        public IActionResult RegisterWithPassword(RegisterRequest registerRequest)
        {
            if (_context.People.Any(p => p.Name == registerRequest.Name))
            {
                return BadRequest("User already in database");
            }

            Role userRole = _context.Roles.FirstOrDefault(r => r.Name == "User");
            if (userRole == null)
            {
                return BadRequest("Role not found");
            }

            Person person = new Person(registerRequest.Name, registerRequest.Password, userRole);

            _context.People.Add(person);
            _context.SaveChanges();

            return Ok("Success");
        }

        [HttpPost("RegisterWithoutPassword")]
        public IActionResult RegisterWithoutPassword(RegisterRequest registerRequest)
        {
            if (_context.People.Any(p => p.Name == registerRequest.Name))
            {
                return BadRequest("User already in database");
            }

            Role userRole = _context.Roles.FirstOrDefault(r => r.Name == "User");
            if (userRole == null)
            {
                return BadRequest("Role not found");
            }

            Person person = new Person(registerRequest.Name, userRole);

            _context.People.Add(person);
            _context.SaveChanges();

            return Ok("Success");
        }

        [HttpPost("RegisterWithPasswordAdmin")]
        public IActionResult RegisterWithPasswordAdmin(RegisterRequest registerRequest)
        {
            if (_context.People.Any(p => p.Name == registerRequest.Name))
            {
                return BadRequest("User already in database");
            }

            Role adminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (adminRole == null)
            {
                return BadRequest("Role not found");
            }

            Person person = new Person(registerRequest.Name, registerRequest.Password, adminRole);

            _context.People.Add(person);
            _context.SaveChanges();

            return Ok("Success");
        }

        [HttpPost("SwitchSpecialFeature")]
        public IActionResult SwitchSpecialFeature(Guid adminId)
        {
            Person admin = _context.People.FirstOrDefault(p => p.Id == adminId && p.Role.Name == "Admin");

            if (admin != null)
            {
                _specialFeatureIsTurnOn = !_specialFeatureIsTurnOn;

                return Ok("Sucess");
            }
            else
            {
                return BadRequest("Admin not found");
            }

        }

        [HttpGet("GetSpecialFeatureInfo")]
        public IActionResult GetSpecialFeatureInfo()
        {
            return Ok(_specialFeatureIsTurnOn);
        }

    }
}
