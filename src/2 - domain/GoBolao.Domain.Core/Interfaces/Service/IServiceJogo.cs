using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Service
{
    public interface IServiceJogo:IDisposable
    {
        Resposta<Jogo> CriarJogo(CriarJogoDTO criarJogoDTO);
        Resposta<Jogo> FinalizarJogo(FinalizarJogoDTO finalizarJogoDTO);
        Resposta<IEnumerable<JogoDTO>> ObterJogosDeHoje(int idUsuario);
        Resposta<IEnumerable<JogoDTO>> ObterJogosDeAmanha(int idUsuario);
        Resposta<IEnumerable<JogoDTO>> ObterJogosFuturos(int idUsuario);
        Resposta<IEnumerable<JogoDTO>> ObterTodosJogos(int idUsuario);
    }
}
