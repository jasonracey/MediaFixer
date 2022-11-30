#!/bin/bash

echo "Removing App..."
rm -Rf /Applications/Media\ Fixer.app

echo "Building..."
rm -Rf ./MediaFixerUI/bin/Release/*
msbuild MediaFixer.sln /property:Configuration=Release

echo "Deploying App..."
cp -Rf ./MediaFixerUI/bin/Release/Media\ Fixer.app /Applications/Media\ Fixer.app

echo "Done. See output for results."