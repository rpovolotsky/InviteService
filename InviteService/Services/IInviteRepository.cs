using InviteService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Services
{
    public interface IInviteRepository
    {
        Task<IEnumerable<Invite>> GetInviteList();
        Task<Invite> GetInvite(int id);
        Task SaveInvite(Invite invite);
        Task SaveInviteRange(IEnumerable<Invite> invites);
        Task<int> GetTodayInvites(int apiId);
        Task<int> GetInvitesSendedByPhonesCount(IEnumerable<string> phones);
    }
}
