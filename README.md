<div align="center">

# 🚀 Desafio Prático: Gestão de Livraria Online - Rocketseat

🔧 Projeto desenvolvido no **Desafio Prático de Gestão de Livraria** da [Rocketseat](https://www.rocketseat.com.br/).  
🎯 O objetivo é construir uma API RESTful em C# para gerenciamento de livros, permitindo operações de criar, listar, editar e excluir.

</div>

---

## 🧩 Funcionalidades

📌 A API oferece os seguintes endpoints:

| Nº | Endpoint                  | Descrição                        |
|----|---------------------------|----------------------------------|
| 1  | **GET /api/Books**        | Retorna todos os livros.         |
| 2  | **GET /api/Books/{id}**   | Retorna um livro por ID.         |
| 3  | **POST /api/Books**       | Cria um novo livro.              |
| 4  | **PUT /api/Books/{id}**   | Atualiza um livro existente.     |
| 5  | **DELETE /api/Books/{id}**| Exclui um livro.                 |

---

## 🧠 Conceitos Praticados

✔️ ASP.NET Core Web API  
✔️ Entity Framework Core  
✔️ Data Annotations para validação de modelo  
✔️ Tratamento de exceções e status codes RESTful (200, 201, 204, 400, 404, 409, 500)  
✔️ Injeção de dependência  
✔️ Documentação automática via Swagger / Swashbuckle  

---

## ▶️ Como Executar

```bash
# Clone o repositório
git clone https://github.com/JonnyYamagushi/DesafioLivrariaAPICSharp.git

# Acesse a pasta do projeto
cd DesafioLivrariaAPICSharp

# Restaure pacotes e execute
dotnet restore
dotnet run
```

---

## 🗂️ Estrutura de Arquivos

```
.
├── Controllers
│   └── BooksController.cs   // Endpoints CRUD de livros
├── Data
│   └── BookstoreContext.cs  // DbContext do EF Core
├── Models
│   └── Book.cs              // Modelo de domínio com data annotations
└── Program.cs               // Configuração e startup da API
```

---

## 💻 Requisitos

- [.NET SDK 8.0 ou superior](https://dotnet.microsoft.com/download)

---

## 🙌 Créditos

Desenvolvido por [Jonny Yamagushi](https://github.com/JonnyYamagushi) como parte do Desafio Prático - Gestão de Livraria da [Rocketseat](https://www.rocketseat.com.br/).

---

## 📄 Licença

Este projeto está licenciado sob a **MIT License**.  
Sinta-se livre para usar, estudar, modificar e distribuir.

---