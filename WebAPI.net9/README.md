# 📦 StockTech - API de Gerenciamento de Periféricos de Computador

O **StockTech** é uma API REST construída com .NET 9 e Entity Framework Core que tem como objetivo facilitar o gerenciamento de estoque de periféricos de computador. A documentação da API é feita com o Scalar.

Ideal para simular o backend de um sistema de controle de estoque voltado para lojas ou distribuidores de componentes de informática.

## 🚀 Funcionalidades

- 🔍 Listar todos os produtos
- 🔎 Buscar produto por ID
- 📝 Atualizar produto existente  
- ✅ Cadastrar novo produto  
- ❌ Remover produto  
- 🎯 Buscar por nome ou marca (filtro dinâmico)  

## 🛠️ Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com)  
- [Entity Framework Core](https://learn.microsoft.com/ef/)  
- [SQL Server](https://www.microsoft.com/sql-server)  
- [Scalar](https://scalar.com/) (substituto moderno do Swagger)  

## 📦 Modelo de Produto

CSHARP
public class ProdutoModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeEstoque { get; set; }
    public string CodigoDeBarras { get; set; }
    public string Marca { get; set; }
}

## 🌐 Endpoints
Método	Rota	Descrição
GET	/api/produto - Lista todos os produtos
GET	/api/produto/{id} - Busca produto por ID
PUT	/api/produto/{id} - Atualiza produto existente
POST	/api/produto - Cadastra um novo produto
DELETE	/api/produto/{id} -	Remove um produto
/api/produto/buscar?nome={nome}&marca={marca} -	Busca por nome, marca ou ambos.

## ▶️ Como Executar Localmente
Clone o repositório:

bash
Copiar
Editar
git clone https://github.com/seu-usuario/stocktech.git
cd stocktech
Configure a string de conexão no arquivo appsettings.json.

Execute a aplicação:

bash
Copiar
Editar
dotnet run
Acesse a documentação interativa com Scalar em:

bash
Copiar
Editar
https://localhost:{porta}/scalar

## 🛡️ Boas Práticas e Melhorias Implementadas
Para garantir a qualidade, robustez e facilidade de manutenção, este projeto incorpora as seguintes práticas e recursos:

- Validação com Data Annotations: O modelo ProdutoModel utiliza atributos para validar dados automaticamente, garantindo que as informações estejam corretas antes de salvar no banco.
- Tratamento de erros com Try-Catch: Todos os métodos dos controllers incluem blocos try-catch para capturar exceções, evitar que a API quebre e retornar respostas claras ao cliente.
- Programação assíncrona com async/await: As operações que acessam o banco de dados são feitas de forma assíncrona, melhorando a performance e a escalabilidade ao evitar bloqueios na aplicação.
- Comentários XML: O código contém comentários XML nos controllers e modelos para documentar a API e facilitar a geração de documentação automática, além de ajudar na manutenção.
- Logs para monitoramento: Implementação de logs em diferentes níveis (Debug, Information, Warning, Error) para acompanhar o comportamento da aplicação em tempo real e facilitar a identificação de problemas.
- Interface IAppDbContext e injeção de dependência: Criação da interface IAppDbContext para desacoplar o acesso ao banco de dados e permitir testes mais fáceis. O controlador ProdutoController passa a depender da interface, seguindo o princípio da inversão de dependência.

## 👨‍💻 Autor
Desenvolvido por **Isac Ribeiro** — estudante de Engenharia de Software, especialista no Desenvolvimento Web, .NET e soluções backend.

Este projeto foi desenvolvido acompanhando um curso em vídeo da Crislaine D'Paula, no entanto, tomei a liberdade de inserir algumas funcionalidades extras para aprimorar a API.

Conecte-se comigo no [LinkedIn](https://www.linkedin.com/in/seu-perfil).


![PROJETOFINALIZADO](https://github.com/user-attachments/assets/39e68d0a-1190-4815-a319-412ec6a2dd11)
