using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Mapper;
using Reddit.Models;

namespace Reddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplcationDBContext _context;

        public UserController(ApplcationDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateUserDto createAuthorDto)
        {
            var author = new User
            {
                Name = createAuthorDto.Name
            };

            await _context.Users.AddAsync(author);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAuthors()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> JoinCommunity(int userID,int communityID)
        {
            var community = await _context.Communities.FindAsync(communityID);

            if (community == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(userID);
            if (user == null)
            {
                return NotFound();
            }

            user.subscribedCommunities.Add(community);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}