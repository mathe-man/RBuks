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
    
    static float time = 0f;
    static (int Minutes, float Seconds) timeForms = (
        (int)(time / 60),
        time % 60f
    );
    
    static void Main()
    {
        // === Startup ===
        Init();

        Cubie c = new Cubie(new Vector3(0,0,0));
        c.SetColorFromChar('O');

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
            DrawPositions();

            c.Draw();
            Console.WriteLine($"{c.IsHoovered(camera)} ; {c.IsClicked(camera, MouseButton.Left)}");

            // Ending frame
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

    
    private static readonly Vector3 Zero = new Vector3(1,1,1) / 100;
    private static readonly Vector3 X = new Vector3(1000,1,1) / 100;
    private static readonly Vector3 Y = new Vector3(1,1000,1) / 100;
    private static readonly Vector3  Z = new Vector3(1,1,1000) / 100;
    private static List<Texture2D> _positionsTextures = new ();
    static void DrawPositions()
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
        
        
        
        Raylib.DrawLine3D(Zero, X, Color.Red);
        Raylib.DrawLine3D(Zero, Y, Color.Green);
        Raylib.DrawLine3D(Zero, Z, Color.Blue);
    }
    
  
}