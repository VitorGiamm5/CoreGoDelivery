#!/bin/bash

echo "🔥 Limpando tudo..."

docker compose -f ../deploy/docker-compose.yml down -v --rmi all --remove-orphans

echo "✅ Ambiente limpo"