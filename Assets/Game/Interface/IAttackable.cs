namespace Game.Interface
{
    public interface IAttackable 
    {
        int DamageAmount { get; }
        void Attack();
    }
}
