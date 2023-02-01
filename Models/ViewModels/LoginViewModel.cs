using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informar usuário")]
    [Display(Name = "Usuário")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Informar Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}
