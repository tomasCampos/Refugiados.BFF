﻿namespace Repositorio.CrossCutting
{
    public class AppConstants
    {
        public const string CONNECTION_STRING = "";

        public const string LISTAR_USUARIO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario WHERE usuario_inativo = 0;";

        public const string OBTER_USUARIO_POR_CODIGO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario WHERE codigo_usuario = @codigo_usuario AND usuario_inativo = 0;";

        public const string OBTER_USUARIO_POR_EMAIL_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.usuario WHERE email_usuario = @email_usuario AND usuario_inativo = 0;";

        public const string DELETAR_USUARIO = @"UPDATE usuario SET usuario_inativo = 1 WHERE codigo_usuario = @codigo_usuario;";

        public const string LISTAR_IDIOMA = @"select codigo_idioma, nome_idioma from idioma;";

        public const string CADASTRAR_USUARIO = @"INSERT INTO `heroku_93ac2d8811d872a`.`usuario`
                                                    (`codigo_usuario`,
                                                    `email_usuario`,
                                                    `senha_usuario`,
                                                    `telefone_usuario`,
                                                    `perfil_usuario`,
                                                    `data_criacao`,
                                                    `data_alteracao`)
                                                    VALUES
                                                    (default,
                                                    @email_usuario,
                                                    @senha_Usuario,
                                                    @telefone_usuario,
                                                    @perfil_usuario,
                                                    default,
                                                    CURRENT_TIMESTAMP);";

        public const string ATUALIZAR_USUARIO = @"UPDATE `heroku_93ac2d8811d872a`.`usuario`
                                                    SET
                                                    `email_usuario` = @email_usuario,
                                                    `senha_usuario` = @senha_usuario,
                                                    `telefone_usuario` = @telefone_usuario,
                                                    `entrevistado` = @entrevistado,
                                                    `data_alteracao` = CURRENT_TIMESTAMP
                                                    WHERE `codigo_usuario` = @codigo_usuario;";

        public const string ATUALIZAR_EMPRESA = @"UPDATE `heroku_93ac2d8811d872a`.`empresa`
                                                    SET
                                                    `razao_social` = @razao_social,
                                                    `cnpj` = @cnpj,
                                                    `nome_fantasia` = @nome_fantasia,
                                                    `data_fundacao` = @data_fundacao,
                                                    `numero_funcionarios` = @numero_funcionarios,
                                                    `data_alteracao` = CURRENT_TIMESTAMP                                                    
                                                    WHERE `codigo_usuario` = @codigo_usuario;";

        public const string CADASTRAR_EMPRESA = @"INSERT INTO `heroku_93ac2d8811d872a`.`empresa`
                                                            (`codigo_empresa`,
                                                            `razao_social`,
                                                            `data_criacao`,
                                                            `data_alteracao`,
                                                            `codigo_usuario`,
                                                            `cnpj`,
                                                            `nome_fantasia`,
                                                            `data_fundacao`,
                                                            `numero_funcionarios`,
                                                            `codigo_endereco`)
                                                            VALUES
                                                            (default,
                                                            @razao_social,
                                                            CURRENT_TIMESTAMP,
                                                            CURRENT_TIMESTAMP,
                                                            @codigo_usuario,
                                                            @cnpj,
                                                            @nome_fantasia,
                                                            @data_fundacao,
                                                            @numero_funcionarios,
                                                            @codigo_endereco);";        

        public const string OBTER_COLABORADOR_POR_CODIGO_USUARIO_SQL = @"SELECT c.codigo_colaborador, c.nome_colaborador, c.codigo_usuario, c.data_alteracao, c.data_criacao, u.email_usuario,
                                                                        c.nacionalidade, c.data_nascimento, c.data_chegada_brasil, c.area_formacao, c.escolaridade, u.entrevistado, u.telefone_usuario, 
                                                                        c.codigo_endereco, e.cidade_endereco, e.bairro_endereco, e.rua_endereco, e.numero_endereco, e.complemento_endereco, 
                                                                        e.cep_endereco, e.estado_endereco
                                                                        FROM heroku_93ac2d8811d872a.colaborador AS c
                                                                        INNER JOIN heroku_93ac2d8811d872a.usuario AS u ON c.codigo_usuario = u.codigo_usuario
                                                                        INNER JOIN endereco AS e ON e.codigo_endereco = c.codigo_endereco
                                                                        WHERE c.codigo_usuario = @codigo_usuario AND u.usuario_inativo = 0;";

        public const string LISTAR_COLABORADORES_SQL = @"SELECT DISTINCT c.codigo_colaborador, c.nome_colaborador, c.codigo_usuario, c.data_alteracao, c.data_criacao, u.email_usuario, c.nacionalidade, 
                                                        c.data_nascimento, c.data_chegada_brasil, c.area_formacao, c.escolaridade, u.entrevistado, u.telefone_usuario, c.codigo_endereco, e.cidade_endereco,
                                                        e.bairro_endereco, e.rua_endereco, e.numero_endereco, e.complemento_endereco, e.cep_endereco, e.estado_endereco
                                                        FROM heroku_93ac2d8811d872a.colaborador AS c
                                                        INNER JOIN heroku_93ac2d8811d872a.usuario AS u ON c.codigo_usuario = u.codigo_usuario
                                                        INNER JOIN endereco AS e ON e.codigo_endereco = c.codigo_endereco
                                                        {0}
                                                        {1}
                                                        WHERE u.usuario_inativo = 0
                                                        {2}
                                                        {3}
                                                        {4}
                                                        {5}
                                                        {6};";

        public const string OBTER_EMPRESA_POR_CODIGO_USUARIO = @"SELECT e.codigo_empresa, e.razao_social, e.codigo_usuario, e.data_alteracao, e.data_criacao, u.email_usuario,
                                                                e.cnpj, e.nome_fantasia, e.data_fundacao, e.numero_funcionarios, u.entrevistado, u.telefone_usuario, e.codigo_endereco,
                                                                en.cidade_endereco, en.bairro_endereco, en.rua_endereco, en.numero_endereco, en.complemento_endereco, en.cep_endereco, en.estado_endereco
                                                                FROM heroku_93ac2d8811d872a.empresa AS e
                                                                INNER JOIN heroku_93ac2d8811d872a.usuario AS u ON e.codigo_usuario = u.codigo_usuario
                                                                INNER JOIN heroku_93ac2d8811d872a.endereco AS en ON e.codigo_endereco = en.codigo_endereco
                                                                WHERE e.codigo_usuario = @codigo_usuario AND u.usuario_inativo = 0;";

        public const string LISTAR_EMPRESAS_SQL = @"SELECT e.codigo_empresa, e.razao_social, e.codigo_usuario, e.data_alteracao, e.data_criacao, u.email_usuario, e.cnpj, 
                                                    e.nome_fantasia, e.data_fundacao, e.numero_funcionarios, u.entrevistado, u.telefone_usuario, e.codigo_endereco,
                                                    en.cidade_endereco, en.bairro_endereco, en.rua_endereco, en.numero_endereco, en.complemento_endereco, en.cep_endereco, en.estado_endereco
                                                    FROM heroku_93ac2d8811d872a.empresa AS e
                                                    INNER JOIN heroku_93ac2d8811d872a.usuario AS u ON e.codigo_usuario = u.codigo_usuario
                                                    INNER JOIN heroku_93ac2d8811d872a.endereco AS en ON e.codigo_endereco = en.codigo_endereco
                                                    {0}
                                                    WHERE u.usuario_inativo = 0
                                                    {1}
                                                    {2}
                                                    {3}
                                                    {4};";

        public const string CADASTRAR_COLABORADOR_SQL = @"INSERT INTO `heroku_93ac2d8811d872a`.`colaborador`
                                                    (`codigo_colaborador`,
                                                    `nome_colaborador`,
                                                    `data_criacao`,
                                                    `data_alteracao`,
                                                    `nacionalidade`,
                                                    `data_nascimento`,
                                                    `data_chegada_brasil`,
                                                    `area_formacao`,
                                                    `escolaridade`,
                                                    `codigo_usuario`,
                                                    `codigo_endereco`)
                                                    VALUES
                                                    (default,
                                                    @nome_colaborador,
                                                    CURRENT_TIMESTAMP,
                                                    CURRENT_TIMESTAMP,
                                                    @nacionalidade,
                                                    @data_nascimento,
                                                    @data_chegada_brasil,
                                                    @area_formacao,
                                                    @escolaridade,
                                                    @codigo_usuario,
                                                    @codigo_endereco);";

        public const string ATUALIZAR_COLABORADOR = @"UPDATE `heroku_93ac2d8811d872a`.`colaborador`
                                                    SET
                                                    `nome_colaborador` = @nome_colaborador,
                                                    `data_alteracao` = CURRENT_TIMESTAMP,
                                                    `nacionalidade` = @nacionalidade,
                                                    `data_nascimento` = @data_nascimento,
                                                    `data_chegada_brasil` = @data_chegada_brasil,
                                                    `area_formacao` = @area_formacao,
                                                    `escolaridade` = @escolaridade
                                                     WHERE `codigo_usuario` = @codigo_usuario;";

        public const string CADASTRAR_IDIOMA_COLABORADOR = @"INSERT INTO `heroku_93ac2d8811d872a`.`colaborador_idioma`
                                                            (`codigo_colaborador_idioma`,
                                                            `codigo_colaborador`,
                                                            `codigo_idioma`)
                                                            VALUES
                                                            (default,
                                                            @codigo_colaborador,
                                                            @codigo_idioma);";

        public const string LISTAR_IDIOMA_COLABORADOR = @"SELECT i.codigo_idioma, i.nome_idioma FROM colaborador_idioma AS ci
                                                        INNER JOIN idioma AS i ON ci.codigo_idioma = i.codigo_idioma
                                                        WHERE codigo_colaborador = @codigo_colaborador;";

        public const string EXCLUIR_IDIOMA_COLABORADOR = @"DELETE FROM colaborador_idioma WHERE codigo_colaborador = @codigo_colaborador";

        public const string EXCLUIR_AREA_TRABALHO_COLABORADOR = @"DELETE FROM colaborador_area_trabalho WHERE codigo_colaborador = @codigo_colaborador";

        public const string EXCLUIR_AREA_TRABALHO_EMPRESA = @"DELETE FROM empresa_area_trabalho WHERE codigo_empresa = @codigo_empresa";

        public const string LISTAR_AREA_TRABALHO = "SELECT * FROM area_trabalho";

        public const string LISTAR_AREA_TRABALHO_COLABORADOR = @"SELECT at.codigo_area_trabalho, at.nome_area_trabalho FROM colaborador_area_trabalho AS cat
                                                                INNER JOIN area_trabalho AS at ON cat.codigo_area_trabalho = at.codigo_area_trabalho
                                                                WHERE cat.codigo_colaborador =  @codigo_colaborador;";

        public const string LISTAR_AREA_TRABALHO_EMPRESA = @"SELECT at.codigo_area_trabalho, at.nome_area_trabalho FROM empresa_area_trabalho AS eat
                                                                INNER JOIN area_trabalho AS at ON eat.codigo_area_trabalho = at.codigo_area_trabalho
                                                                WHERE eat.codigo_empresa =  @codigo_empresa;";

        public const string CADASTRAR_AREA_TRABALHO_COLABORADOR = @"INSERT INTO `heroku_93ac2d8811d872a`.`colaborador_area_trabalho`
                                                                    (`codigo_colaborador_area_trabalho`,
                                                                    `codigo_colaborador`,
                                                                    `codigo_area_trabalho`)
                                                                    VALUES
                                                                    (default,
                                                                    @codigo_colaborador,
                                                                    @codigo_area_trabalho);";

        public const string CADASTRAR_AREA_TRABALHO_EMPRESA = @"INSERT INTO `heroku_93ac2d8811d872a`.`empresa_area_trabalho`
                                                                (`codigo_empresa_area_trabalho`,
                                                                `codigo_empresa`,
                                                                `codigo_area_trabalho`)
                                                                VALUES
                                                                (default,
                                                                @codigo_empresa,
                                                                @codigo_area_trabalho);";

        public const string CADASTRAR_ENDERECO = @"INSERT INTO `heroku_93ac2d8811d872a`.`endereco`
                                                    (`codigo_endereco`,
                                                    `chave_identificacao_endereco`,
                                                    `cidade_endereco`,
                                                    `bairro_endereco`,
                                                    `rua_endereco`,
                                                    `numero_endereco`,
                                                    `complemento_endereco`,
                                                    `cep_endereco`,
                                                    `estado_endereco`)
                                                    VALUES
                                                    (default,
                                                    @chave_identificacao_endereco,
                                                    @cidade_endereco,
                                                    @bairro_endereco,
                                                    @rua_endereco,
                                                    @numero_endereco,
                                                    @complemento_endereco,
                                                    @cep_endereco,
                                                    @estado_endereco);";

        public const string OBTER_ENDERECO_POR_CODIGO = "SELECT * FROM endereco WHERE codigo_endereco = @codigo_endereco";

        public const string OBTER_ENDERECO_POR_CEP_NUMERO_E_COMPLEMENTO = @"SELECT * FROM endereco 
                                                                          WHERE chave_identificacao_endereco = @chave_identificacao_endereco;";

        public const string ALTERAR_ENDERECO = @"UPDATE `heroku_93ac2d8811d872a`.`endereco`
                                                SET
                                                `cidade_endereco` = @cidade_endereco,
                                                `bairro_endereco` = @bairro_endereco,
                                                `rua_endereco` = @rua_endereco,
                                                `numero_endereco` = @numero_endereco,
                                                `complemento_endereco` = @complemento_endereco,
                                                `cep_endereco` = @cep_endereco,
                                                `estado_endereco` = @estado_endereco
                                                WHERE `codigo_endereco` = @codigo_endereco;";

        public enum PerfilUsuario 
        {
            Colaborador = 1,
            Empresa = 2
        };
    }
}
