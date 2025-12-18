run:
  dotnet run --project ./CShLabWorks.App

build_third_lab:
  mkdir -p build
  mkdir -p build/plugins

  dotnet build

  cp ./CShLabWorks.Third.Plugins.Files/bin/Debug/net9.0/CShLabWorks.Third.Plugins.Files.dll ./build/plugins/
  cp ./CShLabWorks.Third.Plugins.Math/bin/Debug/net9.0/CShLabWorks.Third.Plugins.Math.dll ./build/plugins/

gen_files from ext count:
  mkdir -p trash
  for ((i=0; i<{{count}}; i++)); do \
    cat {{from}} | sort -R | head -n 100 > trash/text$i.{{ext}}; \
  done

clean:
  rm -r ./trash
