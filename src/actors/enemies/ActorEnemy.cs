using Godot;

namespace TopdownShooter.actors.enemies
{
    public class ActorEnemy : KinematicBody
    {
        private Navigation _nav;
        private KinematicBody _player;

        private Vector3[] _path;
        private int _currentNode = 0;

        [Export] private float _speed = 2;

        public override void _Ready()
        {
            _nav = GetNode<Navigation>("../Navigation");
            _player = GetNode<KinematicBody>("../Player");
            
            GetNode("Timer").Connect("timeout", this, nameof(_on_Timer_timeout));
        }

        public override void _PhysicsProcess(float delta)
        {
            if (_path == null)
                return;
            
            if (_currentNode >= _path.Length)
                return;

            var direction = _path[_currentNode] - GlobalTransform.origin;
            if (direction.Length() < 1)
                _currentNode += 1;
            else
                MoveAndSlide(direction.Normalized() * _speed);
        }

        private void UpdatePath(Vector3 targetPosition)
        {
            _path = _nav.GetSimplePath(GlobalTransform.origin, targetPosition);
        }

        public void _on_Timer_timeout()
        {
            GD.Print("Looking for player!");
            UpdatePath(_player.GlobalTransform.origin);
            _currentNode = 0;
        }
    }
}
