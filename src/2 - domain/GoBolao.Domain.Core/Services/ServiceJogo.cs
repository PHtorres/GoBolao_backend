using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Services
{
    public class ServiceJogo : IServiceJogo
    {
        public Resposta<Jogo> CriarJogo(CriarJogoDTO criarJogoDTO)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Resposta<Jogo> FinalizarJogo(FinalizarJogoDTO finalizarJogoDTO)
        {
            //1 trazer todos os palpites do jogo
            //2 salvar numa lista os que nao ponturam
            //3 salvar numa lista os que acertaram o resultado
            //4 salvar numa lista os que acertaram o placar
            //5 loop para alterar Pontuacao de cada palpite e status finalizado
            //6 repositorio updaterange de cada lista
            //7 alterar status do jogo para finalizado
            throw new NotImplementedException();
        }

        public Resposta<IEnumerable<JogoDTO>> ObterJogos(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Resposta<IEnumerable<JogoDTO>> ObterJogosDeAmanha(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Resposta<IEnumerable<JogoDTO>> ObterJogosDeHoje(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
