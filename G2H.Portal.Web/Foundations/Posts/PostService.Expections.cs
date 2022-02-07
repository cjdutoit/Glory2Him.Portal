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
using System.Net.Http;
using System.Threading.Tasks;
using G2H.Api.Web.Models.Posts;
using G2H.Portal.Web.Models.Posts.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace G2H.Portal.Web.Foundations.Posts
{
    public partial class PostService
    {
        private delegate ValueTask<List<Post>> ReturningPostsFunction();


        private async ValueTask<List<Post>> TryCatch(ReturningPostsFunction returningPostsFunction)
        {
            try
            {
                return await returningPostsFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedPostDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedPostDependencyException =
                    new FailedPostDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedPostDependencyException);
            }
            catch (Exception exception)
            {
                var failedPostServiceException =
                    new FailedPostServiceException(exception);

                throw CreateAndLogPostServiceException(failedPostServiceException);
            }
        }

        private PostDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var postDependencyException =
                new PostDependencyException(exception);

            this.loggingBroker.LogCritical(postDependencyException);

            return postDependencyException;
        }

        private PostDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var postDependencyException =
                new PostDependencyException(exception);

            this.loggingBroker.LogError(postDependencyException);

            return postDependencyException;
        }

        private PostServiceException CreateAndLogPostServiceException(Xeption exception)
        {
            var postServiceException =
                new PostServiceException(exception);

            this.loggingBroker.LogError(postServiceException);

            return postServiceException;
        }
    }
}
