# SfCompulsory_cs - C# Software Development

## 🚀 3ESPV - Engenharia de Software 3º Ano - Challenge - Sprint 3 🖥️
### 🧑‍💻 Guilherme Barreto Santos - RM97674
### 🧑‍💻 Henrique Parra - RM551973
### 🧑‍💻 Nicolas Oliveira da Silva - RM98939
### 🧑‍💻 Roberto Oliveira - RM551460
### 🧑‍💻 Tony Willian - RM550667

## 📄 Swagger:
	- http://localhost:8080/swagger

# 🖥️  SfCompulsory API

API RESTful desenvolvida em **ASP.NET Core 8** com **Entity Framework Core e Oracle Database**, que permite gerenciar usuários e logs de operações. O sistema utiliza autenticação segura com **JWT (Bearer Token)** e registra todas as ações relevantes em banco de dados e em arquivos locais (`logs.json` e `logs.txt`).

---

## 💡 Sobre o Projeto

O **SfCompulsory** é uma aplicação voltada para ajudar clientes a manter o controle sobre seus aportes financeiros, definindo limites de investimento e registrando logs de todas as ações realizadas. A solução garante **segurança** com autenticação via JWT, **persistência robusta** no Oracle Database e **auditoria transparente** por meio de logs salvos tanto no banco quanto em arquivos locais.

Além de prover o CRUD de usuários, o sistema registra tentativas de login, acessos e modificações no banco de dados, garantindo rastreabilidade total. Esses logs podem ser consumidos pela API ou diretamente dos arquivos para auditoria.

---
## 📱 Interface do Usuário
```bash
✔️ A interface do usuário que fará conexão com esse projeto foi desenvolvida em React Native na disciplina de Mobile Development, e pode ser conferida neste link:  
(A funcionalidade de acompanhamento temporal será implementada na próxima sprint, apesar do painel já estar concluído no front-end.)
```
[🔗 Assistir no YouTube e verificar a Interface desenvolvida no Mobile](https://www.youtube.com/watch?v=-WHlevglnhs)  



## 📈 Diagrama Entidade Relacionamento (DER)
```bash
    ✔️ O DER do sistema pode ser visualizado no arquivo anexado à pasta raíz do projeto de nome: "diagrama_ER.png"
```

## ✅ Requisitos do Sistema
```bash
🔹 Requisitos Funcionais
✔️ Cadastro, consulta, atualização e exclusão de usuários.
✔️ Autenticação via login e senha com retorno de token JWT.
✔️ Proteção de rotas sensíveis com [Authorize].
✔️ Registro de logs para cada operação crítica (login, criação, atualização e exclusão).
✔️ Persistência de logs em banco Oracle e também em arquivos JSON e TXT.
✔️ Consulta de logs pelo endpoint protegido.
✔️ Swagger documentando todas as rotas.

🔹 Requisitos Não Funcionais
✔️ Segurança com hash de senha (BCrypt).
✔️ Logs de auditoria para rastreabilidade.
✔️ Código modular e em camadas (Controller, Service, Data, Model).
✔️ Escalabilidade e integração com Oracle Database.
✔️ Suporte a Swagger com autenticação JWT.
✔️ Documentação detalhada do sistema.
```

## 🔄 Regras de Negócio
```bash
🔐 Controle de Usuários
- Cada usuário deve ter nome, e-mail, chave Pix, limite máximo de aposta, username e senha.
- As senhas são sempre armazenadas com hash (BCrypt).
- Apenas usuários autenticados via JWT podem consumir endpoints protegidos.

🧾 Logs de Operações
- Todo login (sucesso ou falha) é registrado com nível INFO ou WARN.
- Criação, atualização e exclusão de usuários também são logadas.
- Logs ficam disponíveis no banco (tabela LOGS) e nos arquivos Logs/logs.json e Logs/logs.txt.
```

## 🧩 Fluxogramas do Sistema
```bash
Fluxo de Autenticação:
1. Usuário envia login e senha para /api/auth/login
2. Sistema valida o usuário no banco e verifica a senha com BCrypt
3. Se válido → retorna JWT
4. JWT é usado no header Authorization para acessar as demais rotas
5. Todas as operações relevantes são registradas em LOGS (banco e arquivos)
```

## ✅ Funcionalidades principais
```bash
👤 CRUD de usuários (criação, consulta, atualização e exclusão)

🔐 Autenticação JWT com Bearer Token

🧾 Logs automáticos em banco Oracle + arquivos JSON/TXT

📊 Consulta de logs via API protegida

📄 Swagger UI com suporte a autenticação JWT

🔒 Senhas armazenadas com BCrypt

⚡ Estrutura em camadas para escalabilidade e manutenção
```

## 🛠️ Tecnologias Utilizadas
```bash
.NET 8

ASP.NET Core Web API

Entity Framework Core + Oracle Database

Autenticação JWT (Bearer Token)

BCrypt.Net-Next (hash de senha)

Swagger (Swashbuckle)

Newtonsoft.Json (serialização JSON)

RESTful API

C#
```

## 📁 Estrutura de pastas
```bash
SfCompulsory_cs/
├── Controllers/
│   ├── AuthController.cs
│   ├── UsersController.cs
│   └── LogsController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Dtos/
│   └── LoginDto.cs
├── Models/
│   ├── User.cs
│   └── Log.cs
├── Services/
│   └── LogService.cs
├── Program.cs
├── appsettings.json
└── README.md
```

## 🌐 Endpoints
📌 Públicos
```bash
- POST /api/auth/login – Retorna token JWT
```
🔐 Protegidos (requer Bearer Token)
```bash
- GET /api/users – Lista todos os usuários
- GET /api/users/{id} – Consulta usuário por ID
- POST /api/users – Cria novo usuário (senha armazenada com hash)
- PUT /api/users/{id} – Atualiza usuário existente
- DELETE /api/users/{id} – Exclui usuário

- GET /api/logs – Consulta todos os logs registrados
```

## 📊 Logs
```bash
Os logs ficam armazenados em:

Banco Oracle (tabela LOGS)

Arquivos locais:

Logs/logs.txt (texto simples)

Logs/logs.json (estrutura JSON)

Esses logs podem ser consumidos via API (GET /api/logs) ou diretamente nos arquivos.
```

## 🧪 Testando no Swagger
```bash
1. Acesse /api/auth/login
   Body:
   {
     "username": "admin",
     "password": "123456"
   }

2. Copie o token retornado

3. Clique em "Authorize" no canto superior direito do Swagger UI

4. Insira o token no formato:
   Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

## 🐳 Executando com Docker
```bash
docker build -t sfcompulsory-cs:v1.0.0 .
docker run --name sfcompulsory-cs -p 8080:8080 sfcompulsory-cs:v1.0.0
```
