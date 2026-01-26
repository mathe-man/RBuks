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
    public Dictionary<Face, char[,]> Faces { get; protected set; } = new()
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

    protected void RotateFaceCcw(char[,] face)
    {
        char[,] temp = (char[,])face.Clone();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Rotate 90° counter-clockwise
                face[2 - j, i] = temp[i, j];
            }
        }
    }

    public void MoveUpCw()
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
    // Inverse (counter-clockwise)
    public void MoveUpCcw()
    {
        MoveUpCw();
        MoveUpCw();
        MoveUpCw();
    }
    
    public void MoveDownCw()
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

        // Rotate the Down face itself
        RotateFaceCw(Faces[Face.Down]);
    }
    // Inverse (counter-clockwise)
    public void MoveDownCcw()
    {
        MoveDownCw();
        MoveDownCw();
        MoveDownCw();
    }
    
    public void MoveLeftCw()
    {
        // Save U left column
        char[] temp = new char[3];
        for (int i = 0; i < 3; i++)
            temp[i] = Faces[Face.Up][i, 0];

        // U <- Back (right column, reversed)
        for (int i = 0; i < 3; i++)
            Faces[Face.Up][i, 0] = Faces[Face.Back][2 - i, 2];

        // Back <- Down (right column, reversed)
        for (int i = 0; i < 3; i++)
            Faces[Face.Back][2 - i, 2] = Faces[Face.Down][i, 0];

        // Down <- Front (left column)
        for (int i = 0; i < 3; i++)
            Faces[Face.Down][i, 0] = Faces[Face.Front][i, 0];

        // Front <- saved U (temp)
        for (int i = 0; i < 3; i++)
            Faces[Face.Front][i, 0] = temp[i];

        // Rotate the Left face itself
        RotateFaceCw(Faces[Face.Left]);
    }
    // Inverse (counter-clockwise)
    public void MoveLeftCcw()
    {
        MoveLeftCw();
        MoveLeftCw();
        MoveLeftCw();
    }
    
    public void MoveRightCw()
    {
        // Save U right column
        char[] temp = new char[3];
        for (int i = 0; i < 3; i++)
            temp[i] = Faces[Face.Up][i, 2];

        // U <- Front (right column)
        for (int i = 0; i < 3; i++)
            Faces[Face.Up][i, 2] = Faces[Face.Front][i, 2];

        // Front <- Down (right column)
        for (int i = 0; i < 3; i++)
            Faces[Face.Front][i, 2] = Faces[Face.Down][i, 2];

        // Down <- Back (left column, reversed)
        for (int i = 0; i < 3; i++)
            Faces[Face.Down][i, 2] = Faces[Face.Back][2 - i, 0];

        // Back <- saved U (reversed into left column)
        for (int i = 0; i < 3; i++)
            Faces[Face.Back][2 - i, 0] = temp[i];

        // Rotate the Right face itself
        RotateFaceCw(Faces[Face.Right]);
    }
    // Inverse (counter-clockwise)
    public void MoveRightCcw()
    {
        MoveRightCw();
        MoveRightCw();
        MoveRightCw();
    }
    
    public void MoveFrontCw()
    {
        // Save U bottom row
        char[] up = new char[3];
        for (int i = 0; i < 3; i++)
            up[i] = Faces[Face.Up][2, i];

        // Save Right left column
        char[] right = new char[3];
        for (int i = 0; i < 3; i++)
            right[i] = Faces[Face.Right][i, 0];

        // Save D top row
        char[] down = new char[3];
        for (int i = 0; i < 3; i++)
            down[i] = Faces[Face.Down][0, i];

        // Save Left right column
        char[] left = new char[3];
        for (int i = 0; i < 3; i++)
            left[i] = Faces[Face.Left][i, 2];

        // U bottom -> Right left
        for (int i = 0; i < 3; i++)
            Faces[Face.Right][i, 0] = up[i];

        // Right left -> D top (reversed)
        for (int i = 0; i < 3; i++)
            Faces[Face.Down][0, i] = right[2 - i];

        // D top -> Left right
        for (int i = 0; i < 3; i++)
            Faces[Face.Left][i, 2] = down[i];

        // Left right -> U bottom (reversed)
        for (int i = 0; i < 3; i++)
            Faces[Face.Up][2, i] = left[2 - i];

        // Rotate the Front face itself
        RotateFaceCw(Faces[Face.Front]);
    }
    // Inverse (counter-clockwise)
    public void MoveFrontCcw()
    {
        MoveFrontCw();
        MoveFrontCw();
        MoveFrontCw();
    }
    
    public void MoveBackCw()
    {
        // Save U top row
        char[] temp = new char[3];
        for (int i = 0; i < 3; i++)
            temp[i] = Faces[Face.Up][0, i];

        // U <- Left (left column, reversed)
        for (int i = 0; i < 3; i++)
            Faces[Face.Up][0, i] = Faces[Face.Left][2 - i, 0];

        // Left <- D bottom row
        for (int i = 0; i < 3; i++)
            Faces[Face.Left][i, 0] = Faces[Face.Down][2, i];

        // D bottom <- Right (right column, reversed)
        for (int i = 0; i < 3; i++)
            Faces[Face.Down][2, i] = Faces[Face.Right][2 - i, 2];

        // Right <- saved U
        for (int i = 0; i < 3; i++)
            Faces[Face.Right][i, 2] = temp[i];

        // Rotate the Back face itself
        RotateFaceCw(Faces[Face.Back]);
    }
    // Inverse (counter-clockwise)
    public void MoveBackCcw()
    {
        MoveBackCw();
        MoveBackCw();
        MoveBackCw();
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
    
    public void EnterCameraMode(Face startingFace = Face.Front)
    {
        // Valid navigation keys
        ConsoleKey[] binds = new[] { ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.RightArrow, ConsoleKey.LeftArrow };

        Face currentFace = startingFace; // starting face

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
    
    
    public bool IsSolved()
    {
        foreach (var face in Faces)
        {
            char color = face.Value[0, 0];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (face.Value[i, j] != color)
                        return false;
                }
            }
        }
        return true;
    }
    
    public void Reset()
    {
        Faces = new()
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
    }
}