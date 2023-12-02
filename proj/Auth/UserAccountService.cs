using Entities;
using DAL;
using Microsoft.AspNetCore.Components.Forms;

namespace proj.Auth
{
    public class UserAccountService
    {
        //private List<EntEditor> editors = new List<EntEditor>();

        //public UserAccountService() 
        //{
        //    editors = DALEditor.ShowEditor();
        //}
        public EntCompany? GetByUserName(EntCompany _ec)
        {
            return DALCompany.ShowCompany(_ec.Email, _ec.Password);
        }

    }
}