using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RestSharp;
using UKPBS.Domain.Entities.Events;
using UKPBS.Services.ApiClients;
using UKPBS.Services.Exceptions;
using UKPBS.Services.Requests;
using UKPBS.Services.UnitTests.Helpers;

namespace UKPBS.Services.UnitTests.ApiClients
{
    [TestFixture]
    public class EventApiClientTests
    {
        private Mock<IRestClient> _restClientMock;
        private EventApiClient _subject;

        [SetUp]
        public void Setup()
        {
            _restClientMock = new Mock<IRestClient>();
            _subject = new EventApiClient(_restClientMock.Object);
        }

        [Test]
        public void GetEventsAsync_ShouldThrowException_WhenEventRequestNotValid()
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _subject.GetEventsAsync(null));
        }

        [Test]
        public void GetEventsAsync_ShouldThrowException_WhenCalendarApiReturnsNotSuccessResponse()
        {
            // Arrange
            _restClientMock.Setup(x => x.ExecuteTaskAsync<List<Event>>(It.IsAny<RestRequest>())).ReturnsAsync(
                new RestResponse<List<Event>>
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            // Act

            // Assert
            Assert.ThrowsAsync<ExternalServiceException>(() =>
                _subject.GetEventsAsync(new EventRequest()));
        }

        [Test]
        public async Task GetEventsAsync_ShouldReturnsCorrectEvents_WhenCalendarApiReturnsCorrectEvents()
        {
            // Arrange
            var startDate = DateTime.Now;
            var data = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Category = "category 1",
                    Description = "Event1",
                    House = "Commons",
                    StartDate = startDate
                }
            };
            _restClientMock.Setup(x => x.ExecuteTaskAsync<List<Event>>(It.IsAny<RestRequest>())).ReturnsAsync(
                new RestResponse<List<Event>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = data
                });
            // Act
            var result = await _subject.GetEventsAsync(new EventRequest());

            // Assert
            UnitTestHelper.AssertEqualSerialization(data,result, "expected events to match" );
        }

        [Test]
        public void GetEventAsync_ShouldThrowException_WhenCalendarApiReturnsNotSuccessResponse()
        {
            // Arrange
            _restClientMock.Setup(x => x.ExecuteTaskAsync<List<Event>>(It.IsAny<RestRequest>())).ReturnsAsync(
                new RestResponse<List<Event>>
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            // Act

            // Assert
            Assert.ThrowsAsync<ExternalServiceException>(() =>
                _subject.GetEventAsync(1, DateTime.Now));
        }

        [Test]
        public async Task GetEventAsync_ShouldThrowException_WhenCalendarApiReturnsNullAsEvent()
        {
            // Arrange
            _restClientMock.Setup(x => x.ExecuteTaskAsync<List<Event>>(It.IsAny<RestRequest>())).ReturnsAsync(
                new RestResponse<List<Event>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = null
                });

            // Act
            var result = await _subject.GetEventAsync(1, DateTime.Now);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetEventAsync_ShouldReturnsCorrectEvent_WhenCalendarApiReturnsCorrectEvent()
        {
            // Arrange
            var startDate = DateTime.Now;
            var data = new Event
            {
                Id = 1,
                Category = "category 1",
                Description = "Event1",
                House = "Commons",
                StartDate = startDate
            };
            _restClientMock.Setup(x => x.ExecuteTaskAsync<List<Event>>(It.IsAny<RestRequest>())).ReturnsAsync(
                new RestResponse<List<Event>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new List<Event> {data}
                });
            // Act
            var result = await _subject.GetEventAsync(1, startDate);

            // Assert
            UnitTestHelper.AssertEqualSerialization(data,result, "expected events to match" );
        }
    }
}