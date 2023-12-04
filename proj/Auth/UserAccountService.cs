using Entities;
using DAL;
using Microsoft.AspNetCore.Components.Forms;

namespace proj.Auth
{
    public class UserAccountService
    {
        private List<EntCompany> companies = new List<EntCompany>();

        public UserAccountService()
        {
            companies = DALCompany.GetCompanyUsingLinq();
        }
        public EntCompany? GetByUserName(EntCompany _ec)
        {
            return DALCompany.ShowCompany(_ec.Email, _ec.Password);
        }

    }
}