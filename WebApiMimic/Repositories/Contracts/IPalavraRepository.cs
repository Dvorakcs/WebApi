using WebApiMimic.Helpers;
using WebApiMimic.Models;

namespace WebApiMimic.Repositories.Contracts
{
    public interface IPalavraRepository
    {
        PaginationList<Palavra> ObterPalavras(PalavraUrlQuery query);
        Palavra Obter(int id);
        void Cadastrar(Palavra palavra);
        void Atualizar(Palavra palavra);
        void Deletar(int id);
        

    }
}
