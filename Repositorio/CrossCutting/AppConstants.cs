namespace Repositorio.CrossCutting
{
    public class AppConstants
    {
        public const string CONNECTION_STRING = "server=us-cdbr-east-02.cleardb.com;user=bf7492f9f13e58;password=e5fc5916;database=heroku_93ac2d8811d872a;SSL Mode=None";

        public const string LISTAR_USUARIO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario;";

        public const string OBTER_USUARIO_POR_CODIGO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario WHERE codigo_usuario = @codigo_usuario;";

        public const string OBTER_USUARIO_POR_EMAIL_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario WHERE email_usuario = @email_usuario;";

        public const string CADASTRAR_USUARIO = @"INSERT INTO `heroku_93ac2d8811d872a`.`usuario`
                                                    (`codigo_usuario`,
                                                    `email_usuario`,
                                                    `senha_usuario`,
                                                    `perfil_usuario`,
                                                    `data_criacao`,
                                                    `data_alteracao`)
                                                    VALUES
                                                    (default,
                                                    @email_usuario,
                                                    @senha_Usuario,
                                                    @perfil_usuario,
                                                    default,
                                                    CURRENT_TIMESTAMP);";

        public const string ATUALIZAR_USUARIO = @"UPDATE `heroku_93ac2d8811d872a`.`usuario`
                                                    SET
                                                    `email_usuario` = @email_usuario,
                                                    `senha_usuario` = @senha_usuario,
                                                    `data_alteracao` = CURRENT_TIMESTAMP
                                                    WHERE `codigo_usuario` = @codigo_usuario;";

        public const string ATUALIZAR_SENHA_USUARIO = @"UPDATE `heroku_93ac2d8811d872a`.`usuario`
                                                    SET
                                                    `senha_usuario` = @senha_usuario,
                                                    `data_alteracao` = CURRENT_TIMESTAMP
                                                    WHERE `codigo_usuario` = @codigo_usuario;";

        public const string ATUALIZAR_EMAIL_USUARIO = @"UPDATE `heroku_93ac2d8811d872a`.`usuario`
                                                    SET
                                                    `email_usuario` = @email_usuario,
                                                    `data_alteracao` = CURRENT_TIMESTAMP
                                                    WHERE `codigo_usuario` = @codigo_usuario;";

        public const string ATUALIZAR_RAZAO_SOCIAL_EMPRESA = @"UPDATE `heroku_93ac2d8811d872a`.`empresa`
                                                    SET
                                                    `razao_social` = @razao_social,
                                                    `data_alteracao` = CURRENT_TIMESTAMP
                                                    WHERE `codigo_usuario` = @codigo_usuario;";

        public const string CADASTRAR_EMPRESA = @"INSERT INTO `heroku_93ac2d8811d872a`.`empresa`
                                                            (`codigo_empresa`,
                                                            `razao_social`,
                                                            `data_criacao`,
                                                            `data_alteracao`,
                                                            `codigo_usuario`)
                                                            VALUES
                                                            (default,
                                                            @razao_social,
                                                            CURRENT_TIMESTAMP,
                                                            CURRENT_TIMESTAMP,
                                                            @codigo_usuario);";

        public const string OBTER_EMPRESA_POR_CODIGO_USUARIO = @"SELECT * FROM heroku_93ac2d8811d872a.empresa 
                                                    WHERE codigo_usuario = @codigo_usuario;";

        public const string OBTER_COLABORADOR_POR_CODIGO_USUARIO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.colaborador
                                                                   WHERE codigo_usuario = @codigo_usuario;";

        public const string CADASTRAR_COLABORADOR_SQL = @"INSERT INTO `heroku_93ac2d8811d872a`.`colaborador`
                                                    (`codigo_colaborador`,
                                                    `nome_colaborador`,
                                                    `data_criacao`,
                                                    `data_alteracao`,
                                                    `codigo_usuario`)
                                                    VALUES
                                                    (default,
                                                    @nome_colaborador,
                                                    default,
                                                    CURRENT_TIMESTAMP,
                                                    @codigo_usuario);";

        public const string ATUALIZAR_NOME_COLABORADOR = @"UPDATE `heroku_93ac2d8811d872a`.`colaborador`
                                                      SET
                                                      `nome_colaborador` = @nome_colaborador,
                                                      `data_alteracao` = CURRENT_TIMESTAMP
                                                      WHERE `codigo_usuario` = @codigo_usuario;";

        public enum PerfilUsuario 
        {
            Colaborador = 1,
            Empresa = 2
        };
    }
}
