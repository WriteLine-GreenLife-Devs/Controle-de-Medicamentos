# 💊 Sistema de Controle de Medicamentos

Sistema de gerenciamento de medicamentos desenvolvido em C# utilizando aplicação Console e persistência de dados em arquivos JSON.

O projeto foi desenvolvido com foco em programação orientada a objetos, reutilização de código, modularização e arquitetura genérica para operações CRUD.

---

# 📌 Funcionalidades

O sistema permite:

- ✅ Cadastro de fornecedores
- ✅ Cadastro de medicamentos
- ✅ Cadastro de funcionários
- ✅ Cadastro de pacientes
- ✅ Controle de estoque
- ✅ Persistência automática em JSON
- ✅ Validação de CPF/CNPJ
- ✅ Formatação automática de telefones
- ✅ Associação de medicamentos com fornecedores
- ✅ Atualização automática de estoque
- ✅ Identificação de medicamentos com estoque baixo
- ✅ Menus dinâmicos reutilizáveis
- ✅ CRUD genérico reutilizável

---

# 🏗️ Arquitetura do Projeto

O projeto foi estruturado utilizando separação por módulos:

```bash
📁 Compartilhado
📁 Utilidades
📁 ModuloFornecedores
📁 ModuloFuncionarios
📁 ModuloMedicamentos
📁 ModuloPacientes
📁 ModuloEstoque
📄 Program.cs
```

---

# 🧠 Conceitos Utilizados

O sistema utiliza diversos conceitos importantes de C# e POO:

- Herança
- Polimorfismo
- Classes abstratas
- Interfaces
- Generics
- Encapsulamento
- Persistência de dados
- Repository Pattern
- Template Method
- Tratamento de exceções
- Serialização JSON

---

# ⚙️ Estrutura Base do Sistema

## 🔹 EntidadeBase

Classe abstrata responsável por:

- Gerar IDs automáticos únicos
- Padronizar atualização de entidades

```csharp
public abstract class EntidadeBase
{
    public string Id { get; private set; }
    public abstract void AtualizarDados(EntidadeBase entidadeAtualizada);
}
```

---

## 🔹 RepositorioBase<T>

Responsável por:

- Cadastro
- Edição
- Exclusão
- Busca
- Persistência em JSON

Utiliza Generics para reutilização entre módulos.

---

## 🔹 TelaBase<T>

Classe responsável pelo fluxo padrão de:

- Cadastro
- Edição
- Exclusão
- Visualização

Cada módulo herda essa estrutura.

---

# 💾 Persistência de Dados

Os dados são armazenados automaticamente em arquivos `.json`.

Os arquivos são salvos na pasta:

```bash
Documentos/
```

Exemplos:

```bash
fornecedores.json
medicamentos.json
funcionarios.json
pacientes.json
estoque.json
```

---

# 🚚 Módulo de Fornecedores

Permite:

- Cadastro de fornecedores
- Validação de CNPJ
- Formatação automática de telefone
- Verificação de CNPJ duplicado

## Campos

- Nome
- Telefone
- CNPJ

---

# 👨‍💼 Módulo de Funcionários

Permite:

- Cadastro de funcionários
- Validação de CPF
- Formatação automática de telefone
- Verificação de CPF duplicado

## Campos

- Nome
- Telefone
- CPF

---

# 💊 Módulo de Medicamentos

Permite:

- Cadastro de medicamentos
- Associação com fornecedores
- Controle de estoque
- Atualização automática de quantidade

## Campos

- Nome
- Descrição
- Quantidade em estoque
- Fornecedor

## Regras de Negócio

- Medicamentos precisam possuir fornecedor.
- Não é possível cadastrar medicamento sem fornecedor.
- Caso um medicamento já exista:
  - O estoque é atualizado automaticamente.
- Medicamentos com estoque abaixo de 20 unidades:
  - São exibidos como "Em Falta!".

---

# 🧑‍⚕️ Módulo de Pacientes

Permite:

- Cadastro de pacientes
- Validação de CPF
- Cadastro de CNS
- Formatação automática de telefone

## Campos

- Nome
- CPF
- CNS
- Telefone

## Regras

- CNS deve conter exatamente 15 caracteres.
- CPF não pode ser duplicado.

---

# 📦 Módulo de Estoque

Responsável pelo gerenciamento de estoque dos medicamentos.

## Funcionalidades

- Associação de estoque com medicamentos
- Controle de entradas e saídas
- Registro de quantidade em estoque

---

# 🎨 Utilidades do Sistema

## 🔹 Mensagens Coloridas

O sistema possui mensagens coloridas para:

- Sucesso
- Avisos
- Erros
- Confirmações

---

## 🔹 Validações

O sistema possui validações para:

- CPF
- CNPJ
- CNS
- Telefones
- IDs
- Tamanho de textos

---

# ▶️ Como Executar

## Pré-requisitos

- .NET SDK instalado

Recomendado:

- .NET 6 ou superior

---

## Executando o Projeto

Clone o repositório:

```bash
git clone URL_DO_REPOSITORIO
```

Acesse a pasta:

```bash
cd ControleDeMedicamentos.ConsoleApp
```

Execute:

```bash
dotnet run
```

---

# 🖥️ Menu Principal

O sistema possui um menu principal dinâmico:

```bash
1 - Fornecedores
2 - Medicamentos
3 - Funcionários
4 - Pacientes
5 - Estoque
S - Sair
```

---

# 🔐 Segurança e Validações

O sistema realiza:

- Tratamento de exceções
- Validação de entradas
- Verificação de duplicidade
- Controle de IDs válidos

---

# 📈 Possíveis Melhorias

- Interface gráfica
- Banco de dados SQL
- Login de usuários
- Controle de permissões
- Relatórios
- Histórico de movimentações
- Dashboard administrativo
- API REST
- Testes automatizados

---

# 👨‍💻 Autor

Projeto desenvolvido por Gustavo Tessaro e Alec Luí para fins acadêmicos e prática de:

- Programação Orientada a Objetos
- Estruturas Genéricas
- Persistência de Dados
- Arquitetura de Software em C#

---