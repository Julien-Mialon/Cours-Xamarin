#!/bin/bash

current_dir=$(pwd)
cd "$( dirname "${BASH_SOURCE[0]}" )"

./docker-build.sh &
docker-compose down &

wait
docker-compose up -d

cd $current_dir
