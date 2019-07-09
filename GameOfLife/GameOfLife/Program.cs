using System;
using System.IO;
using System.Linq;

namespace GameOfLife
{
    class Program
    {
        static int size = 10;
        static char[,] grid;
        static char[,] grid_new;
        static string filepath = "grid.txt";
        static string timingfilepath = "numberOfTicksPerSecond.txt";
        static int tickSpeed;
        static void Main(string[] args)
        {
            
            Console.CursorVisible = false;
            initArrays(filepath);
            Console.Title = "Conway's Game Of Life";
            readGridFromFile(filepath, grid);
            tickSpeed = setTickSpeed(timingfilepath);
            Console.ForegroundColor = ConsoleColor.Red;
            drawBoundary();
            Console.ForegroundColor = ConsoleColor.Blue;
            while (true)
            {
                Console.CursorVisible = false;
                drawGrid(grid);
                updateGrid();
                delay(tickSpeed);
            }
        }
        
        static int setTickSpeed(string filepath)
        {
            int ticks = int.Parse(File.ReadAllText(filepath));
            return (1000 / ticks);
        }

        static void drawBoundary()
        {
            for (int i = 0; i <= size + 2; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
            }
            for (int i = 0; i <= size + 1; i++)
            {
                Console.SetCursorPosition(size + 2, i);
                Console.Write("#");
            }
            for (int i = size + 2; i >= 0; i--)
            {
                Console.SetCursorPosition(i, size + 1);
                Console.Write("#");
            }
            for (int i = size; i >= 0; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
            }

            //credits

            Console.SetCursorPosition(size + 10, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("CONWAY'S GAME OF LIFE");
            Console.SetCursorPosition(size + 8, 2);
            Console.WriteLine("Programmed by Rohan in C#");
            Console.SetCursorPosition(size + 8, 7);

            Console.WriteLine("Info:");
            Console.SetCursorPosition(size + 8, 9);
            Console.WriteLine("The current grid size is " + size+" by " + size);
            Console.SetCursorPosition(size + 8, 11);
            Console.WriteLine("Number of simulations per second = " + (1000/tickSpeed));
        }

        static void initArrays(string filepath)
        {
            size = File.ReadLines(filepath).Count();
            grid = new char[size, size];
            grid_new = new char[size, size];
        }

        static void updateGrid()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    grid_new[x, y] = applyRules(grid, x, y);
                }
            }
            //copy array
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    grid[x, y] = grid_new[x, y]
;
                }
            }
        }

        static void delay(int millis)
        {
            System.Threading.Thread.Sleep(millis);
        }

        static void drawGrid(char[,] grid)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.SetCursorPosition(i + 1, j + 1);
                    if (grid[i, j] == '0')
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write('R');
                    }
                }
            }
        }

        static void readGridFromFile(string path, char[,] grid)
        {
            size = File.ReadLines(path).Count();

            int y = 0;

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    grid[i, y] = line.ElementAt(i);
                    grid_new[i, y] = line.ElementAt(i);
                }
                y = y + 1;
            }
        }

        static char applyRules(char[,] grid, int x, int y)
        {
            char new_state = '0';
            int liveNeighbours = numberOfLiveNeighbours(grid, x, y);
            if (grid[x, y] == '0') // rules for dead cells
            {
                if (liveNeighbours == 3) //reproduce
                {
                    new_state = '1';
                    return new_state;
                }
            }
            else //rules for alive cells
            {
                if (liveNeighbours < 2) // underpopulation
                {
                    new_state = '0';
                }
                if (liveNeighbours == 2 || liveNeighbours == 3)
                {
                    new_state = '1';
                    //cell is alive and lives on
                }
                else // overpopulation
                {
                    new_state = '0';
                }
            }
            return new_state;
        }

        static int numberOfLiveNeighbours(char[,] grid, int x, int y)
        {
            int liveNeighboursCount = 0;
            if (isAliveNorth(grid, x, y))
            {
                liveNeighboursCount += 1;
            }
            if (isAliveNorthEast(grid, x, y))
            {
                liveNeighboursCount += 1;
            }
            if (isAliveEast(grid, x, y))
            {
                liveNeighboursCount += 1;
            }
            if (isAliveSouthEast(grid, x, y))
            {
                liveNeighboursCount += 1;
            }
            if (isAliveSouth(grid, x, y))
            {
                liveNeighboursCount += 1;
            }
            if (isAliveSouthWest(grid, x, y))
            {
                liveNeighboursCount += 1;
            }
            if (isAliveWest(grid, x, y))
            {
                liveNeighboursCount += 1;
            }
            if (isAliveNorthWest(grid, x, y))
            {
                liveNeighboursCount += 1;
            }
            return liveNeighboursCount;
        }

        static bool isAliveNorth(char[,] grid, int x, int y)
        {
            if (y - 1 < 0)
            {
                y = size - 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                y = y - 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static bool isAliveNorthEast(char[,] grid, int x, int y)
        {
            if (y - 1 < 0 || x + 1 >= size)
            {
                if (y - 1 < 0 && !(x + 1 >= size))
                {
                    y = size - 1;
                    x = x + 1;

                }
                if (x + 1 >= size && !(y - 1 < 0))
                {
                    x = 0;
                    y = y - 1;
                }
                if (y - 1 < 0 && x + 1 >= size)
                {
                    y = size - 1;
                    x = 0;
                }
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                y = y - 1;
                x = x + 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static bool isAliveEast(char[,] grid, int x, int y)
        {
            if (x + 1 >= size)
            {
                x = 0;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                x = x + 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static bool isAliveSouthEast(char[,] grid, int x, int y)
        {
            if (y + 1 >= size || x + 1 >= size)
            {
                if (y + 1 >= size && !(x + 1 >= size))
                {
                    y = 0;
                    x = x + 1;
                }
                if (x + 1 >= size && !(y + 1 >= size))
                {
                    x = 0;
                    y = y + 1;
                }
                if (x + 1 >= size && y + 1 >= size)
                {
                    y = 0;
                    x = 0;
                }
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                y = y + 1;
                x = x + 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static bool isAliveSouth(char[,] grid, int x, int y)
        {
            if (y + 1 >= size)
            {
                y = 0;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                y = y + 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static bool isAliveSouthWest(char[,] grid, int x, int y)
        {
            if (y + 1 >= size || x - 1 < 0)
            {
                if (y + 1 >= size && !(x - 1 < 0))
                {
                    y = 0;
                    x = x - 1;
                }
                if (x - 1 < 0 && !(y + 1 >= size))
                {
                    x = size - 1;
                    y = y + 1;
                }
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                y = y + 1;
                x = x - 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static bool isAliveWest(char[,] grid, int x, int y)
        {
            if (x - 1 < 0)
            {
                x = size - 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                x = x - 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static bool isAliveNorthWest(char[,] grid, int x, int y)
        {
            if (y - 1 < 0 || x - 1 < 0)
            {
                if (y - 1 < 0 && !(x - 1 < 0))
                {
                    x = x - 1;
                    y = size - 1;
                }
                if (x - 1 < 0 && !(y - 1 < 0))
                {
                    x = size - 1;
                    y = y - 1;
                }
                if (x - 1 < 0 && y - 1 < 0)
                {
                    y = size - 1;
                    x = size - 1;
                }
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                y = y - 1;
                x = x - 1;
                if (grid[x, y] == '1')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
