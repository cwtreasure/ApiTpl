tag=`date +%Y%m%d%H%M%S`

docker build -t apitpl:latest -f ./docker/Dockerfile .
docker tag apitpl:latest apitpl:$tag

# 打包到我们的镜像仓库
# docker tag apitpl:$tag xxxxxxxx/apitpl:$tag