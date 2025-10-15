namespace AspireEverything.WebBlazor.Models;

public class Framework
{
    [Key]
    public int Id { get; set; }
    [StringLength(200)]
    [Required]
    public string Name { get; set; } = "";
}
