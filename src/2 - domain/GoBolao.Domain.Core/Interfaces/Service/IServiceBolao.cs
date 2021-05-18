using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Service
{
    public interface IServiceBolao:IDisposable
    {
        Resposta<Bolao> CriarBolao(CriarBolaoDTO criarBolaoDTO, int idUsuarioAcao);
        Resposta<Bolao> AlterarNomeImagemAvatar(AlterarNomeImagemAvatarBolaoDTO alterarNomeImagemAvatarBolaoDTO, int idUsuarioAcao);
        Resposta<BolaoDTO> ObterBolaoPorId(int idBolao, int idUsuarioAcao);
        Resposta<IEnumerable<BolaoDTO>> PesquisarBoloes(string pesquisa);
        Resposta<Bolao> ParticiparDeBolaoPublico(ParticiparDeBolaoPublicoDTO participarDeBolaoPublicoDTO, int idUsuarioAcao);
        Resposta<Bolao> SairDeBolao(int idBolao, int idUsuarioAcao);
    }
}
