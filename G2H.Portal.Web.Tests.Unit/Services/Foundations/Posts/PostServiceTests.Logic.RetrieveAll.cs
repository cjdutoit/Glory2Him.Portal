// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using G2H.Portal.Web.Models.Posts;
using Moq;
using Xunit;

namespace G2H.Portal.Web.Tests.Unit.Services.Foundations.Posts
{
    public partial class PostServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllPostsAsync()
        {
            // given
            List<Post> randomPosts = CreateRandomPosts();
            List<Post> apiPosts = randomPosts;
            List<Post> expectedPosts = apiPosts.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllPostsAsync())
                    .ReturnsAsync(apiPosts);

            // when
            List<Post> retrievedPosts =
                    await this.postService.RetrieveAllPostsAsync();

            // then
            retrievedPosts.Should().BeEquivalentTo(expectedPosts);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllPostsAsync(),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
