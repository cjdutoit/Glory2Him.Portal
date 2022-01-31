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
using System.Threading.Tasks;
using G2H.Api.Web.Models.Posts;

namespace G2H.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string PostsRelativeUrl = "api/posts";

        public async ValueTask<Post> PostPostAsync(Post post) =>
            await this.PostAsync(PostsRelativeUrl, post);

        public async ValueTask<List<Post>> GetAllPostsAsync() =>
            await this.GetAsync<List<Post>>(PostsRelativeUrl);

        public async ValueTask<Post> GetPostByIdAsync(Guid postId) =>
            await this.GetAsync<Post>($"{PostsRelativeUrl}/{postId}");

        public async ValueTask<Post> PutPostAsync(Post post) =>
            await this.PutAsync(PostsRelativeUrl, post);

        public async ValueTask<Post> DeletePostByIdAsync(Guid postId) =>
            await this.DeleteAsync<Post>($"{PostsRelativeUrl}/{postId}");
    }
}