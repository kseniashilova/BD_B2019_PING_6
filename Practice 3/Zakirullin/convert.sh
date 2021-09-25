#! /bin/sh
sed -E '/^\*/y/*/#/; s/(\W)_(\S.*?\S)_(\W)/\1<ins>\2<\/ins>\3/g'
