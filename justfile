run:
  dotnet run --project ./CShLabWorks.App

build_third_lab:
  mkdir -p build
  mkdir -p build/plugins

  dotnet build

  cp ./CShLabWorks.Third.Plugins.Files/bin/Debug/net9.0/CShLabWorks.Third.Plugins.Files.dll ./build/plugins/
  cp ./CShLabWorks.Third.Plugins.Math/bin/Debug/net9.0/CShLabWorks.Third.Plugins.Math.dll ./build/plugins/

gen_files ext:
  mkdir -p trash
  for ((i=0; i<100; i++)); do \
    head /nix/store/iy49v5smw9j488zjgzp4b0grqa6gnz51-seclists-2025.3/share/wordlists/seclists/Passwords/Wikipedia/wikipedia_tr_vowels_no_compounds_top-1000000.txt -n 500 | sort -R | head -n 100 > trash/text$i${{ext}}; \
  done

clean:
  rm -r ./trash
