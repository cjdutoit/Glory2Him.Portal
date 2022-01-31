// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System.Threading.Tasks;
using G2H.Api.Web.Models.Posts;

namespace G2H.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string PostsRelativeUrl = "api/posts";

        public async ValueTask<Post> PostPostAsync(Post post) =>
            await this.PostAsync(PostsRelativeUrl, post);
    }
}