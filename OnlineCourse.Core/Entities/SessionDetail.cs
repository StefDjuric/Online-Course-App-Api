using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core.Entities;

public partial class SessionDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("courseId")]
    public int CourseId { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("videoURL")]
    [StringLength(500)]
    public string? VideoUrl { get; set; }

    [Column("videoOrder")]
    public int VideoOrder { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("SessionDetails")]
    public virtual Course Course { get; set; } = null!;
}
