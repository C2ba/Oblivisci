using UnityEngine;
using System.Collections;
using FMODUnity;
using FMOD.Studio;

public class randomnoise : MonoBehaviour
{
    [SerializeField] private EventReference noise;
    bool canplay = true;
    void Start()
    {
        StartSound();
    }
    void StartSound()
    {
        if (canplay)
        {
            StartCoroutine(PlayRandomSound());
        }
    }
    IEnumerator PlayRandomSound()
    {
        canplay = false;
        yield return new WaitForSeconds(Random.Range(30f, 60f));
        canplay = true;

        FMODUnity.RuntimeManager.PlayOneShot("event:/randomness");
        StartSound();
    }
}

