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

namespace G2H.Portal.Web.Foundations.Posts
{
    public partial class PostService : IPostService
    {
        private readonly IApiBroker apiBroker;

        public PostService(
            IApiBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        public ValueTask<List<Post>> RetrieveAllPostsAsync() =>
            throw new System.NotImplementedException();
    }
}
