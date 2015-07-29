#! /bin/sh

full_version=`./tools/git-version-gen --prefix v .tarball-version`
echo $full_version
version=`echo $full_version | sed -e 's/-/\t/' | cut -f 1`

MAJOR=`echo $full_version | cut -f 1 -d .`
MINOR=`echo $full_version | cut -f 2 -d .`
PATCH=`echo $full_version | cut -f 3 -d .`
BUILD=`echo $full_version | cut -f 4 -d . | cut -f 1 -d '-'`
