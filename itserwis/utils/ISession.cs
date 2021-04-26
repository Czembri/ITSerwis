namespace ItSerwis_Merge_v2
{
    public interface ISession
    {
        void CloseSession();
        void CreateSession(string username, string password);
        bool ManageSessions();
    }
}