.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build lbh-arrears-api

.PHONY: serve
serve:
	docker-compose build lbh-arrears-api && docker-compose up lbh-arrears-api

.PHONY: shell
shell:
	docker-compose run lbh-arrears-api bash

.PHONY: test
test:
	docker-compose up test-database & docker-compose build lbh-arrears-api-test && docker-compose up lbh-arrears-api-test

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format

.PHONY: restart-db
restart-db:
	docker stop $$(docker ps -q --filter ancestor=test-database -a)
	-docker rm $$(docker ps -q --filter ancestor=test-database -a)
	docker rmi test-database
	docker-compose up -d test-database
