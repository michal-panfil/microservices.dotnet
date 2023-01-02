docker run -d --hostname my-rabbit --name some-rabbit -p 8080:15672 -p 5672:5672 -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management

http://localhost:8080