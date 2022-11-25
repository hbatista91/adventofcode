$year = Read-Host -Prompt "Enter Year"
$days = 1..25

foreach ($item in $days)
{
    $day = $item.ToString().PadLeft(2,'0')

    New-Item -Path "$PSScriptRoot\..\src\AdventOfCode$year\data\day$day-sample.data" -ItemType File
    New-Item -Path "$PSScriptRoot\..\src\AdventOfCode$year\data\day$day.data" -ItemType File
    Copy-Item -Path "$PSScriptRoot\Dayx.cs" -Destination "$PSScriptRoot\..\src\AdventOfCode$year\Day$day.cs"
    ((Get-Content -Path "$PSScriptRoot\..\src\AdventOfCode$year\Day$day.cs" -Raw) -Replace "REPLACE_NAME_CLASS","Day$day" -Replace "REPLACE_DAY",$item -Replace "REPLACE_NAMESPACE","AdventOfCode$year") | Set-Content -Path "$PSScriptRoot\..\src\AdventOfCode$year\Day$day.cs"
}