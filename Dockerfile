FROM php:8.2-apache

# Instalamos las extensiones necesarias para conectar PHP con MySQL
RUN docker-php-ext-install pdo pdo_mysql mysqli