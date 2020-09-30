using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Tower_of_Hanoi
{
    /*                                      TOWER OF HANOI
     *                                      
     * 
     *          Welcome, this is a fun little console application that visualizes the 
     *          Tower of Hanoi puzzle.
     * 
     *          It is solved by the recursive Toh algorithm.
     * 
     *          You can select a mode by changing the variable "mode" as stated below.
     * 
     *          You can also change the number of disks by changing the variable "num_discs".
     *          
     *          But remember the minimum steps needed for solving this puzzle are 2^n-1, 
     *          so I ADVISE YOU TO NOT USE ANY NUMBERS HIGHER THAN 10.
     *          
     *          (I mean you can still do it...but I'm not responsible if your computer gets fried...)
     *          
     *          Well then, have fun!!!
     *          
     */
    class Program
    {
        /*                                  MODE SELECTION:
         *                                  
         * 
         *      mode = 0    :   all steps are shown instantly
         * 
         *      mode = 1    :   manual mode = you can click through each step manually
         * 
         *      mode = 2    :   automatic mode = forwards automatically, you can set the 
         *                                       forwarding speed by changing the variable 
         *                                       "delay_in_s" (looks like a nice little animation)
         *        
         */

        const int mode = 2;               // change me to set the mode

        const int num_discs = 10;         // change me if you want more / less discs

        const double delay_in_s = 0.5;    // change me to set the forwarding speed in "automation mode" (mode = 2)


        const char chr_empty = (char)124; // ASCII for the pillar of the tower
        const char chr_disc = (char)4;    // ASCII for the disks of the tower

        const int spacing = 10;           // change me if you want more / less spacing between your towers 

        static string[][] stacks = new string[3][];
        static int counter = 0;

        static void Main(string[] args)
        {
            // search tool for cool ASCII
            //for (int i = 0; i < 200; i++)
            //{
            //    Console.WriteLine($"{i}. {(char)i}");
            //}

            stacks[0] = CreateTower();
            stacks[1] = CreateEmptyTower();
            stacks[2] = CreateEmptyTower();

            // prints the starting state of the towers
            PrintTowers();

            // here is where the magic happens
            Toh(num_discs, 0, 1, 2);
        }

        // creates a string representation of a full stacked tower
        static string[] CreateTower()
        {
            string[] temp = new string[num_discs];

            int total_chars = 2 * num_discs + 1;
            int num_spaces = 0;
            int num_chars = 0;

            for (int i = 0; i < num_discs; i++)
            {
                num_chars = 2 * i + 3;
                num_spaces = (total_chars - num_chars) / 2;

                temp[i] += string.Concat(Enumerable.Repeat(" ", num_spaces));
                temp[i] += string.Concat(Enumerable.Repeat(chr_disc, num_chars));
                temp[i] += string.Concat(Enumerable.Repeat(" ", num_spaces));
            }

            return temp;
        }

        // creates a string representation of an empty tower
        static string[] CreateEmptyTower()
        {
            string[] temp = new string[num_discs];

            int total_chars = 2 * num_discs + 1;
            int num_spaces = total_chars / 2;

            for (int i = 0; i < num_discs; i++)
            {
                temp[i] += string.Concat(Enumerable.Repeat(" ", num_spaces));
                temp[i] += string.Concat(Enumerable.Repeat(chr_empty, 1));
                temp[i] += string.Concat(Enumerable.Repeat(" ", num_spaces));
            }

            return temp;
        }

        // prints all towers in their current state
        static void PrintTowers()
        {
            // prints the number of the current step
            Console.WriteLine($"Step {counter}:\n");

            if (counter < Math.Pow(2, num_discs) - 1)
            {
                counter++;
            }

            for (int i = 0; i < stacks[0].Length; i++)
            {
                for (int j = 0; j < stacks.Length; j++)
                {
                    Console.Write(stacks[j][i]);
                    Console.Write(string.Concat(Enumerable.Repeat(" ", spacing))); 
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        // moves disk from tower in pos_1 to tower in pos_2
        static void MoveDisk(int pos_1, int pos_2)
        {
            string temp = stacks[pos_2][FindTopDisk(pos_2) - 1];
            stacks[pos_2][FindTopDisk(pos_2) - 1] = stacks[pos_1][FindTopDisk(pos_1)];
            stacks[pos_1][FindTopDisk(pos_1)] = temp;
        }

        // returns the position of the highest disk on a stack
        static int FindTopDisk(int pos)
        {
            int mid_char = stacks[pos][0].Length / 2;

            for (int i = 0; i < stacks[pos].Length; i++)
            {
                if (stacks[pos][i][mid_char] == chr_disc)
                {
                    return i;
                }
            }

            // if there is no stack an imaginary disk position under the tower is returned (needed for disk placement)
            return stacks[pos].Length;
        }

        // the recursive Toh algorithm (the brain for solving this puzzle)
        static void Toh(int n, int beg, int aux, int end)
        {
            if (n >= 1)
            {
                Toh(n - 1, beg, end, aux); // recursion with different order of arguments

                // Main actions performed:
                ModeAction();
                MoveDisk(beg, end);
                PrintTowers();

                Toh(n - 1, aux, beg, end); // recursion with different order of arguments
            }
        }

        // performs actions depending on the current mode
        static void ModeAction()
        {
            if (mode == 1)
            {
                Console.ReadKey();
                Console.Clear();
            }
            else if (mode == 2)
            {
                Thread.Sleep((int)(delay_in_s * 1000));
                Console.Clear();
            }
        }
    }
}
