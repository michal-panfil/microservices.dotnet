#Arhitecture

![alt text](./assets/Microservice%20architecture.drawio.svg "Architecture")
Package console manager:
Upadate-database 
Docker volume create dbfolder 
Copy created database to volume folder
docker compose up


Old notes:
docker run -d --hostname my-rabbit --name some-rabbit -p 8080:15672 -p 5672:5672 -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management

http://localhost:8080

{
"orderId": 101,
"status": "Paid",
"date": "2012-04-23T18:25:43.511Z"
