using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterBehavior : MonoBehaviour
{
    [SerializeField] EventReference deathSound;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(deathSound);
            GameManager.instance.GoToEndScene();
        }
    }
}
