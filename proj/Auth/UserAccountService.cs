using Entities;
using DAL;
using Microsoft.AspNetCore.Components.Forms;

namespace proj.Auth
{
    public class UserAccountService
    {
        //private List<EntCompany> company = new List<EntCompany>();

        //public UserAccountService()
        //{
        //    company = DALCompany.ShowCompany();
        //}
        public EntCompany? GetByUserName(EntCompany _ec)
        {
            return DALCompany.ShowCompany(_ec.Email, _ec.Password); //editors.FirstOrDefault(e => e.Email == email);
        }
    }
}