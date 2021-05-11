using GoBolao.Domain.Shared.Entidades;
using System;
using System.Collections.Generic;

namespace GoBolao.Domain.Shared.Interfaces.Repository
{
    public interface IRepositoryGenerico<EntidadeGenerica>:IDisposable where EntidadeGenerica:EntidadeBase
    {
        void Adicionar(EntidadeGenerica obj);
        void Atualizar(EntidadeGenerica obj);
        void AtualizarLista(IEnumerable<EntidadeGenerica> listaObj);
        IEnumerable<EntidadeGenerica> Listar();
        EntidadeGenerica Obter(int id);
        void Remover(EntidadeGenerica obj);
        void Salvar();
    }
}
