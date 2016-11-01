#!/bin/bash

cd ..
dotnet restore
cd UnitTests
dotnet build
dotnet test
