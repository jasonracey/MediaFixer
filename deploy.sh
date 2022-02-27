#!/bin/bash

dotnet build -c Release MediaFixer.sln
rm -rf ../../Tools/MediaFixer
mkdir ../../Tools/MediaFixer
cp -r ./MediaFixerConsole/bin/Release/net6.0/ ../../Tools/MediaFixer

