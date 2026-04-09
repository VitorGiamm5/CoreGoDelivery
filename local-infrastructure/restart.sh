#!/bin/bash

echo "🔄 Reiniciando ambiente..."

docker compose -f ../deploy/docker-compose.yml down
docker compose -f ../deploy/docker-compose.yml up -d --build

echo "✅ Reiniciado"