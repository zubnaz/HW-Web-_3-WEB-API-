namespace BusinessLogic.ApiModels.Accounts
{
    public class RegisterAccount
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        
    }
}
