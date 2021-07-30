
#!/usr/bin/bash
n=1
while [[ "$n" -le 20 ]]
do
    if [[ "$n"%5 -eq 0 ]] && [[ "$n"%3 -eq 0 ]]
    then
    echo "$n fizzbuzz"
    elif [[ "$n"%3 -eq 0 ]]
    then
    echo "$n fizz"
    elif [[ "$n"%5 -eq 0 ]]
    then
    echo "$n buzz"

    fi
n=$(( $n +1 ))
done