chcp 1251
set CURDATE=%DATE%
set h=%TIME:~0,2%
set m=%TIME:~3,2%
set s=%TIME:~6,2%
set ms=%TIME:~9,2%
set CURTIME=%h%_%m%_%s%
set PGUSER=postgres
set PGPASSWORD=123
C:/PostgreSQL/9.4/bin\pg_dump.exe --host localhost --port 5432 --username "postgres" --no-password  --format custom --compress 5 --verbose --file ".\BACKUP\%CURDATE%_%CURTIME%_MIG_BACKUP.sql" --schema "cmodb" "CMO 2017"
set CURDATE=
set CURTIME=
set PGPASSWORD=
set PGUSER=
pause