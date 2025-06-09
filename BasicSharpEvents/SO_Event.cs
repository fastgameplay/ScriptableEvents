namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "Void Event", menuName = "Events/CSharp/Void")]
    public class SO_Event : SO_BaseEventClass<Action>
    {
        public void Invoke()
        => base.eventHandler?.Invoke();
    }
}