# Programação de Funcionalidades

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>, <a href="4-Metodologia.md"> Metodologia</a>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>, <a href="5-Arquitetura da Solução.md"> Arquitetura da Solução</a>

Implementação do sistema descritas por meio dos requisitos funcionais e/ou não funcionais. Deve relacionar os requisitos atendidos os artefatos criados (código fonte) além das estruturas de dados utilizadas e as instruções para acesso e verificação da implementação que deve estar funcional no ambiente de hospedagem.

Para cada requisito funcional, pode ser entregue um artefato desse tipo

|ID    | Descrição do Requisito  | implementado | front-end | back-end |
|------|-------------------------|--------------|-----------|----------|
|RF01| O Sistema deverá Permitir o login de funcionários da empresa | Sim | ..\GHR\SPA\GHR-App\src\app\Components\Conta\Login\login.component.html ..\GHR\SPA\GHR-App\src\app\Components\Conta\Login\login.component.scss ..\GHR\SPA\GHR-App\src\app\Components\Conta\Login\login.component.ts ..\GHR\SPA\GHR-App\src\app\models\Contas\ContaLogin.ts ..\GHR\SPA\GHR-App\src\app\services\contas\Conta.service.ts| ..\GHR\Server\src\GHR.API\Controllers\Contas\ContasController.cs ..\GHR\Server\src\GHR.Application\Services\Contracts\Contas\IContaService.cs ..\GHR\Server\src\GHR.Application\Services\Implements\Contas\IContaService.cs ..\GHR\Server\src\GHR.Application\Dtos\Contas\ContaLoginDto.cs ..\GHR\Server\src\GHR.Domain\DataBase\Contas\Conta.cs ..\GHR\Server\src\GHR.Persistence\Interfaces\Contexts\GHRContext.cs ..\GHR\Server\src\GHR.Persistence\Interfaces\Contracts\Contas\IContaPersistence.cs ..\GHR\Server\src\GHR.Persistence\Interfaces\Implements\Contas\ContaPersistence.cs
|RF03| O Sistema deverá Permitir o gerenciamento de usuários (CRUD) | ALTA | 
|RF04| O Sistema deverá Permitir que o Usuário Silver RH deverá gerenciar funcionários (CRUD) | ALTA | 
|RF05| O Sistema deverá Permitir que o Usuário Golden RH deverá gerenciar Departamentos (CRUD) | ALTA |
|RF06| O Sistema deverá Permitir que o Usuário Silver RH deverá gerenciar metas (CRUD) | ALTA | 
|RF07| O Sistema deverá Permitir que o Usuário Silver RH deverá gerenciar objetivos (CRUD) | BAIXA | 
|RF08| O Sistema deverá Permitir que o Usuário Silver RH deverá gerenciar cargos e salários (CRUD) | BAIXA | 
|RF09| O Sistema deverá Permitir que o Usuário Silver RH deverá cadastrar Metas para Funcionário | ALTA |
|RF10| O Sistema deverá Permitir que o Usuário Silver RH deverá cadastrar Objetivos para Funcionário | BAIXA | 
|RF11| O Sistema deverá Permitir que o Usuário Silver/Golden RH deverá associar um Funcionário a um deparamento | ALTA |
|RF12| O Sistema deverá Permitir que o Usuário Bronze/Silver/Golden deverá consultar relatórios estatísticos | ALTA | 
|RF13| O Sistema deverá Permitir que o Usuário Bronze/Silver/Golden RH deverá associar um funcionário a um cargo e salário | MÉDIA | 
|RF14| O Sistema deve permitir que o Usuário Bronze/Silver/Golden possa consultar relatório de acompanhamento das metas | MÉDIA | 
|RF15| O Sistema deve permitir que o Usuário Bronze/Silver/Golden possa consultar relatório de acompanhamento anual dos objetivos | BAIXA | 
|RF16| O Sistema deve permitir que o Usuário Bronze/Silver/Golden RH possa cadastrar metas atingidas/não atingidas| ALTA | 
|RF17| O sistema deverá emitir ranqueamento (top 5) de: metas atingidas, metas não atingidas. | ALTA |

> **Links Úteis**:
>
> - [Trabalhando com HTML5 Local Storage e JSON](https://www.devmedia.com.br/trabalhando-com-html5-local-storage-e-json/29045)
> - [JSON Tutorial](https://www.w3resource.com/JSON)
> - [JSON Data Set Sample](https://opensource.adobe.com/Spry/samples/data_region/JSONDataSetSample.html)
> - [JSON - Introduction (W3Schools)](https://www.w3schools.com/js/js_json_intro.asp)
> - [JSON Tutorial (TutorialsPoint)](https://www.tutorialspoint.com/json/index.htm)