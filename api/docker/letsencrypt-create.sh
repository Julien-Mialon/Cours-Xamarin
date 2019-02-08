#!/bin/bash

function create_certificate {
    docker run -it --rm \
        -v $(pwd)/letsencrypt/etc/letsencrypt:/etc/letsencrypt \
        -v $(pwd)/letsencrypt/var/lib/letsencrypt:/var/lib/letsencrypt \
        -v $(pwd)/letsencrypt/www:/data/letsencrypt \
        -v $(pwd)/letsencrypt/var/log/letsencrypt:/var/log/letsencrypt \
        certbot/certbot \
        certonly --webroot \
        --email mialon.julien@gmail.com --agree-tos --no-eff-email \
        --webroot-path=/data/letsencrypt \
        -d $1 
}

function create_certificate_dryrun {
    docker run -it --rm \
        -v $(pwd)/letsencrypt/etc/letsencrypt:/etc/letsencrypt \
        -v $(pwd)/letsencrypt/var/lib/letsencrypt:/var/lib/letsencrypt \
        -v $(pwd)/letsencrypt/www:/data/letsencrypt \
        -v $(pwd)/letsencrypt/var/log/letsencrypt:/var/log/letsencrypt \
        certbot/certbot \
        certonly --webroot \
        --email mialon.julien@gmail.com --agree-tos --no-eff-email \
        --webroot-path=/data/letsencrypt \
        --staging \
        -d $1 
}

#create_certificate td-api.julienmialon.com
#create_certificate_dryrun td-api.julienmialon.com
