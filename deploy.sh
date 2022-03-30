#!/bin/bash

# deploy console
echo "Deploying Console..."
rm -Rf ../../Tools/MediaFixer
mkdir ../../Tools/MediaFixer
cp -R ./MediaFixerConsole/bin/Release/netstandard2.1/ ../../Tools/MediaFixer

# deploy app
echo "Deploying UI..."
rm -Rf /Applications/Media\ Fixer.app
cp -Rf ./MediaFixerUI/bin/Release/Media\ Fixer.app /Applications/Media\ Fixer.app

echo "Done!"

