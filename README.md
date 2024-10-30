# Monitoramento de Ativos

O Monitoramento de ativo é uma aplicação de console em C# que monitora a cotação de ativos da B3 e envia alertas por e-mail quando a cotação de um ativo cai abaixo ou sobe acima de níveis predefinidos.

## Objetivo

O sistema visa avisar via e-mail caso a cotação de um ativo da B3:

- Caia abaixo de um preço de referência para compra.
- Suba acima de um preço de referência para venda.

## Como Usar

A aplicação recebe da linha de comando os parâmetros, atualmente eles estão configurados pelo launchSettings para testar via visual studio:

1. O ativo a ser monitorado (por exemplo, `PETR4`).
2. O preço de referência para venda.
3. O preço de referência para compra.

### Configuração

A aplicação lê as configurações de um arquivo de configuração (appSettings) que contêm:

- O e-mail de destino para os alertas.
- As configurações de acesso ao servidor SMTP para envio dos e-mails.
- As informações da API consumida
- Informações de log

### Monitoramento Contínuo

O programa ficará em execução contínua, monitorando a cotação do ativo enquanto estiver rodando.

### Configurações de E-mail

As configurações de e-mail usadas para o envio dos alertas foram baseadas no serviço [Mailtrap](https://mailtrap.io/).

### API Utilizada

A API utilizada para obter as cotações é a [brapi.dev](https://brapi.dev/), especificamente o endpoint `GET /api/quote/list`.
