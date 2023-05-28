namespace POE_ST10152183.Models
{
    public class Farmers
    {
        public Farmers()
        {
        }

        public Farmers(int farmerId, string fUserName, string fPassword, string fFirstName, string fLastName, string fPhoneNumber, string fEmailAddress)
        {
            FarmerId = farmerId;
            FUserName = fUserName;
            FPassword = fPassword;
            FFirstName = fFirstName;
            FLastName = fLastName;
            FPhoneNumber = fPhoneNumber;
            FEmailAddress = fEmailAddress;
        }

        public int FarmerId { get; set; }
        public string FUserName { get; set; }
        public string FPassword { get; set; }
        public string FFirstName { get; set; }
        public string FLastName { get; set; }
        public string FPhoneNumber { get; set; }
        public string FEmailAddress { get; set; }
    }
}
