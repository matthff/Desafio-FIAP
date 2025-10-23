\# 🧪 Teste Técnico FIAP

## 🎯 Desafio

O candidato deverá desenvolver uma aplicação para a **Secretaria da FIAP**.  
O objetivo é criar uma **API RESTful** que será consumida por páginas de um **sistema administrativo**, permitindo aos usuários **gerenciar alunos, turmas e matrículas**.

O administrador terá acesso a três entidades dentro do sistema:

1. **ALUNO**
2. **TURMA**
3. **MATRÍCULA**

---

### 👨‍🎓 ALUNO
O sistema deve armazenar:
- Nome  
- Data de nascimento  
- CPF  
- E-mail  
- Senha  

Deve ser possível realizar:
- Cadastro  
- Listagem  
- Edição  
- Exclusão  

*(CRUD completo)*

---

### 🏫 TURMA
Deve armazenar:
- Nome  
- Descrição  

Deve ser possível realizar:
- Cadastro  
- Listagem  
- Edição  
- Exclusão  

*(CRUD completo)*

---

### 🧾 MATRÍCULA
O sistema deve permitir:
- Realizar a matrícula de um aluno em uma turma.  
- Visualizar alunos matriculados em uma determinada turma.

---

## 🧩 Bônus (opcionais, mas recomendados)

### 🎯 BÔNUS 1
Criar **testes unitários** para cada caso de uso, utilizando:
- `xUnit`
- `Moq` ou `NSubstitute`
- `FluentAssertions`

### 💻 BÔNUS 2
Desenvolver o **front-end** que consumirá as rotas da API RESTful.  
Preferencialmente utilizar tecnologias do ecossistema .NET:
- MVC
- Razor Pages
- Blazor  

Se utilizar React, descrever no `README.md`:
- Como executar o projeto  
- Versão do Node.js necessária  

---

## ⚙️ Requisitos Funcionais (Obrigatórios)

### ✅ REQUISITO 1
As listagens (aluno e turma) devem:
- Ser ordenadas alfabeticamente.  
- Ser paginadas a cada **10 itens** por padrão.

### ✅ REQUISITO 2
Na listagem da turma, exibir **quantos alunos** cada turma possui.

### ✅ REQUISITO 3
Validações:
- Nome do aluno e da turma: **entre 3 e 100 caracteres**.  
- Descrição da turma: **entre 10 e 250 caracteres**.

### ✅ REQUISITO 4
Validações obrigatórias:
- Todos os campos devem ser preenchidos.  
- CPF válido (11 dígitos, formato correto etc.).  
- E-mail válido (contém `@`, domínio válido etc.).  
- Data de nascimento válida (não futura, nem extremamente antiga).

### ✅ REQUISITO 5
Um aluno **não pode ser matriculado duas vezes** no mesmo curso.

### ✅ REQUISITO 6
Um aluno só pode ser cadastrado **uma única vez** pelo **CPF** ou **e-mail** (unicidade).

### ✅ REQUISITO 7
O sistema não deve permitir **senhas fracas**:
- Mínimo de **8 caracteres**
- Deve conter: letras maiúsculas e minúsculas, números e símbolos.

### ✅ REQUISITO 8
A senha deve ser armazenada **criptografada** utilizando **funções hash**.

### ✅ REQUISITO 9
Permitir busca de alunos:
- Pelo **nome**
- Pelo **CPF**

### ✅ REQUISITO 10
Adicionar **autenticação e autorização** com **JWT (JSON Web Token)**.  
Todas as rotas devem ser acessadas **somente por um administrador autenticado**, via e-mail e senha.

---

## 🧱 Requisitos Não Funcionais (Obrigatórios)

### 🔹 REQUISITO 1
Respeitar os padrões de **APIs RESTful**:
- Verbos HTTP corretos (`GET`, `POST`, `PUT`, `DELETE`, etc.)  
- Códigos de status HTTP adequados (`200`, `201`, `400`, `401`, `403`, `404`, `500`, `503`, etc.)

### 🔹 REQUISITO 2
Modelagem adequada do **banco de dados**:
- Uso correto de **chaves primárias**, **estrangeiras**, **índices** e **relacionamentos**.  
- Caso utilize **Entity Framework Core**, desenvolver com o conceito de **database first**.

### 🔹 REQUISITO 3
Documentar a API com **Swagger/OpenAPI**:
- Endpoints agrupados por recurso (ex: `/api/alunos`, `/api/turmas`, `/api/matriculas`)  
- Descrições claras (`summary` e `description`)  
- Métodos HTTP corretos  
- Versionamento explícito (`/api/v1/...`)  
- Uso de **tags** para organizar endpoints por contexto.

---

## 🧰 Tecnologias

- **Framework:** .NET Core ≥ 6  
- **Banco de Dados:** SQL Server  
- **ORM:** `Dapper` ou **Entity Framework Core**

---

## 🧠 Boas Práticas Recomendadas

- Clean Architecture  
- DDD (Domain-Driven Design)  
- TDD (Test-Driven Development)  
- CQRS (Command Query Responsibility Segregation)  
- Repository Pattern  
- Result Pattern  
- Problem Details  
- Clean Code  

---

## 📦 Orientações para Entrega

- Adicionar **script de criação do banco de dados** em `dump.sql` (com tabelas e dados iniciais).  
- Adicionar orientações de **instalação e execução** no `README.md`.  
- Enviar o(s) projeto(s) para o **GitHub público** e responder o e-mail com o link.  
- **Docker** é opcional, mas se usado:
  - Deve permitir inicialização completa com `docker compose up --build`.  
  - Caso haja passos manuais, descrevê-los claramente no `README.md`.

---

## 💡 Observações Gerais

Você tem **total liberdade** para aprimorar o projeto:
- Melhorias como novas validações, design patterns, layout aprimorado, classes customizadas e uso de pacotes externos são bem-vindos.  
- Os **bônus** não são obrigatórios, mas agregam valor e demonstram domínio técnico.

Para o front-end, recomenda-se o uso de frameworks como:
- **Bootstrap**, **DataTables**, ou qualquer outro que melhore a experiência do usuário.

> Um bom sistema é aquele que resolve o problema proposto de forma **eficiente**, **organizada** e **funcional**.  
> Foque em entregar uma solução **clara**, **consistente** e **bem estruturada**, usando as tecnologias adequadas.
