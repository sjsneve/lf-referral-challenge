using System.Globalization;
using CartonCaps.Api.Contexts;
using CartonCaps.Api.Entities;
using CartonCaps.Api.Enums;
using Microsoft.EntityFrameworkCore;

public static class DataGenerator
{
    public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
        var options = serviceProvider.GetRequiredService<DbContextOptions<ReferralDb>>();
        using var context = new ReferralDb(options);
        if (context.Members.Any())
        {
            return app;
        }

        var member1 = new Member(1, "John", "Doe", "ABC123");
        var member2 = new Member(2, "Jane", "Doe", "ABC124");
        var member3 = new Member(3, "Sam", "Smith", "ABC125");
        context.Members.AddRange(member1, member2, member3);

        var referral1 = new Referral(1, "ABC123", "Jane Doe", ReferralStatus.Complete);
        var referral2 = new Referral(2, "ABC123", "Sam Smith", ReferralStatus.Complete);

        context.Referrals.AddRange(referral1, referral2);

        context.SaveChanges();

        return app;
    }
}