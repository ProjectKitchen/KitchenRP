using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Queries;
using NodaTime;
using NodaTime.Testing;
using Xunit;

namespace KitchenRP.Test.IntegrationTests.Database
{
    [Collection("IntegrationTests")]
    public class ReservationRepositoryTests : IClassFixture<RepositoryFixture>
    {
        public RepositoryFixture Fixture;

        public IClock MockTime = new FakeClock(Instant.FromUnixTimeSeconds(0));

        public ReservationRepositoryTests(RepositoryFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task QueryOnlyReturnsForTheSpecifiedUser()
        {
            using var data = await TestData.Init(Fixture, MockTime);

            var query = new ReservationQuery()
                .ForOwner(await Fixture.Users.FindById(1));

            var onlyUser1Query = await Fixture.Reservations.Query(query);

            Assert.Collection(onlyUser1Query,
                r => Assert.Equal(r, data.Reservation1),
                r => Assert.Equal(r, data.Reservation2)
            );
            Fixture.Database.Ctx.RemoveRange(data.Reservation1, data.Reservation2, data.Reservation3);
        }

        [Fact]
        public async Task QueryReturnsForAllUsers()
        {
            using var data = await TestData.Init(Fixture, MockTime);

            var query = new ReservationQuery();

            var onlyUser1Query = await Fixture.Reservations.Query(query);

            Assert.Collection(onlyUser1Query,
                r => Assert.Equal(r, data.Reservation1),
                r => Assert.Equal(r, data.Reservation2),
                r => Assert.Equal(r, data.Reservation3)
            );
            Fixture.Database.Ctx.RemoveRange(data.Reservation1, data.Reservation2, data.Reservation3);
        }

        [Fact]
        public async Task QueryFiltersBasedOnStatus1and2and4()
        {
            Assert.Empty(Fixture.Reservations.All().Result);
            using var data = await TestData.Init(Fixture, MockTime);


            var query = new ReservationQuery()
                .WithStatuses(new[]
                {
                    await Fixture.ReservationStatuses.ById(1),
                    await Fixture.ReservationStatuses.ById(2),
                    await Fixture.ReservationStatuses.ById(4),
                });

            var result = await Fixture.Reservations.Query(query);

            Assert.Collection(result,
                r => Assert.Equal(data.Reservation1, r),
                r => Assert.Equal(data.Reservation2, r)
            );
        }

        [Fact]
        public async Task QueryFiltersBasedOnStatus1()
        {
            Assert.Empty(Fixture.Reservations.All().Result);
            using var data = await TestData.Init(Fixture, MockTime);


            var query = new ReservationQuery()
                .WithStatuses(new[]
                {
                    await Fixture.ReservationStatuses.ById(1),
                });

            var result = await Fixture.Reservations.Query(query);

            Assert.Collection(result,
                r => Assert.Equal(data.Reservation1, r)
            );
        }

        [Fact]
        public async Task QueryFiltersBasedOnStatus4()
        {
            Assert.Empty(Fixture.Reservations.All().Result);
            using var data = await TestData.Init(Fixture, MockTime);


            var query = new ReservationQuery()
                .WithStatuses(new[]
                {
                    await Fixture.ReservationStatuses.ById(4),
                });

            var result = await Fixture.Reservations.Query(query);

            Assert.Empty(result);
        }

        [Fact]
        public async Task QueryReturnsOverlapOnEnd()
        {
            //       *---- current reservation ----*
            //                           * ------- overlap ------*

            Assert.Empty(Fixture.Reservations.All().Result);
            using var data = await TestData.Init(Fixture, MockTime);


            var query = new ReservationQuery()
                .CollideWith(
                    MockTime.GetCurrentInstant().Plus(Duration.FromHours(11)),
                    MockTime.GetCurrentInstant().Plus(Duration.FromHours(13))
                );

            var result = await Fixture.Reservations.Query(query);

            Assert.Contains(result, r => r.Equals(data.Reservation1));
        }

        [Fact]
        public async Task QueryReturnsOverlapOnStart()
        {
            //            *---- current reservation ----*
            //  * ------- overlap ------*

            Assert.Empty(Fixture.Reservations.All().Result);
            using var data = await TestData.Init(Fixture, MockTime);


            var query = new ReservationQuery()
                .CollideWith(
                    MockTime.GetCurrentInstant().Plus(Duration.FromHours(9)),
                    MockTime.GetCurrentInstant().Plus(Duration.FromHours(11))
                );

            var result = await Fixture.Reservations.Query(query);

            Assert.Contains(result, r => r.Equals(data.Reservation1));
        }

        [Theory]
        [InlineData(10.5, 11)]
        [InlineData(10, 11)]
        [InlineData(11, 12)]
        [InlineData(10, 12)]
        public async Task QueryReturnsInsideOfRange(double startHours, double endHours)
        {
            //       *---- current reservation ----*
            //          * ---- overlap ------*

            Assert.Empty(Fixture.Reservations.All().Result);
            using var data = await TestData.Init(Fixture, MockTime);


            var query = new ReservationQuery()
                .CollideWith(
                    MockTime.GetCurrentInstant().Plus(Duration.FromHours(startHours)),
                    MockTime.GetCurrentInstant().Plus(Duration.FromHours(endHours))
                );

            var result = await Fixture.Reservations.Query(query);

            Assert.Contains(result, r => r.Equals(data.Reservation1));
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(10, 100)]
        [InlineData(0, 17)]
        [InlineData(10, 17)]
        public async Task QueryReturnsOverlapOfRange(int startHours, int endHours)
        {
            //       *---- current reservation ----*
            //   * ----------------------- overlap ------*

            Assert.Empty(Fixture.Reservations.All().Result);
            using var data = await TestData.Init(Fixture, MockTime);


            var query = new ReservationQuery()
                .CollideWith(
                    MockTime.GetCurrentInstant().Plus(Duration.FromHours(startHours)),
                    MockTime.GetCurrentInstant().Plus(Duration.FromHours(endHours))
                );

            var result = await Fixture.Reservations.Query(query);

            Assert.Collection(result,
                r => Assert.Equal(data.Reservation1, r),
                r => Assert.Equal(data.Reservation2, r),
                r => Assert.Equal(data.Reservation3, r)
            );
        }
    }

    internal class TestData : IDisposable
    {
        private RepositoryFixture _fixture;
        private IClock _mockTime;

        public Reservation Reservation1 { get; private set; }
        public Reservation Reservation2 { get; private set; }
        public Reservation Reservation3 { get; private set; }

        private TestData(RepositoryFixture fixture, IClock mockTime)
        {
            _fixture = fixture;
        }

        public static async Task<TestData> Init(RepositoryFixture fixture, IClock mockTime)
        {
            var data = new TestData(fixture, mockTime)
            {
                Reservation1 = await fixture.Reservations.CreateNewReservation(
                    mockTime.GetCurrentInstant().Plus(Duration.FromHours(10)),
                    mockTime.GetCurrentInstant().Plus(Duration.FromHours(12)),
                    await fixture.Resources.FindById(1),
                    await fixture.Users.FindById(1),
                    true
                ),
                Reservation2 = await fixture.Reservations.CreateNewReservation(
                    mockTime.GetCurrentInstant().Plus(Duration.FromHours(15)),
                    mockTime.GetCurrentInstant().Plus(Duration.FromHours(17)),
                    await fixture.Resources.FindById(1),
                    await fixture.Users.FindById(1),
                    true
                ),
                Reservation3 = await fixture.Reservations.CreateNewReservation(
                    mockTime.GetCurrentInstant().Plus(Duration.FromHours(10)),
                    mockTime.GetCurrentInstant().Plus(Duration.FromHours(17)),
                    await fixture.Resources.FindById(2),
                    await fixture.Users.FindById(2),
                    true
                )
            };


            // resource 1 form 15 to +17 hours

            // resource 2 (user 2) form 10 to +17 hours

            data.Reservation3.StatusChanges = new List<StatusChange>
            {
                new StatusChange
                {
                    ChangedAt = mockTime.GetCurrentInstant().Plus(Duration.FromHours(1)), Reason = "changed",
                    PreviousStatus = null,
                    CurrentStatus = await fixture.ReservationStatuses.ById(1),
                    ChangedBy = null
                },
                new StatusChange
                {
                    ChangedAt = mockTime.GetCurrentInstant().Plus(Duration.FromHours(2)), Reason = "changed",
                    PreviousStatus = null,
                    CurrentStatus = await fixture.ReservationStatuses.ById(3),
                    ChangedBy = null
                },
            };

            data.Reservation2.StatusChanges = new List<StatusChange>
            {
                new StatusChange
                {
                    ChangedAt = mockTime.GetCurrentInstant().Plus(Duration.FromHours(1)), Reason = "changed",
                    PreviousStatus = null,
                    CurrentStatus = await fixture.ReservationStatuses.ById(2),
                    ChangedBy = null
                },
                new StatusChange
                {
                    ChangedAt = mockTime.GetCurrentInstant().Plus(Duration.FromHours(2)), Reason = "changed",
                    PreviousStatus = null,
                    CurrentStatus = await fixture.ReservationStatuses.ById(4),
                    ChangedBy = null
                },
                new StatusChange
                {
                    ChangedAt = mockTime.GetCurrentInstant().Plus(Duration.FromHours(3)), Reason = "changed",
                    PreviousStatus = null,
                    CurrentStatus = await fixture.ReservationStatuses.ById(2),
                    ChangedBy = null
                },
            };

            data.Reservation1.StatusChanges = new List<StatusChange>
            {
                new StatusChange
                {
                    ChangedAt = mockTime.GetCurrentInstant().Plus(Duration.FromHours(1)), Reason = "changed",

                    PreviousStatus = null,
                    CurrentStatus = await fixture.ReservationStatuses.ById(1),
                    ChangedBy = null
                },
            };
            data._fixture.Database.Ctx.SaveChanges();
            return data;
        }

        public void Dispose()
        {
            _fixture.Database.Ctx.RemoveRange(_fixture.Database.Ctx.Reservations);
            _fixture.Database.Ctx.SaveChanges();
        }
    }
}