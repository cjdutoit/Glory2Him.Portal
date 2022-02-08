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
using G2H.Portal.Web.Models.PostViews;
using G2H.Portal.Web.Models.PostViews.Exceptions;
using Moq;
using Xunit;

namespace G2H.Portal.Web.Tests.Unit.Services.Views.PostViews
{
    public partial class PostViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionIfDependecyErrorOccursAndLogItAsync(
            Exception dependencyException)
        {
            // given
            var expectedPostViewDependencyException =
                new PostViewDependencyException(dependencyException);

            this.postServiceMock.Setup(service =>
                service.RetrieveAllPostsAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<List<PostView>> retrieveAllPostViewsTask =
                this.postViewService.RetrieveAllPostViewsAsync();

            // then
            await Assert.ThrowsAsync<PostViewDependencyException>(() =>
                retrieveAllPostViewsTask.AsTask());

            this.postServiceMock.Verify(service =>
                service.RetrieveAllPostsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostViewDependencyException))),
                        Times.Once);

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
