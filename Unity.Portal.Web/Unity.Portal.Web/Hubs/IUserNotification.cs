namespace Unity.Portal.Web.Hubs
{
    public interface IUserNotification
    {
        Task ReceiveNotification(string user, string message);
    }
}