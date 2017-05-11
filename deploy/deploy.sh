 
echo "Deploying CharacterGen to NuGet"

ApiKey=$1
Source=$2

echo "Nuget Source is $Source"
echo "Nuget API Key is $ApiKey (should be secure)"

echo "Listing bin directory"
for entry in "./CharacterGen/bin"/*
do
  echo "$entry"
done

echo "Packing CharacterGen"
nuget pack ./CharacterGen/CharacterGen.nuspec -Verbosity detailed

echo "Packing CharacterGen.Domain"
nuget pack ./CharacterGen.Domain/CharacterGen.Domain.nuspec -Verbosity detailed

echo "Pushing CharacterGen"
nuget push ./CharacterGen.*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source

echo "Pushing CharacterGen.Domain"
nuget push ./CharacterGen.Domain.*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source