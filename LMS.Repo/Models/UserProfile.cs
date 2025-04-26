using System;
using System.ComponentModel.DataAnnotations;

public class UserProfile
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string CurrentJob { get; set; }

    [Required]
    [MaxLength(255)]
    public string EducationLevel { get; set; }

    [Required]
    [MaxLength(255)]
    public string FieldOfStudy { get; set; }

    [Required]
    public string CurrentSkills { get; set; } // Store as comma-separated or JSON string

    [Required]
    public string InterestedSkills	 { get; set; }

    [Required]
    public string Passion { get; set; }

    [Required]
    public string Goal { get; set; }
}
