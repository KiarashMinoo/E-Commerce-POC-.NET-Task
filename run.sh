#!/bin/bash

docker-compose -f docker-compose.yml -f docker-compose.stage.yml up -d --remove-orphans
