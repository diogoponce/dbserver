FROM debian:latest

# Update the repository sources list
RUN apt-get update

# Install and run apache
RUN apt-get install -y apache2 && apt-get clean
RUN a2enmod proxy proxy_http proxy_balancer lbmethod_byrequests rewrite ssl
ENV APACHE_RUN_USER www-data
ENV APACHE_RUN_GROUP www-data
ENV APACHE_LOG_DIR /var/log/apache2

EXPOSE 80
EXPOSE 443
CMD apachectl -D FOREGROUND