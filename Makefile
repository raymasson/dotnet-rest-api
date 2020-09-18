test:
	@./run-tests.sh

run:
	@dotnet clean
	@dotnet restore
	@dotnet build
	@dotnet run -p Api 