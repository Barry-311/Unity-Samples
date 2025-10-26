// IWeapon.cs
public interface IWeapon
{
    void OnPress();   // 鼠标按下
    void OnHold();    // 鼠标持续按下每帧
    void OnRelease(); // 鼠标松开
}
