# CITOrenSAU


Порты, на которых располагаются микросервисы:
* AuthService - 7086
* TicketManager - 7215
* UserService - 5235

## Стек проекта:
* Asp Net Core Web Api
* [Entity Framework Core](https://docs.microsoft.com/ru-ru/ef/core/)
* Unit Tests
* [Docker](https://www.docker.com/)
* [RabbitMQ](https://www.rabbitmq.com/)

  ### Функциональность проекта
Возможности клиента:
1. На микросервисе AuthService:
  - авторизоваться
  - узнать информацию о себе
2. На микросервисе TicketManager:
  - посмотреть список всех тикетов
  - создать новый тикет
  - узнать детальную информацию по конкретному тикету
  - отменить созданный тикет
3. На микросервисе UserService:
  - получение всех юзеров
