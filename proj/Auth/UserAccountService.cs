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
        public EntCompany? GetByUserName(EntCompany _ed)
        {
            return DALCompany.ShowCompany(_ed.Email, _ed.Password); //editors.FirstOrDefault(e => e.Email == email);
        }
    }
}