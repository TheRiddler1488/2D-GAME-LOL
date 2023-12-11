namespace Game.Interface
{
  public interface IHealth
  {

    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
    void TakeDamage(int damageAmount);
    
  }
}
