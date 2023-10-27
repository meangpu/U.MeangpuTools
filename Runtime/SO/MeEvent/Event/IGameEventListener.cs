namespace Meangpu.SOEvent
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T data);
    }
}