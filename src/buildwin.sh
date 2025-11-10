#!/bin/sh

dotnet publish \
   -f net8.0 \
   -r win-x86 \
   -c Release \
   -o dist \
   --sc \
   ProcSpector

