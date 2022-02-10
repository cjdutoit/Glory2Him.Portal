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
using G2H.Portal.Web.Models.PostViews;
using G2H.Portal.Web.Models.Views.Components.Timelines;
using G2H.Portal.Web.Services.Views.PostViews;
using G2H.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace G2H.Portal.Web.Views.Components.Timelines
{
    public partial class TimelineComponent
    {
        [Inject]
        public IPostViewService PostViewService { get; set; }

        public TimelineComponentState State { get; set; }
        public List<PostView> PostViews { get; set; }
        public SpinnerBase Spinner { get; set; }
        public string ErrorMessage { get; set; }
        public LabelBase Label { get; set; }
        public ImageBase ErrorImage { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                this.PostViews =
                    await this.PostViewService.RetrieveAllPostViewsAsync();

                this.State = TimelineComponentState.Content;
            }
            catch (Exception exception)
            {
                this.ErrorMessage = exception.Message;
                this.State = TimelineComponentState.Error;
            }
        }
    }
}
