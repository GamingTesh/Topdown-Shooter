using Godot;

namespace TopdownShooter.objects.guns.bullets
{
    public class ObjectBullet : Spatial
    {
        [Export] public float Speed = 70f;

        private const float KillTime = 2f;
        private float _timer = 0;

        public override void _PhysicsProcess(float delta)
        {
            var forwardDirection = GlobalTransform.basis.z.Normalized();
            GlobalTranslate(forwardDirection * Speed * delta);

            _timer += delta;
            if(_timer >= KillTime)
                QueueFree();
        }
    }
}
