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

    public Color Color { get; set; }
    
    public Cubie(Vector3 position, Vector3? size = null, Vector3? scale = null)
    {
        Position = position;
        Size = size ?? new Vector3(1,1,1);
    }

    public void SetColorFromChar(char c)
    {
        Color = c switch
        {
            'W' => Color.White,
            'Y' => Color.Yellow,
            'R' => Color.Red,
            'O' => Color.Orange,
            'B' => Color.Blue,
            'G' => Color.Green,
            _ => Color.Lime
        };
    }
    
    public void Draw()
    {
        Raylib.DrawCubeV(Position, Size, Color);
        Raylib.DrawCubeWiresV(Position, Size, Color.Black);
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
    
    public void RotateAroundPoint(
        Vector3 pivot,
        Vector3 axis,
        float angleDeg,
        float t
    )
    {
        axis = Vector3.Normalize(axis);
    
        Vector3 v = Position - pivot;
    
        float angleRad = angleDeg * t * (float)Math.PI / 180f;
        float cos = MathF.Cos(angleRad);
        float sin = MathF.Sin(angleRad);
    
        Vector3 rotated =
            v * cos +
            Vector3.Cross(axis, v) * sin +
            axis * Vector3.Dot(axis, v) * (1 - cos);
    
        Position =  pivot + rotated;
    }
}