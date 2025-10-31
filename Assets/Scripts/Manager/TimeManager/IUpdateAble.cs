/// <summary>
/// 继承这个接口就可以注册在TimeManager中每秒调用了
/// </summary>
public interface IUpdateAble
{
    public void DoUpdate(float deltaTime);
}

public interface IUpdateAblePerS
{
    /// <summary>
    /// 每秒一次
    /// </summary>
    public void DoUpdatePerS();
}
