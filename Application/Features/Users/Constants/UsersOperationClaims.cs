namespace Application.Features.Users.Constants;

public static class UsersOperationClaims
{
    public const string Admin = "Admin";
    public const string ModelStatic = "100";

    public const string Add = ModelStatic+"101";
    public const string Update = ModelStatic + "102";
    public const string Delete = ModelStatic + "103";

    public const string GetAll = ModelStatic + "104";
    public const string GetSingle = ModelStatic + "105";
}