using Fiap.Web.Alunos.Data.Repository;
using Fiap.Web.Alunos.Models;

namespace Fiap.Web.Alunos.Services
{
    public class RepresentanteService : IRepresentanteService
    {

        private readonly IRepresentanteRepository _repository;

        public RepresentanteService(IRepresentanteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<RepresentanteModel> ListarRepresentantes() => _repository.GetAll();

        public RepresentanteModel ObterRepresentantePorId(int id) => _repository.GetById(id);

        public void CriarRepresentante(RepresentanteModel representante) => _repository.Add(representante);

        public void AtualizarRepresentante(RepresentanteModel representante) => _repository.Update(representante);

        public void DeletarRepresentante(int id)
        {
            var cliente = _repository.GetById(id);
            if (cliente != null)
            {
                _repository.Delete(cliente);
            }
        }

    }
}
