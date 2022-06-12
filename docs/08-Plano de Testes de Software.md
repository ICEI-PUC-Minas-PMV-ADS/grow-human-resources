# Plano de Testes de Software| 
|  `Caso de Teste`           |  `CT-01`
|----------------------------|-----------------------------------------------------------------|
| `Requisitos  Associados`    | RF01: O sistema deverá permitir o login de colaboradores no sistema;  |
|   `Objetivo do Teste`      | verificar que a autenticação falhou ao realizar login para um usuário inexistente na base de dados. |
|         `Passos`           |1. Abrir o navegador Web utilizado pela empresa; <br> 2. Acessar o atalho presente na intranet da empresa;</br> 3. Fazer login e visualizar que a crendencial não foi  |
|   `Critérios de Êxito`     |• Falha no direcionamento correto ao site;|

|  `Caso de Teste`           |  `CT-02`
|----------------------------|-----------------------------------------------------------------|
| `Requisitos  Associados`    | RF03: O Sistema deverá Permitir o gerenciamento de usuários (CRUD) <br> RF01: O sistema deverá permitir o login de colaboradores no sistema; </br> |
|   `Objetivo do Teste`      | verficar que o usuário foi cadastrado e autenticado no sistema. |
|         `Passos`           |1. Abrir o navegador Web utilizado pela empresa; <br> 2. Acessar o atalho presente na intranet da empresa;</br> 3. Ir para o link "Quero me cadastrar" <br> 4. Efetua o cadastro dos dados solicitatos. </br> 5. Clicar em "Registrar" |
|   `Critérios de Êxito`     |• Usuários cadastrado na base de dados;|

|  `Caso de Teste`           |  `CT-03`
|----------------------------|-----------------------------------------------------------------|
| `Requisitos  Associados`    | RF03: O Sistema deverá Permitir o gerenciamento de usuários (CRUD) <br> RF01: O sistema deverá permitir o login de colaboradores no sistema; </br>  |
|   `Objetivo do Teste`      | Atualização do Perfil realizada com sucesso. |
|         `Passos`           |1.Selecioar a opção Perfil abaixo do nome do usuário <br> 2. Acessar o atalho presente na intranet da empresa;</br> 3. Preencher os dados solicitaos <br> 4. Clicar em Salvar </br> |
|   `Critérios de Êxito`     |• Perfil atualizado na base de dados;|


|  `Caso de Teste`           |  `CT-04`
|----------------------------|-----------------------------------------------------------------|
| `Requisitos  Associados`    |RF03: O Sistema deverá Permitir o gerenciamento de usuários (CRUD). ; <br>RF04: O Sistema deverá Permitir que o Usuário Silver RH deverá gerenciar funcionários (CRUD) | ALTA |  </br> RF08: O Sistema deverá Permitir que o Usuário Silver RH deverá gerenciar cargos e salários (CRUD) <br>RF11: O Sistema deverá Permitir que o Usuário Silver/Golden RH deverá associar um Funcionário a um deparamento</br> RF13: O Sistema deverá Permitir que o Usuário Bronze/Silver/Golden RH deverá associar um funcionário a um cargo e salário|
|   `Objetivo do Teste`      | Efetivar o cadastro do usuário como funcionário da empresa.|
|         `Passos`           |1. Logar no sistema com visão Silver RH ou superior RH; <br> 2. Navegar para funcionários;</br> 3. Clicar em Novo; <br> 4. informar o username e clicar na lupa. </br> 5. Definir a visão do funcionário; <br> 6. Preenchear os dados solicitados| </br> 7. Clicar em salvar para cada aba preenchida <br>  8. Clicar em listar funcionarios para verificar se usário foi salvo com funcioário.
|   `Critérios de Êxito`     |•  Funcionário criado na base de dados. |

=====

|  `Caso de Teste`           |  `CT-03`
|----------------------------|-----------------------------------------------------------------|
| `Requisitos  Associados`    |RF-07: O sistema deve permitir ao supervisor a inserção, edição e exclusão de funcionários. |
|   `Objetivo do Teste`      | Verificar se as funções estão funcionando de forma correta.  |
|         `Passos`           |1. Abrir o navegador web utilizado pela empresa; <br> 2. Acessar o atalho presente na intranet;</br> 3. Acessar o funcionário desejado e realizar a ação requerida. |
|   `Critérios de Êxito`     |• O gestor deve conseguir realizar com êxito as ações de acordo com suas necessidades. |

=====

|  `Caso de Teste`           |  `CT-04`
|----------------------------|-----------------------------------------------------------------|
| `Requisitos  Associados`    |RF-05: O sistema deve permitir a visualização de indicador de produtividade de metas atingidas/não atingidas. <br> RF-06: O sistema deve apresentar ranqueamento(top5) individual de cada funcionário, apresentando sua posição nos indicadores. </br>  |
|   `Objetivo do Teste`      | Verificar se o conteúdo está sendo disponibilizado de forma correta.  |
|         `Passos`           |1. Abrir o navegador web utilizado pela empresa; <br> 2. Acessar o atalho presente na intranet;</br> 3. Acessar o funcionário desejado e realizar análise de dados. |
|   `Critérios de Êxito`     |• O gestor deve conseguir visualizar de forma clara e objetiva os indicadores dispostos. |

=====

> **Links Úteis**:
> - [IBM - Criação e Geração de Planos de Teste](https://www.ibm.com/developerworks/br/local/rational/criacao_geracao_planos_testes_software/index.html)
> - [Práticas e Técnicas de Testes Ágeis](http://assiste.serpro.gov.br/serproagil/Apresenta/slides.pdf)
> -  [Teste de Software: Conceitos e tipos de testes](https://blog.onedaytesting.com.br/teste-de-software/)
> - [Criação e Geração de Planos de Teste de Software](https://www.ibm.com/developerworks/br/local/rational/criacao_geracao_planos_testes_software/index.html)
> - [Ferramentas de Test para Java Script](https://geekflare.com/javascript-unit-testing/)
> - [UX Tools](https://uxdesign.cc/ux-user-research-and-user-testing-tools-2d339d379dc7)
