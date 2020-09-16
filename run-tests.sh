#!/bin/bash
for dir in ./*.Tests/;do
    dotnet test "$dir"         
done