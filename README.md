# Desafio FIAP - API .NET 9

API RESTful desenvolvida em .NET 9 para gerenciamento administrativo de alunos, turmas e matrículas.

## Pré-requisitos

- [Docker](https://www.docker.com/get-started) instalado
- [Docker Compose](https://docs.docker.com/compose/install/) instalado
- Portas disponíveis: `1433` (SQL Server) e `5000` (API)

## Como Rodar o Projeto

### 1. Clone o repositório

```bash
git clone <seu-repositorio>
cd <pasta-do-projeto>
```

### 2. Configure as variáveis de ambiente

Crie um arquivo `.env` na **raiz do projeto** (mesmo nível do `docker-compose.yml`):

```env
SA_PASSWORD=SuaSenhaForte123!
```

> ⚠️ **IMPORTANTE**: A senha deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.

### 3. Ajuste o nome do banco de dados (se necessário)

Abra o arquivo `docker-compose.yml` e verifique/ajuste o nome do banco na connection string:

```yaml
- DB_CONNECTION_STRING=Server=sqlserver,1433;Database=FIAPSecretaria;User Id=sa;Password=${SA_PASSWORD};TrustServerCertificate=True;
```

Certifique-se de que o nome do banco (`FIAPSecretaria`) corresponde ao banco criado no seu `dump.sql`. Não é necessário alterar caso o nome do banco seja o mesmo do arquivo em `database/dump.sql`. Por padrão o banco criado nesse script tem o nome de `FIAPSecretaria`, que já está por padrão como nome no arquivo `docker-compose.yml`.

### 4. Inicie os containers

```bash
docker-compose up -d --build
```

Este comando irá:
- Construir a imagem da API
- Subir o SQL Server
- Executar o script de inicialização do banco (`dump.sql`)
- Iniciar a API

### 5. Acompanhe os logs (opcional)

```bash
# Ver todos os logs
docker-compose logs -f

# Ver apenas logs da API
docker-compose logs -f api

# Ver apenas logs do SQL Server
docker-compose logs -f sqlserver
```

### 6. Acesse a aplicação

**Swagger UI (Documentação da API):**
```
http://localhost:5000/swagger
```

**Endpoints da API:**
```
http://localhost:5000/api/...
```

## Estrutura do Projeto

```
.
├── api/
│   ├── Application/       # Camada de apresentação (Controllers)
│   ├── Service/           # Camada de serviços (Lógica de negócio)
│   ├── Domain/            # Camada de domínio (Entidades, DTOs, Interfaces)
│   ├── Data/              # Camada de dados (Repositórios, Context)
│   ├── CrossCutting/      # Configurações e Dependências
│   ├── Dockerfile         # Dockerfile da API
│   └── Desafio-FIAP.sln   # Solution
├── database/
│   └── dump.sql           # Script de inicialização do banco
├── docker-compose.yml     # Orquestração dos containers
└── .env                   # Variáveis de ambiente (criar localmente)
```

## Comandos Úteis

### Parar os containers
```bash
docker-compose down
```

### Parar e remover volumes (apaga dados do banco)
```bash
docker-compose down -v
```

### Reconstruir após mudanças no código
```bash
docker-compose up -d --build
```

### Verificar status dos containers
```bash
docker-compose ps
```

### Acessar o container da API
```bash
docker exec -it desafio-fiap-api bash
```

### Acessar o SQL Server via linha de comando
```bash
docker exec -it desafio-fiap-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "SuaSenhaForte123!" -C
```

## Configuração de Ambiente

A API utiliza as seguintes variáveis de ambiente (configuradas no `docker-compose.yml`):

| Variável | Descrição | Valor Padrão |
|----------|-----------|--------------|
| `ASPNETCORE_ENVIRONMENT` | Ambiente de execução | `Development` |
| `ASPNETCORE_HTTP_PORTS` | Porta interna da API | `8080` |
| `DB_PROVIDER` | Provider do banco de dados | `SQLSERVER` |
| `DB_CONNECTION_STRING` | String de conexão com o banco | (ver docker-compose.yml) |

## Troubleshooting

### Porta 5000 já está em uso
Altere a porta no `docker-compose.yml`:
```yaml
ports:
  - "5001:8080"  # Mude 5000 para 5001 ou outra porta livre
```

### Porta 1433 já está em uso
Se você tem SQL Server instalado localmente, altere:
```yaml
ports:
  - "1434:1433"  # Mude 1433 para 1434 ou outra porta livre
```

E ajuste a connection string também:
```yaml
- DB_CONNECTION_STRING=Server=sqlserver,1434;Database=...
```

### Erro "SA_PASSWORD does not meet password requirements"
A senha deve ter pelo menos 8 caracteres com maiúsculas, minúsculas, números e caracteres especiais.

Exemplo de senha válida: `P@ssw0rd123!`

### Swagger não aparece
1. Verifique se a API está rodando: `docker-compose logs api`
2. Certifique-se de que `ASPNETCORE_ENVIRONMENT=Development` no docker-compose.yml
3. Tente acessar: `http://localhost:5000/swagger/index.html`

### Banco de dados não inicializa
1. Verifique os logs: `docker-compose logs sqlserver-init`
2. Certifique-se de que o arquivo `database/dump.sql` existe
3. Verifique se a senha no `.env` está correta

## Tecnologias Utilizadas

- **.NET 9** - Framework principal
- **Entity Framework Core** - ORM (Database First)
- **SQL Server 2022** - Banco de dados
- **Docker & Docker Compose** - Containerização
- **Swagger/OpenAPI** - Documentação da API
- **AutoMapper** - Mapeamento de objetos
- **JWT** - Autenticação

Este projeto foi desenvolvido como parte do Desafio FIAP.

---
