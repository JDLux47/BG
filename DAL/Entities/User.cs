namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Costs = new HashSet<Costs>();
            Income = new HashSet<Income>();
        }

        public int ID { get; set; }

        public int Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public double Balance { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        public double? SumLimiter { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateUpdateBalance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Costs> Costs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Income> Income { get; set; }
    }
}
