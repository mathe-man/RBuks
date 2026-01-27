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
        Console.WriteLine(cube.IsSolved());
        
        cube.MoveDownCw(); cube.MoveDownCcw();
        
        Console.WriteLine(cube.IsSolved());
        
        Console.ReadKey();
        
        cube.EnterCameraMode();

    }
}