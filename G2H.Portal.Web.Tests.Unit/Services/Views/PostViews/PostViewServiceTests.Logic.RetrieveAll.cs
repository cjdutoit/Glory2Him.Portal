// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using G2H.Portal.Web.Models.Posts;
using G2H.Portal.Web.Models.PostViews;
using Moq;
using Xunit;

namespace G2H.Portal.Web.Tests.Unit.Services.Views.PostViews
{
    public partial class PostViewServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllPostViewsAsync()
        {
            // given
            List<dynamic> dynamicPostViewPropertiesCollection =
                   CreateRandomPostViewCollections();

            List<Post> randomPosts =
                    dynamicPostViewPropertiesCollection.Select(property =>
                        new Post
                        {
                            Id = property.Id,
                            Content = property.Content,
                            CreatedDate = property.CreatedDate,
                            UpdatedDate = property.UpdatedDate,
                            Author = property.Author
                        }).ToList();

            List<Post> retrievedPosts = randomPosts;

            List<PostView> randomPostViews =
                dynamicPostViewPropertiesCollection.Select(property =>
                    new PostView
                    {
                        Id = property.Id,
                        Content = property.Content,
                        CreatedDate = property.CreatedDate,
                        UpdatedDate = property.UpdatedDate,
                        Author = property.Author
                    }).ToList();

            List<PostView> expectedPostViews = randomPostViews;

            this.postServiceMock.Setup(service =>
                service.RetrieveAllPostsAsync())
                    .ReturnsAsync(retrievedPosts);

            // when
            List<PostView> retrievedPostViews =
                await this.postViewService.RetrieveAllPostViewsAsync();

            // then
            retrievedPostViews.Should().BeEquivalentTo(expectedPostViews);

            this.postServiceMock.Verify(service =>
                service.RetrieveAllPostsAsync(),
                    Times.Once());

            this.postServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
