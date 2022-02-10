// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System;
using G2H.Portal.Web.Models.Base;
using G2H.Portal.Web.Models.Statuses;

namespace G2H.Portal.Web.Models.Approvals
{
    public class Approval : IKey, IStatus, IAudit
    {
        public Guid Id { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}