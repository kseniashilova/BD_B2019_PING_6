#! /bin/sh
sed -E '/^\*/y/*/#/; s/ _(\S.*?\S)_ ?/ <ins>\1<\/ins>/g'
