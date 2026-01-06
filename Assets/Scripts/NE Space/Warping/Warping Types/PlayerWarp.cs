using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerWarp : AbstractWarp
{
    [Tooltip("The location that the player will warp to")]
    [SerializeField] Transform[] warpLocations;

    private class HeldObjectInfo
    {
        public Transform heldObject;
        public Transform hand;
        public Vector3 relativePosition;
        public Quaternion relativeRotation;
        public Rigidbody rigidbody;
    }

    protected override void Awake()
    {
        base.Awake();

        maxSwitchVersions = warpLocations.Length;
        wType = WarpType.PlayerWarp;
    }

    protected override void PerformWarp(Collider other, int numOfSwitch)
    {
        StartCoroutine(DelayWarp(other, numOfSwitch));
    }

    IEnumerator DelayWarp(Collider other, int numOfSwitch)
    {
        yield return null;

        GameObject player = other.transform.root.gameObject;

        List<HeldObjectInfo> heldObjects = new List<HeldObjectInfo>();

        XRBaseInteractor[] interactors = player.GetComponentsInChildren<XRBaseInteractor>();
        foreach (XRBaseInteractor interactor in interactors)
        {
            if (interactor.hasSelection)
            {
                foreach (var selected in interactor.interactablesSelected)
                {
                    Transform heldTransform = selected.transform;
                    
                    // Store the held object's position and rotation relative to the hand
                    Vector3 relativePosition = interactor.transform.InverseTransformPoint(heldTransform.position);
                    Quaternion relativeRotation = Quaternion.Inverse(interactor.transform.rotation) * heldTransform.rotation;
                    
                    heldObjects.Add(new HeldObjectInfo{ heldObject = heldTransform, hand = interactor.transform, relativePosition = relativePosition, relativeRotation = relativeRotation, rigidbody = heldTransform.GetComponent<Rigidbody>()});
                }
            }
        }

        //saves the local transform to warp the player seamlessly
        Vector3 localOffset = transform.InverseTransformPoint(player.transform.position);
        Quaternion relativeRot = Quaternion.Inverse(transform.rotation) * player.transform.rotation;


        // Character Controller movement caused some issues this we disable before use
        //For PC port
        CharacterController characterController = player.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        //Warps the player to the set location with the given offset
        player.transform.position = warpLocations[numOfSwitch].TransformPoint(localOffset);
        player.transform.rotation = warpLocations[numOfSwitch].rotation * relativeRot;

        foreach (HeldObjectInfo heldInfo in heldObjects)
        {
            if (heldInfo.heldObject != null && heldInfo.hand != null)
            {
                if (heldInfo.rigidbody != null)
                {
                    heldInfo.rigidbody.isKinematic = true;
                }

                heldInfo.heldObject.position = heldInfo.hand.TransformPoint(heldInfo.relativePosition);
                heldInfo.heldObject.rotation = heldInfo.hand.rotation * heldInfo.relativeRotation;
            }
        }

        if (characterController != null)
        {
            characterController.enabled = true;
        }

        yield return null;

        foreach (HeldObjectInfo heldInfo in heldObjects)
        {
            if (heldInfo.rigidbody != null)
            {
                heldInfo.rigidbody.isKinematic = false;
            }
        }

        EndWarp(); //Have to end the warp because unity doesnt think that you are leaving the trigger
    }
}