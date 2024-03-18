# REST API Агрегатор Новостей

REST API агрегатор новостей - это сервис, который позволяет пользователям собирать новости с различных новостных сайтов или их RSS-лент и агрегировать их в единой базе данных. Пользователи могут просматривать собранные новости и искать их по подстроке в теле или заголовке новости.

## Возможности

- Добавление источников новостей: Пользователи могут добавлять адреса новостных сайтов или их RSS-ленты для агрегации новостей.
- Агрегация новостей: База данных агрегатора автоматически пополняется новостями из добавленных источников.
- Просмотр новостей: Пользователи могут просматривать список агрегированных новостей.
- Поиск по новостям: Предоставляется возможность поиска новостей по подстроке в теле или заголовке.

## Технологии

- Язык программирования: C#
- База данных: PostgreSQL 14
- ORM: Entity Framework (EF)
- Контейнеризация: Docker

## Запуск проекта

Для запуска проекта необходимо иметь установленные Docker и Docker Compose.

### Шаг 1. Клонирование репозитория

Клонируйте репозиторий в желаемую директорию:

```
git clone https://github.com/HinodeMyojo/newssite_restapi.git
```

### Шаг 2. Запуск с помощью Docker Compose

Используйте Docker Compose для сборки и запуска приложения вместе с базой данных:

```
docker-compose up --build
```

### Шаг 3. Миграции

После запуска контейнеров необходимо провести миграции базы данных для создания необходимых таблиц:

```
dotnet ef database update
```

### Тестирование

Для тестирования REST API агрегатора новостей вы можете использовать следующие адреса новостных сайтов/RSS-лент:

- https://lenta.ru/
- https://news.mail.ru/
- https://www.nytimes.com/section/business
  
- https://news.un.org/feed/subscribe/ru/news/all/rss.xml
- https://rss.nytimes.com/services/xml/rss/nyt/World.xml
- https://www.edu.ru/news/export/

### Документация API

После запуска агрегатор новостей будет доступен по адресу http://localhost:5044. Полная документация по использованию API будет доступна по адресу http://localhost:5044/swagger.

---
