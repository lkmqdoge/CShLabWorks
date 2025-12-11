run:
  dotnet run --project ./CShLabWorks.App

build_third_lab:
  mkdir -p build
  mkdir -p build/Plugins

  dotnet build ./CShLabWorks.Third.Plugins.Lib/
  dotnet build --self-contained ./CShLabWorks.Third.Plugins.Host/

  cp ./CShLabWorks.Third.Plugins.Lib/bin/Debug/net9.0/CShLabWorks.Third.Plugins.Lib.dll build/Plugins/
  cp ./CShLabWorks.Third.Plugins.Host/bin/Debug/net9.0/CShLabWorks.Third.Plugins.Host build/
  cp ./CShLabWorks.Third.Plugins.Host/bin/Debug/net9.0/CShLabWorks.Third.Plugins.Host.dll build

