namespace OneLayer_ASP_NET_API_Template;

public static class MapperProfile
{
    public static void ConfigMapster(this IServiceCollection services)
    {
        TypeAdapterConfig<Project, GetProjectDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);
    }
}
