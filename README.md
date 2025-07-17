<!DOCTYPE html>
<html lang="pt">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>

<body>
<h1>📚 Gestão de Biblioteca - Aplicação </h1>

<h2>🎯 Objetivo</h2>
<p>Aplicação para gestão de livros, leitores e requesições, com uma interface gráfica amigável e conexão com base de dados em SQL Server. Esta aplicação segue o padrão arquitetural <span class="highlight">MVVM</span>.</p>

<h2>🛠️ Tecnologias e Ferramentas</h2>
<ul>
    <li><strong>WPF</strong> (Windows Presentation Foundation)</li>
    <li><strong>C#</strong> (.NET)</li>
    <li><strong>SQL Server</strong> + <strong>Stored Procedures</strong></li>
    <li><strong>XAML</strong> para interface</li>
    <li><strong>SSMS</strong> (SQL Server Management Studio)</li>
    <li><strong>Visual Studio 2022</strong></li>
</ul>

<h2>📦 Estrutura do Projeto</h2>
<pre><code>GestaoBibliotecaApp/
├── Models/           # Livro, Leitor, Emprestimo, Categoria e Filtro
├── Views/            # XAML das interfaces
├── ViewModels/       # Lógica das views
├── Helper/           # RelayCommand.cs ligar ações da interface a métodos da lógica 
├── Data/             # DBHelper.cs com acesso à base de dados
└── SQL/              # Scripts das Tabelas e Stored Procedures</code></pre>

<h2>🧩 Padrão MVVM</h2>
<p>Separação entre:</p>
<ul>
    <li><strong>Model:</strong> Dados e regras (Livro, Leitor, Emprestimo...)</li>
    <li><strong>View:</strong> Interface em XAML</li>
    <li><strong>ViewModel:</strong> Ligação entre dados e interface</li>
</ul>

<h2>⚙️ Funcionalidades</h2>
<h3>📚 Livros</h3>
<ul>
    <li>Inserção, visualização e eliminação</li>
    <li>ComboBox para seleção de categoria</li>
    <li>Exibição do nome da categoria</li>
</ul>

<h3>👤 Leitores</h3>
<ul>
    <li>Validação de nome e email</li>
    <li>Impossibilidade de apagar leitores com empréstimos</li>
</ul>

<h3>📖 Empréstimos</h3>
<ul>
    <li>Registo e devolução</li>
    <li>Atualização da data de devolução</li>
    <li>Mensagens de erro para seleção inválida</li>
</ul>

<h3>📂 Histórico</h3>
<ul>
    <li>Filtragem por livro ou leitor</li>
    <li>Mensagens informativas quando não há resultados</li>
</ul>

<h2>🗄️ Base de Dados</h2>
<ul>
    <li>Tabelas relacionais com <code>FOREIGN KEY</code></li>
    <li><strong>Stored Procedures:</strong> Inserir, Eliminar, Atualizar, Listar, etc</li>
    <li>Queries otimizadas com <code>JOIN</code></li>
</ul>

<h2>🚀 Como Executar Localmente</h2>
<ol>
    <li>Clonar o repositório: <code>git clone https://github.com/dgsvl21/GestaoBibliotecaApp.git</code></li>
    <li>Abrir o projeto no Visual Studio</li>
    <li>No SSMS, executar os scripts: <code>Tabelas.sql</code> &rarr; <code>StoredProcedures.sql</code> &rarr;</li>
    <li>Configurar a connection string no <code>DBHelper.cs</code></li>
    <li>Correr o código</li>
</ol>

<h2>🧪 Testes e Validações</h2>
<ul>
    <li>Verificações com <code>try/catch</code> e <code>MessageBox</code></li>
    <li>Validação de campos obrigatórios</li>
    <li>Mensagens de aviso se não houver dados a mostrar</li>
</ul>

<h2>✅ Conclusão</h2>
<p>Implementado um CRUD completo com boas práticas de organização e arquitetura. Está preparada para expansões futuras (penalizações, exportação, relatórios).</p>

</body>
</html>
