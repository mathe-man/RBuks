/*
    MIT License
    Copyright (c) 2026 mathe man

    This source file is licensed under the MIT License.
    You may use, modify, and distribute it freely.
*/

using Rubik;

namespace Runner;

class Program
{
    static void Main(string[] args)
    {
        Cube cube = new Cube();
        Console.WriteLine("Initial Up face:");      cube.PrintFace(Face.Up);    // Print the Up face
        Console.WriteLine("Initial Front face:");   cube.PrintFace(Face.Front);    // Print the Front face
        
        cube.MoveUp();
        Console.WriteLine("Clockwise rotation applied to the Up face.\n\n");
        
        Console.WriteLine("Final Up face:");        cube.PrintFace(Face.Up);
        Console.WriteLine("Final Front face:");     cube.PrintFace(Face.Front);

    }
}