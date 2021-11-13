# Compile and install todo
sudo dotnet build --nologo --verbosity minimal --configuration Release
cp -r ./bin/Release/net5.0 ~/.local/bin/todo
PATH=~/.local/bin/todo:$PATH
