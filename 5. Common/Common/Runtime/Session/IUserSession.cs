namespace Common.Runtime.Session
{
    public interface IUserSession
    {
        int UserId { get; }

        int? GetUserId();

        string UserName { get; }
    }
}
