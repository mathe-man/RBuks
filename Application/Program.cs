/*
    MIT License
    Copyright (c) 2026 mathe man

    This source file is licensed under the MIT License.
    You may use, modify, and distribute it freely.
*/

namespace Application;

using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;


class Program
{
    static Camera3D camera;

    static bool Init()
    {
        // === Raylib Window ===
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);                 // Resizable window
        Raylib.InitWindow(1280, 800, "RBuks Application");   // Start dimensions
        Raylib.SetTargetFPS(60);
        
        // === Camera ===
        camera = new Camera3D
        {
            Position = new Vector3(6, 6, 6),
            Target = Vector3.Zero,
            Up = Vector3.UnitY,
            FovY = 45,
            Projection = CameraProjection.Perspective
        };
        
        
        // === ImGui ===
        rlImGui.Setup(true, true);    // Init with dark mode and docking enabled
        
        // === Success ===
        return true;
    }
    
    static void Main()
    {
        // === Startup ===
        Init();
        
        // === Cube / Scene ===
        Cubie c = new (new Vector3(0,0,0));
        
        Vector3 cubePos = Vector3.One;
        float cubeSize = 2f;

        BoundingBox cubeBox = new BoundingBox(
            cubePos - new Vector3(cubeSize / 2),
            cubePos + new Vector3(cubeSize / 2)
        );

        
        float time = 50f;
        (int Minutes, float Seconds) timeForms = (
            (int)(time / 60),
            time % 60f
        );

        // === Loop ===
        while (!Raylib.WindowShouldClose())
        {
            // === Update ===
            // Update time
            time += Raylib.GetFrameTime();
            timeForms = (
                (int)(time / 60),
                time % 60f
            );

            // Update camera
            CameraUpdate();
            
            
            // === ImGui Begin ===
            rlImGui.Begin();
            

            // === Inspector window ===
            ImGui.Begin("Inspector");
            ImGui.Text(
                $"FPS: {Raylib.GetFPS()}\n" +
                $"Frame time (ms): {(Raylib.GetFrameTime() * 1000):F2} ms \n" +
                $"Time: {timeForms.Minutes}min {timeForms.Seconds:F2}s\n");
            
            ImGui.End();

            // === Draw ===
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGray); // Dark gray background

            Raylib.BeginMode3D(camera);  

            Raylib.DrawGrid(10, 1);     // Draw a grid to visualize the 3D space

            Raylib.DrawCube(
                /*RotateAroundPoint(
                    cubePos,
                    Vector3.Zero,
                    Vector3.UnitY,
                    360f,
                    time % 10f / 10f
                )*/ Vector3.Zero,
                cubeSize,
                cubeSize,
                cubeSize,
                 Color.Violet
            );
            DrawPosition();

            Raylib.EndMode3D();

            // ImGui rendering
            rlImGui.End();

            Raylib.EndDrawing();
        }

        // === Cleanup ===
        rlImGui.Shutdown();
        
        Raylib.CloseWindow();
    }
    
    static bool CameraUpdate()
    {
        if (!Raylib.IsMouseButtonDown(MouseButton.Right))
            return false;
        
        // Update camera if right mouse button is held down
        Raylib.UpdateCamera(ref camera, CameraMode.ThirdPerson);
        return true;
    }

    private static List<Texture2D> _positionsTextures = new ();
    static void DrawPosition()
    {
        // Generate position textures if not already done
        if (_positionsTextures.Count == 0)
            // Loop from (-1, -1, -1) to (1, 1, 1)
            for (int x = -1; x <= 1; x++)       // X axis
                for (int y = -1; y <= 1; y++)   // Y axis
                    for (int z = -1; z <= 1; z++) // Z axis
                    {
                        // Generate texture with position and text (x;y;z)
                        Image img = Raylib.ImageText($"{x};{y};{z}", 5, Color.White);
                        // Convert to texture
                        _positionsTextures.Add(Raylib.LoadTextureFromImage(img));
                    }

        // Draw textures at their respective positions in 3D
        for (int x = -1; x <= 1; x++)       // X
            for (int y = -1; y <= 1; y++)   // Y
                for (int z = -1; z <= 1; z++) // Z
                {
                    // Compute linear index for 3D grid (x,y,z) in range 0..26
                    int idx = (x + 1) * 9 + (y + 1) * 3 + (z + 1);
                    bool isFaceCenter = Math.Abs(x) + Math.Abs(y) + Math.Abs(z) == 1;
                    
                    float scale = 0.1f; // Default scale
                    // Enlarge textures on the faces of the cube
                    if (isFaceCenter)
                        scale = 0.2f;
                    
                    
                    float offset = 1.05f; // Default offset
                    // Add extra offset for textures on the faces of the cube (they are larger)
                    if (isFaceCenter)
                        offset = 1.15f;
                    
                    // Draw billboard at position (x,y,z) with offset
                    Vector3 pos = new Vector3(x * offset, y * offset, z * offset);

                    
                    Raylib.DrawBillboard(camera, _positionsTextures[idx], pos, scale, Color.White);
                }
    }
    
    public static Vector3 RotateAroundPoint(
        Vector3 position,
        Vector3 pivot,
        Vector3 axis,
        float angleDeg,
        float t
    )
    {
        axis = Vector3.Normalize(axis);
    
        Vector3 v = position - pivot;
    
        float angleRad = angleDeg * t * (float)Math.PI / 180f;
        float cos = MathF.Cos(angleRad);
        float sin = MathF.Sin(angleRad);
    
        Vector3 rotated =
            v * cos +
            Vector3.Cross(axis, v) * sin +
            axis * Vector3.Dot(axis, v) * (1 - cos);
    
        return pivot + rotated;
    }
}