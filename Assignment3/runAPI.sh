#!/bin/bash

dotnet restore
cd API
dotnet build
dotnet run
