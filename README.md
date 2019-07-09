# GameOfLife
Conway's game of life implemented in a c# console application


Create the following text files next to the exe:
1) grid.txt
2) numberOfTicksPerSecond.txt

In grid.txt make a pattern. Read below for intructions
In numberOfTicksPerSecond.txt enter a number and save the file. Read below for details.


The file "grid.txt" stores the starting pattern.
1)  0 means the cell is dead.
2)  1 means the cell is alive.


Please ensure that the pattern in grid.txt is a square.
This means that the number of columns should be equal to number of rows. 
Thus

This will work:

0000
0000
0000
0000

beacuse this is square

This will NOT work:

0000000
0000000
0000000

beacuse the number of columns is not equal to number of rows.

grid.txt comes with a default pattern but try making your own. An example pattern with a glider and a blinker is at the bottom of this text file.
----------------------------------------------------------


To set speed of simulation open "numberOfTicksPerSecond.txt"

The number signifies the number of simulations per second.
Increase it to speed up simulation

Recommended value is 5

-----------------------------------------------------------

Here is an example grid for you to paste in grid.txt




0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000100000000000000
0000000000100000000000000
0000000000100000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000100000000000000000000
0000010000000000000000000
0001110000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000
0000000000000000000000000


It contains a glider and a blinker.
