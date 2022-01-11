// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace G2H.Portal.Web.Views.Bases
{
    public partial class CardBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string SubTitle { get; set; }

        [Parameter]
        public string ImageUrl { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public RenderFragment Footer { get; set; }

        public void SetHeaderTitle(string title)
        {
            Title = title;
            InvokeAsync(StateHasChanged);
        }

        public void SetHeaderSubTitle(string subTitle)
        {
            SubTitle = subTitle;
            InvokeAsync(StateHasChanged);
        }

        public void SetHeaderImageUrl(string imageUrl)
        {
            ImageUrl = imageUrl;
            InvokeAsync(StateHasChanged);
        }
    }
}
