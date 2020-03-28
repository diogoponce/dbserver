# Teste prático - Conta Bancária

## Regra de negócio


>Criar um microservices que, através de um http post efetue uma operação de debito (origem) e credito (destino) nas contas correntes.<br />
>**Entidades:** ContaCorrente, Lancamentos (voce pode incrementar com  outras entidades se achar necessário)

# Instalação
A instalação é um processo simples e requer apenas **Docker** instalado no host.

## Criando os diretórios 
0 - Crie uma pasta chamada **data** em **C:**
 > O caminho deve ficar C:\data 
 
1 - No projeto DBServer.Infra abra a pasta **Setup** e descompacte o arquivo **dbserver.zip**.<br />
2 - Mova a pasta **dbserver** extraída para **C:\data**<br />

> A estrura de diretórios deve ficar assim.<br /><br />
>  **C:\data\dbserver\apache2**<br />
>  **C:\data\dbserver\appsettings**<br />
> **C:\data\dbserver\mssql** <br />

3 - Se necessário configure as permissões do diretório

Caso escolha outra estrutura de diretório será necessário alterar a váriavel **ROOT_PATH** no arquivo **.env** 
para que o docker-compose funcione corretamente.

## Levantando o aplicativo

Na raiz da aplicação abra o terminal e execute o comando, é necessário ter o docker instalado.
Aguarde o processo terminar.

>docker-compose up --build

## Criando o banco de dados

No projeto DBServer.Infra abra a pasta **Setup** e execute o arquivo **Setup.sql**.
Esse script irá criar toda a estrutura e os dados para testes.

# Contas corrente
Após a instalação serão criadas 2 contas correntes com R$100.00 de saldo.
Caso precise adicionar saldo terá que fazer manualmente.

## Contas para teste

Duas contas foram criadas.<br />

>Titular: DIOGO LUIZ PONCE<br />
>Numero: 895623<br />
>Digito: 1<br />
>Senha: 235689<br />

-----
>Titular: SOPHIA ROBERTA PONCE<br />
>Numero: 784512<br />
>Digito: 1<br />
>Senha: 215487<br />

# Microsserviços

Foram criados 3 Microserviços 
0 - Autenticação
1 - Transferência entre contas
2 - Saldo

## Autenticação

Esse serviços autentica e retorna o token para que as outras operações possam ser realizadas.

#### Exemplo 
```
curl -d '{"Numero":"784512", "Digito":"1", "Senha": "215487"}' -H "Content-Type: application/json" -X POST -sS http://localhost/api/auth
```

## Transferência entre contas

Esse serviço executa uma ordem de transferência de R$5.00 para a conta 895623-1.
O débito ocorrerá na conta representada pelo **TOKEN** retornado na autenticação.
Retorna **Status 200** e **true**  em caso de sucesso.

#### Exemplo 
```
curl -d '{"Numero": 895623,"Digito": 1,"Valor": 5.00 }' -H "Content-Type: application/json" -H 'Authorization: Bearer ${TOKEN}' -sS http://localhost/api/transaction
```
## Saldo
Esse serviço retorna apenas o saldo da conta.
O saldo exibido será da conta representada pelo **TOKEN** retornado na autenticação.

```
curl -H "Content-Type: application/json" -H 'Authorization: Bearer ${TOKEN}' -sS http://localhost/api/balance
```

# Tecnologias
0 - Docker
1 - .Net Core 2.2, conforme requisito
2 - Apache 2 para proxy reverso
3 - Sql Server 

