using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MWS_SocialNetwork.ViewModels;

namespace MWS_SocialNetwork.Services
{
  public  interface IAdminService
    {
        ControlViewModel Get();
        bool ChangePolicy(string code);
        bool AddRelationship(string title);
    }
}
