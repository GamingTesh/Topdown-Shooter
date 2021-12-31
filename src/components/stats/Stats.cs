using Godot;

namespace TopdownShooter.components.stats
{
    public class Stats : Node
    {
        [Signal]
        private delegate void Dead();
        
        [Export] private float _maxHealth = 10;
        private float _currentHealth;

        public override void _Ready()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            if (!(_currentHealth <= 0))
                return;

            _currentHealth = 0;
            Death();
        }

        public void AddHealth(float amount)
        {
            _currentHealth += amount;

            if (_currentHealth >= _maxHealth)
                _currentHealth = _maxHealth;
        }

        private void Death()
        {
            EmitSignal(nameof(Dead));
        }
    }
}
