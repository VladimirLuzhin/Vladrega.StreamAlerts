# Vladrega.StreamAlerts

В данном репозитории приведен пример приложения, которое позволяет отправлять алерты и отображать их в OBS (по типу Donation Alerts)

Данный репозиторий содержит backend на ASP.NET Core и frontend на React\TypeScript. Доставка данных в режиме реального времени обеспечивается благодаря библиотеке SignalR и технологии WebSocket.

Для запуска и проверки используйте docker compose

```
docker compose up -d
```

Далее необходимо получить ссылку на виджет. Сделать это можно отправив следующий запрос:

```
GET http://localhost:5006/api/widget?userName=Vladrega
```

В ответ будет получения ссылка, при открытии которой произойдет подключение к серверу. Ссылка содержит JWT токен для авторизованного соединения по WebSocket-у
Чтобы отправить Alert отправте следующий запрос:

```
POST http://localhost:5006/api/alerts
Content-Type: application/json

{
    "senderName": "Viewer",
    "name": "Vladrega",
    "text": "привет butabrot"
}
```
