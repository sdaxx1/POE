namespace POE_ST10152183.Models
{
    public class Users
    {
       public  Users() { }
        public Users(string username, string?password, string usertype)
        {
            this.username = username;
            this.password = password;
            this.usertype = usertype;
        }

        public string username {get;set;}
        public string password { get;set;} 
        public string usertype { get;set;}
    }
}
