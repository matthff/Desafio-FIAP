#!/bin/bash

# Inicia o SQL Server em background
/opt/mssql/bin/sqlservr &

# Aguarda o SQL Server iniciar completamente
echo "Aguardando SQL Server iniciar..."
sleep 30s

# Executa o script de inicialização
echo "Executando script de inicialização..."
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -C -i /dump.sql

if [ $? -eq 0 ]; then
  echo "Script executado com sucesso!"
else
  echo "Erro ao executar o script!"
fi

# Mantém o container rodando
wait
