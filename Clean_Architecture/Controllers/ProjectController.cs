using Clean_Architecture.Applicaiton.Project.Commands.CreateProject;
using Clean_Architecture.Share.Project.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Clean_Architecture.Applicaiton.Project.Commands.DeleteProject.DeleteProject;
using static Clean_Architecture.Applicaiton.Project.Commands.UpdateProject.UpdateProject;
using static Clean_Architecture.Applicaiton.Project.Queries.GetProjectById;

namespace Clean_Architecture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectController(IMediator mediator) => _mediator = mediator;


        // create project
        [HttpPost("create")]
        public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectRequest request)
        {
            var result = await _mediator.Send(new CreateProjectCommand(request));
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("get/{projectId}")]
        public async Task<IActionResult> GetProjectByIdAsync(int projectId)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(projectId));
            return Ok(result);
        }


        // delete project by id
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("delete/{projectId}")]
        public async Task<IActionResult> DeleteProjectByIdAsync(int projectId)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(projectId));
            return Ok(result);
        }


        // delete project by id
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update/{projectId}")]
        public async Task<IActionResult> UpdateProjectByIdAsync(int projectId, [FromBody] ProjectRequest request)
        {
            var result = await _mediator.Send(new UpdateProjectCommand(projectId, request));
            return Ok(result);
        }

    }
}
