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

        #region QUERIES 

        private const string LISTAR_USUARIO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario;";

        private const string OBTER_USUARIO_POR_CODIGO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario WHERE codigo_usuario = @codigo_usuario;";

        private const string OBTER_USUARIO_POR_EMAIL_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario WHERE email_usuario = @email_usuario;";

        private const string CADASTRAR_USUARIO = @"INSERT INTO `heroku_93ac2d8811d872a`.`usuario`
                                                    (`codigo_usuario`,
                                                    `email_usuario`,
                                                    `senha_usuario`,
                                                    `data_criacao`,
                                                    `data_alteracao`)
                                                    VALUES
                                                    (default,
                                                    @email_usuario,
                                                    @senha_Usuario,
                                                     default,
                                                    CURRENT_TIMESTAMP);";

        private const string ATUALIZAR_USUARIO = @"UPDATE `heroku_93ac2d8811d872a`.`usuario`
                                                    SET
                                                    `email_usuario` = @email_usuario,
                                                    `senha_usuario` = @senha_usuario,
                                                    `data_alteracao` = CURRENT_TIMESTAMP
                                                    WHERE `codigo_usuario` = @codigo_usuario;";

        private const string ATUALIZAR_SENHA_USUARIO = @"UPDATE `heroku_93ac2d8811d872a`.`usuario`
                                                    SET
                                                    `senha_usuario` = @senha_usuario,
                                                    `data_alteracao` = CURRENT_TIMESTAMP
                                                    WHERE `codigo_usuario` = @codigo_usuario;";

        private const string ATUALIZAR_EMAIL_USUARIO = @"UPDATE `heroku_93ac2d8811d872a`.`usuario`
                                                    SET
                                                    `email_usuario` = @email_usuario,
                                                    `data_alteracao` = CURRENT_TIMESTAMP
                                                    WHERE `codigo_usuario` = @codigo_usuario;";
        #endregion

        public UsuarioRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public List<UsuarioDto> ListarUsuarios() 
        {
            var result = _dataBase.Selecionar<UsuarioDto>(LISTAR_USUARIO_SQL).ToList();
            return result;
        }

        public List<UsuarioDto> ListarUsuarios(int codigo) 
        {
            var result = _dataBase.Selecionar<UsuarioDto>(OBTER_USUARIO_POR_CODIGO_SQL, new { codigo_usuario = codigo }).ToList();
            return result;
        }

        public List<UsuarioDto> ListarUsuarios(string email)
        {
            var result = _dataBase.Selecionar<UsuarioDto>(OBTER_USUARIO_POR_EMAIL_SQL, new { email_usuario = email }).ToList();
            return result;
        }

        public void CadastrarUsuario(string email, string senha) 
        {
            _dataBase.Executar(CADASTRAR_USUARIO, new { email_usuario = email, senha_usuario = senha });
        }

        public void AtualizarUsuario(string email, string senha, int codigo)
        {
            if(string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(senha))
                _dataBase.Executar(ATUALIZAR_SENHA_USUARIO, new { senha_usuario = senha, codigo_usuario = codigo });
            else if(string.IsNullOrWhiteSpace(senha) && !string.IsNullOrWhiteSpace(email))
                _dataBase.Executar(ATUALIZAR_EMAIL_USUARIO, new { email_usuario = email, codigo_usuario = codigo });
            else if(!string.IsNullOrWhiteSpace(senha) && !string.IsNullOrWhiteSpace(email))
                _dataBase.Executar(ATUALIZAR_USUARIO, new { email_usuario = email, senha_usuario = senha, codigo_usuario = codigo });
        }
    }

    public interface IUsuarioRepositorio 
    {
        List<UsuarioDto> ListarUsuarios();
        public List<UsuarioDto> ListarUsuarios(int codigo);
        public List<UsuarioDto> ListarUsuarios(string email);
        void CadastrarUsuario(string email, string senha);
        public void AtualizarUsuario(string email, string senha, int codigo);
    }
}
