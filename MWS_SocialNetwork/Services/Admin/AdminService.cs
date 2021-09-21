using MWS_SocialNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;
using MWS_SocialNetwork.Models;
using Microsoft.EntityFrameworkCore;
using MWS_SocialNetwork.Settings;

namespace MWS_SocialNetwork.Services
{
    public class AdminService:IAdminService
    {

        private readonly DatabaseContext _context;
        private readonly INotificationService _notificationService;
        public  AdminService(DatabaseContext context , INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public ControlViewModel Get()
        {
            var result = new ControlViewModel
            {
                ActivePolicyCode = GetActivePolicy(),
                PoliciesCode = GetPolicies(),
                Relationships = GetRelationships()
            };
            return result;
        }

        public List<RelationPercentage> GetRelationships()
        {
            var result = new List<RelationPercentage>();

            var relations = _context.Set<RelationshipType>().OrderBy(x => x.Order);

            double all_counts = _context.Set<U2URelationship>().Count();

            foreach(var item in relations)
            {
                double count = _context.Set<U2URelationship>().Where(x => x.RelationshipTypeId == item.Id) .Count();
                var relation = new RelationPercentage {
                    Name = item.Title,
                    Percentage =Convert.ToInt32(Math.Round(100 * count / all_counts))
                };
                result.Add(relation);
            }
            return result;

        }

        public string[] GetPolicies()
        {
            var policies = _context.Set<ConflictResolutionPolicy>().Select(x => x.PolicyCode).ToList();
            return policies.ToArray();
        }

        public string GetActivePolicy()
        {
            var policy = _context.Set<ConflictResolutionPolicy>().Where(x => x.Active).Select(x => x.PolicyCode).ToList();
            return policy.FirstOrDefault();
        }

        public bool ChangePolicy(string code)
        {
            var previousActive = _context.Set<ConflictResolutionPolicy>().Where(x => x.Active).FirstOrDefault();
            previousActive.Active = false;
            _context.Update(previousActive);
            var currentActive = _context.Set<ConflictResolutionPolicy>().Where(x => x.PolicyCode == code).FirstOrDefault();
            currentActive.Active = true;
            _context.Update(currentActive);
           var result =  _context.SaveChanges();
           if (result == 2)
                {
                    var users = _context.Set<ApplicationUser>().Select(x => x.Id).ToList();
                  _notificationService.AddManyNotification("admin", users, code, (int)NotificationCodesEnum.ChangeConflictPolicy);
                    return true;
                }
                else
                return false;

        }

        public bool AddRelationship(string title)
        {
            var entity = new SocialEntity
            {
                EntityTypeId = (int)EntityTypeEnum.Relationship
            };
           _context.Add(entity);
            var newRelationship = new RelationshipType {
                Id = entity.Id,
                Title = title,
                Order = GetRelationships().Count + 1 
            };
            _context.Add(newRelationship);
            var result = _context.SaveChanges();
            if (result == 2)
            {
                var users = _context.Set<ApplicationUser>().Select(x => x.Id).ToList();
                _notificationService.AddManyNotification("admin", users, title, (int)NotificationCodesEnum.AddRelationship);
                return true;
            }  
            else
                return false;

        }
    }
}
