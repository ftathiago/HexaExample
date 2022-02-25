#!/bin/bash
ls
dotnet sln remove ./**/*.EfInfraData*
find **/*.EfInfraData* -delete
