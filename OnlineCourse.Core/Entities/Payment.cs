using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core.Entities;

[Table("Payment")]
public partial class Payment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("enrollmentId")]
    public int EnrollmentId { get; set; }

    [Column("amount", TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [Column("paymentDate", TypeName = "datetime")]
    public DateTime PaymentDate { get; set; }

    [Column("paymentMethod")]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = null!;

    [Column("paymentStatus")]
    [StringLength(20)]
    public string? PaymentStatus { get; set; }

    [ForeignKey("EnrollmentId")]
    [InverseProperty("Payments")]
    public virtual Enrollment Enrollment { get; set; } = null!;
}
