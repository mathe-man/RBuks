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
        
        Vector3 cubePos = Vector3.Zero;
        float cubeSize = 2f;

        BoundingBox cubeBox = new BoundingBox(
            cubePos - new Vector3(cubeSize / 2),
            cubePos + new Vector3(cubeSize / 2)
        );

        bool cubeSelected = false;

        // === Loop ===
        while (!Raylib.WindowShouldClose())
        {
            // === Move camera === 
            CameraUpdate();
            
            // === Mouse picking ===
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Ray ray = Raylib.GetMouseRay(Raylib.GetMousePosition(), camera);
                RayCollision hit = Raylib.GetRayCollisionBox(ray, cubeBox);

                if (hit.Hit)
                    cubeSelected = !cubeSelected;
            }

            // === ImGui Begin ===
            rlImGui.Begin();
            

            // === Inspector window ===
            ImGui.Begin("Inspector");
            ImGui.Checkbox("Cube selected", ref cubeSelected);
            ImGui.End();

            // === Draw ===
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGray); // Dark gray background

            Raylib.BeginMode3D(camera);  

            Raylib.DrawGrid(10, 1);     // Draw a grid to visualize the 3D space

            Raylib.DrawCube(
                cubePos,
                cubeSize,
                cubeSize,
                cubeSize,
                cubeSelected ? Color.Green : Color.Red
            );


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
}