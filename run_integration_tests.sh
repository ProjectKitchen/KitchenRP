#!/usr/bin/env bash
cd docker
docker-compose -f integration-tests.docker-compose.yml build
docker-compose -f integration-tests.docker-compose.yml run tester
# docker-compose -f integration-tests.docker-compose.yml down -v --remove-orphans

