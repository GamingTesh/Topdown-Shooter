using Godot;
using TopdownShooter.components.stats;

namespace TopdownShooter.objects.guns.bullets
{
    public class ObjectBullet : Spatial
    {
        [Export] public float Speed = 70f;
        [Export] private float _damage = 5;

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

        public void _on_Area_body_entered(Node body)
        {
            QueueFree();

            if (body.HasNode("Stats"))
            {
                body.GetNode<Stats>("Stats")?.TakeDamage(_damage);
            }
        }
    }
}
