using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Mapper;
using Reddit.Models;


namespace Reddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ApplcationDBContext _context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Community>>> GetCommunities()
        {
            return await _context.Communities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Community>> GetCommunity(int id)
        {
            var community = await _context.Communities.FindAsync(id);

            if(community == null)
            {
                return NotFound();
            }

            return community;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommunity(CreateCommunityDto communityDto)
        {
            var community = new Community()
            {
                Name = communityDto.Name,
                ID = communityDto.ID,
                Owner = communityDto.Owner
            };

            await _context.Communities.AddAsync(community);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunity(int id)
        {
            var community = await _context.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }

            _context.Communities.Remove(community);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
