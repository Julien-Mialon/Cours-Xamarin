#!/bin/bash

dbhost=$WAIT_HOST
dbport=$WAIT_PORT

echo "wait for host:" $dbhost "and port" $dbport
while ! ./wait-for-it.sh $dbhost:$dbport; do 
	sleep 2;
done

$@
