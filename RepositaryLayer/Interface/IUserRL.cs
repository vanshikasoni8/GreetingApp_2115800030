using Modellayer.Model;

namespace RepositaryLayer.Interface
{
    public interface IUserRL
    {
        public bool Registeration(RegisterModel registerModel);

        public string Login(LoginModel loginModel);

        //public string ForgotPassword(string email);
        //public string ResetPassword(string email, string newPassword, string token);
    }
}
