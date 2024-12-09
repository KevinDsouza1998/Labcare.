using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public  interface IContactService
    {
        PageResult<ContactViewModel> GetAll(int pageNumber, int pageSize);
        ContactViewModel GetContactById(int ContactId);
        void UpdateContact(ContactViewModel contact);
        void InsertContact(ContactViewModel contact);
        void DeleteContact(int id);
    }
}
