using System.Collections.Generic;

namespace SistemaBiblioteca.Interfaces
{
    public interface IRepository<T>
    {
        void Adicionar(T entidade);
        T BuscarPorId(int id);
        List<T> BuscarTodos();
        void Atualizar(T entidade);
        void Remover(int id);
        bool Existe(int id);
    }
}