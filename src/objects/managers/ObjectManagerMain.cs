using Godot;

namespace TopdownShooter.objects.managers
{
    public class ObjectManagerMain : Spatial
    {
        private Vector3 _rayOrigin;
        private Vector3 _rayTarget;

        public override void _PhysicsProcess(float delta)
        {
            var mousePosition = GetViewport().GetMousePosition();

            _rayOrigin = GetNode<Camera>("Camera").ProjectRayOrigin(mousePosition);

            _rayTarget = _rayOrigin + GetNode<Camera>("Camera").ProjectRayNormal(mousePosition) * 2000;

            var spaceState = GetWorld().DirectSpaceState;
            var intersection = spaceState.IntersectRay(_rayOrigin, _rayTarget);

            if (intersection.Count == 0) return;

            var pos = (Vector3) intersection["position"];
            var lookAtMe = new Vector3(pos.x, GetNode<KinematicBody>("Actors/ActorPlayer").Translation.y, pos.z);
            GetNode<KinematicBody>("Actors/ActorPlayer").LookAt(lookAtMe, Vector3.Up);
        }
    }
}


