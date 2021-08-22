namespace ServiceLayer.Responses
{
    public class LoginResponse
    {
        public static readonly LoginResponse InvalidCreds = new LoginResponse { Success = false, Message = "No user matches those credentials." };

        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
