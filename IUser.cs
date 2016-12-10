namespace authorization
{
    public interface IUser
    {
        string Id { get; set; }
        string Name { get; set; }
        string Login { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string[] Roles { get; set; }
    }
}
