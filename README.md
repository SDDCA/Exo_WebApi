# Sistema de Gerenciamento de Projetos - ExoApi

---

## Descrição do Projeto
API desenvolvida em **.NET 9.0.1** para gerenciamento de projetos e usuários da empresa ExoApi. A aplicação permite operações CRUD (Create, Read, Update, Delete) em projetos e usuários, com controle de acesso baseado em autenticação JWT. A API segue as melhores práticas de desenvolvimento, incluindo retorno de dados em JSON, status HTTP adequados e documentação via Swagger.

---

## Tecnologias Utilizadas
- **.NET 9.0.1**: Framework principal
- **Entity Framework Core**: ORM para acesso ao banco de dados
- **SQL Server**: Banco de dados relacional
- **JWT (JSON Web Tokens)**: Autenticação e autorização
- **Swagger/OpenAPI**: Documentação interativa
- **ASP.NET Core**: Construção de endpoints RESTful

---

## Funcionalidades da API

### **Projetos**
- `GET /api/projetos`: Lista todos os projetos (ID, Nome, Área, Status)
- `GET /api/projetos/{id}`: Busca projeto por ID
- `POST /api/projetos`: Cria novo projeto
- `PUT /api/projetos/{id}`: Atualiza projeto completo
- `PATCH /api/projetos/{id}`: Atualiza campo específico
- `DELETE /api/projetos/{id}`: Remove projeto (requer permissão)

### **Usuários**
- `GET /api/usuarios`: Lista todos os usuários (ID, Email)
- `POST /api/usuarios`: Cria novo usuário
- `PUT /api/usuarios/{id}`: Atualiza dados do usuário
- `DELETE /api/usuarios/{id}`: Remove usuário (admin apenas)

### **Autenticação**
- `POST /api/auth/login`: Gera token JWT para acesso
- `POST /api/auth/registrar`: Cria novo usuário (admin apenas)

---

## Estrutura do Projeto

### **Models**
```csharp
public class Projeto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Area { get; set; }
    public bool Status { get; set; }
}

public class Usuario
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public string? Permissao { get; set; }
}
```

### **Controllers**
- **ProjetosController**:
  - Gerencia operações CRUD para projetos.
  - Endpoints:
    - `GET /api/projetos`: Lista todos os projetos.
    - `GET /api/projetos/{id}`: Busca um projeto específico.
    - `POST /api/projetos`: Cria um novo projeto.
    - `PUT /api/projetos/{id}`: Atualiza um projeto existente.
    - `DELETE /api/projetos/{id}`: Remove um projeto (requer permissão de administrador).

- **UsuariosController**:
  - Gerencia operações CRUD para usuários.
  - Endpoints:
    - `GET /api/usuarios`: Lista todos os usuários.
    - `POST /api/usuarios`: Cria um novo usuário.
    - `PUT /api/usuarios/{id}`: Atualiza um usuário existente.
    - `DELETE /api/usuarios/{id}`: Remove um usuário (requer permissão de administrador).

- **AuthController**:
  - Responsável pela autenticação e geração de tokens JWT.
  - Endpoints:
    - `POST /api/auth/login`: Autentica um usuário e retorna um token JWT.
    - `POST /api/auth/registrar`: Cria um novo usuário (apenas para administradores).

---

### **Services**
- **ProjetoService**:
  - Contém a lógica de negócios para operações relacionadas a projetos.
  - Validações e regras de negócio para garantir a integridade dos dados.

- **UsuarioService**:
  - Gerencia a lógica de negócios para operações relacionadas a usuários.
  - Inclui validações de senha, criptografia e controle de permissões.

- **TokenService**:
  - Responsável pela geração e validação de tokens JWT.
  - Implementa a lógica de autenticação e autorização.

---

### **Repositories**
- **ProjetoRepository**:
  - Responsável pela interação com o banco de dados para operações relacionadas a projetos.
  - Implementa métodos para consultas, inserções, atualizações e exclusões.

- **UsuarioRepository**:
  - Responsável pela interação com o banco de dados para operações relacionadas a usuários.
  - Inclui métodos para buscar usuários por e-mail, verificar credenciais e gerenciar permissões.

---

### **Configurações e Dependências**
- **appsettings.json**:
  - Configurações do banco de dados e JWT.
- **Program.cs**:
  - Configuração dos serviços e middlewares.
- **Startup.cs**:
  - Configuração de injeção de dependência.
- **Migrations**:
  - Controle de versão do banco de dados via Entity Framework.

---

### **Como Executar o Projeto**

#### Pré-requisitos
- .NET 9.0 SDK
- SQL Server 2019+
- Git

#### Passo a Passo
1. Clone o repositório:
   ```bash
   git clone https://github.com/SDDCA/Exo_WebApi.git
   cd Exo.WebApi
   ```
2. Configure o banco de dados no **ExoContext.cs**

3. Execute a aplicação:
   ```bash
   dotnet run
   ```
4. Acesse a API via Swagger:
   ```bash
   http://localhost:----/swagger
   ```
