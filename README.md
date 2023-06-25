# AS-Orientacao-Objetos
Avaliação Semestral referente a cadeira de Programação Orientada a Objetos, nosso "prompt" é uma web API de Sistema de Gerenciamento de Biblioteca, feito em ASP.NET core


## *Detalhes de versão atual*
Ainda existem algumas falhas a serem corrigidas, principalmente em torno de UserBooks e AuthorBooks, e outras menores no retorno de dados e/ou DTOs, como alguns dos itens ou propriedades aparecendo vazios embora estejam corretos no banco (por exemplo em user pode-se ver uma lista de livros mas esse livro possui genero nulo)

## Endpoints/Solicitações
Executando via localhost no postman, as solicitações possiveis são:
# **Genre**
- /api/genre/

    *GET*
    - all 
        - _Retorna uma lista de todos os gêneros literários cadastrados_
    - {id} 
        - _Retorna o gênero que corresponda a Id informada_
    
    *POST*
    - _Permite a inserção de um gênero no banco, confira na GenreViewmodel os requisitos_
    
    *PUT*
    - {id} 
        - _Permite a alteração do gênero de id informada pelos dados informados no body_
    
    *DELETE*
    - {id} 
        - _Permite a remoção do gênero de id informada do banco_

# **Author**
- /api/author/

    *GET*
    - all 
        - _Retorna uma lista de todos os autores cadastrados_
    - {id} 
        - _Retorna o autor que corresponda a Id informada_
    
    *POST*
    - _Permite a inserção de um autor no banco, confira no Viewmodel respectivo os requisitos_
    
    *PUT*
    - {id} 
        - _Permite alterar o autor de id informado, confira viewmodel_

    *DELETE*
    - {id} 
        - _Apaga o autor de id informada do banco_

# **User**
- /api/user/

    *GET*
    - all 
        - _Retorna uma lista de todos os usuários cadastrados_
    - {id} 
        - _Retorna o usuário que corresponda a Id informada_
    - {name} 
        - _Retorna usuários com nome começando com a string informada na url, no lugar de "name" ("Ed" rertornará Eduardo e Edmundo, por exemplo)_
    
    *POST*
    - _Permite a inserção de um usuário no banco_
    
    *PUT*
    - {id} 
        - _Permite alterar o usuário de id informado_

    *DELETE*
    - {id} 
        - _Apaga o autor de id informada do banco_

# **Book**
- /api/book/

    *GET*
    - all 
        - _Retorna uma lista de todos os livros cadastrados_
    - {id} 
        - _Retorna o livro que corresponda a Id informada_
    - available 
        - _Retorna todos os livros disponíveis para locação_
    - donor/{id} 
        - _Retorna todos os livros doados pelo usuário de id informada_
    - byGenre/{id} 
        - _Retorna todos os livros com gênero literário corrsepondente ao da id informada_
    
    *POST*
    - _Permite a inserção de um livro no banco_
    
    *PUT*
    - {id} 
        - _Permite alterar o livro de id informado_

    *DELETE*
    - {id} 
        - _Apaga o livro de id informada do banco, porém cuidado com restrições de ForeignKeys_

# **AuthorBooks**
- /api/writer/

    *GET*
    - all 
        - _Retorna uma lista de todas as relações de autores e livros cadastrados, uma vez que um livro pode ter mais de um autor, e um autor pode escrever mais de um livro_
    - {idBook}/{idAuthor} 
        - _Retorna a relação de autor e livro que corresponda as Ids informadas_
    - byAuthor/{id} 
        - _Retorna uma lista de livros escritos pelo autor da id informada_
    
    *POST*
    - _Permite a inserção de uma nova relação de autor e livro no banco_
    
    *PUT*
    - {idBook}/{idAuthor} 
        - _Permite alterar a relação de autores e livros das ids informadas_

    *DELETE*
    - {idBook}/{idAuthor} 
        - _Permite excluir a relação de autores e livros informada_

# **UserBooks**
- /api/rental/

    *GET*
    - all 
        - _Retorna uma lista de todas as relações de usuários e livros cadastrados, já que um usuário pode alugar um ou mais livros, que serão devolvidos e alugados pelo mesmo ou outros usuários_
    - {idBook}/{idUser} 
        - _Retorna a relação de usuário e livro ("aluguel") que corresponda as Ids informadas_
    - usersRenting 
        - _Retorna uma lista de empréstimos que contem apenas os que estão em aberto (onde o livro ainda não foi devolvido para a biblioteca")_
    
    *POST*
    - _Permite a inserção de um novo aluguel no banco_
    
    *PUT*
    - {idBook}/{idUser} 
        - _Permite alterar o aluguel das ids informadas_
    - returnBook/{idBook}/{idUser} 
        - _Realiza a devolução do livro de id informada pelo usuário de id informada_

    *DELETE*
    - {idBook}/{idUser} 
        - _Permite excluir um aluguel, alterando a disponibilidade do livro informado para disponível novamente_