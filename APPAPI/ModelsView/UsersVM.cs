namespace APPAPI.ViewModels {
    public partial class UsersVM {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobileno { get; set; }
        public int? RoleId { get; set; }
        public int? RoleType { get; set; }
        public string Publickey { get; set; }
        public string RegisterCode { get; set; }
        public bool? Status { get; set; }
    }

    public class UserLogin {
        public string Username { get; set; }
        public string Password { get; set; }
    }




}