using CartonCaps.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartonCaps.Api.Contexts;

public class ReferralDb : DbContext
{
    public ReferralDb() { }
    public ReferralDb(DbContextOptions<ReferralDb> options) : base(options) { }
    
    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<Referral> Referrals { get; set; }
}