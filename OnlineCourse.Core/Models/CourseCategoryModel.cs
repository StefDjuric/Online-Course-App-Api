using OnlineCourse.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Core.Models
{   
    // Never use entities directly, always create models
    public class CourseCategoryModel
    {
        public int Id { get; set; }

        
        public string CategoryName { get; set; } = null!;

        
        public string? Description { get; set; }

       
  
    }
}
