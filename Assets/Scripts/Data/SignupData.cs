
// Helper class for JSON payload
[System.Serializable]
public struct SignupData
{
    public string username;
    public string firstName;
    public string lastName;
    public string email;
    public string password;

    public override string ToString()
    {
        return $"Full name: {firstName} {lastName}\n" +
               $"Email: {email}\n" +
               $"Username: {username}\n" +
               $"Password: {password}";
    }
}