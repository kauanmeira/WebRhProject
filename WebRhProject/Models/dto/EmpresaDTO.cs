using WebRhProject.Data.Migrations;

namespace WebRhProject.Models.dto
{
    public class EmpresaDTO
    {
       public string Cnpj { get; set; }
       public string RazaoSocial { get; set; }
       public string NomeFantasia { get; set; }
       public string CEP { get; set; }
       public string Numero { get; set; }
    }

}
