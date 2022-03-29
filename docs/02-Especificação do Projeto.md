# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="1-Documentação de Contexto.md"> Documentação de Contexto</a></span>

Definição do problema e ideia de solução a partir da perspectiva do usuário. É composta pela definição do  diagrama de personas, histórias de usuários, requisitos funcionais e não funcionais além das restrições do projeto.

Apresente uma visão geral do que será abordado nesta parte do documento, enumerando as técnicas e/ou ferramentas utilizadas para realizar a especificações do projeto

## Personas

As personas levantadas durante o processo de entendimento do problema são apresentadas nas figuras que se seguem:

| **Lucas De Oliveira Santos** |      |      |
|------------------------------|------|------|
|<img src="/docs/img/photo-lucas.jpg"> | **Idade:** 32 anos  | **Ocupação:** foi promovido a Gerente do setor de RH há aproximadamente 2 anos, trabalha em uma empresa de grande porte.   |
| **Motivações:** Lucas tem ótimas idéias de gerenciamento e controle da produção, bem como, a gestão de expectativas de carreira e motivacional.	| **Frustrações:** Atualmente Lucas tem dificuldades de acompanhar/avaliar as metas e os objetivos profissionais de cada funcionario, pois não possui uma ferramenta sistematizada que fornece dados estatísticos dos funcionários. | **Hobbies, História:** Lucas sonha em revolucionar as relação funcionario x empregador e, para isso, dedica parte de seu tempo em estudo, pesquisas e treinamentos em inovações de RH.| 

| **João Francisco de Almeida** |      |      |
|-------------------------------|------|------|
|<img src="/docs/img/photo-joao.png"> | **Idade:** 57 anos  | **Ocupação:** psicólogo atuante há 35 anos, auxiliou diversas empresas e atualmente busca uma recolocação no mercado.       |
| **Motivações:** João é um profissional dedicado e inovador no desempenho de suas funções. Apesar da aparente idade avançada, João acredita que pode "dar um gás" a mais na sua carreira, promovendo a inovações em multinacionais.	| **Frustrações:** O mercado não fornece recursos de realocação a partir de determinada idade, então ela sente falta de uma ferramenta que o insira no mercado, avaliando seu currículo e suas habilidades. | **Hobbies, História:** João é um pé-de-valsa nato. Gosta de se relacionar com pessoas, não importando etnia, idade ou grau de instrução, o importante é aumentar sua network. | 

| **Melissa Fernandes Santos** |      |      |
|------------------------------|------|------|
|<img src="/docs/img/photo-melissa.jpng.jpeg"> | **Idade:** 22 anos  | **Ocupação:** Estagiária do Lucas. Faz faculdade de Ciências Contábeis e obteve uma oportunidade de expressar seus conhecimentos contábeis junto ao setor de RH.                        |
| **Motivações:** Melissa está conhecendo o mercado de trabalho e viu a oprtunidade de trabalhar na empresa de Lucas desafiadora pois coniciliar contabilidade com RH será um desafio inovador para sua carreira.	| **Frustrações:** Não domina a maratona de atividades do RH e precisa se orientar dentro da empresa através de uma ferramenta de suporte e capacitação. | **Hobbies, História:** Além de ser uma jogador de vôlei e estudante, Melissa dedica uma fração do tempo em pesquisa e desenvolvimento interpessoal. | 

| **Lúcia De Medeiros Silva** |      |      |
|-----------------------------|------|------|
|<img src="/docs/img/photo-lucia.png"> | **Idade:** 42 anos  | **Ocupação:** Supervisora de RH na empresa do Lucas. Faz o gerenciamento do time e atividades de RH.|
| **Motivações:** Lúcia está na empresa a 10 anos e foi recentemente promovida à supervisora de RH. Está motivada com as idéias e metodologias de Lucas e está empenhada em fazer acontecer a inovação na empresa.	| **Frustrações:** A ausência de ferramentas sistematizadas de gestão de RH, onera seu tempo de pensamento inovador com funções operacionais de preenchimento de formulários de cadastros, envio e recebimento de maolotes, dentre outras necessidades do setor. | **Hobbies, História:** Luica realiza um sonho de estudar e conhecer belas artes. |

## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Administrador (Lucas)  | Realizar cadastros de pessoal       | Adicionar funcionários ao Banco de Dados da empresa          |
|Usuário (Lucia)       | Cadastrar novas metas            | Gerenciar melhor o desempenho funcional dos colaboradores |
|Usuário (Melissa)  | Realizar buscas      | Gerar relatórios funcionais que os diferentes setores venham a requisitar       |
|Usuário (Lucas)  | Realizar buscas por funcionários      | Acompanhar os resultados dos funcionários        |
|Usuário (Joao)  | Realizar pesquisas com filtros     | Melhor direcionar os treinamentos da empresa         |
|Usuário (Melissa)  | Realizar pesquisa sobre entrada e saída      | Gerar relatório de ponto         |
|Usuário (Joao)  | Realizar pesquisas sobre colaboradores      | Acompanhar a presença nos treinamentos         |




## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|-------|----------------------------|------|
|RNF-001| Cadastrar funcionários     | ALTA | 
|RNF-002| Gerenciar dados funcionais | ALTA | 
|RNF-003| Emitir relatórios          | ALTA | 
|RNF-004| Gerenciar o cadastro de Funcionários  | ALTA | 
|RNF-005| Gerenciar setores de atuação | ALTA | 
|RNF-006| Gerenciar cargos e salários de funcionários | ALTA | 
|RNF-007| Gerenciar cadastro de metas (CRUD) | ALTA | 
|RNF-008| Permitir o acompanhamento trimestral das metas | ALTA | 
|RNF-009| Emitir controle de metas atingidas/não atingidas| ALTA | 
|RNF-0010| Gerenciar acesso: Permitir a conexão/desconexão de um usuário/funcionário no sistema, carregando suas atribuições de acesso. | ALTA | 
|RNF-0011| Emitir ranqueamento (top 5) de: metas priorizadas, metas atingidas, metas não atingidas. | ALTA | 



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


## Diagrama de Casos de Uso

A imagem a seguir demonstra modelagem de caso de uso:

![image](https://user-images.githubusercontent.com/91227083/160296317-342e67a1-abdb-417b-b386-85723c97a242.png)
