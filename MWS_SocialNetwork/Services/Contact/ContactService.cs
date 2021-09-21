using MWS_SocialNetwork.Data;
using MWS_SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace MWS_SocialNetwork.Services
{
    public class ContactService:IContactService
    {
        private readonly DatabaseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private  string userId;

        public ContactService (DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
           
        }

        public MyContactsViewModel Get()
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var contacts = GetAllContacts();
            var model = new MyContactsViewModel();
            if (contacts != null)
            {
                 model = new MyContactsViewModel
                {
                    ContactsList = contacts,
                    ContatsCount = contacts.Count,
                    ContactsOfRelationship = GetContactsOfRelationships(userId)
                };
            }
            else
            {
                 model = new MyContactsViewModel
                {
                    ContactsList = null,
                    ContatsCount = 0,
                    ContactsOfRelationship = null
                };
            }
           

            return model;
        }


        public List<ContactModel> GetAllContacts()
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var contacts = _context.Set<U2URelationship>().Where(x => x.UserId == userId)
               .Include(x => x.Contact).Include(x => x.RelationshipType).OrderBy(x => x.Contact.FullName)
              .Select(x => new ContactModel
              {
                  Id = x.ContactId,
                  FullName = x.Contact.FullName,
                  ImageUrl = x.Contact.ImageUrl,
                  RelationshipTitle = x.RelationshipType.Title,
                  RelationshipTypeId = x.RelationshipTypeId
              }).ToList();

            return contacts;

        }

        public List<ContactsOfRelationshipModel> GetContactsOfRelationships(string userId)
        {
           // userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var contacts = _context.Set<U2URelationship>()
                .Where(x => x.UserId == userId)
                .Include(x => x.Contact).Include(x => x.RelationshipType)
            .Select(x => new ContactModel
                 {
                     Id = x.ContactId,
                     FullName = x.Contact.FullName,
                     ImageUrl = x.Contact.ImageUrl,
                     RelationshipTitle = x.RelationshipType.Title
                 }).ToList();

            var result =  contacts.GroupBy(x => x.RelationshipTitle)
                .Select(x => new ContactsOfRelationshipModel
                {
                    RelationshipTitle = x.Key,
                    ContactsList = x.ToList(),
                    Count = x.ToList().Count()
            }).ToList();

            var relations = _context.Set<RelationshipType>().OrderBy(x=> x.Order)
                .Select(x => new ContactsOfRelationshipModel
                {
                    Id = x.Id,
                    RelationshipTitle = x.Title,
                    ContactsList = null ,
                    Count = 0 
                }).ToList();

            foreach (var item in result)
            {
                foreach (var i in relations)
                {
                    if (item.RelationshipTitle == i.RelationshipTitle)
                    {
                        i.Count = item.Count;
                        i.ContactsList = item.ContactsList;
                    }
                }
            }
           

            return relations;
        }



        public List<ContactModel> FindContact( string text)
        {
            userId = UserManagerExtensions.GetCurrentUserId(_httpContextAccessor);
            var contacts = _context.Set<U2URelationship>().Where(x => x.Contact.FullName.ToLower().Contains(text.Trim().ToLower()) && x.UserId == userId) 
                .Include(x => x.Contact).Include(x => x.RelationshipType).OrderBy(x => x.RelationshipTypeId)
                 .Select(x => new ContactModel
                 {
                     Id = x.ContactId,
                     FullName = x.Contact.FullName,
                     ImageUrl = x.Contact.ImageUrl,
                     RelationshipTitle = x.RelationshipType.Title
                 }).ToList();
            
            var strangers = _context.Set<ApplicationUser>()
                .Where(x => x.FullName.ToLower().Contains(text.Trim().ToLower()) && !contacts.Select(y => y.Id).Contains(x.Id) && x.Id != userId)
                .OrderBy(x => x.FullName)
                .Select(x => new ContactModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    ImageUrl = x.ImageUrl,
                    RelationshipTitle = "Stranger"
                }).ToList();



            var result = contacts.Concat(strangers);
            return result.ToList();

        }



    }
}
