using System;
using System.Collections.Generic;
using System.Net;
using Moq;
using NUnit.Framework;
using RestSharp;
using UKPBS.Domain.Entities.ParliamentMembers;
using UKPBS.Services.ApiClients;
using UKPBS.Services.Exceptions;
using UKPBS.Services.UnitTests.Helpers;

namespace UKPBS.Services.UnitTests.ApiClients
{
    [TestFixture]
    public class MemberApiClientTests
    {
        private Mock<IRestClient> _restClientMock;
        private MemberApiClient _subject;

        [SetUp]
        public void Setup()
        {
            _restClientMock = new Mock<IRestClient>();
            _subject = new MemberApiClient(_restClientMock.Object);
        }

        [TestCase(new int[]{})]
        [TestCase(null)]
        public void GetMembersByIds_ShouldThrowException_WhenMemberIdsNotValid(int[] memberIds)
        {
            // Arrange
            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
                _subject.GetMembersByIds(memberIds));
        }

        [Test]
        public void GetMembersByIds_ShouldThrowException_WhenMemberApiReturnsNotSuccessResponse()
        {
            // Arrange
            _restClientMock.Setup(x => x.ExecuteTaskAsync<Members>(It.IsAny<RestRequest>())).ReturnsAsync(
                new RestResponse<Members>
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            // Act
            
            // Assert
            Assert.Throws<ExternalServiceException>(() =>
                _subject.GetMembersByIds(new[]{1}));
        }

        [Test]
        public void GetMembersByIds_ShouldReturnsCorrectMembers_WhenMemberApiReturnsCorrectResponse()
        {
            // Arrange
            _restClientMock.Setup(x => x.ExecuteTaskAsync<Members>(It.IsAny<RestRequest>())).ReturnsAsync(
                new RestResponse<Members>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = 
                        new Members
                        {
                            Member = new Member
                            {
                                MemberId = 1,
                                FullTitle = "MP full title",
                                Party = new Party
                                {
                                    Text = "Conservative"
                                }
                            }
                        }
                });

            var expectedResult = new List<Member>
            {
                new Member
                {
                    MemberId = 1,
                    FullTitle = "MP full title",
                    Party = new Party
                    {
                        Text = "Conservative"
                    }
                }
            };

            // Act
            var result = _subject.GetMembersByIds(new[] {1});
            
            // Assert
            UnitTestHelper.AssertEqualSerialization(expectedResult, result, "expected members to match");
        } 
    }
}