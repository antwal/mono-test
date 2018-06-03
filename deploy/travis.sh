#!/bin/bash

set -ex

main() {
  local src=$(pwd) \
        tmp=

  case $TRAVIS_OS_NAME in
    linux)
      tmp=$(mktemp -d)
      ;;
    osx)
      tmp=$(mktemp -d -t tmp)
      ;;
  esac

  cp $src/src/bin/Debug/mono_test.exe $tmp/

  cd $tmp
  tar czf $src/mono-test-$TRAVIS_TAG-mono.tar.gz *
  cd $src

  if [ "$TRAVIS_OS_NAME" == "linux" ]; then rm -rf $tmp; fi
  if [ "$TRAVIS_OS_NAME" == "osx" ]; then rm -Rf $tmp; fi
}

main

