namespace AspireEverything.FrameworkApi.Data;

public class Framework
{
    [Key]
    public int Id { get; set; }
    [StringLength(200)]
    [Required]
    public string Name { get; set; } = "";
}
