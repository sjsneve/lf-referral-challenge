﻿using CartonCaps.Api.Contexts;
using CartonCaps.Api.Entities;
using CartonCaps.Api.Enums;
using CartonCaps.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace CartonCaps.UnitTests;

public class ReferralServiceTests
{
    [Fact]
    public void GetMemberReturnsCorrectMember()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<MemberService>>();
        var mockReferralDb = CreateMockDbContext();

        var service = new MemberService(mockLogger.Object, mockReferralDb.Object);

        // Act
        var result = service.GetMember(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task GetReferralsByReferralCodeReturnsCorrectReferrals()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ReferralService>>();
        var mockReferralDb = CreateMockDbContext();

        var service = new ReferralService(mockLogger.Object, mockReferralDb.Object);

        // Act
        var result = await service.GetReferralsByReferralCode("ABC123");

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task AddReferralCreatesReferral()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ReferralService>>();
        var mockReferralDb = CreateMockDbContext();

        var service = new ReferralService(mockLogger.Object, mockReferralDb.Object);
        var newMemberId = 2;
        var expectedReferral = new Referral
        {
            Id = 0,
            ReferredMemberId = newMemberId,
            ReferralCode = "ABC123",
            Status = ReferralStatus.Complete,
            CreatedAt = DateTime.MinValue,
            UpdatedAt = DateTime.MinValue
        };

        // Act
        var referral = await service.AddReferral(newMemberId, "ABC123");

        // Assert
        Assert.IsType<Referral>(referral);
        
        referral.CreatedAt = DateTime.MinValue; // Ignore CreatedAt for comparison
        referral.UpdatedAt = DateTime.MinValue; // Ignore UpdatedAt for comparison
        Assert.Equal(expectedReferral, referral);
    }

    private static Mock<ReferralDb> CreateMockDbContext()
    {

        var members = new List<Member>
        {
            new (1, "John", "Doe", "ABC123"),
            new (2, "Jane", "Doe", "ABC124"),
            new (3, "Sam", "Smith", "ABC125")
        }.AsQueryable();

        var referrals = new List<Referral>
        {
            new (1, "ABC123", 2, ReferralStatus.Complete),
            new (2, "ABC123", 3, ReferralStatus.Complete)
        }.AsQueryable();

        var mockMemberSet = CreateMockDbSet(members);
        var mockReferralSet = CreateMockDbSet(referrals);

        var mockContext = new Mock<ReferralDb>();
        mockContext.Setup(c => c.Members).Returns(mockMemberSet.Object);
        mockContext.Setup(c => c.Referrals).Returns(mockReferralSet.Object);

        return mockContext;
    }

    private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        
        mockSet.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(CancellationToken.None))
            .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

        mockSet.As<IQueryable<T>>()
            .Setup(m => m.Provider)
            .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));
        
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        return mockSet;
    }
}