using UnityEngine;      

public abstract class BaseStateHandler : MonoBehaviour, IState
{
    public virtual void OnStateChange(bool newState)
    {
    }
}
