CREATE TABLE Chamado(
	Id bigint NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	Status varchar(20) NOT NULL,
	Nome varchar(150) NOT NULL,
	Descricao varchar(max) NULL,
	Valor float NULL,
	Responsavel varchar(100) NULL,
	IdCardJira bigint NULL
);
 


EXEC sp_configure 'show advanced options', 1
RECONFIGURE

EXEC sp_configure 'Ole Automation Procedures', 1
RECONFIGURE



CREATE PROC SPX_MAKE_API_REQUEST(@RTYPE VARCHAR(MAX),@authHeader VARCHAR(MAX), @RPAYLOAD VARCHAR(MAX), @URL VARCHAR(MAX),@OUTSTATUS VARCHAR(MAX) OUTPUT,@OUTRESPONSE VARCHAR(MAX) OUTPUT
)
AS
 
DECLARE @contentType NVARCHAR(64);
DECLARE @postData NVARCHAR(2000);
DECLARE @responseText NVARCHAR(2000);
DECLARE @responseXML NVARCHAR(2000);
DECLARE @ret INT;
DECLARE @status NVARCHAR(32);
DECLARE @statusText NVARCHAR(32);
DECLARE @token INT;
SET @contentType = 'application/json';
-- Open the connection.
EXEC @ret = sp_OACreate 'MSXML2.ServerXMLHTTP', @token OUT;
IF @ret <> 0 RAISERROR('Unable to open HTTP connection.', 10, 1);
-- Send the request.
EXEC @ret = sp_OAMethod @token, 'open', NULL, @RTYPE, @url, 'false';
EXEC @ret = sp_OAMethod @token, 'setRequestHeader', NULL, 'Authentication', @authHeader;
EXEC @ret = sp_OAMethod @token, 'setRequestHeader', NULL, 'Content-type', 'application/json';
SET @RPAYLOAD = (SELECT CASE WHEN @RTYPE = 'Get' THEN NULL ELSE @RPAYLOAD END )
EXEC @ret = sp_OAMethod @token, 'send', NULL, @RPAYLOAD; -- IF YOUR POSTING, CHANGE THE LAST NULL TO @postData
-- Handle the response.
EXEC @ret = sp_OAGetProperty @token, 'status', @status OUT;
EXEC @ret = sp_OAGetProperty @token, 'statusText', @statusText OUT;
EXEC @ret = sp_OAGetProperty @token, 'responseText', @responseText OUT;
-- Show the response.
PRINT 'Status: ' + @status + ' (' + @statusText + ')';
PRINT 'Response text: ' + @responseText;
SET @OUTSTATUS = 'Status: ' + @status + ' (' + @statusText + ')'
SET @OUTRESPONSE = 'Response text: ' + @responseText;
-- Close the connection.
EXEC @ret = sp_OADestroy @token;
IF @ret <> 0 RAISERROR('Unable to close HTTP connection.', 10, 1);



CREATE TRIGGER CriarCardJira
ON Chamado
AFTER INSERT
AS 
BEGIN
	DECLARE @OUTSTATUS VARCHAR(MAX),@OUTRESPONSE VARCHAR(MAX),@POSTDATA VARCHAR(MAX),@NOME VARCHAR(MAX),@DESCRICAO VARCHAR(MAX),@INSERTEDID VARCHAR(MAX)
	SET @NOME = (SELECT Nome FROM INSERTED)
	SET @DESCRICAO = (SELECT Descricao FROM INSERTED)
	SET @INSERTEDID = (SELECT Id FROM INSERTED)
	SET @POSTDATA = '{ "id": "'+@INSERTEDID+'", "nome": "'+@NOME+'", "descricao" : "'+@DESCRICAO+'"}'
	EXEC SPX_MAKE_API_REQUEST 'POST','',@POSTDATA,'https://1374bb68c913.ngrok.io/api/v1/WebHooks/jira',@OUTSTATUS OUTPUT,@OUTRESPONSE OUTPUT
	UPDATE Chamado
	SET IdCardJira = STUFF(@OUTRESPONSE, 1, 15, '')
	WHERE Id = @INSERTEDID
END


DROP TRIGGER CriarCardJira



INSERT INTO Chamado(status, nome, descricao)
VALUES('To Do', 'Correção', 'Problema 1\nProblema 2')

UPDATE Chamado SET Status='To Do' WHERE IdCardJira=10003
UPDATE Chamado SET Status='In Progress' WHERE IdCardJira=10002
UPDATE Chamado SET Status='Done' WHERE IdCardJira=10002


