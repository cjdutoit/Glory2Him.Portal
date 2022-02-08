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
using G2H.Portal.Web.Models.PostViews;
using G2H.Portal.Web.Services.Views.PostViews;
using G2H.Portal.Web.Views.Components.Timelines;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syncfusion.Blazor;
using Tynamix.ObjectFiller;

namespace G2H.Portal.Web.Tests.Unit.Components.Timelines
{
    public partial class TimelineComponentTests : TestContext
    {
        private readonly Mock<IPostViewService> postViewServiceMock;
        private IRenderedComponent<TimelineComponent> renderedTimelineComponent;

        public TimelineComponentTests()
        {
            this.postViewServiceMock = new Mock<IPostViewService>();
            this.Services.AddTransient(service => this.postViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static List<PostView> CreateRandomPostViews() =>
            CreatePostViewFiller().Create(count: GetRandomNumber()).ToList();

        private static Filler<PostView> CreatePostViewFiller()
        {
            var filler = new Filler<PostView>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset());

            return filler;
        }
    }
}
