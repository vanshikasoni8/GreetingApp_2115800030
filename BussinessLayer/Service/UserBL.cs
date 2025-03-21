using BussinessLayer.Interface;
using Modellayer.Model;
using RepositaryLayer.Interface;
using RepositaryLayer.Service;

namespace BussinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _UserRL;
        public UserBL(IUserRL userRL)
        {
            _UserRL = userRL;
        }

        public bool Registeration(RegisterModel registerModel)
        {
            return _UserRL.Registeration(registerModel);
        }

        public bool Login(LoginModel loginModel)
        {
            return _UserRL.Login(loginModel);
        }

        //string ForgotPassword(string email)
        //{
        //    return _UserRL.ForgotPassword(email);
        //}
        //string ResetPassword(string email, string newPassword, string token)
        //{
        //    return _UserRL.ResetPassword(email, newPassword, token);
        //}

    }
}
