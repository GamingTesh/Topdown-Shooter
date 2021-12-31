using Godot;
using TopdownShooter.objects.guns;

namespace TopdownShooter.actors.player
{
	public class ActorPlayer : KinematicBody
	{
		[Export] private float _speed = 5f;
		private Vector3 _direction;

		private ObjectControllerGun _objectControllerGun;

		public override void _Ready()
		{
			_objectControllerGun = GetNode<ObjectControllerGun>("GunController");
		}

		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(float delta)
		{
			// movement
			_direction = new Vector3();

			if (Input.IsActionPressed("move_right"))
				_direction.x += 1;
			if (Input.IsActionPressed("move_left"))
				_direction.x -= 1;
			if (Input.IsActionPressed("move_forward"))
				_direction.z -= 1;
			if (Input.IsActionPressed("move_backward"))
				_direction.z += 1;

			var velocity = _direction.Normalized() * _speed;

			MoveAndSlide(velocity);
			
			// shoot
			if(Input.IsActionPressed("fire"))
				_objectControllerGun.Shoot();
		}
	}
}


