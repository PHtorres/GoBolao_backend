﻿using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Shared.Interfaces.Service;
using GoBolao.Domain.Usuarios.DTO;
using GoBolao.Domain.Usuarios.Entidades;
using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Interfaces.Rules;
using GoBolao.Domain.Usuarios.Interfaces.Service;
using GoBolao.Domain.Usuarios.ManualMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Domain.Usuarios.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario RepositorioUsuario;
        private readonly IRulesUsuario RulesUsuario;
        private readonly IServiceCriptografia Criptografia;
        private Resposta<UsuarioDTO> Resposta;

        public ServiceUsuario(IRepositoryUsuario repositorioUsuario, IServiceCriptografia criptografia, IRulesUsuario rulesUsuario)
        {
            RepositorioUsuario = repositorioUsuario;
            Resposta = new Resposta<UsuarioDTO>();
            Criptografia = criptografia;
            RulesUsuario = rulesUsuario;
        }

        public Resposta<UsuarioDTO> AlterarUsuario(AlterarUsuarioDTO alterarUsuarioDTO, int idUsuarioAcao)
        {
            var usuario = RepositorioUsuario.Obter(idUsuarioAcao);
            usuario.AlterarApelido(alterarUsuarioDTO.Apelido);
            usuario.AlterarEmail(alterarUsuarioDTO.Email);

            if (usuario.Invalido)
            {
                Resposta.AdicionarNotificacao(usuario._Erros);
                return Resposta;
            }

            if (!RulesUsuario.AptoParaAlterar(alterarUsuarioDTO, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesUsuario.ObterFalhas());
                return Resposta;
            }

            RepositorioUsuario.Atualizar(usuario);
            RepositorioUsuario.Salvar();

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }

        public Resposta<UsuarioDTO> CriarUsuario(CriarUsuarioDTO criarUsuarioDTO)
        {
            var usuario = new Usuario(criarUsuarioDTO.Apelido, criarUsuarioDTO.Email, criarUsuarioDTO.Senha);

            if (usuario.Invalido)
            {
                Resposta.AdicionarNotificacao(usuario._Erros);
                return Resposta;
            }

            if (!RulesUsuario.AptoParaCriar(criarUsuarioDTO))
            {
                Resposta.AdicionarNotificacao(RulesUsuario.ObterFalhas());
                return Resposta;
            }

            usuario.AlterarSenha(Criptografia.Criptografar(criarUsuarioDTO.Senha));
            RepositorioUsuario.Adicionar(usuario);
            RepositorioUsuario.Salvar();

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }

        public void Dispose()
        {
            RepositorioUsuario.Dispose();
            Criptografia.Dispose();
            RulesUsuario.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<UsuarioDTO> ObterUsuarioPeloId(int IdUsuario)
        {
            var usuario = RepositorioUsuario.Obter(IdUsuario);
            if (usuario == null)
            {
                Resposta.AdicionarNotificacao("Usuário inexistente.");
                return Resposta;
            }

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }

        public Resposta<IEnumerable<UsuarioDTO>> ObterUsuarios()
        {
            var respostaLista = new Resposta<IEnumerable<UsuarioDTO>>();

            var usuarios = RepositorioUsuario.Listar().Select(item => item.UsuarioParaDTO());

            respostaLista.AdicionarConteudo(usuarios);
            return respostaLista;
        }

        public Resposta<UsuarioDTO> RemoverUsuario(int IdUsuarioAcao)
        {
            if (!RulesUsuario.AptoParaRemover(IdUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesUsuario.ObterFalhas());
                return Resposta;
            }

            var usuario = RepositorioUsuario.Obter(IdUsuarioAcao);

            RepositorioUsuario.Remover(usuario);
            RepositorioUsuario.Salvar();

            return Resposta;
        }

        public Resposta<UsuarioDTO> AlterarNomeImagemAvatar(AlterarNomeImagemAvatarDTO alterarNomeImagemAvatarDTO, int idUsuarioAcao)
        {
            var usuario = RepositorioUsuario.Obter(idUsuarioAcao);
            usuario.AlterarNomeImagemAvatar(alterarNomeImagemAvatarDTO.NomeImagemAvatar);
            if (usuario.Invalido)
            {
                Resposta.AdicionarNotificacao(usuario._Erros);
                return Resposta;
            }

            RepositorioUsuario.Atualizar(usuario);
            RepositorioUsuario.Salvar();

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }

        public Resposta<UsuarioDTO> AlterarSenha(AlterarSenhaDTO alterarSenhaDTO, int idUsuarioAcao)
        {
            if(!RulesUsuario.AptoParaAlterarSenha(alterarSenhaDTO, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesUsuario.ObterFalhas());
                return Resposta;
            }

            var usuario = RepositorioUsuario.Obter(idUsuarioAcao);
            usuario.AlterarSenha(alterarSenhaDTO.NovaSenha);
            if (!usuario.Valido)
            {
                Resposta.AdicionarNotificacao(usuario._Erros);
                return Resposta;
            }

            usuario.AlterarSenha(Criptografia.Criptografar(alterarSenhaDTO.NovaSenha));
            RepositorioUsuario.Atualizar(usuario);
            RepositorioUsuario.Salvar();

            Resposta.AdicionarConteudo(usuario.UsuarioParaDTO());
            return Resposta;
        }
    }
}
