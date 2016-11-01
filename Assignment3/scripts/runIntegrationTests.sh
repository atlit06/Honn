#!/bin/bash

cd ..
dotnet restore
cd IntegrationTests
dotnet build
dotnet test
