using GoBolao.Domain.Usuarios.DTO;
using GoBolao.Domain.Usuarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Usuarios.ManualMapper
{
    public static class UsuarioMapper
    {
        public static UsuarioDTO UsuarioParaDTO(this Usuario usuario)
        {
            return new UsuarioDTO { Apelido = usuario.Apelido, Email = usuario.Email, Id = usuario.Id, UrlAvatar = usuario.UrlAvatar };
        }
    }
}
