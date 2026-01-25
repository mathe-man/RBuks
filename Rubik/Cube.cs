/*
    MIT License
    Copyright (c) 2026 mathe man

    This source file is licensed under the MIT License.
    You may use, modify, and distribute it freely.
*/

namespace Rubik;

public enum Face
{
    Up, 
    Down,
    Front,
    Back,
    Left,
    Right
}

public class Cube
{
    protected Dictionary<Face, char[,]> Faces = new()
    {
        [Face.Up] = new[,] {
            { 'W','W','W' },
            { 'W','W','W' },
            { 'W','W','W' }
        },

        [Face.Down] = new[,] {
            { 'Y','Y','Y' },
            { 'Y','Y','Y' },
            { 'Y','Y','Y' }
        },

        [Face.Front] = new[,] {
            { 'G','G','G' },
            { 'G','G','G' },
            { 'G','G','G' }
        },

        [Face.Back] = new[,] {
            { 'B','B','B' },
            { 'B','B','B' },
            { 'B','B','B' }
        },

        [Face.Left] = new[,] {
            { 'O','O','O' },
            { 'O','O','O' },
            { 'O','O','O' }
        },

        [Face.Right] = new[,] {
            { 'R','R','R' },
            { 'R','R','R' },
            { 'R','R','R' }
        }
    };
    
    
    protected void RotateFaceCw(char[,] face)
    {
        char[,] temp = (char[,])face.Clone();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Rotate 90° clockwise
                face[j, 2 - i] = temp[i, j];
            }
        }
    }

    public void MoveUp()
    {
        // Save F top row
        char[] temp = new char[3];
        for (int i = 0; i < 3; i++)
            temp[i] = Faces[Face.Front][0, i];

        // L -> F
        for (int i = 0; i < 3; i++)
            Faces[Face.Front][0, i] = Faces[Face.Left][0, i];

        // B -> L
        for (int i = 0; i < 3; i++)
            Faces[Face.Left][0, i] = Faces[Face.Back][0, i];

        // R -> B
        for (int i = 0; i < 3; i++)
            Faces[Face.Back][0, i] = Faces[Face.Right][0, i];

        // temp -> R
        for (int i = 0; i < 3; i++)
            Faces[Face.Right][0, i] = temp[i];

        // Rotate the U face itself
        RotateFaceCw(Faces[Face.Up]);
    }

    
    public void MoveDown()
    {
        // Save F bottom row
        char[] temp = new char[3];
        for (int i = 0; i < 3; i++)
            temp[i] = Faces[Face.Front][2, i];

        // L -> F
        for (int i = 0; i < 3; i++)
            Faces[Face.Front][2, i] = Faces[Face.Left][2, i];

        // B -> L
        for (int i = 0; i < 3; i++)
            Faces[Face.Left][2, i] = Faces[Face.Back][2, i];

        // R -> B
        for (int i = 0; i < 3; i++)
            Faces[Face.Back][2, i] = Faces[Face.Right][2, i];

        // temp -> R
        for (int i = 0; i < 3; i++)
            Faces[Face.Right][2, i] = temp[i];

        // Rotate the B face itself (existing behavior preserved)
        RotateFaceCw(Faces[Face.Back]);
    }
    
    public void PrintFace(Face face)
    {
        Console.WriteLine($"'{face}' face:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
                Console.Write(Faces[face][i, j] + " ");
            Console.WriteLine();
        }
    }
    
    public void EnterCameraMode()
    {
        // Valid navigation keys
        ConsoleKey[] binds = new[] { ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.RightArrow, ConsoleKey.LeftArrow };

        Face currentFace = Face.Front; // starting face

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Camera mode — appuyez sur Escape ou Q pour quitter.");
            Console.WriteLine();
            // Show the current face
            PrintFace(currentFace);

            // Read key without echoing
            var key = Console.ReadKey(true).Key;

            // Quit conditions
            if (key == ConsoleKey.Escape || key == ConsoleKey.Q)
                break;

            // If it's not a navigation key, ignore
            if (!binds.Contains(key))
                continue;

            // Handle navigation from the current face
            switch (currentFace)
            {
                case Face.Front:
                    if (key == ConsoleKey.UpArrow) currentFace = Face.Up;
                    else if (key == ConsoleKey.DownArrow) currentFace = Face.Down;
                    else if (key == ConsoleKey.RightArrow) currentFace = Face.Right;
                    else if (key == ConsoleKey.LeftArrow) currentFace = Face.Left;
                    break;

                case Face.Back:
                    if (key == ConsoleKey.UpArrow) currentFace = Face.Up;
                    else if (key == ConsoleKey.DownArrow) currentFace = Face.Down;
                    else if (key == ConsoleKey.RightArrow) currentFace = Face.Left;  // mirrored
                    else if (key == ConsoleKey.LeftArrow) currentFace = Face.Right; // mirrored
                    break;

                case Face.Up:
                    if (key == ConsoleKey.UpArrow) currentFace = Face.Back;
                    else if (key == ConsoleKey.DownArrow) currentFace = Face.Front;
                    else if (key == ConsoleKey.RightArrow) currentFace = Face.Right;
                    else if (key == ConsoleKey.LeftArrow) currentFace = Face.Left;
                    break;

                case Face.Down:
                    if (key == ConsoleKey.UpArrow) currentFace = Face.Front;
                    else if (key == ConsoleKey.DownArrow) currentFace = Face.Back;
                    else if (key == ConsoleKey.RightArrow) currentFace = Face.Right;
                    else if (key == ConsoleKey.LeftArrow) currentFace = Face.Left;
                    break;

                case Face.Left:
                    if (key == ConsoleKey.UpArrow) currentFace = Face.Up;
                    else if (key == ConsoleKey.DownArrow) currentFace = Face.Down;
                    else if (key == ConsoleKey.RightArrow) currentFace = Face.Front;
                    else if (key == ConsoleKey.LeftArrow) currentFace = Face.Back;
                    break;

                case Face.Right:
                    if (key == ConsoleKey.UpArrow) currentFace = Face.Up;
                    else if (key == ConsoleKey.DownArrow) currentFace = Face.Down;
                    else if (key == ConsoleKey.RightArrow) currentFace = Face.Back;
                    else if (key == ConsoleKey.LeftArrow) currentFace = Face.Front;
                    break;
            }
        }

        Console.Write("Clear screen? (Y/N): ");
        if (Console.ReadKey().Key == ConsoleKey.Y)
            Console.Clear();
    }
}