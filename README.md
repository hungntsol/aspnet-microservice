# aspnet-microservice

### Basket

### Catalog

### Discount

### Ordering
- Migrate schema: at Order.Infrastructure run `dotnet ef migrations add ${version} -s ../Order.API`

### RabbitMQ
- Username: guest
- Password: guest

### Run, deploy
- Run: at src folder: `docker-compose --env-file .env.development up -d`
