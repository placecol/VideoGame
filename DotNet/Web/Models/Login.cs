namespace Riddley.VideoGame.Web.Models
{
    public class Login
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool Persistent { get; set; }
    }
}