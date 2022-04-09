# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="1-Documentação de Contexto.md"> Documentação de Contexto</a></span>

Definição do problema e ideia de solução a partir da perspectiva do usuário. É composta pela definição do  diagrama de personas, histórias de usuários, requisitos funcionais e não funcionais além das restrições do projeto.

Apresente uma visão geral do que será abordado nesta parte do documento, enumerando as técnicas e/ou ferramentas utilizadas para realizar a especificações do projeto

## Personas

As personas levantadas durante o processo de entendimento do problema são apresentadas nas figuras que se seguem:

| **Lucas De Oliveira Santos** |      |      |
|------------------------------|------|------|
|<img src="/docs/img/photo-lucas.jpg"> | **Idade:** 32 anos  | **Ocupação:** foi promovido a Gerente do setor de RH há aproximadamente 2 anos, trabalha na empresa Xing Ling do Brasil, uma multinacional de grande porte.   |
| **Motivações:** Lucas tem ótimas idéias de gerenciamento e controle da produção, bem como, a gestão de expectativas de carreira e motivacional.	| **Frustrações:** Atualmente Lucas tem dificuldades de acompanhar/avaliar as metas e os objetivos profissionais de cada funcionario, pois não possui uma ferramenta sistematizada que fornece dados estatísticos dos funcionários. | **Hobbies, História:** Lucas sonha em revolucionar as relação funcionario x empregador e, para isso, dedica parte de seu tempo em estudo, pesquisas e treinamentos em inovações de RH.|

| **João Francisco de Almeida** |      |      |
|-------------------------------|------|------|
|<img src="/docs/img/photo-joao.png"> | **Idade:** 57 anos  | **Ocupação:** psicólogo atuante há 35 anos, auxiliou diversas empresas e atualmente busca uma recolocação no mercado.       |
| **Motivações:** João é um profissional dedicado e inovador no desempenho de suas funções. Apesar da aparente idade avançada, João acredita que pode "dar um gás" a mais na sua carreira, promovendo a inovações em multinacionais.	| **Frustrações:** O mercado não fornece recursos de realocação a partir de determinada idade, então ela sente falta de uma ferramenta que o insira no mercado, avaliando seu currículo e suas habilidades. | **Hobbies, História:** João é um pé-de-valsa nato. Gosta de se relacionar com pessoas, não importando etnia, idade ou grau de instrução, o importante é aumentar sua network. | 

| **Melissa Fernandes Santos** |      |      |
|------------------------------|------|------|
|<img src="/docs/img/photo-melissa.jpng.jpeg"> | **Idade:** 22 anos  | **Ocupação:** Estagiária do setor de RH na empresa Xing Ling do Brasil. Faz faculdade de Ciências Contábeis e obteve uma oportunidade de expressar seus conhecimentos contábeis junto ao setor de RH.                        |
| **Motivações:** Melissa está conhecendo o mercado de trabalho e viu a oprtunidade de trabalhar na empresa de Lucas desafiadora pois coniciliar contabilidade com RH será um desafio inovador para sua carreira.	| **Frustrações:** Não domina a maratona de atividades do RH e precisa se orientar dentro da empresa através de uma ferramenta de suporte e capacitação. | **Hobbies, História:** Além de ser uma jogador de vôlei e estudante, Melissa dedica uma fração do tempo em pesquisa e desenvolvimento interpessoal. |                    |

| **Lúcia De Medeiros Silva** |      |      |
|-----------------------------|------|------|
|<img src="/docs/img/photo-lucia.png"> | **Idade:** 42 anos  | **Ocupação:** Supervisora de RH na empresa Xing Ling do Brasil. Faz o gerenciamento do time e atividades de RH.|
| **Motivações:** Lúcia está na empresa a 10 anos e foi recentemente promovida à supervisora de RH. Está motivada com as idéias e metodologias de Lucas e está empenhada em fazer acontecer a inovação na empresa.	| **Frustrações:** A ausência de ferramentas sistematizadas de gestão de RH, onera seu tempo de pensamento inovador com funções operacionais de preenchimento de formulários de cadastros, envio e recebimento de maolotes, dentre outras necessidades do setor. | **Hobbies, História:** Lucia realiza um sonho de estudar e conhecer belas artes. |

| **Lara Fernandes da Silva** |      |      |
|-----------------------------|------|------|
|<img src="/docs/img/photo-lara.png"> | **Idade:** 27 anos  | **Ocupação:** Engenheira de Software na empresa Xing Ling do Brasil. Promove as mudanças de TI.|
| **Motivações:** Lara entende que se existe mudança na organização é poorque a organização está crescendo.	| **Frustrações:** Sente-se "limitada" na contribuição do crescimento da organização por não conhecer o perfil dos profissionais que transitam pela empresa. | **Hobbies, História:** Estudante de Tecnolgodia de desenvovilemnto de sistema e, nas horas de folga, nadadora. |

| **Matheus de Souza** |      |      |
|-----------------------------|------|------|
|<img src="/docs/img/photo-matheus.png"> | **Idade:** 25 anos  | **Ocupação:** Engenheiro Industrial na empresa Xing Ling do Brasil. Operações de produção.|
| **Motivações:** Matheus é um jovem engenheiro que se preocupa em prommover a integração profissional e desenvolvimento do time.	| **Frustrações:** O acesso às ações motivacionais propostas pela empresa são lentas e muitas vezes não pode ser cumprida pelo time por chegarem vencidas. | **Hobbies, História:** Instrutor do Senai e fisioculturista. |



## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Lucas | Receber dados estatísticos de funcionários | Analisar tendências, cumprimento das metas e atendimento aos objetivos. |
|Lucas | Fornecer metas trimestrais | Balanceamento e cadastro pelo time de RH. |
|Lucas | Fornecer objetivos anuais | Balanceamento e cadastro pelo time de RH. |
|Lucia  | Realizar cadastros de funcionários       | Adicionar funcionários ao Banco de Dados da empresa          |
|Lucia  | Realizar cadastros de metas       | Gerar controle de metas          |
|Lucia  | Realizar cadastros de objetivos       | Gerar controle de objetivos         |
|Melissa| Controlar metas      | Que reflitam com clareza os indicadores de performance|
|Melissa| Controlar objetivo   | Que reflitam com clareza os indicadores de aperfeiçoamento profissional|
|João Francisco | Consultar oportunidades em abero | Candidatar a vagas disponíveis |
|Melissa | Realizar pesquisa sobre entrada e saída      | Gerar relatório de ponto         |
|Joao Matheus| Realizar consulta de ponto      | Acompanhar a absenteísmo da equipe        |
|Joao Matheus| Acompanhar metas individuais    | Que cada membro da equipe cumpra as metas individuais  |
|Joao Matheus| Acompanhar metas de Equipe    | Que cada equipe cumpra metas as metas de equipe  |
|Joao Matheus| Acompanhar objetivos individuais  | Garantir o aperfeicoamento profissional de cada membro da equipe|
|Lara| Conhecer as equipes de trabalho  | Promover o engajamento entre TI e Negócios.|




## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|-------|----------------------------|------|
|RF01| O Sistema deverá Permitir o login de funcionários de uma empresa     | ALTA | 
|RF02| O Sistema deverá Permitir o login de um usuário externo     | BAIXA | 
|RF03| O Sistema deverá Permitir o gerenciamento de usuários (CRUD)     | ALTA | 
|RF04| O Supervisor de RH deverá gerenciar funcionários (CRUD)     | ALTA | 
|RF05| O Sistema deverá gerenciar Usuários (CRUD)     | BAIXA | 
|RF06| O Supervisor de RH deverá gerenciar Departamentos (CRUD)    | ALTA | 
|RF07| O Supervisor de RH deverá gerenciar metas (CRUD)     | ALTA | 
|RF08| O Supervisor de RH deverá gerenciar objetivos (CRUD) | MEDIA | 
|RF09| O Supervisor de RH deverá gerenciar cargos e salários (CRUD) | MEDIA | 
|RF10| O Funcionário de RH deverá gerenciar Oferta de Vagas (CRUD) | BAIXA | 
|RF11| O Supervisor de Departamento deverá cadastrar Metas para Funcionário | ALTA | 
|RF12| O Supervisor de Departamento deverá cadastrar Objetivos para Funcionário | MEDIA | 
|RF13| O Funcionário de RH deverá associar um Funcionário a um deparamento | ALTA | 
|RF14| O Gerente de RH deverá consultar relatórios estatísticos | ALTA | 
|RF15| O Funcionário de RH deverá associar um funcionário a um cargo e salário | BAIXA | 
|RF16| O Gerente deve consultar relatório de acompanhamento trimestral das metas | ALTA | 
|RF17| O Gerente deve consultarr relatório de acompanhamento anual dos objetivos | MEDIA | 
|RF18| O Funcionário deverá cadastrar metas atingidas/não atingidas| ALTA |
|RF19| O Supervisor de departamento deverá validar metas atingidas/não atingidas| MEDIA | 
|RF20| O Funcionário deverá cadastrar objetivos atingidos/não atingidao| MEDIA | 
|RF21| O Supervisor de departamento deverá validar objetivos atingidos/não atingidos| MEDIA | 
|RF22| O Supervisor de RH deverá gerenciar os acessos de funcionarios: Permitir a conexão/desconexão de um usuário/funcionário no sistema, carregando suas atribuições de acesso. | ALTA | 
|RF23| O sistema deverá emitir ranqueamento (top 5) de: metas priorizadas, metas atingidas, metas não atingidas. | ALTA | 
|RF24| O usuário deverá consultar as vagas disponíveis em uma empresa. | BAIXA | 



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
|Usuário|	Pessoa interessada em realizar consultas na empresa. Possui um cadastro para logar no sistema.|
|Funcionário| Pessoa que possui as mesmas atribuições de usuário, porém é capaz de realizar acões específicas da organização|
|Funcionário RH| Realiza todoas as atribuições de funcionário, mas também possui atribuições específicas da gestão de RH|
|Supervisor| O supervisor é um funcionário da organização que realiza atividades de controle operacional e pessoal da equipe.|
|Supervisor RH| Realiza o gerenciamento de equipe e é responsável pelo contorle das funções de RH  |
|Gerente| Responsável pelo controle e gerenciamento de um departamento|
|Gerente RH| Responsável pelo controle e gerenciamento de um departamento com atribuições gerenciais de RH


|CASO DE USO|	DESCRIÇÃO|	RF|
|-|-|-|
|Realizar login no sistema|	O usuário deve conseguir realizar um login no sistema com suas credenciais cadastradas|	RF01 RF02|
|Gerenciar Perfil|	O usuário deve conseguir gerenciar o seu perfil|	RF03 RF05 |
|Gerenciar metas pessoais| O funcionário deve gerenciar a situação de suas metas| RF18 |
|Gerenciar objetivos pessoais| O funcionário deve gerenciar a situação de seus objetivos| RF20 |
|Gerenciar oferta de vagas| O funcionário de RH deve efetuar os cadastros de vagas disponíveis para consulta pelos usuários| RF10 |
|Consultar Vagas disponíveis | O usuário dever consultar as vagas disponíveis | RF24|
|Associcar funcionário a departamento| O funcionário de RH deverá associar um funcionário a um departamento| RF13 |
|Gerenciar Funcionários | O supervisor de RH deve gerenciar os funcionários da empresa. | RF04 | 
|Gerenciar Departamentos | O supervisor de RH deve gerenciar os departamentos da empresa. | RF06 | 
|Gerenciar Metas Globais| O supervisor de RH deve gerenciar as metas para os funcionários. | RF07 | 
|Gerenciar Objetivos Globais | O supervisor de RH deve gerenciar os objetivos para os funcionários. | RF08 | 
|Gerenciar Cargos e Salários | O supervisor de RH deve gerenciar os cargos e salários de acordo com a função dos funcionários da empresa. | RF09 | 
|Gerenciar Metas de Equipe| O supervisor deve gerenciar as metas individuais dos funcionários da empresa. | RF11 | 
|Gerenciar Objetivos de Equipe | O supervisor deve gerenciar os objetivos dos funcionários da empresa. | RF12 | 
|Consultar relatório Trimestral | O gerente deve consultar o relatório trimestral de acompanhamento de metas. | RF16 | 
|Consultar relatório Anual | O gerebte deve consultar o relatório anual de acompanhamento de objetivos. | RF17 | 
|Gerenciar Cargos e Salários | O supervisor de RH deve gerenciar Cargos e Salários da empresa. | RF15 | 
|Gerenciar Acessos | O supervisor de RH deve gerenciar os acessos dos funcionários. | RF22 | 
|Consultar Ranqueamento| O Gerente de RH deve consultar o ranqueamento de metas e objetivos| RF23|

https://viewer.diagrams.net/?tags=%7B%7D&highlight=0000ff&edit=_blank&layers=1&nav=1#G1b94imjTEjeaUMr_QMCaFIUFmTRBVSjKR

<img src="/docs/img/User case.drawio.png"> 
