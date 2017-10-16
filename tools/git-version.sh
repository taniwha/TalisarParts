#! /bin/sh

assembly=Assembly/AssemblyInfo

full_version=`../tools/git-version-gen --prefix v .tarball-version`
version=`echo $full_version | sed -e 's/-/\t/' | cut -f 1 | sed -e 's/UNKNOWN/0.0.0/'` 

sed -e "s/@MODNAME@/$MODNAME/" -e"s/@DLLNAME@/$DLLNAME/" -e "s/@FULL_VERSION@/$full_version/" -e "s/@VERSION@/$version/" $assembly.in > $assembly.cs-

cmp -s $assembly.cs $assembly.cs- || mv $assembly.cs- $assembly.cs

rm -f $assembly.cs-
