namespace Game.Interface
{
    public interface IMovement 
    {
        float MoveSpeed { get; set; }
        bool IsColliding { get; }
    
    }
}
