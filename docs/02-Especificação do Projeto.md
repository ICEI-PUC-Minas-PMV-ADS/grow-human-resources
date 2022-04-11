# Especificações do Projeto


Como citado anteriormente, nosso projeto tem foco em desenvolver um sistema que auxilie diversas empresas na análise de crescimento e desenvolvimento de cada colaborador, criando metas e relatórios trimestrais com o intuito de trazer agilidade e simplicidade de informações, e assim, trazendo um crescimento contínuo entre empresas e colaboradores ou vice-versa.


## Personas

| **Lucas De Oliveira Santos** |      |      |
|------------------------------|------|------|
|<img src="/docs/img/photo-lucas.jpg"> | **Idade:** 32 anos  | **Ocupação:** foi promovido a Gerente do setor de RH há aproximadamente 2 anos, trabalha na empresa Xing Ling do Brasil, uma multinacional de grande porte.   |
| **Motivações:** Lucas tem ótimas ideias de gerenciamento e controle da produção, bem como, a gestão de expectativas de carreira e motivacional.	| **Frustrações:** Atualmente Lucas tem dificuldades de acompanhar/avaliar as metas e os objetivos profissionais de cada funcionário, pois não possui uma ferramenta sistematizada que fornece dados estatísticos dos funcionários. | **Hobbies, História:** Lucas sonha em revolucionar as relação funcionário x empregador e, para isso, dedica parte de seu tempo em estudo, pesquisas e treinamentos em inovações de RH.|

| **Melissa Fernandes Santos** |      |      |
|------------------------------|------|------|
|<img src="/docs/img/photo-melissa.jpng.jpeg"> | **Idade:** 22 anos  | **Ocupação:** Estagiária do setor de RH na empresa Xing Ling do Brasil. Faz faculdade de Ciências Contábeis e obteve uma oportunidade de expressar seus conhecimentos contábeis junto ao setor de RH.                        |
| **Motivações:** Melissa está conhecendo o mercado de trabalho e viu a oportunidade de trabalhar na empresa de Lucas desafiadora pois conciliar contabilidade com RH será um desafio inovador para sua carreira.	| **Frustrações:** Não domina a maratona de atividades do RH e precisa se orientar dentro da empresa através de uma ferramenta de suporte e capacitação. | **Hobbies, História:** Além de ser uma jogador de vôlei e estudante, Melissa dedica uma fração do tempo em pesquisa e desenvolvimento interpessoal. |                    |

| **Lúcia De Medeiros Silva** |      |      |
|-----------------------------|------|------|
|<img src="/docs/img/photo-lucia.png"> | **Idade:** 42 anos  | **Ocupação:** Supervisora de RH na empresa Xing Ling do Brasil. Faz o gerenciamento do time e atividades de RH.|
| **Motivações:** Lúcia está na empresa a 10 anos e foi recentemente promovida à supervisora de RH. Está motivada com as ideias e metodologias de Lucas e está empenhada em fazer acontecer a inovação na empresa.	| **Frustrações:** A ausência de ferramentas sistematizadas de gestão de RH, onera seu tempo de pensamento inovador com funções operacionais de preenchimento de formulários de cadastros, envio e recebimento de malotes, dentre outras necessidades do setor. | **Hobbies, História:** Lucia realiza um sonho de estudar e conhecer belas artes. |

## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Lucas | Fornecer metas trimestrais | Balanceamento e cadastro pelo time de RH. |
|Lucas | Visualizar relatórios individuais de cada departamento | Acompanhar o desenvolvimento da empresa. |
|Lucas | Apresentar aos funcionarios as metas atingidas e não atingidas | Realizar um feedback consistente e com dados precisos dos pontos de melhoria. |
|Lucia  | Realizar cadastros de funcionários       | Adicionar funcionários ao Banco de Dados da empresa.          |
|Lucia  | Realizar cadastros de metas       | Gerar controle de metas.          |
|Lucia| Controlar metas      | Que reflitam com clareza os indicadores de performance.|
|Melissa| Emitir situação de metas   | Informar aos gerentes e supervisores a conclusão ou não dos objetivos atribuidos.|
|Melissa| Alterar minha senha do sistema   | Poder recuperar a senha de acesso em caso de esquecimento.|
|Melissa|  Efetuar Login no sistema  | Acessar o sistema para dar inicio ao expediente.|
|Lucas| Acompanhar metas individuais    | Que cada membro da equipe cumpra as metas individuais.  |
|Lucas| Acompanhar metas de Equipe    | Que cada equipe cumpra metas as metas de equipe.  |




## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|------|-------------------------|------|
|RF01| O Sistema deverá permitir o login e alteração de senha dos colaboradores.| ALTA | 
|RF02| O Sistema deve permitir ao supervisor a inserção e exclusão de perfis cadastratos.| ALTA | 
|RF03| O Sistema deve permitir que o supervisor associe o funcionário a um departamento.| ALTA | 
|RF04| O Sistema deve permitir que o supervisor acompanhe o relatório de desempenho e metas de seus subordinados.| ALTA | 
|RF05| O Sistema deve permitir ao gerente e ao supervisor visualizar relatórios de cada departamento.| ALTA | 
|RF06| O Sistema deve emitir status de metas atingidas/não atingidas que pode ser consultado pelo supervisor, gerente e funcionário.| ALTA | 
|RF07| O Sistema deverá emitir ranqueamento (top 5) de: metas atingidas, metas não atingidas.| ALTA | 


### Requisitos não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|----------------------------|------|
|RNF-001| O sistema de ser feito usando práticas de UX e IxD   | ALTA | 
|RNF-002| O sistema deve ser disponibilizado publicamente no GitHub. | ALTA | 
|RNF-003| O sistema deve apresentar baixo tempo de resposta nas requisições. | ALTA | 
|RNF-004| O sistema deve apresentar um filtro para pesquisas. | ALTA | 
|RNF-005| O sistema deve ser implementado em C#. | ALTA | 
|RNF-006| O sistema deve ser responsivo e compatível com os principais navegadores. | ALTA | 


## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

|ID| Restrição                                             |
|--|-------------------------------------------------------|
|01| O projeto deverá ser entregue até o final do semestre |
|02| Não pode ser desenvolvido um módulo de backend        |
|03| A Proposta contempla a construção de no máximo 03 relatórios|
|04| Cada relatório deverá conter no máximo 1 gráfico|


## Diagrama de Casos de Uso

|ATOR|	DESCRIÇÃO|
|----|-----------|
|Funcionário| Consulta seu desempenho e metas, realiza seu cadastro, login e alteração de senha. |
|Supervisor| O supervisor é um funcionário responsável pelo controle de perfis, consulta o desempenho, controle de funcionários, controla departamentos e visualiza relatório.|
|Gerente| Responsável pelo gerenciamento de departamentos, funcionário e visualiza relatório de desempenho dos mesmos.|

|CASO DE USO|	DESCRIÇÃO|	RF|
|-|-|-|
|Realizar login no sistema|	O funcionário deve conseguir realizar login com suas credenciais no sistema.| RF01|
|Gerenciar Perfil|	O funcionário deve conseguir gerenciar perfil: (atualizar dados do pessoas e alterar senha) no seu perfil.|	RF01 |
|Gerenciar Desempenho| O funcionário deve consultar seu desempenho e status de metas.| RF06|
|Associar funcionário a departamento| O supervisor deverá associar cada funcionário a um departamento.| RF03 |
|Gerenciar Departamentos | O supervisor e o gerente devem visualizar relatório de cada departamento. | RF05 | 
|Gerenciar Metas | O supervisor e o gerente devem gerenciar o desempenho e metas do funcionário. | RF04 | 
|Gerenciar Acessos | O supervisor deve gerenciar (incluir, excluir) o acesso do funcionário. | RF02| 
|Consultar Ranqueamento| O ranqueamento (top 5) metas atingidas e não atingidas poderá ser consultado por funcionário, gerente e supervisor.  | RF07|



 ## Representação Visual

 ![Diagrama de Classes-Diagrama de Caso de Uso drawio](https://user-images.githubusercontent.com/91227083/162646497-b8ce7d2e-08cf-4c81-b868-dfe941c2fbb7.png)

