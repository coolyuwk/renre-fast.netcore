FROM registry.cn-shenzhen.aliyuncs.com/coolyuwk/microsoft:aspnetcore-libgdiplus-3.1 AS base
ARG environment
ENV ASPNETCORE_ENVIRONMENT=$environment
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /app
EXPOSE 9003

FROM registry.cn-hangzhou.aliyuncs.com/blqw/dotnet-core:sdk-3.1-buster AS publish
WORKDIR /src
COPY . .
RUN dotnet publish RenRen.Fast.Api/RenRen.Fast.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RenRen.Fast.Api.dll"]
