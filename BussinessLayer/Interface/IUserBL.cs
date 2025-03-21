using Modellayer.Model;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        bool Registeration(RegisterModel registerModel);

        string Login(LoginModel loginModel);

        //string ForgotPassword(string email);
        //string ResetPassword(string email, string newPassword, string token);
    }
}
