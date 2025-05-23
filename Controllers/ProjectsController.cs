﻿using OneLayer_ASP.NET_API_Template;

namespace OneLayer_ASP_NET_API_Template.Controllers;

[ApiController]
public class ProjectsController(AppDbContext _context, ILogger<ProjectsController> _logger) : ControllerBase
{

    #region Project Endpoints

    /// <summary>
    /// Retrieves all projects with pagination.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paginated list of projects.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.Projects.GetAll)]
    public async Task<IActionResult> GetAllProject(int pageNumber = 1, int pageSize = 10)
    {
        var projects = _context.Projects
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<GetProjectDto>();

        var pagedResult = await PagedResponse.GetPagedResultAsync<GetProjectDto>(projects, pageSize, pageNumber);

        var response = BaseResponse<PagedResult<GetProjectDto>>.SuccessResponse("Projects retrieved successfully", pagedResult);
        _logger.LogInformation("GetAllProject executed successfully with {Count} projects on page {PageNumber}", pagedResult.TotalItems, pageNumber);
        return Ok(response);
    }


    /// <summary>
    /// Retrieves a project by its ID.
    /// </summary>
    /// <param name="id">The ID of the project.</param>
    /// <returns>The project with the specified ID.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiSystemRouts.Projects.GetById)]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await _context.Projects
            .ProjectToType<GetProjectDto>()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (project is null)
        {
            _logger.LogWarning("GetProjectById failed. No project found with Id: {Id}", id);
            return NotFound($"no project with Id {id}");
        }

        _logger.LogInformation("GetProjectById executed successfully for Id: {Id}", id);
        return Ok(project);
    }

    /// <summary>
    /// Adds a new project.
    /// </summary>
    /// <param name="projectDto">The project data transfer object.</param>
    /// <returns>The created project.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost(ApiSystemRouts.Projects.Add)]
    public async Task<IActionResult> AddProject(AddProjectDto projectDto)
    {
        var newProject = projectDto.Adapt<Project>();
        await _context.Projects.AddAsync(newProject);
        await _context.SaveChangesAsync();

        _logger.LogInformation("AddProject executed successfully for Name: {Name}", projectDto.Name);
        return Created();
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">The ID of the project to update.</param>
    /// <param name="projectDto">The updated project data.</param>
    /// <returns>No content if successful.</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut(ApiSystemRouts.Projects.Update)]
    public async Task<IActionResult> UpdateProject(int id, UpdateProjectDto projectDto)
    {
        if (id != projectDto.Id)
        {
            _logger.LogWarning("UpdateProject failed. Invalid Id: {Id}", id);
            return BadRequest("invalid Id");
        }

        var project = await _context.Projects.FindAsync(id);

        if (project is null)
        {
            _logger.LogWarning("UpdateProject failed. No project found with Id: {Id}", id);
            return NotFound($"no project with Id {id}");
        }

        project.Name = projectDto.Name;
        project.Description = projectDto.Description;

        await _context.SaveChangesAsync();
        _logger.LogInformation("UpdateProject executed successfully for Id: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a project by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <returns>A success or error message.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(ApiSystemRouts.Projects.Delete)]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var rows = await _context.Projects.Where(x => x.Id == id).ExecuteDeleteAsync();

        if (rows > 0)
        {
            _logger.LogInformation("DeleteProject executed successfully for Id: {Id}", id);
            return Ok("Project Deleted Successfully");
        }

        _logger.LogWarning("DeleteProject failed. No project found with Id: {Id}", id);
        return NotFound($"no project with Id {id}");
    }

    #endregion
}
