.PHONY: default build docker

default: docker

build:
	dotnet build

docker:
	docker build -t blockmason/link-sdk.cs:latest .
