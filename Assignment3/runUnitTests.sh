#!/bin/bash

dotnet restore
cd UnitTests
dotnet test
