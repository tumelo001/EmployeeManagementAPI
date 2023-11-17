namespace Employee_Management.Models
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
