using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core.Entities;

[Table("Course")]
public partial class Course
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("price", TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column("courseType")]
    [StringLength(10)]
    public string CourseType { get; set; } = null!;

    [Column("seatsAvailable")]
    public int? SeatsAvailable { get; set; }

    [Column("duration", TypeName = "decimal(5, 2)")]
    public decimal Duration { get; set; }

    [Column("categoryId")]
    public int CategoryId { get; set; }

    [Column("instructorId")]
    public int InstructorId { get; set; }

    [Column("startDate", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("endDate", TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Courses")]
    public virtual CourseCategory Category { get; set; } = null!;

    [InverseProperty("Course")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [ForeignKey("InstructorId")]
    [InverseProperty("Courses")]
    public virtual Instructor Instructor { get; set; } = null!;

    [InverseProperty("Course")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Course")]
    public virtual ICollection<SessionDetail> SessionDetails { get; set; } = new List<SessionDetail>();
}
