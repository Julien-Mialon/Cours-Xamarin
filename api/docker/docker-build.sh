#!/bin/bash

docker build -t timetracker-buildcontainer -f build.dockerfile ..

docker build -t timetracker-migrations -f migrations.dockerfile .. 

docker build -t timetracker-api -f api.dockerfile .. 