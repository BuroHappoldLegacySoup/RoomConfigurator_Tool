using namespace System.IO
using namespace System.Collections.Generic

$RoomConfigurator_Tool = Read-Host "`nPlease enter the software name."

try 
{
    # Replace occurrences of "RoomConfigurator_Tool" in all files
    Write-Host "`nReplacing occurrences of 'RoomConfigurator_Tool' with "$RoomConfigurator_Tool" in all files.`n"

    $replace_successful = $true
    Get-ChildItem -File -Recurse | ForEach-Object {
        try {
            If ((Get-Content $_.Extension -eq ".ps1") -or (Get-Content $_.Extension -eq ".bat")) 
                {
                    continue
                }
        } catch {}

        try
        {
            (Get-Content $_.FullName) -replace 'RoomConfigurator_Tool', $RoomConfigurator_Tool | Set-Content $_.FullName
        } 
        catch 
        {
            Write-Host "`nAn error occurred when replacing file contents of " $_.FullName ":"
            Write-Host $_
            $replace_successful = $false
        }
     }

    If($replace_successful)
    {
        Write-Host "Successfully replaced files contents.`n"
    }
    else
    {
        Write-Host "`nERROR: Could not replace some file contents.`n"
    }
    

    Write-Host "`nRenaming files and directories using "$RoomConfigurator_Tool":`n"


    # Rename files and folders
    $stack = [Stack[string]]::new()
    $allPaths = [List[string]]::new()


    # Get all files and directories containing "RoomConfigurator_Tool" recursively
    Get-ChildItem -Recurse -Directory | ForEach-Object {
        $dirpath = $_.FullName
        $dirname = Split-Path  $dirpath -Leaf
        if ($dirname.Contains("RoomConfigurator_Tool"))
        {
            $stack.Push($dirpath)
            $allPaths.Add($dirpath)
        }

        # Write-Host $_.FullName

        foreach ($file in [Directory]::EnumerateFiles($dirpath)) 
        {
            $filename = [Path]::GetFileName($file)
            if ($filename.Contains('RoomConfigurator_Tool') -and -not $allPaths.Contains($file))
            {
                $stack.Push($file)
                $allPaths.Add($file)
            }
        }
    }

    # Add root files
    Get-ChildItem -File | ForEach-Object {
        if ($_.FullName.Contains("RoomConfigurator_Tool")) {
            $stack.Push($_.FullName)
        }
    }

    # Rename files and folders
    while ($stack.Count) {
        $poppedFullName = $stack.Pop()
        $pathExists = (-not ([string]::IsNullOrEmpty($poppedFullName))) -and (Test-Path -Path $poppedFullName)

        $filename = [Path]::GetFileName($poppedFullName)

        if($filename.Contains('RoomConfigurator_Tool') -and $pathExists)
        {
            $newName = $filename.Replace('RoomConfigurator_Tool', $RoomConfigurator_Tool)

            Write-Host "Renaming: " $poppedFullName " to: " $newName

            Rename-Item -LiteralPath $poppedFullName -NewName $newName #-WhatIf
        }
    }

    Write-Host "`nAll files and folders renamed successfully."
}
catch 
{
    Write-Host "`nERROR:"
    Write-Host $_
}

