using System.Collections.Generic;

public class UserData
{
    public int ID;
    public string Username;
    public string Password;

    public UserData() { }
    public UserData(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

