# Phone book app

## Projects
	This folder contains :
		- Angular 9 client app
		- .NetCore API
		- Video demo

## How to run
- Download and install Docker
- Extract file
- Open up a cmd window
- Navigate to the folder where you extracted the file and type
	- `docker pull ninjanine/phonebookclient`
	- `docker pull ninjanine/phonebookapi`
- To start up the api type
	- `docker-compose -f phonebookapi/docker-compose.production.yml up -d`
- To start up the client type
	- `docker run -it -p 80:80 --rm ninjanine:phonebookclient`
	- OR
	- `cd client-angular-app && ng serve`
- Navigate in a browser to `http://localhost:80/` to view the app

## Urls
- Client app 		- `http://localhost:80/`
- API 				- `http://localhost:5000/api/phonebook/`
- MongoExpress 		- `http://localhost:8081/`
- Swagger			- `http://localhost:5001/swagger/index.html`