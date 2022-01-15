# findlog
A window console utility to find text in text files by regular expression and to show the line before or after the matched line

## Usage: 
    findlog [path] [pattern] [showname:(s|h)?] [rowoffset:int?]

## Example
    findlog C:\log\*.txt "Error occurred" s 1

It means find the phrase "Error occurred" all txt files in C:\log. The result will show the file name and 1 line after the matched line

    findlog C:\log\*.txt "Error occurred" h -1

It means find the phrase "Error occurred" all txt files in C:\log. The result will show 1 line before the matched line

    findlog .\log\*.log "[0-9]{6}" s 0

It means find the pattern with 6 numbers in all log files in [Current directory]\log. The result will show the file name and the matched line
