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
|<img src="/docs/img/Lara_Fernandes_da_Silva.jpg"> | **Idade:** 27 anos  | **Ocupação:** Engenheira de Software na empresa Xing Ling do Brasil. Promove as mudanças de TI.|
| **Motivações:** Lara entende que se existe mudança na organização é poorque a organização está crescendo.	| **Frustrações:** Sente-se "limitada" na contribuição do crescimento da organização por não conhecer o perfil dos profissionais que transitam pela empresa. | **Hobbies, História:** Estudante de Tecnolgodia de desenvovilemnto de sistema e, nas horas de folga, nadadora. |

| **João Matheus de Souza** |      |      |
|-----------------------------|------|------|
|<img src="/docs/img/João Matheus de Souza.jpg"> | **Idade:** 25 anos  | **Ocupação:** Engenheiro Industrial na empresa Xing Ling do Brasil. Operações de produção.|
| **Motivações:** João é um jovem engenheiro que se preocupa em prommover a integração profissional e desenvolvimento do time.	| **Frustrações:** O acesso às ações motivacionais propostas pela empresa são lentas e muitas vezes não pode ser cumprida pelo time por chegarem vencidas. | **Hobbies, História:** Instrutor do Senai e fisioculturista. |



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
|RF02| O Sistema deverá Permitir o login de um usuário externo     | Baixa | 
|RF03| O Supervisor de RH deverá gerenciar funcionários (CRUD)     | ALTA | 
|RF04| O Sistema deverá gerenciar Usuários (CRUD)     | Baixa | 
|RF05| O Supervisor de RH deverá gerenciar Departamentos (CRUD)    | ALTA | 
|RF06| O Supervisor de RH deverá gerenciar metas (CRUD)     | ALTA | 
|RF07| O Supervisor de RH deverá gerenciar objetivos (CRUD) | MEDIA | 
|RF08| O Supervisor de RH deverá gerenciar cargos e salários (CRUD) | MEDIA | 
|RF09| O Funcionário de RH deverá gerenciar Oferta de Vagas (CRUD) | BAIXO | 
|RF10| O Supervisor de Departamento deverá cadastrar Metas para Funcionário | ALTA | 
|RF11| O Supervisor de Departamento deverá cadastrar Objetivos para Funcionário | MEDIA | 
|RF12| O Funcionário de RH deverá associar um Funcionário a um deparamento | ALTA | 
|RF13| O Gerente de RH deverá consultar relatórios estatísticos | ALTA | 
|RF14| O Setor de RH deverá associar um funcionário a um cargo e salário | BAIXA | 
|RF15| O Sistema deverá emitir relatório de acompanhamento trimestral das metas | ALTA | 
|RF16| O Sistema deverá emitir relatório de acompanhamento anual dos objetivos | MEDIA | 
|RF17| O Funcionário deverá cadastrar metas atingidas/não atingidas| ALTA |
|RF18| O Supervisor de departamento deverá validar metas atingidas/não atingidas| MEDIA | 
|RF19| O Funcionário deverá cadastrar objetivos atingidos/não atingidao| MEDIA | 
|RF20| O Supervisor de departamento deverá validar objetivos atingidos/não atingidos| MEDIA | 
|RF21| O Supervisor de RH deverá gerenciar os acessos de funcionarios: Permitir a conexão/desconexão de um usuário/funcionário no sistema, carregando suas atribuições de acesso. | ALTA | 
|RF22| O sistema deverá emitir ranqueamento (top 5) de: metas priorizadas, metas atingidas, metas não atingidas. | ALTA | 



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

A imagem a seguir demonstra modelagem de caso de uso:

![image](https://user-images.githubusercontent.com/91227083/160296317-342e67a1-abdb-417b-b386-85723c97a242.png)
