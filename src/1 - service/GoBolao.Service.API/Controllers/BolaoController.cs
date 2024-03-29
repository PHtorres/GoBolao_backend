﻿using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Shared.DomainObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GoBolao.Service.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize]
    public class BolaoController : SharedController
    {
        private readonly IServiceBolao ServicoBolao;
        public BolaoController(IHttpContextAccessor _httpContextAccessor, IServiceBolao servicoBolao) : base(_httpContextAccessor)
        {
            ServicoBolao = servicoBolao;
        }


        [HttpGet("{idBolao}")]
        public ActionResult<Resposta<BolaoDTO>> Get(int idBolao)
        {
            return Ok(ServicoBolao.ObterBolaoPorId(idBolao, IdUsuarioAcao));
        }

        [HttpGet]
        [Route("pesquisa/{pesquisa}")]
        public ActionResult<Resposta<IEnumerable<BolaoDTO>>> GetPesquisa(string pesquisa)
        {
            return Ok(ServicoBolao.PesquisarBoloes(pesquisa, IdUsuarioAcao));
        }

        [HttpGet]
        [Route("meus")]
        public ActionResult<Resposta<IEnumerable<BolaoDTO>>> GetDoUsuario()
        {
            return Ok(ServicoBolao.ObterBoloesDoUsuario(IdUsuarioAcao));
        }

        [HttpGet]
        [Route("ranking/{idBolao}")]
        public ActionResult<Resposta<RankingBolaoDTO>> GetRanking(int idBolao)
        {
            return Ok(ServicoBolao.ObterRankingBolao(idBolao));
        }

        [HttpGet]
        [Route("ranking")]
        public ActionResult<Resposta<IEnumerable<RankingBolaoDTO>>> GetRankingsUsuario()
        {
            return Ok(ServicoBolao.ObterRankingsBoloesDoUsuario(IdUsuarioAcao));
        }

        [HttpPost]
        public ActionResult<Resposta<Bolao>> Post([FromBody] CriarBolaoDTO criarBolaoDTO)
        {
            return Ok(ServicoBolao.CriarBolao(criarBolaoDTO, IdUsuarioAcao));
        }

        [HttpPatch]
        [Route("avatar")]
        public ActionResult<Resposta<Bolao>> Patch([FromBody] AlterarNomeImagemAvatarBolaoDTO alterarNomeImagemAvatarBolaoDTO)
        {
            return Ok(ServicoBolao.AlterarNomeImagemAvatar(alterarNomeImagemAvatarBolaoDTO, IdUsuarioAcao));
        }

        [HttpPost]
        [Route("participar")]
        public ActionResult<Resposta<BolaoUsuario>> PostParticipar([FromBody] ParticiparDeBolaoPublicoDTO participarDeBolaoPublicoDTO)
        {
            return Ok(ServicoBolao.ParticiparDeBolaoPublico(participarDeBolaoPublicoDTO, IdUsuarioAcao));
        }

        [HttpDelete]
        [Route("sair/{idBolao}")]
        public ActionResult<Resposta<BolaoUsuario>> DeleteSair(int idBolao)
        {
            return Ok(ServicoBolao.SairDeBolao(idBolao, IdUsuarioAcao));
        }
    }
}
