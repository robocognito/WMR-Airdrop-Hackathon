public class LightStateHandler : BaseStateHandler
{
    public override void OnStateChange(bool newState)
    {
        gameObject.SetActive(newState);
    }
}
