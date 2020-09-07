using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        private const string LISTAR_USUARIO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario;";

        public UsuarioRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public List<UsuarioDto> ListarUsuarios() 
        {
            var result = _dataBase.Listar<UsuarioDto>(LISTAR_USUARIO_SQL);
            return result.ToList();
        }
    }

    public interface IUsuarioRepositorio 
    {
        List<UsuarioDto> ListarUsuarios();
    }
}
