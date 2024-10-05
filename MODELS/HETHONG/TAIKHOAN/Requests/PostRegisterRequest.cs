namespace MODELS.HETHONG.TAIKHOAN.Requests
{
    public class PostRegisterRequest
    {
        public Guid Id { get; set; }

        public string Fullname { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? RePassword { get; set; }

        public string PhoneNumber { get; set; } = null!;
    }
}
