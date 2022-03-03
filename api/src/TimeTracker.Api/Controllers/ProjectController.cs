using System.Net;
using Microsoft.AspNetCore.Mvc;
using Storm.Api.Controllers;
using Storm.Api.Dtos;
using Storm.Api.Swaggers.Attributes;
using TimeTracker.Api.Domains.Projects.Actions;
using TimeTracker.Dtos;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Controllers;

public class ProjectController : BaseController
{
    public const string CATEGORY = "Projects";

    public ProjectController(IServiceProvider services) : base(services)
    {
    }

    // projects
    [HttpGet]
    [Route(Urls.LIST_PROJECTS)]
    [Category(CATEGORY)]
    [Response(typeof(Response<List<ProjectItem>>), HttpStatusCode.OK)]
    public async Task<IActionResult> ListProjects()
    {
        return await Action<ListProjectQuery, ListProjectQueryParameter, List<ProjectItem>>(new());
    }

    [HttpPost]
    [Route(Urls.ADD_PROJECT)]
    [Category(CATEGORY)]
    [Response(typeof(Response<ProjectItem>), HttpStatusCode.OK)]
    public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request)
    {
        return await Action<AddProjectCommand, AddProjectCommandParameter, ProjectItem>(new()
        {
            Request = request
        });
    }

    [HttpPut]
    [Route(Urls.UPDATE_PROJECT)]
    [Category(CATEGORY)]
    [Response(typeof(Response<ProjectItem>), HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProject([FromRoute] long projectId, [FromBody] AddProjectRequest request)
    {
        return await Action<UpdateProjectCommand, UpdateProjectCommandParameter, ProjectItem>(new()
        {
            ProjectId = projectId,
            Request = request
        });
    }

    [HttpDelete]
    [Route(Urls.DELETE_PROJECT)]
    [Category(CATEGORY)]
    [Response(typeof(Response), HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProject([FromRoute] long projectId)
    {
        return await Action<DeleteProjectCommand, DeleteProjectCommandParameter>(new()
        {
            ProjectId = projectId,
        });
    }

    // tasks
    [HttpGet]
    [Route(Urls.LIST_TASKS)]
    [Category(CATEGORY)]
    [Response(typeof(Response<List<TaskItem>>), HttpStatusCode.OK)]
    public async Task<IActionResult> ListTasks([FromRoute] long projectId)
    {
        return await Action<ListProjectTaskQuery, ListProjectTaskQueryParameter, List<TaskItem>>(new()
        {
            ProjectId = projectId
        });
    }

    [HttpPost]
    [Route(Urls.CREATE_TASK)]
    [Category(CATEGORY)]
    [Response(typeof(Response<TaskItem>), HttpStatusCode.OK)]
    public async Task<IActionResult> AddTask([FromRoute] long projectId, [FromBody] AddTaskRequest request)
    {
        return await Action<AddProjectTaskCommand, AddProjectTaskCommandParameter, TaskItem>(new()
        {
            ProjectId = projectId,
            Request = request,
        });
    }

    [HttpPut]
    [Route(Urls.UPDATE_TASK)]
    [Category(CATEGORY)]
    [Response(typeof(Response<TaskItem>), HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateTask([FromRoute] long projectId, [FromRoute] long taskId, [FromBody] AddTaskRequest request)
    {
        return await Action<UpdateProjectTaskCommand, UpdateProjectTaskCommandParameter, TaskItem>(new()
        {
            ProjectId = projectId,
            ProjectTaskId = taskId,
            Request = request,
        });
    }

    [HttpDelete]
    [Route(Urls.DELETE_TASK)]
    [Category(CATEGORY)]
    [Response(typeof(Response), HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteTask([FromRoute] long projectId, [FromRoute] long taskId)
    {
        return await Action<DeleteProjectTaskCommand, DeleteProjectTaskCommandParameter>(new()
        {
            ProjectId = projectId,
            ProjectTaskId = taskId
        });
    }
    
    // times
    [HttpPost]
    [Route(Urls.ADD_TIME)]
    [Category(CATEGORY)]
    [Response(typeof(Response<TimeItem>), HttpStatusCode.OK)]
    public async Task<IActionResult> AddTime([FromRoute] long projectId, [FromRoute] long taskId, [FromBody] AddTimeRequest request)
    {
        return await Action<AddTimeCommand, AddTimeCommandParameter, TimeItem>(new()
        {
            ProjectId = projectId,
            ProjectTaskId = taskId,
            Request = request,
        });
    }

    [HttpPut]
    [Route(Urls.UPDATE_TIME)]
    [Category(CATEGORY)]
    [Response(typeof(Response<TimeItem>), HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateTime([FromRoute] long projectId, [FromRoute] long taskId, [FromRoute] long timeId, [FromBody] AddTimeRequest request)
    {
        return await Action<UpdateTimeCommand, UpdateTimeCommandParameter, TimeItem>(new()
        {
            ProjectId = projectId,
            ProjectTaskId = taskId,
            TimeId = timeId,
            Request = request,
        });
    }

    [HttpDelete]
    [Route(Urls.DELETE_TIME)]
    [Category(CATEGORY)]
    [Response(typeof(Response), HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteTime([FromRoute] long projectId, [FromRoute] long taskId, [FromRoute] long timeId)
    {
        return await Action<DeleteTimeCommand, DeleteTimeCommandParameter>(new()
        {
            ProjectId = projectId,
            ProjectTaskId = taskId,
            TimeId = timeId
        });
    }
}