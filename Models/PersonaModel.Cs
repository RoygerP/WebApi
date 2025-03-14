using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models{
    
public class PersonaModel
{

    public enum Generos{
        M = 0,
        F = 1
    }
    [Key]
    public int Id { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [StringLength(50, ErrorMessage = "Maximo de caracteres 50")] 
    public string? Nombre { get; set; } 
    [required]
    [range(0, 100, ErrorMessage = "Edad debe ser entre 0 y 100")]
    public int? Edad { get; set; } 
    [Required(ErrorMessage = "Favor de seleccionar una opcion")]
    [EnumDataType(typeof(Generos), ErrorMessage = "Solo se puede selecionar 0 Masculino 1 Femenino ")]
    public Generos? Sexo { get; set; }

} 

}
