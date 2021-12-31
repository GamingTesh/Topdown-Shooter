using Godot;

namespace TopdownShooter.objects.guns.bullets
{
    public class ObjectBullet : Spatial
    {
        [Export] public float Speed = 70f;

        private const float KillTime = 2f;
        private float _timer = 0;

        public override void _Ready()
        {
            GetNode("Area").Connect("body_entered", this, nameof(_on_Area_body_entered));
        }

        public override void _PhysicsProcess(float delta)
        {
            var forwardDirection = GlobalTransform.basis.z.Normalized();
            GlobalTranslate(forwardDirection * Speed * delta);

            _timer += delta;
            if(_timer >= KillTime)
                QueueFree();
        }

        public void _on_Area_body_entered(Node body)
        {
            GD.Print("I hit you!");
            QueueFree();
        }
    }
}
