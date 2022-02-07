// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using G2H.Api.Web.Models.Posts;
using G2H.Portal.Web.Models.Posts.Exceptions;
using Moq;
using Xunit;

namespace G2H.Portal.Web.Tests.Unit.Services.Foundations.Posts
{
    public partial class PostServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            var failedPostDependencyException =
                new FailedPostDependencyException(criticalDependencyException);

            var expectedPostDependencyException =
                new PostDependencyException(
                    failedPostDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllPostsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Post>> retrieveAllPostsTask =
                this.postService.RetrieveAllPostsAsync();

            // then
            await Assert.ThrowsAsync<PostDependencyException>(() =>
               retrieveAllPostsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllPostsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPostDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
