

namespace OneLayer_ASP_NET_API_Template.Helper;

public static class SeeData
{
    public static List<Project> SeedProjects()
    {
        var projects = new List<Project>
        {
            new Project
            {
                Id = 1,
                Name = "Project Alpha",
                Description = "Description for Project Alpha",
                DateCreated = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Project
            {
                Id = 2,
                Name = "Project Beta",
                Description = "Description for Project Beta",
                DateCreated = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Project
            {
                Id = 3,
                Name = "Project Gamma",
                Description = "Description for Project Gamma",
                DateCreated = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        return projects;
    }
}
