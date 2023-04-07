using Microsoft.AspNetCore.Mvc;
using ProjectManagementTracker.Models;

namespace ProjectManagementTracker.Controllers
{
    [Route("[controller]")]
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
        public async Task<ActionResult<List<MemberDetails>>> Get()
        {
            var data = _context.MemberDetails.OrderByDescending(m => m.numberOfYearsOfExperience).ToList();
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
        [Route("taskDetails/{memberId}")]
        public async Task<ActionResult<List<ViewTask>>> GetTask(int memberId)
        {
            var memberData = await _context.MemberDetails.FindAsync(memberId);
            var taskData = await _context.TaskDetails.FindAsync(memberId);
            var taskInfo = new ViewTask();
            taskInfo.MemberID = memberData.memberID;
            taskInfo.MemberName = memberData.memberName;
            taskInfo.TaskName = taskData.TaskName;
            taskInfo.Deliverables = taskData.Deliverables;
            taskInfo.TaskStartDate = taskData.TaskStartDate.HasValue ? taskData.TaskStartDate.Value : null;
            taskInfo.TaskEndDate = taskData.TaskEndDate.HasValue ? taskData.TaskEndDate.Value : null;
            taskInfo.projectStartDate = memberData.projectStartDate;
            taskInfo.projectEndDate = memberData.projectEndDate;
            taskInfo.allocationPercentage = memberData.allocationPercentage;

            List<ViewTask> viewTask = new List<ViewTask>();
            viewTask.Add(taskInfo);
            return viewTask;
        }

        [HttpPost]
        [Route("update/{allocationPercentage}")]
        public async Task<ActionResult<MemberDetails>> Update(int memberId, int allocationPercentage)
        {
            var MemberData = await _context.MemberDetails.FindAsync(memberId);
            if (memberId != MemberData.memberID)
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
