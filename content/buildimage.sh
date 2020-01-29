env=""

if [ -n "$1" ]; then
    env=$1
else
    echo "Please enter the env param!!"
    exit 1
fi

d=`date +%Y%m%d%H%M%S`
tag=${d}

echo apitpl_${env}:${tag}
docker build -t apitpl_${env}:${tag} -f docker/Dockerfile.${env} .

# docker stop apitpl_${env} 
# docker rm apitpl_${env} 
# docker run -d -p 9000:80 --name apitpl_${env} 