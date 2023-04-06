using UnityEngine;
using UnityEngine.Events;

public class TargetCheck : MonoBehaviour
{
    public bool IsTriggered { get; private set; }

    [SerializeField] private UnityEvent _onEnter, _onExit;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            IsTriggered = true;
            _onEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            IsTriggered = false;
            _onExit?.Invoke();
        }
    }
}