#generate certificate
openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout id-local.key -out id-local.crt -config id-local.conf -passin pass:MyStrongPassword
#export certificate
openssl pkcs12 -export -out id-local.pfx -inkey id-local.key -in id-local.crt -passout pass:MyStrongPassword
