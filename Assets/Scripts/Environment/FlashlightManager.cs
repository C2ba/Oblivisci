using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using FMODUnity;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] GameObject lightSource;
    [SerializeField] EventReference flashlighton;
    [SerializeField] EventReference flashlightoff;
    private bool isOn = false;

    public void ToggleFlashlight()
    {
        if (isOn)
        {
            lightSource.SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot(flashlightoff);
            isOn = false;
        }
        else
        {
            lightSource.SetActive(true);
            FMODUnity.RuntimeManager.PlayOneShot(flashlighton);
            isOn = true;
        }
    }
}
