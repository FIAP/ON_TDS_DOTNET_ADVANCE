using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiap.Web.Alunos.ViewModels
{
    public class ClienteCreateViewModel
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Observacao { get; set; }
        public int RepresentanteId { get; set; }
        public SelectList Representantes { get; set; }

        public ClienteCreateViewModel()
        {
            Representantes = new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }
}
