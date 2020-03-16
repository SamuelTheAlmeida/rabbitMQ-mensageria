# Microsserviço de mensageria
- .NET Core 2.2
- Docker
- Docker compose
- RabbitMQ
- SignalR
- Serilog para logging

### Instruções:
```
docker-compose build
docker-compose up
```

A mensagem e o intervalo de envio podem ser parametrizados via appSettings.json, nos parâmetros abaixo:
```
  "MessageText":  "Hello World!",
  "SendMessageInterval": "5"
```

