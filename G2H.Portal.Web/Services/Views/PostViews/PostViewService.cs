﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using G2H.Portal.Web.Brokers.Loggings;
using G2H.Portal.Web.Foundations.Posts;
using G2H.Portal.Web.Models.PostViews;

namespace G2H.Portal.Web.Services.Views.PostViews
{
    public partial class PostViewService : IPostViewService
    {
        private readonly IPostService postService;
        private readonly ILoggingBroker loggingBroker;

        public PostViewService(
            IPostService postService,
            ILoggingBroker loggingBroker)
        {
            this.postService = postService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<PostView>> RetrieveAllPostViewsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
