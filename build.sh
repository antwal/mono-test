#!/bin/bash

nuget restore src/mono-test.sln

msbuild /p:Configuration=Debug src/mono-test.sln
