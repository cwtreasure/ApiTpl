env=""
ver=""
port="8010"

if [ -n "$1" ]; then
    env=$1
else
    echo "Please enter the env param!!"
    exit 1
fi

if [ -n "$2" ]; then
    ver=$2
else
    echo "Please enter the version param!!"
    exit 1
fi

if [ -n "$3" ]; then
    port=$3
else
    echo "Please enter the version param!!"
    exit 1
fi

e=""

if [ env = "1" ]; then
    e="Test"
elif [ env = "2" ]; then
    e="Production"
else    
    e="Development" 
fi

docker run -d -p $port:80 \
    -e ASPNETCORE_ENVIRONMENT=$e \
    -e TZ=Asia/Shanghai \
    -v /data/projects/apitpl-$e-$port/logs:/app/logs \
    --name apitpl-$e-$port \
    --restart=always  \
    apitpl