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
using G2H.Portal.Web.Models.Posts.Exceptions;
using G2H.Portal.Web.Models.PostViews;
using G2H.Portal.Web.Models.PostViews.Exceptions;
using Xeptions;

namespace G2H.Portal.Web.Services.Views.PostViews
{
    public partial class PostViewService
    {
        private delegate ValueTask<List<PostView>> ReturningPostViewsFunction();

        private async ValueTask<List<PostView>> TryCatch(ReturningPostViewsFunction returningPostViewsFunction)
        {
            try
            {
                return await returningPostViewsFunction();
            }
            catch (PostDependencyException postDependencyException)
            {
                throw CreateAndLogDependencyException(postDependencyException);
            }
            catch (PostServiceException postServiceException)
            {
                throw CreateAndLogDependencyException(postServiceException);
            }
            catch (Exception serviceException)
            {
                var failedPostViewServiceException =
                    new FailedPostViewServiceException(serviceException);

                throw CreateAndLogServiceException(failedPostViewServiceException);
            }
        }

        private PostViewDependencyException CreateAndLogDependencyException(Xeption innerException)
        {
            var postViewDependencyException = new PostViewDependencyException(innerException);
            this.loggingBroker.LogError(postViewDependencyException);

            return postViewDependencyException;
        }

        private Exception CreateAndLogServiceException(Xeption innerException)
        {
            var postViewServiceException = new PostViewServiceException(innerException);
            this.loggingBroker.LogError(postViewServiceException);

            return postViewServiceException;
        }
    }
}
