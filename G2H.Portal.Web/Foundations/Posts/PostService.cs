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
using G2H.Api.Web.Models.Posts;
using G2H.Portal.Web.Brokers.Apis;
using G2H.Portal.Web.Brokers.Loggings;

namespace G2H.Portal.Web.Foundations.Posts
{
    public partial class PostService : IPostService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public PostService(
         IApiBroker apiBroker,
         ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<Post>> RetrieveAllPostsAsync() =>
            TryCatch(async () => await apiBroker.GetAllPostsAsync());
    }
}
