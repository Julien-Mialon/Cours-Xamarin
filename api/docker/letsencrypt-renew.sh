#!/bin/bash
# crontab : 0 5,17 * * * letsencrypt-renew.sh
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo $(date) >> $DIR/cron.log
echo $(date) >> $DIR/cron_detail.log

docker run --rm \
    --name certbot \
    -v $DIR/letsencrypt/etc/letsencrypt:/etc/letsencrypt \
    -v $DIR/letsencrypt/var/lib/letsencrypt:/var/lib/letsencrypt \
    -v $DIR/letsencrypt/www:/data/letsencrypt \
    -v $DIR/letsencrypt/var/log/letsencrypt:/var/log/letsencrypt \
    certbot/certbot \
    renew &>> $DIR/cron_detail.log

