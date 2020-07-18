param (
    $buildName ="PawnmorpherInsects",
    $buildDir = "Build" ,
    $VSVersion = "2019",
    $SolName = "PawnmorphInsects",
    $OutVersion = ""
)


If(!(test-path $buildDir))
{
      New-Item -ItemType Directory -Force -Path $buildDir
}

If(!(test-path "$buildDir/Tmp"))
{
    New-Item -ItemType Directory -Force -Path "$buildDir\Tmp"

}else { 
    Remove-Item -Recurse -Force "$buildDir\Tmp"
    New-Item -ItemType Directory -Force -Path "$buildDir\Tmp" 
    #clean contents of temp directory 
}

If(Test-Path "$buildDir/$buildName$OutVersion.zip")
{
    Remove-Item -Path "$buildDir/$buildName$OutVersion.zip" -Force
}

dotnet restore "Source/PawnmorphInsects/$SolName.sln"

if(!$?)
{
    Write-Error "could not restore project"
    exit 1 
}

."C:\Program Files (x86)\Microsoft Visual Studio\$VSVersion\Community\MSBuild\Current\Bin\MSBuild.exe"  "Source/PawnmorphInsects/$SolName.sln" /t:restore


if(!$?)
{
    Write-Error "could not restore project"
    exit 1 
}


."C:\Program Files (x86)\Microsoft Visual Studio\$VSVersion\Community\MSBuild\Current\Bin\MSBuild.exe"  "Source/PawnmorphInsects/$SolName.sln" /t:Rebuild /p:Configuration=Debug /p:Platform="any cpu"

if(!$?)
{
    Write-Error "could not build project"
    exit 1 
}

Copy-Item -Path Defs, About, Assemblies, Languages, Patches, Source, Textures -Destination "$buildDir/Tmp" -Recurse


#check for .vs folders and get rid of them 
if(Test-Path "$buildDir/Tmp/.vs")
{
    Remove-Item "$buildDir/Tmp/.vs" -Recurse -Force 
}

#get rid of harmony dll if it was copied over 
if(Test-Path "$buildDir/Tmp/Assemblies/0Harmony.dll")
{
    Remove-Item "$buildDir/Tmp/Assemblies/0Harmony.dll"
}

#get rid of pdbs if they were copied over 
if(Test-Path "$buildDir/Tmp/Assemblies/$SolName.pdb")
{
    Remove-Item "$buildDir/Tmp/Assemblies/$SolName.pdb"
}


if(Test-Path "$buildDir/Tmp/Source/PawnmorphInsects/.vs")
{
    Remove-Item "$buildDir/Tmp/Source/PawnmorphInsects/.vs" -Recurse -Force
}
#get rid of nuget packages

if(Test-Path "$buildDir/Tmp/Source/PawnmorphInsects/packages")
{
    Remove-Item "$buildDir/Tmp/Source/PawnmorphInsects/packages" -Force -Recurse
}

Compress-Archive -Path  "$buildDir/Tmp/*" -CompressionLevel Optimal -Force -DestinationPath "$buildDir/$buildName-$OutVersion $(get-date -f MM-dd).zip"

if(!$?)
{
    Write-Error "unable to create archive"
    exit 1
}

Remove-Item -Path "$buildDir/Tmp" -Recurse -Force 
Write-Output "file $buildDir/$buildName-$OutVersion $(get-date -f MM-dd).zip created successfully" 