namespace OneLayer_ASP_NET_API_Template.Helper;

public static class ApiSystemRouts
{
    public static class Projects
    {
        public const string Base = "api/projects";
        public const string GetAll = Base;
        public const string GetById = Base + "/{id}";
        public const string Add = Base;
        public const string Update = Base + "/{id}";
        public const string Delete = Base + "/{id}";
    }
}
