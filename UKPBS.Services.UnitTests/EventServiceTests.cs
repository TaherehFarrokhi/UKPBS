using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using UKPBS.Domain.Entities.Events;
using UKPBS.Domain.Entities.ParliamentMembers;
using UKPBS.Services.ApiClients;
using UKPBS.Services.Exceptions;
using UKPBS.Services.Requests;
using UKPBS.Services.Responses;
using UKPBS.Services.UnitTests.Helpers;
using UKPBS.Services.ViewModels;

namespace UKPBS.Services.UnitTests
{
    [TestFixture]
    public class EventServiceTests
    {
        private Mock<IEventApiClient> _eventApiClientMock;
        private Mock<IMemberApiClient> _memberApiClientMock;
        private Mock<ILogger<EventService>> _loggerMock;
        private EventService _subject;

        [SetUp]
        public void Setup()
        {
            _eventApiClientMock = new Mock<IEventApiClient>();
            _memberApiClientMock = new Mock<IMemberApiClient>();
            _loggerMock = new Mock<ILogger<EventService>>();
            _subject= new EventService(_eventApiClientMock.Object, _memberApiClientMock.Object, _loggerMock.Object);
        }

        #region GetEventsAsync

        [Test]
        public async Task GetEventsAsync_ShouldReturnsExpectedEvents_WhenEventApiCLientReturnsEventsCorrectly()
        {
            // Arrange
            var dateTime = new DateTime(2019, 6,11);
            var events = new List<Event>
            {
                new Event
                {
                    Id =  1,
                    StartDate = dateTime.AddDays(-3),
                    StartTime = dateTime.AddDays(-3).ToShortTimeString(),
                    EndDate = dateTime.AddDays(-3),
                    EndTime = dateTime.AddDays(-3).AddHours(2).ToShortTimeString(),
                    Description = "Event1 Description",
                    Category = "Category 1",
                    House = "Commons",
                    Type = "Main Chamber"
                },
                new Event
                {
                    Id =  2,
                    StartDate = dateTime.AddDays(-2),
                    Description = "Event2 Description",
                    Category = "Category 1",
                    House = "Commons",
                    Type = "Main Chamber"
                }
            };
            _eventApiClientMock.Setup(x => x.GetEventsAsync(It.IsAny<EventRequest>())).ReturnsAsync(events);
            var expectedResponse = new ClientResponse<IEnumerable<EventViewModel>>
            {
                Success = true,
                Payload = new List<EventViewModel>
                {
                    new EventViewModel
                    {
                        Id = 1,
                        Description = "Event1 Description",
                        StartDate = dateTime.AddDays(-3),
                        EndDate = dateTime.AddDays(-3).AddHours(2)
                    },
                    new EventViewModel
                    {
                        Id = 2,
                        Description = "Event2 Description",
                        StartDate = dateTime.AddDays(-2)
                    }
                }
            };

            // Act
            var response = await _subject.GetEventsAsync(It.IsAny<EventRequest>());
            
            // Assert
            UnitTestHelper.AssertEqualSerialization(expectedResponse, response, "expected response to match");
        }

        [Test]
        public async Task GetEventsAsync_ShouldReturnsFailedResponse_WhenEventApiThrowError()
        {
            // Arrange
            _eventApiClientMock.Setup(x => x.GetEventsAsync(It.IsAny<EventRequest>()))
                .ThrowsAsync(new ExternalServiceException("Error", new Exception()));
            var expectedResponse = new ClientResponse<IEnumerable<EventViewModel>>
            {
                ErrorMessage = "Error",
                Success = false
            };

            // Act
            var response = await _subject.GetEventsAsync(new EventRequest());

            // Assert
            UnitTestHelper.AssertEqualSerialization(expectedResponse, response, "expected response to match");
        }

        #endregion

        #region GetEventDetailAsync

        [Test]
        public async Task GetEventDetailAsync_ShouldReturnsFailedResponse_WhenEventClientApiThrowError()
        {
            // Arrange
            _eventApiClientMock.Setup(x => x.GetEventAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ExternalServiceException("Error", new Exception()));
            var expectedResponse = new ClientResponse<EventDetailViewModel>
            {
                ErrorMessage = "Error",
                Success = false
            };

            // Act
            var response = await _subject.GetEventDetailAsync(1, DateTime.Today);

            // Assert
            UnitTestHelper.AssertEqualSerialization(expectedResponse, response, "expected response to match");
        }

        [Test]
        public async Task GetEventDetailAsync_ShouldReturnsFailedResponse_WhenEventIsNotDefinedInCalendar()
        {
            // Arrange
            var dateTime = new DateTime(2019, 6, 11); ;
            _eventApiClientMock.Setup(x => x.GetEventAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync((Event)null);
            
            var expectedResponse = new ClientResponse<EventDetailViewModel>
            {
                ErrorMessage = "Event Not Found.",
                Success = false
            };

            // Act
            var response = await _subject.GetEventDetailAsync(1, DateTime.Today);

            // Assert
            UnitTestHelper.AssertEqualSerialization(expectedResponse, response, "expected response to match");
        }

        [Test]
        public async Task GetEventDetailAsync_ShouldReturnsFailedResponse_WhenMemberApiThrowError()
        {
            // Arrange
            var dateTime = new DateTime(2019, 6, 11); ;
            _eventApiClientMock.Setup(x => x.GetEventAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new Event
                {
                    Id = 1,
                    StartDate = dateTime,
                    StartTime = dateTime.AddHours(1).ToShortTimeString(),
                    EndDate = dateTime,
                    EndTime = dateTime.AddHours(3).ToShortTimeString(),
                    Description = "Event1 Description",
                    Category = "Category 1",
                    House = "Commons",
                    Type = "Main Chamber",
                    Members = new List<EventMember>
                    {
                        new EventMember
                        {
                            Id = 1,
                            Name = "First MP"
                        }
                    }
                });

            _memberApiClientMock.Setup(x => x.GetMembersByIds(It.IsAny<int[]>()))
                .Throws(new ExternalServiceException("Error in getting member", new Exception()));
            var expectedResponse = new ClientResponse<EventDetailViewModel>
            {
                ErrorMessage = "Error in getting member",
                Success = false
            };

            // Act
            var response = await _subject.GetEventDetailAsync(1, DateTime.Today);

            // Assert
            UnitTestHelper.AssertEqualSerialization(expectedResponse, response, "expected response to match");
        }

        [Test]
        public async Task GetEventDetailAsync_ShouldReturnsExpectedEvents_WhenEventClientApiReturnsEvent_AndMemberApiReturnsCorrectly()
        {
            // Arrange
            var dateTime = new DateTime(2019, 6, 11); ;
            _eventApiClientMock.Setup(x => x.GetEventAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new Event
                {
                    Id = 1,
                    StartDate = dateTime,
                    StartTime = dateTime.ToShortTimeString(),
                    EndDate = dateTime,
                    EndTime = dateTime.AddHours(2).ToShortTimeString(),
                    Description = "Event1 Description",
                    Category = "Category 1",
                    House = "Commons",
                    Type = "Main Chamber",
                    Members = new List<EventMember>
                    {
                        new EventMember
                        {
                            Id = 1,
                            Name = "First MP"
                        }
                    }
                });

            _memberApiClientMock.Setup(x => x.GetMembersByIds(It.IsAny<int[]>()))
                .Returns(new List<Member>
                {
                    new Member
                    {
                        MemberId = 1,
                        Party = new Party
                        {
                            Id = 1,
                            Text = "Conservative"
                        },
                        FullTitle = "MP Full Title"
                        
                    }
                });
            var expectedResponse = new ClientResponse<EventDetailViewModel>
            {
                Success = true,
                Payload = new EventDetailViewModel
                {
                    Id = 1,
                    StartDate = dateTime,
                    EndDate = dateTime.AddHours(2),
                    Description = "Event1 Description",
                    Category = "Category 1",
                    Members = new List<MemberViewModel>
                    {
                        new MemberViewModel
                        {
                            FullTitle = "MP Full Title",
                            Party = "Conservative"
                        }
                    }
                }
            };

            // Act
            var response = await _subject.GetEventDetailAsync(1, DateTime.Today);

            // Assert
            UnitTestHelper.AssertEqualSerialization(expectedResponse, response, "expected response to match");
        }

        #endregion

    }
}