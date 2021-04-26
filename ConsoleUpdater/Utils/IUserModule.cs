namespace ConsoleUpdater
{
    public interface IUserModule
    {
        string Age { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string Username { get; set; }

        void InsertUser();
    }
}