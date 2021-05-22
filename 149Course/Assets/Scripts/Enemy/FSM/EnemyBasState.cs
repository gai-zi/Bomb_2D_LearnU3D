

public abstract class EnemyBasState  //有限状态机
{
    public abstract void EnterState(Enemy enemy);
    public abstract void OnUpdate(Enemy enemy);

}
