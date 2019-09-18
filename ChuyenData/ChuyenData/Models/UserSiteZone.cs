namespace ChuyenData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserSiteZone
    {
        public int UserSiteZoneID { get; set; }

        public int UserID { get; set; }

        public int SiteZoneID { get; set; }

        public virtual SiteZone SiteZone { get; set; }

        public virtual User User { get; set; }
    }
}
