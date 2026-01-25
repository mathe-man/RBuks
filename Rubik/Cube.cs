/*
    MIT License
    Copyright (c) 2026 mathe man

    This source file is licensed under the MIT License.
    You may use, modify, and distribute it freely.
*/

namespace Rubik;

public class Cube
{
    protected Dictionary<string, char[,]> Faces = new()
    {
        ["U"] = new char[,] {
            { 'W','W','W' },
            { 'W','W','W' },
            { 'W','W','W' }
        },

        ["D"] = new char[,] {
            { 'Y','Y','Y' },
            { 'Y','Y','Y' },
            { 'Y','Y','Y' }
        },

        ["F"] = new char[,] {
            { 'G','G','G' },
            { 'G','G','G' },
            { 'G','G','G' }
        },

        ["B"] = new char[,] {
            { 'B','B','B' },
            { 'B','B','B' },
            { 'B','B','B' }
        },

        ["L"] = new char[,] {
            { 'O','O','O' },
            { 'O','O','O' },
            { 'O','O','O' }
        },

        ["R"] = new char[,] {
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

    public void MoveU()
    {
        // Save F top row
        char[] temp = new char[3];
        for (int i = 0; i < 3; i++)
            temp[i] = Faces["F"][0, i];

        // L -> F
        for (int i = 0; i < 3; i++)
            Faces["F"][0, i] = Faces["L"][0, i];

        // B -> L
        for (int i = 0; i < 3; i++)
            Faces["L"][0, i] = Faces["B"][0, i];

        // R -> B
        for (int i = 0; i < 3; i++)
            Faces["B"][0, i] = Faces["R"][0, i];

        // temp -> R
        for (int i = 0; i < 3; i++)
            Faces["R"][0, i] = temp[i];

        // Rotate the U face itself
        RotateFaceCw(Faces["U"]);
    }

    
    public void MoveD()
    {
        // Save F bottom row
        char[] temp = new char[3];
        for (int i = 0; i < 3; i++)
            temp[i] = Faces["F"][2, i];

        // L -> F
        for (int i = 0; i < 3; i++)
            Faces["F"][2, i] = Faces["L"][2, i];

        // B -> L
        for (int i = 0; i < 3; i++)
            Faces["L"][2, i] = Faces["B"][2, i];

        // R -> B
        for (int i = 0; i < 3; i++)
            Faces["B"][2, i] = Faces["R"][2, i];

        // temp -> R
        for (int i = 0; i < 3; i++)
            Faces["R"][2, i] = temp[i];

        // Rotate the U face itself
        RotateFaceCw(Faces["B"]);
    }
}