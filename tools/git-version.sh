#! /bin/sh

full_version=`./tools/git-version-gen --prefix v .tarball-version`
echo $full_version
