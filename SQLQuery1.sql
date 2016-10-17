Create Table Documento
(
 idDocumento bigint not null primary key identity,
 nomeDocumento varchar(200) not null,
 tipoDocumento char(1) not null,
 observacaoDocumento varchar(MAX) not null,
 dataCadastro datetime not null default getDae


);