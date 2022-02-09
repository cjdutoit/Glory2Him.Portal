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
using Bunit;
using FluentAssertions;
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
        public void ShouldRenderErrorIfExceptionOccurs()
        {
            // given
            TimelineComponentState expectedState =
                TimelineComponentState.Error;

            string randomMessage = GetRandomString();
            string exceptionMessage = randomMessage;
            string expectedErrorMessage = exceptionMessage;
            string expectedImageUrl = "imgs/error.png";

            var exception =
                new Exception(message: exceptionMessage);

            this.postViewServiceMock.Setup(service =>
                service.RetrieveAllPostViewsAsync())
                    .ThrowsAsync(exception);

            // when
            this.renderedTimelineComponent =
                RenderComponent<TimelineComponent>();

            // then
            this.renderedTimelineComponent.Instance.State
                .Should().Be(expectedState);

            this.renderedTimelineComponent.Instance.ErrorMessage
                .Should().Be(expectedErrorMessage);

            this.renderedTimelineComponent.Instance.Label.Value
                .Should().Be(expectedErrorMessage);

            this.renderedTimelineComponent.Instance.ErrorImage.Source
                .Should().Be(expectedImageUrl);

            IReadOnlyList<IRenderedComponent<CardBase>> postComponents =
                this.renderedTimelineComponent.FindComponents<CardBase>();

            postComponents.Should().HaveCount(0);

            this.renderedTimelineComponent.Instance.Spinner
                .Should().BeNull();

            this.postViewServiceMock.Verify(service =>
                service.RetrieveAllPostViewsAsync(),
                    Times.Once);

            this.postViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
