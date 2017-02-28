namespace authorization
{
    public interface IProfileManager
    {
        IUser Find(string userName, string password);
    }
}
