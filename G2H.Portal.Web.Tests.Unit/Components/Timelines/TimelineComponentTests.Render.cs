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
using System.Linq;
using Bunit;
using FluentAssertions;
using G2H.Portal.Web.Models.PostViews;
using G2H.Portal.Web.Models.Views.Components.Timelines;
using G2H.Portal.Web.Views.Bases;
using G2H.Portal.Web.Views.Components.Timelines;
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
            TimelineComponentState expectedState =
                TimelineComponentState.Loading;

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
        public void ShouldDisplayLoadingBeforeRenderingPosts()
        {
            // given
            TimelineComponentState expectedState =
                TimelineComponentState.Loading;

            string expectedSpinnerValue = "Loading ...";
            List<PostView> somePostViews = CreateRandomPostViews();

            this.postViewServiceMock.Setup(service =>
                service.RetrieveAllPostViewsAsync())
                    .ReturnsAsync(
                        value: somePostViews,
                        delay: TimeSpan.FromMilliseconds(500));

            // when
            this.renderedTimelineComponent =
                RenderComponent<TimelineComponent>();

            // then
            this.renderedTimelineComponent.Instance.State
                .Should().Be(expectedState);

            this.renderedTimelineComponent.Instance.Spinner.IsVisible
                .Should().BeTrue();

            this.renderedTimelineComponent.Instance.Spinner.Value
                .Should().Be(expectedSpinnerValue);

            this.postViewServiceMock.Verify(service =>
                service.RetrieveAllPostViewsAsync(),
                    Times.Once());

            this.postViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRenderPosts()
        {
            // given
            TimelineComponentState expectedState =
                TimelineComponentState.Content;

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

            postComponents.ToList().ForEach(component =>
            {
                bool componentContentExists =
                    expectedPostViews.Any(postView =>
                    component.Markup.Contains($"{postView.Title} by {postView.Author}")
                        && component.Markup.Contains(postView.Content)
                        && component.Markup.Contains(postView.UpdatedDate.ToString("dd/MM/yyyy")));

                componentContentExists.Should().BeTrue();
            });

            this.postViewServiceMock.Verify(service =>
                service.RetrieveAllPostViewsAsync(),
                    Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
