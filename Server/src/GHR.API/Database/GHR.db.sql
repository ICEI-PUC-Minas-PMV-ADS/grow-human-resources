BEGIN;
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
	`MigrationId`	varchar NOT NULL,
	`ProductVersion`	varchar NOT NULL,
	CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY(`MigrationId`)
);
CREATE TABLE IF NOT EXISTS `AspNetRoles` (
	`Id`	INTEGER NOT NULL,
	`NomeFuncao`	varchar,
	`Name`	varchar,
	`NormalizedName`	varchar,
	`ConcurrencyStamp`	varchar,
	CONSTRAINT `PK_AspNetRoles` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `AspNetUsers` (
	`Id`	INTEGER NOT NULL,
	`NomeCompleto`	varchar,
	`Visao`	varchar,
	`Descricao`	varchar,
	`ImagemURL`	varchar,
	`UserName`	varchar,
	`NormalizedUserName`	varchar,
	`Email`	varchar,
	`NormalizedEmail`	varchar,
	`EmailConfirmed`	INTEGER NOT NULL,
	`PasswordHash`	varchar,
	`SecurityStamp`	varchar,
	`ConcurrencyStamp`	varchar,
	`PhoneNumber`	varchar,
	`PhoneNumberConfirmed`	INTEGER NOT NULL,
	`TwoFactorEnabled`	INTEGER NOT NULL,
	`LockoutEnd`	varchar,
	`LockoutEnabled`	INTEGER NOT NULL,
	`AccessFailedCount`	INTEGER NOT NULL,
	CONSTRAINT `PK_AspNetUsers` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `DadosPessoais` (
	`Id`	INTEGER NOT NULL,
	`CPF`	varchar,
	`TituloEleitor`	varchar,
	`Identidade`	varchar,
	`DataExpedicaoIdentidade`	varchar,
	`OrgaoExpedicaoIdentidade`	varchar,
	`UfIdentidade`	varchar,
	`EstadoCivil`	varchar,
	`CarteiraTrabalho`	varchar,
	`DataExpedicaoCarteiraTrabalho`	varchar,
	CONSTRAINT `PK_DadosPessoais` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `Departamentos` (
	`Id`	INTEGER NOT NULL,
	`NomeDepartamento`	varchar,
	`SiglaDepartamento`	varchar,
	`MetaId`	INTEGER,
	`Diretor`	varchar,
	`Gerente`	varchar,
	`Supervisor`	varchar,
	CONSTRAINT `PK_Departamentos` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `Enderecos` (
	`Id`	INTEGER NOT NULL,
	`CEP`	varchar,
	`Logradouro`	varchar,
	`Numero`	varchar,
	`Complemento`	varchar,
	`Bairro`	varchar,
	`Cidade`	varchar,
	`UF`	varchar,
	`Pais`	varchar,
	`CaixaPostal`	varchar,
	`ComplementoEndereco`	varchar,
	CONSTRAINT `PK_Enderecos` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `AspNetRoleClaims` (
	`Id`	INTEGER NOT NULL,
	`RoleId`	INTEGER NOT NULL,
	`ClaimType`	varchar,
	`ClaimValue`	varchar,
	CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY(`RoleId`) REFERENCES `AspNetRoles`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `AspNetUserClaims` (
	`Id`	INTEGER NOT NULL,
	`UserId`	INTEGER NOT NULL,
	`ClaimType`	varchar,
	`ClaimValue`	varchar,
	CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY(`UserId`) REFERENCES `AspNetUsers`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `AspNetUserLogins` (
	`LoginProvider`	varchar NOT NULL,
	`ProviderKey`	varchar NOT NULL,
	`ProviderDisplayName`	varchar,
	`UserId`	INTEGER NOT NULL,
	CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY(`UserId`) REFERENCES `AspNetUsers`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY(`LoginProvider`,`ProviderKey`)
);
CREATE TABLE IF NOT EXISTS `AspNetUserRoles` (
	`UserId`	INTEGER NOT NULL,
	`RoleId`	INTEGER NOT NULL,
	CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY(`RoleId`) REFERENCES `AspNetRoles`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY(`UserId`) REFERENCES `AspNetUsers`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY(`UserId`,`RoleId`)
);
CREATE TABLE IF NOT EXISTS `AspNetUserTokens` (
	`UserId`	INTEGER NOT NULL,
	`LoginProvider`	varchar NOT NULL,
	`Name`	varchar NOT NULL,
	`Value`	varchar,
	CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY(`UserId`) REFERENCES `AspNetUsers`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY(`UserId`,`LoginProvider`,`Name`)
);
CREATE TABLE IF NOT EXISTS `Cargos` (
	`Id`	INTEGER NOT NULL,
	`NomeCargo`	varchar,
	`Funcao`	varchar,
	`DepartamentoId`	INTEGER NOT NULL,
	CONSTRAINT `FK_Cargos_Departamentos_DepartamentoId` FOREIGN KEY(`DepartamentoId`) REFERENCES `Departamentos`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `PK_Cargos` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `Funcionarios` (
	`Id`	INTEGER NOT NULL,
	`CargoId`	INTEGER,
	`ContaId`	INTEGER,
	`DadosPessoaisId`	INTEGER,
	`DataAdmissao`	varchar,
	`DataDemissao`	varchar,
	`DepartamentoId`	INTEGER,
	`EnderecoId`	INTEGER,
	`FuncionarioAtivo`	INTEGER NOT NULL,
	`Salario`	REAL NOT NULL,
	CONSTRAINT `FK_Funcionarios_Departamentos_DepartamentoId` FOREIGN KEY(`DepartamentoId`) REFERENCES `Departamentos`(`Id`) ON DELETE RESTRICT,
	CONSTRAINT `FK_Funcionarios_Enderecos_EnderecoId` FOREIGN KEY(`EnderecoId`) REFERENCES `Enderecos`(`Id`) ON DELETE RESTRICT,
	CONSTRAINT `FK_Funcionarios_DadosPessoais_DadosPessoaisId` FOREIGN KEY(`DadosPessoaisId`) REFERENCES `DadosPessoais`(`Id`) ON DELETE RESTRICT,
	CONSTRAINT `FK_Funcionarios_AspNetUsers_ContaId` FOREIGN KEY(`ContaId`) REFERENCES `AspNetUsers`(`Id`) ON DELETE RESTRICT,
	CONSTRAINT `FK_Funcionarios_Cargos_CargoId` FOREIGN KEY(`CargoId`) REFERENCES `Cargos`(`Id`) ON DELETE RESTRICT,
	CONSTRAINT `PK_Funcionarios` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `Metas` (
	`Id`	INTEGER NOT NULL,
	`Descricao`	varchar,
	`FimPlanejado`	varchar,
	`FimRealizado`	varchar,
	`InicioPlanejado`	varchar,
	`InicioRealizado`	varchar,
	`MetaAprovada`	INTEGER NOT NULL,
	`MetaCumprida`	INTEGER NOT NULL,
	`NomeMeta`	varchar,
	`Supervisor`	varchar,
	CONSTRAINT `PK_Metas` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
CREATE TABLE IF NOT EXISTS `FuncionariosMetas` (
	`Id`	INTEGER NOT NULL,
	`FimAcordado`	varchar,
	`FimRealizado`	varchar,
	`FuncionarioId`	INTEGER NOT NULL,
	`InicioAcordado`	varchar,
	`InicioRealizado`	varchar,
	`MetaCumprida`	INTEGER NOT NULL,
	`MetaId`	INTEGER NOT NULL,
	`Supervisor`	varchar,
	CONSTRAINT `FK_FuncionariosMetas_Metas_MetaId` FOREIGN KEY(`MetaId`) REFERENCES `Metas`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `FK_FuncionariosMetas_Funcionarios_FuncionarioId` FOREIGN KEY(`FuncionarioId`) REFERENCES `Funcionarios`(`Id`) ON DELETE CASCADE,
	CONSTRAINT `PK_FuncionariosMetas` PRIMARY KEY(`Id` AUTO_INCREMENT)
);
INSERT INTO `__EFMigrationsHistory` VALUES ('20220531181027_Initial','5.0.17');
INSERT INTO `__EFMigrationsHistory` VALUES ('20220601111958_Initial-review','5.0.17');
INSERT INTO `__EFMigrationsHistory` VALUES ('20220601112327_Initial-review2','5.0.17');
INSERT INTO `__EFMigrationsHistory` VALUES ('20220601125636_Initial-review3','5.0.17');
INSERT INTO `__EFMigrationsHistory` VALUES ('20220601145402_Initial-review4','5.0.17');
INSERT INTO `__EFMigrationsHistory` VALUES ('20220601213947_Initial-review5','5.0.17');
INSERT INTO `AspNetUsers` VALUES (1,'administrador','masterRH',NULL,NULL,'admin','ADMIN','admin@ghr.com.br','ADMIN@GHR.COM.BR',0,'AQAAAAEAACcQAAAAEKSUpr4RTg/q/Qw3+ul0OhckmxjHjoKgoL4rp8s1UoDuUq7j/qFWPvB1FSJpSqkHpw==','OIVGYWOIPKG7YOJYY4CYNJBURLU2ALAC','197bee38-9455-48d3-8f4b-0ca282131e4a','(11) 1111-1111',0,0,NULL,1,0);
INSERT INTO `AspNetUsers` VALUES (2,'Francisco Braga','Diretor RH',NULL,NULL,'franciscobraga','FRANCISCOBRAGA','francisco.braga@ghr.com.br','FRANCISCO.BRAGA@GHR.COM.BR',0,'AQAAAAEAACcQAAAAEF8rBfume/q278KkunTFlpd8s2rkBKD0NYBT5KiDkMOI4og+hKHeiRD02JJMwTa8zw==','YMAOBI3JBMXKMSZ3KS5UTPZT3NC423ND','43c7a997-4a86-4724-944a-b70de837baf5','(31) 3001-3001',0,0,NULL,1,0);
INSERT INTO `AspNetUsers` VALUES (3,'Clóvis Regis Fogaça','Gerente RH',NULL,NULL,'clovisregis','CLOVISREGIS','clovis.regis@ghr.com.br','CLOVIS.REGIS@GHR.COM.BR',0,'AQAAAAEAACcQAAAAEHidK2MeNKdbu3RvT85Z7ZO65mEW32X1mwfjL2okcCqHnrYh2iQE/IDehepkae9eIQ==','KIOSVVTP67VNDANY23YV5PF3EYCRRJVT','e3c1ecf7-4d7b-444f-98b9-0a2b72cae714','(31) 3001-3002',0,0,NULL,1,0);
INSERT INTO `AspNetUsers` VALUES (4,'Breno Vitorino Silva','Supervisor RH',NULL,NULL,'brenosilva','BRENOSILVA','breno.silva@ghr.com.br','BRENO.SILVA@GHR.COM.BR',0,'AQAAAAEAACcQAAAAENRSM/SNQrB9FSdtbZMq8gEm0aY98RHyJzSpjN8yPhkpFQlbcq96soPRmUGKbbXWuQ==','JV5NBC3EPYY6PNHXA57J4VCS5WITNUML','347f962e-b52f-4d9f-9aa7-24ee92eaaa03','(31) 3001-3003',0,0,NULL,1,0);
INSERT INTO `AspNetUsers` VALUES (5,'Jueliete Felismina Souza','Funcionário RH ',NULL,NULL,'julietesouza','JULIETESOUZA','juliete.souza@ghr.com.br','JULIETE.SOUZA@GHR.COM.BR',0,'AQAAAAEAACcQAAAAEENxmi8XdOwNUq3VKHLer1vCuKwelMz6k8LDKId7Ol8QlUViLqjielVHympHgzuJGw==','R544OGBPCQIMDYSLX32A5Q4OINAYIW3M','62968939-d937-4aa7-a2ce-80b7919956af','(31) - 3001-3004',0,0,NULL,1,0);
INSERT INTO `DadosPessoais` VALUES (1,'adm','adm','adm','0001-01-01T03:06:28.000Z','adm','AC','Não Informado','adm','0001-01-01T03:06:28.000Z');
INSERT INTO `DadosPessoais` VALUES (2,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `DadosPessoais` VALUES (3,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `DadosPessoais` VALUES (4,'111.111.111-11','1111111','1111111','2000-01-01T02:00:00.000Z','SSP','MG','Solteiro','12123','2000-01-01T02:00:00.000Z');
INSERT INTO `DadosPessoais` VALUES (5,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `DadosPessoais` VALUES (6,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `DadosPessoais` VALUES (8,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `Departamentos` VALUES (1,'Administrador do sistema','adm-sis',0,'NaoInformado','NaoInformado','NaoInformado');
INSERT INTO `Departamentos` VALUES (2,'Diretoria Geral','DG',0,'NaoInformado','NaoInformado','NaoInformado');
INSERT INTO `Departamentos` VALUES (3,'Departamento de Marketing','DEP-MKT',0,'NaoInformado','NaoInformado','NaoInformado');
INSERT INTO `Departamentos` VALUES (4,'Departamento de Vendas','DEP-VEN',0,'NaoInformado','NaoInformado','NaoInformado');
INSERT INTO `Departamentos` VALUES (6,'Deparamento de Recursos Humanos','DEP-RH',0,'Francisco Braga','Clóvis Regis Fogaça','Breno Vitorino Silva');
INSERT INTO `Departamentos` VALUES (7,'Deparamento de Administração','DEP-ADM',0,'NaoInformado','NaoInformado','NaoInformado');
INSERT INTO `Departamentos` VALUES (8,'Deparamento de Produção','DEP-PRO',0,'NaoInformado','NaoInformado','NaoInformado');
INSERT INTO `Departamentos` VALUES (9,'Deparamento de Auditorias','DEP-AUD',0,'NaoInformado','NaoInformado','NaoInformado');
INSERT INTO `Departamentos` VALUES (10,'Deparamento de Tecnologia-Informação-Comunicação','DEP-TIC',0,'NaoInformado','NaoInformado','NaoInformado');
INSERT INTO `Enderecos` VALUES (1,'adm','adm','adm','adm','adm','adm','AC','adm','adm','adm');
INSERT INTO `Enderecos` VALUES (2,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `Enderecos` VALUES (3,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `Enderecos` VALUES (4,'11392-121','um','323','casa','Treze de maio','Belo Horizonte','MG','Brasil','12123','23331');
INSERT INTO `Enderecos` VALUES (5,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `Enderecos` VALUES (6,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `Enderecos` VALUES (8,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO `Cargos` VALUES (1,'Adminstrador Sistema','Auxiliar',1);
INSERT INTO `Cargos` VALUES (2,'Presidente','Presidente',2);
INSERT INTO `Cargos` VALUES (3,'Vice-Presidente','Presidente',2);
INSERT INTO `Cargos` VALUES (4,'Primeiro Secretário','Assistente',2);
INSERT INTO `Cargos` VALUES (5,'Segundo Secretário','Assistente',2);
INSERT INTO `Cargos` VALUES (6,'Primeiro Tesoureiro','Analista',2);
INSERT INTO `Cargos` VALUES (7,'Segundo Tesoureiro','Analista',2);
INSERT INTO `Cargos` VALUES (8,'Gerente de Marketing','Gerente',3);
INSERT INTO `Cargos` VALUES (9,'Supervisor de Marketing','Supervisor',3);
INSERT INTO `Cargos` VALUES (10,'Analista de Marketing','Analista',3);
INSERT INTO `Cargos` VALUES (11,'Redator','Analista',3);
INSERT INTO `Cargos` VALUES (12,'Asistente de Marketing','Assistente',3);
INSERT INTO `Cargos` VALUES (13,'Programador','Analista',3);
INSERT INTO `Cargos` VALUES (14,'Designer','Analista',3);
INSERT INTO `Cargos` VALUES (15,'Diretor de Marketing','Diretor',3);
INSERT INTO `Cargos` VALUES (16,'Diretor de Vendas','Diretor',4);
INSERT INTO `Cargos` VALUES (17,'Gerente de Vendas','Gerente',4);
INSERT INTO `Cargos` VALUES (18,'Supervisor de Vendas','Supervisor',4);
INSERT INTO `Cargos` VALUES (19,'Coodenador de Vendas','Coordenador',4);
INSERT INTO `Cargos` VALUES (20,'Consultor de Vendas','Analista',4);
INSERT INTO `Cargos` VALUES (21,'Analista de Vendas','Analista',4);
INSERT INTO `Cargos` VALUES (22,'Suporte a Vendas','Analista',4);
INSERT INTO `Cargos` VALUES (23,'Asistente de Vendas','Assistente',4);
INSERT INTO `Cargos` VALUES (24,'Auxiliar Administrativo de Vendas','Auxiliar',4);
INSERT INTO `Cargos` VALUES (25,'Diretor de Recursos Humanos','Diretor',6);
INSERT INTO `Cargos` VALUES (26,'Gerente de Recursos Humanos','Gerente',6);
INSERT INTO `Cargos` VALUES (27,'Supervisor de Recursos Humanos','Supervisor',6);
INSERT INTO `Cargos` VALUES (28,'Consultor de Recursos Humanos','Analista',6);
INSERT INTO `Cargos` VALUES (29,'Gestor de Recrutamento e Seleção','Analista',6);
INSERT INTO `Cargos` VALUES (30,'Analista de Cargos, Salários e Carreiras','Analista',6);
INSERT INTO `Cargos` VALUES (31,'Analista de Treinamento e Capacitação','Analista',6);
INSERT INTO `Cargos` VALUES (32,'Analista de Recursos Humanos','Analista',6);
INSERT INTO `Cargos` VALUES (33,'Diretor Administrativo','Diretor',7);
INSERT INTO `Cargos` VALUES (34,'Gerente Administrativo','Gerente',7);
INSERT INTO `Cargos` VALUES (35,'Supervisor Administrativo','Supervisor',7);
INSERT INTO `Cargos` VALUES (36,'Auxiliar Administrativo','Auxiliar',7);
INSERT INTO `Cargos` VALUES (37,'Auxiliar de Contabilidade','Auxiliar',7);
INSERT INTO `Cargos` VALUES (38,'Auxiliar de Serviços Gerais','Auxiliar',7);
INSERT INTO `Cargos` VALUES (39,'Agente de Portaria','Auxiliar',7);
INSERT INTO `Cargos` VALUES (40,'Motorista','Auxiliar',7);
INSERT INTO `Cargos` VALUES (41,'Assistente Administrativo','Assistente',7);
INSERT INTO `Cargos` VALUES (42,'Técnico de Contabilidade','Analista',7);
INSERT INTO `Cargos` VALUES (43,'Secretária','Assistente',7);
INSERT INTO `Cargos` VALUES (44,'Administrador','Assistente',7);
INSERT INTO `Cargos` VALUES (45,'Auditor','Assistente',7);
INSERT INTO `Cargos` VALUES (46,'Analista Administrativo','Analista',7);
INSERT INTO `Cargos` VALUES (47,'Contador','Analista',7);
INSERT INTO `Cargos` VALUES (48,'Diretor de Produção','Diretor',8);
INSERT INTO `Cargos` VALUES (49,'Gerente de Produção','Gerente',8);
INSERT INTO `Cargos` VALUES (50,'Supervisor de Produção','Supervisor',8);
INSERT INTO `Cargos` VALUES (51,'Analista de Produção','Analista',8);
INSERT INTO `Cargos` VALUES (52,'Auxiliar de Produção','Auxiliar',8);
INSERT INTO `Funcionarios` VALUES (1,1,1,1,'2022-05-31T20:45:44.000Z',NULL,1,1,0,110.0);
INSERT INTO `Funcionarios` VALUES (2,25,2,4,'2022-06-01T09:45:49.000Z',NULL,6,4,0,12123.0);
INSERT INTO `Funcionarios` VALUES (3,26,3,5,'2022-06-01T10:25:32.000Z',NULL,6,5,0,12421.0);
INSERT INTO `Funcionarios` VALUES (4,27,4,6,'2022-06-01T10:31:38.000Z',NULL,6,6,0,9210.0);
INSERT INTO `Funcionarios` VALUES (6,32,5,8,'2022-06-01T11:52:51.000Z',NULL,6,8,0,4210.0);
INSERT INTO `Metas` VALUES (1,'Obter Certificação em Gerenciamento de Recursos Humanos conforme normativa c-GHR','2023-06-01T11:39:41.000Z',NULL,'2022-06-01T11:39:41.000Z',NULL,1,0,'c-GRH - Certificação em Gerenciamento de Recursos Humanos',NULL);
INSERT INTO `Metas` VALUES (2,'Obter Certificação em Supervisionamento de Recursos Humanos conforme normativa c-SHR','2023-06-01T11:39:41.000Z',NULL,'2022-06-01T11:39:41.000Z',NULL,1,0,'c-SRH - Certificação em Superviosionamento de Recursos Humanos',NULL);
INSERT INTO `Metas` VALUES (3,'Obter Certificação em Análise de Recursos Humanos conforme normativa c-AHR','2023-06-01T11:39:41.000Z',NULL,'2022-06-01T11:39:41.000Z',NULL,1,0,'c-ARH - Certificação em Análise de Recursos Humanos',NULL);
INSERT INTO `Metas` VALUES (4,'Obter Certificação em Suporte a Recursos Humanos conforme normativa c-SupHR','2023-06-01T11:39:41.000Z',NULL,'2022-06-01T11:39:41.000Z',NULL,1,0,'c-SupRH - Certificação em Suporte a de Recursos Humanos',NULL);
INSERT INTO `FuncionariosMetas` VALUES (1,'2023-06-01T11:39:41.000Z',NULL,3,'2022-06-01T11:39:41.000Z',NULL,0,1,NULL);
INSERT INTO `FuncionariosMetas` VALUES (2,'2023-06-01T11:39:41.000Z',NULL,3,'2022-06-01T11:39:41.000Z',NULL,0,2,NULL);
CREATE INDEX IF NOT EXISTS `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (
	`RoleId`
);
CREATE UNIQUE INDEX IF NOT EXISTS `RoleNameIndex` ON `AspNetRoles` (
	`NormalizedName`
);
CREATE INDEX IF NOT EXISTS `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (
	`UserId`
);
CREATE INDEX IF NOT EXISTS `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (
	`UserId`
);
CREATE INDEX IF NOT EXISTS `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (
	`RoleId`
);
CREATE INDEX IF NOT EXISTS `EmailIndex` ON `AspNetUsers` (
	`NormalizedEmail`
);
CREATE UNIQUE INDEX IF NOT EXISTS `UserNameIndex` ON `AspNetUsers` (
	`NormalizedUserName`
);
CREATE INDEX IF NOT EXISTS `IX_Cargos_DepartamentoId` ON `Cargos` (
	`DepartamentoId`
);
CREATE INDEX IF NOT EXISTS `IX_Funcionarios_CargoId` ON `Funcionarios` (
	`CargoId`
);
CREATE INDEX IF NOT EXISTS `IX_Funcionarios_ContaId` ON `Funcionarios` (
	`ContaId`
);
CREATE INDEX IF NOT EXISTS `IX_Funcionarios_DadosPessoaisId` ON `Funcionarios` (
	`DadosPessoaisId`
);
CREATE INDEX IF NOT EXISTS `IX_Funcionarios_DepartamentoId` ON `Funcionarios` (
	`DepartamentoId`
);
CREATE INDEX IF NOT EXISTS `IX_Funcionarios_EnderecoId` ON `Funcionarios` (
	`EnderecoId`
);
CREATE INDEX IF NOT EXISTS `IX_FuncionariosMetas_FuncionarioId` ON `FuncionariosMetas` (
	`FuncionarioId`
);
CREATE INDEX IF NOT EXISTS `IX_FuncionariosMetas_MetaId` ON `FuncionariosMetas` (
	`MetaId`
);
COMMIT;
