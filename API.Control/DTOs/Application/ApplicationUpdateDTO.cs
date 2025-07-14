using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO para atualização de aplicativo.
/// </summary>
public class ApplicationUpdateDTO
{
    [Required, StringLength(50)]
    public string NameID { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Version { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string FileName { get; set; } = string.Empty;

    [StringLength(250)]
    public string Argument { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Source { get; set; } = string.Empty;

    [StringLength(100)]
    public string Filter { get; set; } = string.Empty;

    [Required, StringLength(64)]
    public string Hash { get; set; } = string.Empty;

    public bool Enabled { get; set; } = true;
}
