#!/bin/bash

docker build -t td-api -f api.dockerfile .. &

docker build -t td-sql-migrations -f migrations.dockerfile .. &

wait