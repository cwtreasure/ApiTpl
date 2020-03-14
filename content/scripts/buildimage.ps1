$tag=Get-Date -Format 'yyyyMMddHHmmss'

docker build -t apitpl:latest -f ./docker/Dockerfile .
docker tag apitpl:latest apitpl:$tag

# 打包到私有镜像仓库
# docker tag apitpl:$tag xxxxxxxx/apitpl:$tag