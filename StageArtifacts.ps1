$target = "C:\inetpub\wwwroot\cw_7510\" 

function DeleteIfExistsAndCreateEmptyFolder($dir )
{
    if ( Test-Path $dir ) {    
           Get-ChildItem -Path  $dir -Force -Recurse | Remove-Item -force –
							  recurse
           Remove-Item $dir -Force
    }
    New-Item -ItemType Directory -Force -Path $dir
}
# Clean up target directory
DeleteIfExistsAndCreateEmptyFolder($target )

# msdeploy creates a web artifact with multiple levels of folders. We only need the content 
# of the folder that has Web.config within it 
function GetWebArtifactFolderPath($path)
{
    foreach ($item in Get-ChildItem $path)
    {   
        if (Test-Path $item.FullName -PathType Container)
        {   
            # return the full path for the folder which contains Global.asax
            if (Test-Path ($item.fullname + "\Global.asax"))
            {
                #$item.FullName
                return $item.FullName;
            }
            GetWebArtifactFolderPath $item.FullName
        }
    }
}

$path = GetWebArtifactFolderPath("C:\temp\webApp\")
$path2 = $path + "\*"
Copy-Item $path2 $target -recurse -force
