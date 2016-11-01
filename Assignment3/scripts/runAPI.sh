#!/bin/bash

cd ..
dotnet restore
cd API
dotnet build
dotnet run
