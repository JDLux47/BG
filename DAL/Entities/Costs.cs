namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Costs
    {
        public int ID { get; set; }

        public int? ID_CostsCategory { get; set; }

        [Column(TypeName = "money")]
        public decimal Sum { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int ID_User { get; set; }

        [ForeignKey("ID_CostsCategory")]
        public virtual CostsCategory CostsCategory { get; set; }

        [ForeignKey("ID_User")]
        public virtual User User { get; set; }
    }
}
