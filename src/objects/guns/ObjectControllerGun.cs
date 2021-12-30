using Godot;

namespace TopdownShooter.objects.guns
{
    public class ObjectControllerGun : Node
    {
        [Export] private PackedScene _startingWeapon = null;

        private Position3D _hand;
        private ObjectGun _equippedWeapon;

        public override void _Ready()
        {
            _hand = (Position3D) GetParent().FindNode("Hand");

            if (_startingWeapon != null)
            {
                EquipWeapon(_startingWeapon);
            }
        }

        private void EquipWeapon(PackedScene weapon)
        {
            if (_equippedWeapon != null)
                _equippedWeapon.QueueFree();
            else
            {
                GD.Print("No weapon equipped");
                _equippedWeapon = (ObjectGun) weapon.Instance();
                _hand.AddChild(_equippedWeapon);
            }
        }

        public void Shoot()
        {
            _equippedWeapon?.Shoot();
        }
    }
}


