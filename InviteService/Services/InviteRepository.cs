using InviteService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Services
{
    public class InviteRepository : IInviteRepository
    {
        private readonly InviteContext _dbContext;
        private readonly IDateTimeService _dateTimeService;
        public InviteRepository(IDateTimeService dateTimeService, InviteContext dbContext)
        {
            _dbContext = dbContext;
            _dateTimeService = dateTimeService;
        }
        public async Task<Invite> GetInvite(int id)
        {
            return await _dbContext.Invites.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Invite>> GetInviteList()
        {
            return await _dbContext.Invites.ToArrayAsync();
        }

        public async Task<int> GetInvitesSendedByPhonesCount(IEnumerable<string> phones)
        {
            var phonesList = phones.ToList();
            return await _dbContext.Invites.Where(r => phonesList.Contains(r.Phone)).CountAsync();
        }

        public async Task<int> GetTodayInvites(int apiId)
        {
            var today = _dateTimeService.GetCurrentDate();
            return await _dbContext.Invites.Where(r => r.ApiId == apiId && r.EventDate >= today.AddDays(-1) && r.EventDate.Date <= today).CountAsync();
        }

        public async Task SaveInvite(Invite invite)
        {
            await _dbContext.AddAsync(invite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveInviteRange(IEnumerable<Invite> invites)
        {
            await _dbContext.AddRangeAsync(invites);
            await _dbContext.SaveChangesAsync();
        }
    }
}
