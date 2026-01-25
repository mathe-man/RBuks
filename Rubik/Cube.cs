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
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
                Console.Write(Faces[face][i, j] + " ");
            Console.WriteLine();
        }
    }
}