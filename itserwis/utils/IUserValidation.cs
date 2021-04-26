namespace ItSerwis_Merge_v2
{
    public interface IUserValidation
    {
        bool CheckLog(string encryptedLog, string encryptedPass);
        UserValidation.UserCredentials GetUserCredentials();
        bool ValidateUser(int empID);
    }
}