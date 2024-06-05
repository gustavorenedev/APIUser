# API de Usuário

## Descrição
Esta API permite aos usuários realizar várias operações, como registro, login, gerenciamento de usuários e redefinição de senha.

## Endpoints

### Registro
- **URL:** `/api/user/register`
- **Método:** `POST`
- **Descrição:** Registrar um novo usuário.

### Login
- **URL:** `/api/user/login`
- **Método:** `POST`
- **Descrição:** Login com nome de usuário e senha.

### Obter Usuários
- **URL:** `/api/user`
- **Método:** `GET`
- **Descrição:** Obter todos os usuários.

### Obter Usuário por ID
- **URL:** `/api/user/{id}`
- **Método:** `GET`
- **Descrição:** Obter usuário por ID.

### Redefinir Senha
- **URL:** `/api/user/reset-password`
- **Método:** `PUT`
- **Descrição:** Redefinir senha do usuário.

### Excluir Usuário
- **URL:** `/api/user/{id}`
- **Método:** `DELETE`
- **Descrição:** Excluir usuário por ID.

### Atualizar Usuário
- **URL:** `/api/user/update?id=`
- **Método:** `PUT`
- **Descrição:** Atualizar informações do usuário.

## Importação e Configuração

Para usar esta API, você precisa seguir estas etapas:

1. **Baixar ou Clonar o Projeto:** Baixe o projeto do repositório ou clone-o usando Git.

2. **Abrir no IDE:** Abra o projeto no seu IDE preferido, como o Visual Studio.

3. **Compilar a Solução:** Compile a solução para restaurar as dependências e garantir que seja compilada sem erros.

4. **Configurar o Banco de Dados:** Configure a string de conexão do banco de dados no arquivo `appsettings.json`.

5. **Executar Migrações:** Execute as migrações do banco de dados para criar as tabelas necessárias. Você pode usar as migrações do Entity Framework Core para isso.

6. **Instalar o Insomnia (Opcional):** Se desejar testar os endpoints da API, você pode instalar o Insomnia, importar a coleção fornecida e começar a testar.

7. **Executar a Aplicação:** Inicie a aplicação e teste os endpoints usando o método de sua preferência.
