namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Income")]
    public partial class Income
    {
        public int ID { get; set; }

        public int? ID_IncomeCategory { get; set; }

        [Column(TypeName = "money")]
        public decimal Sum { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int ID_User { get; set; }

        [ForeignKey("ID_IncomeCategory")]
        public virtual IncomeCategory IncomeCategory { get; set; }

        [ForeignKey("ID_User")]
        public virtual User User { get; set; }
    }
}
