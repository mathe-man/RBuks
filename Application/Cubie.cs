using System.Numerics;
using Raylib_cs;

namespace Application;

public class Cubie
{
    public Vector3 Position {get; private set;}

    private Vector3 size;
    public Vector3 Size
    {
        get => size;
        private set
        {
            size = value;
            // Update bounding box when size changes to keep it accurate
            BoundingBox = new BoundingBox(
                Position - new Vector3(Size.X / 2, Size.Y / 2, Size.Z / 2),
                Position + new Vector3(Size.X / 2, Size.Y / 2, Size.Z / 2)
            );
        }
    }

    public BoundingBox BoundingBox { get; private set; }
    
    public Cubie(Vector3 position, Vector3? size = null, Vector3? scale = null)
    {
        Position = position;
        Size = size ?? new Vector3(1,1,1);
    }
    
    public bool IsHoovered(Camera3D camera)
    {
        Ray ray = Raylib.GetMouseRay(Raylib.GetMousePosition(), camera);
        RayCollision hit = Raylib.GetRayCollisionBox(ray, BoundingBox);

        return hit.Hit;
    }

    public bool IsClicked(Camera3D camera, MouseButton mouseButton)
    {
        if (Raylib.IsMouseButtonPressed(mouseButton))
            return IsHoovered(camera);
        return false;
    }
}