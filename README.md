This is a simple domain specific language for rolling various sided dice written in F#.

It understands the typical syntax used for TTRGs, and will roll the requested dice and then print the result to stdout. It expects a file with code within it, so you can run a program with "dotnet run [relative path]" while you're in the code directory.

An example program might look like:

10d20+16+3d6+4

where 10d20 refers to rolling a 20 sided die 10 times.


An example output for this program might look like:


10d20 + 16 + 3d6 + 4


2 + 12 + 3 + 16 + 12 + 8 + 16 + 6 + 4 + 18 + 16 + 3 + 1 + 4 + 4   


125


The first line is its understanding of the program you wrote (as whitespace does not matter), and the second line contains all the values rolled, as well as the static numbers- 16 and 4 in this case. The last line is the final sum.
