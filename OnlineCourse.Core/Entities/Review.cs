using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core.Entities;

[Table("Review")]
public partial class Review
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("courseId")]
    public int CourseId { get; set; }

    [Column("userId")]
    public int UserId { get; set; }

    [Column("rating")]
    public int Rating { get; set; }

    [Column("comments")]
    public string? Comments { get; set; }

    [Column("reviewDate", TypeName = "datetime")]
    public DateTime ReviewDate { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Reviews")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Reviews")]
    public virtual UserProfile User { get; set; } = null!;
}
