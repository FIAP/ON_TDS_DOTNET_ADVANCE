using Fiap.Web.Alunos.Models;

namespace Fiap.Web.Alunos.Data.Repository
{
    public interface IRepresentanteRepository
    {
        IEnumerable<RepresentanteModel> GetAll();
        RepresentanteModel GetById(int id);
        void Add(RepresentanteModel representante);
        void Update(RepresentanteModel representante);
        void Delete(RepresentanteModel representante);

    }
}
