#!/bin/bash

# deploy app
echo "Deploying UI..."
rm -Rf /Applications/Media\ Fixer.app
cp -Rf ./MediaFixerUI/bin/Release/Media\ Fixer.app /Applications/Media\ Fixer.app

echo "Done!"

