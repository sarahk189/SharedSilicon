using System.ComponentModel.DataAnnotations;

namespace SharedSilicon.Models;

public class DeleteAccountModel
{
    [Display(Name = "Delete Account", Order = 1)]


    public bool DeleteAccount { get; set; } = false;

}
