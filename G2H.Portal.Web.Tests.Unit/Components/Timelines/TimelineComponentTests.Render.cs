// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System.Collections.Generic;
using Bunit;
using FluentAssertions;
using G2H.Portal.Web.Models.PostViews;
using G2H.Portal.Web.Models.Views.Components;
using G2H.Portal.Web.Views.Bases;
using G2H.Portal.Web.Views.Components.TimeLines;
using Moq;
using Xunit;

namespace G2H.Portal.Web.Tests.Unit.Components.Timelines
{
    public partial class TimelineComponentTests
    {
        [Fact]
        public void ShouldInitComponent()
        {
            // given
            TimeLineComponentState expectedState =
                TimeLineComponentState.Loading;

            // when
            var initialTimelineComponent =
                new TimelineComponent();

            // then
            initialTimelineComponent.State.Should().Be(expectedState);
            initialTimelineComponent.PostViewService.Should().BeNull();
            initialTimelineComponent.PostViews.Should().BeNull();
            initialTimelineComponent.Label.Should().BeNull();
            initialTimelineComponent.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderPosts()
        {
            // given
            TimeLineComponentState expectedState =
                TimeLineComponentState.Content;

            List<PostView> randomPostViews =
                CreateRandomPostViews();

            List<PostView> retrievedPostViews =
                randomPostViews;

            List<PostView> expectedPostViews =
                retrievedPostViews;

            this.postViewServiceMock.Setup(service =>
                service.RetrieveAllPostViewsAsync())
                    .ReturnsAsync(retrievedPostViews);

            // when
            this.renderedTimelineComponent =
                RenderComponent<TimelineComponent>();

            // then
            this.renderedTimelineComponent.Instance.State
                .Should().Be(expectedState);

            this.renderedTimelineComponent.Instance.PostViews
                .Should().BeEquivalentTo(expectedPostViews);

            IReadOnlyList<IRenderedComponent<CardBase>> postComponents =
                this.renderedTimelineComponent.FindComponents<CardBase>();

            postComponents.Should().HaveCount(expectedPostViews.Count);

            this.postViewServiceMock.Verify(service =>
                service.RetrieveAllPostViewsAsync(),
                    Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
