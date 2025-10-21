-- =============================================
-- Script de Criação e População de Dados
-- Sistema de Gerenciamento - Secretaria FIAP
-- =============================================

USE master;
GO

-- Criar o banco de dados se não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FIAPSecretaria')
BEGIN
    CREATE DATABASE FIAPSecretaria;
END
ELSE
BEGIN
    PRINT 'Banco de dados FIAPSecretaria já existe. Pulando inicialização.';
    RETURN;
END
GO

USE FIAPSecretaria;
GO

-- =============================================
-- Tabela: ADMINISTRADOR
-- Para autenticação JWT (REQUISITO 10)
-- =============================================
IF OBJECT_ID('dbo.Administrador', 'U') IS NOT NULL
    DROP TABLE dbo.Administrador;
GO

CREATE TABLE dbo.Administrador (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    SenhaHash NVARCHAR(500) NOT NULL, -- Senha criptografada (REQUISITO 8)
    Ativo BIT DEFAULT 1,
    DataCadastro DATETIME DEFAULT GETDATE(),
    DataAtualizacao DATETIME DEFAULT GETDATE(),
    CONSTRAINT CK_Administrador_Nome_Length CHECK (LEN(Nome) >= 3 AND LEN(Nome) <= 100),
    CONSTRAINT CK_Administrador_Email_Format CHECK (Email LIKE '%_@__%.__%')
);
GO

-- =============================================
-- Tabela: ALUNO
-- =============================================
IF OBJECT_ID('dbo.Aluno', 'U') IS NOT NULL
    DROP TABLE dbo.Aluno;
GO

CREATE TABLE dbo.Aluno (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    DataNascimento DATE NOT NULL,
    CPF CHAR(11) NOT NULL UNIQUE, -- REQUISITO 6: Unicidade por CPF
    Email NVARCHAR(200) NOT NULL UNIQUE, -- REQUISITO 6: Unicidade por Email
    SenhaHash NVARCHAR(500) NOT NULL, -- REQUISITO 8: Senha criptografada
    Ativo BIT DEFAULT 1,
    DataCadastro DATETIME DEFAULT GETDATE(),
    DataAtualizacao DATETIME DEFAULT GETDATE(),
    
    -- REQUISITO 3: Validação de tamanho do nome (3-100 caracteres)
    CONSTRAINT CK_Aluno_Nome_Length CHECK (LEN(Nome) >= 3 AND LEN(Nome) <= 100),
    
    -- REQUISITO 4: Validação de CPF (11 dígitos)
    CONSTRAINT CK_Aluno_CPF_Length CHECK (LEN(CPF) = 11 AND CPF NOT LIKE '%[^0-9]%'),
    
    -- REQUISITO 4: Validação de Email
    CONSTRAINT CK_Aluno_Email_Format CHECK (Email LIKE '%_@__%.__%'),
    
    -- REQUISITO 4: Data de nascimento válida (entre 1900 e data atual)
    CONSTRAINT CK_Aluno_DataNascimento CHECK (
        DataNascimento >= '1900-01-01' AND 
        DataNascimento <= CAST(GETDATE() AS DATE)
    )
);
GO

-- =============================================
-- Tabela: TURMA
-- =============================================
IF OBJECT_ID('dbo.Turma', 'U') IS NOT NULL
    DROP TABLE dbo.Turma;
GO

CREATE TABLE dbo.Turma (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(250) NOT NULL,
    Ativo BIT DEFAULT 1,
    DataCadastro DATETIME DEFAULT GETDATE(),
    DataAtualizacao DATETIME DEFAULT GETDATE(),
    
    -- REQUISITO 3: Validação de tamanho do nome (3-100 caracteres)
    CONSTRAINT CK_Turma_Nome_Length CHECK (LEN(Nome) >= 3 AND LEN(Nome) <= 100),
    
    -- REQUISITO 3: Validação de tamanho da descrição (10-250 caracteres)
    CONSTRAINT CK_Turma_Descricao_Length CHECK (LEN(Descricao) >= 10 AND LEN(Descricao) <= 250)
);
GO

-- =============================================
-- Tabela: MATRICULA
-- =============================================
IF OBJECT_ID('dbo.Matricula', 'U') IS NOT NULL
    DROP TABLE dbo.Matricula;
GO

CREATE TABLE dbo.Matricula (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AlunoId INT NOT NULL,
    TurmaId INT NOT NULL,
    DataMatricula DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20) DEFAULT 'Ativa',
    
    -- Chaves estrangeiras
    CONSTRAINT FK_Matricula_Aluno FOREIGN KEY (AlunoId) 
        REFERENCES dbo.Aluno(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Matricula_Turma FOREIGN KEY (TurmaId) 
        REFERENCES dbo.Turma(Id) ON DELETE CASCADE,
    
    -- REQUISITO 5: Aluno não pode ser matriculado duas vezes na mesma turma
    CONSTRAINT UQ_Matricula_Aluno_Turma UNIQUE (AlunoId, TurmaId),
    
    -- Validação de status
    CONSTRAINT CK_Matricula_Status CHECK (Status IN ('Ativa', 'Cancelada', 'Concluida'))
);
GO

-- Índices para ADMINISTRADOR
CREATE INDEX IX_Administrador_Email ON dbo.Administrador(Email);
CREATE INDEX IX_Administrador_Ativo ON dbo.Administrador(Ativo);

-- Índices para ALUNO
-- REQUISITO 9: Busca por Nome e CPF
CREATE INDEX IX_Aluno_Nome ON dbo.Aluno(Nome);
CREATE INDEX IX_Aluno_CPF ON dbo.Aluno(CPF);
CREATE INDEX IX_Aluno_Email ON dbo.Aluno(Email);
CREATE INDEX IX_Aluno_Ativo ON dbo.Aluno(Ativo);

-- Índices para TURMA
-- REQUISITO 1: Ordenação alfabética
CREATE INDEX IX_Turma_Nome ON dbo.Turma(Nome);
CREATE INDEX IX_Turma_Ativo ON dbo.Turma(Ativo);

-- Índices para MATRICULA
CREATE INDEX IX_Matricula_AlunoId ON dbo.Matricula(AlunoId);
CREATE INDEX IX_Matricula_TurmaId ON dbo.Matricula(TurmaId);
CREATE INDEX IX_Matricula_Status ON dbo.Matricula(Status);
GO

-- =============================================
-- Views para facilitar consultas
-- =============================================

-- View para listar turmas com quantidade de alunos (REQUISITO 2)
IF OBJECT_ID('dbo.vw_TurmasComAlunos', 'V') IS NOT NULL
    DROP VIEW dbo.vw_TurmasComAlunos;
GO

CREATE VIEW dbo.vw_TurmasComAlunos AS
SELECT 
    t.Id,
    t.Nome,
    t.Descricao,
    t.Ativo,
    t.DataCadastro,
    t.DataAtualizacao,
    COUNT(m.Id) AS QuantidadeAlunos
FROM dbo.Turma t
LEFT JOIN dbo.Matricula m ON t.Id = m.TurmaId AND m.Status = 'Ativa'
GROUP BY t.Id, t.Nome, t.Descricao, t.Ativo, t.DataCadastro, t.DataAtualizacao;
GO

-- View para listar alunos com suas turmas
IF OBJECT_ID('dbo.vw_AlunosComTurmas', 'V') IS NOT NULL
    DROP VIEW dbo.vw_AlunosComTurmas;
GO

CREATE VIEW dbo.vw_AlunosComTurmas AS
SELECT 
    a.Id AS AlunoId,
    a.Nome AS AlunoNome,
    a.CPF,
    a.Email,
    a.DataNascimento,
    t.Id AS TurmaId,
    t.Nome AS TurmaNome,
    m.DataMatricula,
    m.Status AS StatusMatricula
FROM dbo.Aluno a
LEFT JOIN dbo.Matricula m ON a.Id = m.AlunoId
LEFT JOIN dbo.Turma t ON m.TurmaId = t.Id;
GO

-- =============================================
-- Stored Procedures úteis
-- =============================================

-- Procedure para buscar alunos (REQUISITO 9: Busca por Nome ou CPF)
IF OBJECT_ID('dbo.sp_BuscarAlunos', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BuscarAlunos;
GO

CREATE PROCEDURE dbo.sp_BuscarAlunos
    @Nome NVARCHAR(100) = NULL,
    @CPF CHAR(11) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    -- REQUISITO 1: Paginação e ordenação alfabética
    SELECT 
        Id,
        Nome,
        DataNascimento,
        CPF,
        Email,
        Ativo,
        DataCadastro,
        DataAtualizacao
    FROM dbo.Aluno
    WHERE 
        (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%')
        AND (@CPF IS NULL OR CPF = @CPF)
        AND Ativo = 1
    ORDER BY Nome
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    
    -- Retornar total de registros
    SELECT COUNT(*) AS TotalRegistros
    FROM dbo.Aluno
    WHERE 
        (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%')
        AND (@CPF IS NULL OR CPF = @CPF)
        AND Ativo = 1;
END;
GO

-- =============================================
-- Popular com dados de exemplo
-- =============================================

-- Inserir Administrador padrão
-- Senha: Admin@123 (deve ser hashada na aplicação - BCrypt)
-- Hash exemplo abaixo é apenas ilustrativo
INSERT INTO dbo.Administrador (Nome, Email, SenhaHash)
VALUES 
    ('Administrador Principal', 'admin@fiap.com.br', '$2a$11$examplehash123456789012345678901234567890123456');
GO

-- Inserir Alunos
-- Senhas devem ser hashadas na aplicação real
INSERT INTO dbo.Aluno (Nome, DataNascimento, CPF, Email, SenhaHash)
VALUES 
    ('João Silva Santos', '2000-05-15', '12345678901', 'joao.silva@fiap.com.br', '$2a$11$examplehash1'),
    ('Maria Oliveira Costa', '1999-08-22', '23456789012', 'maria.oliveira@fiap.com.br', '$2a$11$examplehash2'),
    ('Pedro Henrique Alves', '2001-03-10', '34567890123', 'pedro.alves@fiap.com.br', '$2a$11$examplehash3'),
    ('Ana Carolina Souza', '2000-11-30', '45678901234', 'ana.souza@fiap.com.br', '$2a$11$examplehash4'),
    ('Carlos Eduardo Lima', '1998-07-18', '56789012345', 'carlos.lima@fiap.com.br', '$2a$11$examplehash5'),
    ('Juliana Ferreira Rocha', '2002-01-25', '67890123456', 'juliana.rocha@fiap.com.br', '$2a$11$examplehash6'),
    ('Lucas Martins Pereira', '2001-09-14', '78901234567', 'lucas.pereira@fiap.com.br', '$2a$11$examplehash7'),
    ('Fernanda Santos Dias', '2000-04-08', '89012345678', 'fernanda.dias@fiap.com.br', '$2a$11$examplehash8'),
    ('Rafael Costa Barbosa', '1999-12-20', '90123456789', 'rafael.barbosa@fiap.com.br', '$2a$11$examplehash9'),
    ('Beatriz Almeida Nunes', '2001-06-05', '01234567890', 'beatriz.nunes@fiap.com.br', '$2a$11$examplehash10'),
    ('Gabriel Rodrigues', '2000-02-14', '11122233344', 'gabriel.rodrigues@fiap.com.br', '$2a$11$examplehash11'),
    ('Isabela Mendes Silva', '1999-07-30', '22233344455', 'isabela.mendes@fiap.com.br', '$2a$11$examplehash12'),
    ('Matheus Oliveira', '2001-11-08', '33344455566', 'matheus.oliveira@fiap.com.br', '$2a$11$examplehash13'),
    ('Larissa Fernandes', '2000-09-19', '44455566677', 'larissa.fernandes@fiap.com.br', '$2a$11$examplehash14'),
    ('Thiago Santos', '1998-12-03', '55566677788', 'thiago.santos@fiap.com.br', '$2a$11$examplehash15');
GO

-- Inserir Turmas (descrições com 10-250 caracteres - REQUISITO 3)
INSERT INTO dbo.Turma (Nome, Descricao)
VALUES 
    ('Desenvolvimento .NET 2025', 'Turma focada em desenvolvimento de aplicações modernas com .NET Core, Entity Framework, APIs RESTful e arquitetura de microsserviços utilizando as melhores práticas do mercado.'),
    ('Java Spring Boot Avançado', 'Turma especializada em desenvolvimento backend com Java e Spring Framework, abordando Spring Boot, Spring Data, Spring Security e integração com bancos de dados relacionais.'),
    ('Arquitetura de Software', 'Curso sobre padrões de arquitetura como Clean Architecture, DDD, CQRS, microserviços e cloud computing com foco em soluções escaláveis e resilientes.'),
    ('DevOps e CI/CD', 'Práticas de DevOps incluindo Docker, Kubernetes, Jenkins, GitLab CI, monitoramento e automação de pipelines de integração e entrega contínua em ambientes cloud.'),
    ('Banco de Dados Avançado', 'Otimização de queries, modelagem de dados, índices, stored procedures, triggers, administração de SGBDs relacionais e introdução a bancos NoSQL modernos.'),
    ('Mobile Development', 'Desenvolvimento de aplicações mobile multiplataforma com React Native e Flutter, incluindo consumo de APIs, gerenciamento de estado e publicação nas stores.'),
    ('Python para Data Science', 'Análise de dados com Python utilizando pandas, numpy, matplotlib, machine learning com scikit-learn e visualização de dados para tomada de decisões estratégicas.'),
    ('Segurança da Informação', 'Práticas de segurança em desenvolvimento, OWASP Top 10, criptografia, autenticação, autorização, testes de penetração e conformidade com LGPD e GDPR.'),
    ('Cloud Computing AWS', 'Arquitetura e serviços AWS incluindo EC2, S3, RDS, Lambda, API Gateway, CloudFormation e implementação de soluções serverless e escaláveis na nuvem.'),
    ('Front-end React Avançado', 'Desenvolvimento front-end moderno com React, TypeScript, Redux, hooks customizados, performance optimization, testes unitários e integração com APIs REST.');
GO

-- Inserir Matrículas
INSERT INTO dbo.Matricula (AlunoId, TurmaId, Status)
VALUES 
    -- Alunos com múltiplas matrículas
    (1, 1, 'Ativa'), (1, 3, 'Ativa'), (1, 4, 'Ativa'),
    (2, 2, 'Ativa'), (2, 5, 'Ativa'), (2, 8, 'Ativa'),
    (3, 1, 'Ativa'), (3, 6, 'Ativa'),
    (4, 3, 'Ativa'), (4, 4, 'Ativa'), (4, 5, 'Ativa'),
    (5, 2, 'Ativa'), (5, 3, 'Ativa'), (5, 9, 'Ativa'),
    (6, 6, 'Ativa'), (6, 10, 'Ativa'),
    (7, 1, 'Ativa'), (7, 4, 'Ativa'), (7, 7, 'Ativa'),
    (8, 2, 'Ativa'), (8, 3, 'Ativa'), (8, 5, 'Ativa'),
    (9, 1, 'Ativa'), (9, 6, 'Ativa'),
    (10, 4, 'Ativa'), (10, 5, 'Ativa'), (10, 8, 'Ativa'),
    (11, 7, 'Ativa'), (11, 9, 'Ativa'),
    (12, 10, 'Ativa'), (12, 6, 'Ativa'),
    (13, 3, 'Ativa'), (13, 8, 'Ativa'), (13, 9, 'Ativa'),
    (14, 2, 'Ativa'), (14, 7, 'Ativa'),
    (15, 1, 'Ativa'), (15, 4, 'Ativa'), (15, 10, 'Ativa');
GO
