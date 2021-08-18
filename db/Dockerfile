FROM mysql:5.7.22
EXPOSE 3306
COPY ./Web_Api/db/migrations/ /home/database/
COPY ./Web_Api/db/dataset/ /home/database/
COPY ./Web_Api/db/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh