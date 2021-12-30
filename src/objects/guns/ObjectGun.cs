using Godot;
using TopdownShooter.objects.guns.bullets;

namespace TopdownShooter.objects.guns
{
    public class ObjectGun : Spatial
    {
        private bool _canShoot = true;
        
        [Export] private PackedScene _bullet = null;

        [Export] private float _muzzleSpeed = 30f;
        [Export] private float _rateOfFire = 100f;

        public override void _Ready()
        {
            GetNode("RofTimer").Connect("timeout", this, nameof(_on_RofTimer_timeout));

            GetNode<Timer>("RofTimer").WaitTime = _rateOfFire / 1000f;
        }

        // public override void _Process(float delta)
        // {
        //     Shoot();
        // }

        public void Shoot()
        {
            if (!_canShoot) return;
            
            var newBullet = (ObjectBullet)_bullet.Instance();
            newBullet.GlobalTransform = GetNode<Position3D>("Muzzle").GlobalTransform;
            newBullet.Speed = _muzzleSpeed;
            
            var sceneRoot = (Spatial)GetTree().Root.GetChildren()[0];
            sceneRoot.AddChild(newBullet);
            
            _canShoot = false;
            GetNode<Timer>("RofTimer").Start();
        }

        public void _on_RofTimer_timeout()
        {
            _canShoot = true;
        }
    }
}
