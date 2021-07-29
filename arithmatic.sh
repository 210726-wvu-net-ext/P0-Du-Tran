#!/usr/bash

#activity1

read -p "Enter your number: " number
if [ `expr "$number" / 2 ` -eq 0 ]
then    
    echo 'number is an even'
else
    echo 'number is an odd'
fi
read -p "Please enter your marks: " marks

#activity2

if [[ "$mark" -lt 40 ]]
then
    echo "Your grade is F"
elif [[ "$mark" -ge 41 &&  "$mark" -le 50 ]]
then    
    echo "Your grade is D"
elif [[ "$mark" -ge 51 &&  "$mark" -le 60 ]]
then
    echo "Your grade is C"
elif [[ "$mark" -ge 61 &&  "$mark" -le 70 ]]
then
    echo "Your grade is B"
else
    echo "Your grade is A"
fi
