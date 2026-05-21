# 📚 BookWise API — Sistema de Gestão e Empréstimo de Livros

Este repositório contém uma **Web API de alto desempenho** desenvolvida em **C#** utilizando as tecnologias mais modernas do ecossistema **.NET 8**. O projeto foi estruturado com foco em simplicidade, eficiência e organização de código, implementando o conceito de **Minimal APIs** para construir uma aplicação extremamente leve e escalável.

A API simula o controle interno de uma biblioteca ou acervo pessoal, permitindo o cadastro de livros, busca detalhada, gerenciamento completo do status de empréstimo e devolução, e filtragem avançada por disponibilidade.

---

## 🚀 Tecnologias Utilizadas

A pilha tecnológica foi criteriosamente escolhida para garantir rapidez no desenvolvimento, facilidade de implantação e robustez:

*   **Linguagem:** C# 12 (com recursos modernos de sintaxe)
*   **Framework Principal:** ASP.NET Core (.NET 8.0)
*   **Padrão de Rotas:** Minimal APIs (rotas expressivas e de baixíssimo overhead)
*   **Persistência de Dados:** Entity Framework Core 8.0 (EF Core)
*   **Banco de Dados:** SQLite (leve, embarcado e ideal para portabilidade)
*   **Versionamento do Banco:** EF Core Migrations
*   **Ferramenta de Testes:** Arquivo HTTP Nativo (`.http`) para execução direta pelo VS Code ou Visual Studio

---

## 🏛️ Arquitetura e Organização do Projeto

O projeto segue um padrão arquitetural enxuto e de fácil manutenção, dividido logicamente em componentes estruturados:

```
├── GustavoHenriqueCavalliGarcia/
│   ├── Data/
│   │   └── AppDataContext.cs       # Configuração do DbContext do EF Core e conexão SQLite
│   ├── Migrations/                 # Histórico de alterações e esquema do banco de dados
│   ├── Models/
│   │   └── Livro.cs                # Classe de domínio (Entidade Livro)
│   ├── Properties/
│   │   └── launchSettings.json     # Configuração de inicialização do servidor local
│   ├── appsettings.json            # Configuração geral do app
│   ├── Pedro_Gustavo.db            # Banco de dados SQLite pré-configurado
│   ├── Program.cs                  # Ponto de entrada (Startup) e definição de endpoints
│   ├── testes.http                 # Script de testes automatizados das rotas da API
│   └── GustavoHenriqueCavalliGarcia.csproj # Gerenciador de dependências e metadados
└── PedroHenriquePoliceno.sln       # Arquivo de solução do Visual Studio
```

### 🔹 Modelo de Dados (`Livro`)
O livro é a entidade central do domínio, contendo regras implícitas como a data de cadastro automática e o estado de empréstimo:

```csharp
public class Livro
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Autor { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public bool Emprestado { get; set; } = false;
}
```

---

## 🔌 Endpoints da API

A API expõe rotas RESTful bem estruturadas sob o prefixo `/api/livro`. Abaixo está a documentação completa dos recursos disponíveis:

| Método | Endpoint | Descrição | Regras de Negócio / Status HTTP |
| :--- | :--- | :--- | :--- |
| **POST** | `/api/livro/cadastrar` | Cadastra um novo livro no sistema | Evita duplicidade pelo nome (`400 Bad Request` se já existir; `201 Created` em caso de sucesso). |
| **GET** | `/api/livro/listar` | Lista todos os livros cadastrados | Retorna `404 Not Found` caso a biblioteca esteja vazia ou `200 Ok` com a lista. |
| **GET** | `/api/livro/buscar/{nome}` | Busca um livro específico pelo nome | Busca exata pelo nome do livro (`200 Ok` se encontrado; `404 Not Found` se não existir). |
| **PUT** | `/api/livro/emprestar` | Altera o status do livro para Emprestado | Altera a flag para `true` (`404 Not Found` se ID inválido; `400 Bad Request` se já emprestado; `200 Ok` com o livro atualizado). |
| **PUT** | `/api/livro/devolver` | Altera o status do livro para Disponível | Altera a flag para `false` (`404 Not Found` se ID inválido; `400 Bad Request` se não estiver emprestado; `200 Ok` com o livro atualizado). |
| **GET** | `/api/livro/disponiveis` | Lista todos os livros livres para empréstimo | Retorna a lista contendo apenas registros onde `Emprestado == false`. |
| **GET** | `/api/livro/emprestados` | Lista todos os livros que estão emprestados | Retorna a lista contendo apenas registros onde `Emprestado == true`. |

---

## 🛠️ Como Executar a Aplicação Localmente

Siga o passo a passo abaixo para rodar o projeto em sua máquina local:

### Pré-requisitos
*   **SDK do .NET 8.0** ou superior instalado ([Download oficial](https://dotnet.microsoft.com/download/dotnet/8.0))
*   **VS Code** ou **Visual Studio 2022**

### Executando pelo Terminal
1. Clone o repositório na sua máquina:
   ```bash
   git clone https://github.com/gustavogarciacavalli-sudo/Prova-C-.git
   cd Prova-C-
   ```
2. Restaure as dependências do projeto NuGet:
   ```bash
   dotnet restore
   ```
3. Execute o servidor de desenvolvimento:
   ```bash
   dotnet run --project GustavoHenriqueCavalliGarcia
   ```
4. A API estará de pé e escutando por padrão em:
   *   `http://localhost:5000` (ou outra porta especificada nas configurações de runtime)

---

## 🧪 Testando os Endpoints

O projeto conta com um arquivo altamente prático chamado `testes.http` localizado no diretório raiz do projeto C#. 

Se você usa o **VS Code**, pode instalar a extensão **REST Client**. Com ela instalada, basta abrir o arquivo `testes.http` e clicar no botão `Send Request` que aparece no topo de cada rota para testar instantaneamente todos os cenários sem precisar de ferramentas externas como Postman ou Insomnia!

Exemplo de requisição no arquivo `testes.http`:
```http
POST http://localhost:5000/api/livro/cadastrar
Content-Type: application/json

{
    "nome": "O Programador Pragmático",
    "autor": "Andy Hunt & Dave Thomas"
}
```

---

## 🛡️ Licença

Este projeto é de cunho acadêmico e de portfólio. Sinta-se livre para clonar, estudar e propor melhorias!

---
Desenvolvido com 💻, ☕ e C# por **Gustavo Henrique Cavalli Garcia** & **Pedro Henrique Policeno**.
