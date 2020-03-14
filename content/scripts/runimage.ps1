param(
[int]$env=$(throw "Parameter missing: -name env"),
[int]$port=$(throw "Parameter missing: -name port") 
)

$e = ""

# 开发
if ($env -eq 0)
{
    $e = "Development"
}

# 测试
if ($env -eq 1)
{
    $e = "Test"
}

# 生产
if ($env -eq 2)
{
    $e = "Production"
}

docker stop apitpl-$e-$port
docker rm apitpl-$e-$port

docker run -d -p $port:80 `
    -e ASPNETCORE_ENVIRONMENT=$e `
    -e TZ=Asia/Shanghai `
    -v D:\appdockerlogs\apitpl-${e}-${port}:/app/logs `
    --name apitpl-$e-$port `
    --restart=always  `
    apitpl
