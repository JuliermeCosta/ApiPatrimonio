1 - ENDPOINTS

1.1 - PATRIMÔNIO

1.1.1 - Obter todos os patrimônios 
ROTA: /patrimonios
HTTP METHOD: GET
ENTRADA: Nenhum
RETORNO ESPERADO: Lista de todos os patrimônios (JSON)
QUERYSTRINGS:
marcaId (número inteiro positivo, não obrigatório) - Filtra por marca

1.1.2 - Obter um patrimônio por ID
ROTA: /patrimonios/{Id}
PARÂMETROS: Id (número inteiro positivo, obrigatório)
HTTP METHOD: GET
ENTRADA: Nenhum
RETORNO ESPERADO: Patrimônio com o ID inserido (JSON)

1.1.3 - Inserir um novo patrimônio 
ROTA: /patrimonios
HTTP METHOD: POST
ENTRADA: Nome (texto, obrigatório), Descricao (texto, não obrigatório), MarcaId(número inteiro positivo, obrigatório) (JSON)
RETORNO ESPERADO: Nenhum
QUERYSTRINGS:
-validarMarca(true ou false, não obrigatório) - Verifica a existência da marca pela MarcaId
-validarNome(true ou false, não obrigatório) - Verifica se o nome do patrimônio já foi incluído

1.1.4 - Alterar dados de um patrimônio
ROTA: /patrimonios/{Id}
PARÂMETROS: Id (número inteiro positivo, obrigatório)
HTTP METHOD: PUT
ENTRADA: Nome (texto, obrigatório), Descricao (texto, não obrigatório), MarcaId(número inteiro positivo, obrigatório) (JSON)
RETORNO ESPERADO: Nenhum
QUERYSTRINGS:
-validarMarca(true ou false, não obrigatório) - Verifica a existência da marca pela MarcaId
-validarNome(true ou false, não obrigatório) - Verifica se o nome do patrimônio já foi incluído

1.1.5 - Excluir um patrimônio
ROTA: /patrimonios/{Id}
PARÂMETROS: Id (número inteiro positivo, obrigatório)
HTTP METHOD: DELETE
ENTRADA: Nenhum
RETORNO ESPERADO: Nenhum

1.2 MARCA

1.2.1 - Obter todas as marcas
ROTA: /marcas
HTTP METHOD: GET
ENTRADA: Nenhum
RETORNO ESPERADO: Lista de todas as marcas (JSON)

1.2.2 - Obter uma marca por ID
ROTA: /marcas/{Id}
PARÂMETROS: Id (número inteiro positivo, obrigatório)
HTTP METHOD: GET
ENTRADA: Nenhum
RETORNO ESPERADO: Marca com o ID inserido (JSON)

1.2.3 - Obter todos os patrimônios de uma marca
ROTA: /marcas/{Id}/patrimonios
PARÂMETROS: Id (número inteiro positivo, obrigatório)
HTTP METHOD: GET
ENTRADA: Nenhum
RETORNO ESPERADO: Lista de todos os patrimônios da marca (JSON)

1.2.4 - Inserir uma nova marca
ROTA: /marcas
HTTP METHOD: POST
ENTRADA: Nome (texto, obrigatório) (JSON)
RETORNO ESPERADO: Nenhum
RESTRIÇÕES: Nome deve ser único

1.2.5 - Alterar dados de uma marca
ROTA: /marcas/{Id}
PARÂMETROS: Id (número inteiro positivo, obrigatório)
HTTP METHOD: PUT
ENTRADA: Nome (texto, obrigatório) (JSON)
RETORNO ESPERADO: Nenhum
RESTRIÇÕES: Nome deve ser único

1.2.6 - Excluir uma marca
ROTA: /marcas/{Id}
PARÂMETROS: Id (número inteiro positivo, obrigatório)
HTTP METHOD: DELETE
ENTRADA: Nenhum
RETORNO ESPERADO: Nenhum

2 - PROGRAMAÇÃO
LINGUAGEM: C#
FRAMEWORK: .NET Core 3.1
ARQUITETURA: MVC
BANCO DE DADOS: SQL Server 2019 Express
CONNECTION STRING: appsettings.json

2.1 - PASTA REPOSITORY
Contém o conjunto de classes para manipulação do banco de dados. A interface IRepository é usada para aplicar a injeção de dependência através da classe Startup.

2.2 - CLASSE CONFIGURATION MANAGER
Classe estática usada para deserializar o arquivo de configuração appsettings.json.

2.3 - MÉTODOS DE EXTENSÃO PARA CONVERSÃO
A classe ConversionExtension contém métodos de extensão usados para conversão. Obs.: a função TryParse faz com que a performance das conversões seja maior que a performance das funções contidas na classe Convert.