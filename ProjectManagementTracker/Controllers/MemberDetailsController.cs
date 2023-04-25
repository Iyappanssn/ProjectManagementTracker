using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ProjectManagementTracker.Models;

namespace ProjectManagementTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberDetailsController : ControllerBase
    {
        private readonly MyDataContext _context;

        public MemberDetailsController(MyDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("addMember")]
        public async Task<ActionResult<MemberDetails>> POST(MemberDetails memberDetails)
        {
            string[] words = memberDetails.skillset.Split(',');
            if (memberDetails.numberOfYearsOfExperience < 4 || words.Length < 3)
            {
                throw new Exception("Member cannot be part of project");
            }
            else
            {
                _context.MemberDetails.Add(memberDetails);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(POST), new { id = memberDetails.memberID }, memberDetails);
            }
        }

        [HttpGet]
        [Route("memberDetails")]
        public async Task<ActionResult<List<MemberDetails>>> Get ()
        {
            var data = _context.MemberDetails.OrderBy(m => m.memberID).ToList();
            return data;
        }

        [HttpPost]
        [Route("assign-task")]
        public async Task<ActionResult<TaskDetails>> AddTask(TaskDetails taskDetails)
        {
            var data = await _context.MemberDetails.FindAsync(taskDetails.MemberID);
            if (data != null)
            {
                taskDetails.MemberName = data.memberName;
                _context.TaskDetails.Add(taskDetails);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = taskDetails.MemberID }, taskDetails);

            }
            else
            {
                throw new Exception("We cannot add task to the member who is not part of project");
            }

        }

        [HttpGet]
        [Route("taskDetails")]
        public async Task<ActionResult<List<TaskDetails>>> GetTask()
        {
            var taskData = _context.TaskDetails.ToList();
            return taskData;
        }

        [HttpPost]
        [Route("update/{memberID}/{allocationPercentage}")]
        public async Task<ActionResult<MemberDetails> > Update(int memberID, int allocationPercentage)
        {
            var MemberData = await _context.MemberDetails.FindAsync(memberID);
            if (memberID != MemberData.memberID)
            {
                return BadRequest();
            }

            if (MemberData == null)
            {
                return NotFound();
            }

            MemberData.allocationPercentage = allocationPercentage;

            await _context.SaveChangesAsync();
            return MemberData;
        }
    }
}
