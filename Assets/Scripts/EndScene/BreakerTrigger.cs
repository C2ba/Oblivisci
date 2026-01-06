using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class BreakerTrigger : MonoBehaviour
{
    [SerializeField] EventReference generatoron;
    public UnityEvent onEnter;

    void OnTriggerEnter(Collider other)
    {
        FMODUnity.RuntimeManager.PlayOneShot(generatoron);
        onEnter?.Invoke();
    }
}
