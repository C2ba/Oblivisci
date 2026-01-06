using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum HandType
{
    Left,
    Right
}
public class HandController : MonoBehaviour
{
    public HandType handType;

    public float thumbMoveSpeed = 0.1f;
    private Animator animator;
    private InputDevice inputDevice;

    private float indexValue;
    private float thumbValue;
    private float threeFingersValue;

    void Start()
    {
        animator = GetComponent<Animator>();
        inputDevice = GetInputDevice();
    }

    void Update()
    {
        AnimateHand();
    }

    private InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics inputDeviceCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        if (handType == HandType.Left)
        {
            inputDeviceCharacteristics = inputDeviceCharacteristics | InputDeviceCharacteristics.Left;
        }
        else
        {
            inputDeviceCharacteristics = inputDeviceCharacteristics | InputDeviceCharacteristics.Right;
        }

        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, inputDevices);

        return inputDevices[0];
    }
    void AnimateHand()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out indexValue);
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out threeFingersValue);

        animator.SetFloat("Index", indexValue);
        animator.SetFloat("ThreeFingers", threeFingersValue);

        inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryTouched);
        inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryTouched);

        if (primaryTouched || secondaryTouched)
        {
            thumbValue += thumbMoveSpeed;
        }
        else
        {
            thumbValue -= thumbMoveSpeed;
        }
        Mathf.Clamp(thumbValue, 0, 1);

        animator.SetFloat("Thumb", thumbValue);
    }
}
