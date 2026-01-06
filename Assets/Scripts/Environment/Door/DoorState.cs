using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.UIElements;
using FMODUnity;

public class DoorState : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] EventReference doorUnlockedSound;
    [SerializeField] EventReference doorLockedSound;

    [SerializeField] bool isLocked;
    private Rigidbody rb;

    Vector3 initialPosition;
    Quaternion initialRotation;
    Vector3 initialScale;
    Vector3 initialLocalPosition;
    Quaternion initialLocalRotation;
    Quaternion parentInitialRotation;
    [SerializeField] UnityEvent doorUnlock;
    XRGrabInteractable grabComponent;
    bool isDoorRotated;

    //Check For Closed
    float posCheck = 0.1f;
    float rotCheck = 1.0f;
    float scaleCheck = 0.01f;
    [Tooltip("The time that the door has to be closed in order to be classified as 'closed'")]
    [SerializeField] float maxTimeUntilClosed = 0.5f;
    float currentTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabComponent = GetComponent<XRGrabInteractable>();

        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;

        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;

        parentInitialRotation = transform.parent.rotation;

        currentTime = maxTimeUntilClosed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key") && isLocked)
        {
            isLocked = false;
            SetRigidbody();
            RuntimeManager.PlayOneShot(doorUnlockedSound);
            Destroy(other.gameObject);

            doorUnlock?.Invoke();
        }
    }

    void Start()
    {
        SetRigidbody();
    }

    private void SetRigidbody()
    {
        if (isLocked)
        {
            rb.isKinematic = true;
            grabComponent.enabled = false;
        }
        else
        {
            rb.isKinematic = false;
            grabComponent.enabled = true;
        }
    }

    public void SetLocked()
    {
        isLocked = true;
        RuntimeManager.PlayOneShot(doorLockedSound);
        SetRigidbody();
    }
    public void SetUnlocked()
    {
        isLocked = false;

        SetRigidbody();
    }

    public void ResetDoorPos()
    {
        ResetDoorToBeginning();
    }
    void ResetDoorToBeginning()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (isDoorRotated)
        {
            HingeJoint hinge = GetComponent<HingeJoint>();
            bool hadHinge = hinge != null;

            if (hadHinge)
            {
                Rigidbody connectedBody = hinge.connectedBody;
                Vector3 anchor = hinge.anchor;
                Vector3 axis = hinge.axis;
                Vector3 connectedAnchor = hinge.connectedAnchor;
                bool useSpring = hinge.useSpring;
                JointSpring spring = hinge.spring;
                bool useMotor = hinge.useMotor;
                JointMotor motor = hinge.motor;
                bool useLimits = hinge.useLimits;
                JointLimits limits = hinge.limits;

                DestroyImmediate(hinge);

                transform.parent.rotation = parentInitialRotation;

                hinge = gameObject.AddComponent<HingeJoint>();
                hinge.connectedBody = connectedBody;
                hinge.anchor = anchor;
                hinge.axis = axis;
                hinge.connectedAnchor = connectedAnchor;
                hinge.useSpring = useSpring;
                if (useSpring) hinge.spring = spring;
                hinge.useMotor = useMotor;
                if (useMotor) hinge.motor = motor;
                hinge.useLimits = useLimits;
                if (useLimits) hinge.limits = limits;
            }
            else
            {
                transform.parent.rotation = parentInitialRotation;
            }
            isDoorRotated = false;
        }

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        SetRigidbody();
    }

    void OnTriggerExit(Collider other)
    {
        IXRSelectInteractor interactor = grabComponent.firstInteractorSelecting;

        if (interactor != null)
        {
            grabComponent.interactionManager.SelectExit(interactor, grabComponent);
        }
    }

    public void RotateDoor()
    {
        RotateDoorWithHinge();
    }

    void RotateDoorWithHinge()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        HingeJoint hinge = GetComponent<HingeJoint>();
        bool hadHinge = hinge != null;
        if (hadHinge)
        {
            Rigidbody connectedBody = hinge.connectedBody;
            Vector3 anchor = hinge.anchor;
            Vector3 axis = hinge.axis;
            Vector3 connectedAnchor = hinge.connectedAnchor;
            bool useSpring = hinge.useSpring;
            JointSpring spring = hinge.spring;
            bool useMotor = hinge.useMotor;
            JointMotor motor = hinge.motor;
            bool useLimits = hinge.useLimits;
            JointLimits limits = hinge.limits;

            DestroyImmediate(hinge);

            transform.parent.Rotate(Vector3.up, 180f);

            hinge = gameObject.AddComponent<HingeJoint>();
            hinge.connectedBody = connectedBody;
            hinge.anchor = anchor;
            hinge.axis = axis;
            hinge.connectedAnchor = connectedAnchor;
            hinge.useSpring = useSpring;
            if (useSpring) hinge.spring = spring;
            hinge.useMotor = useMotor;
            if (useMotor) hinge.motor = motor;
            hinge.useLimits = useLimits;
            if (useLimits) hinge.limits = limits;
        }
        else
        {
            transform.parent.Rotate(Vector3.up, 180f);
        }
        isDoorRotated = !isDoorRotated;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;

        SetRigidbody();
    }

    public bool IsInClosedPosition()
    {
        bool posMatch = Vector3.Distance(transform.localPosition, initialLocalPosition) < posCheck;
        bool rotMatch = Quaternion.Angle(transform.localRotation, initialLocalRotation) < rotCheck;
        bool scaleMatch = Vector3.Distance(transform.localScale, initialScale) < scaleCheck;

        if (posMatch && rotMatch && scaleMatch)
        {
            return currentTime <= 0;
        }

        return false;
    }

    void Update()
    {
        bool posMatch = Vector3.Distance(transform.localPosition, initialLocalPosition) < posCheck;
        bool rotMatch = Quaternion.Angle(transform.localRotation, initialLocalRotation) < rotCheck;
        bool scaleMatch = Vector3.Distance(transform.localScale, initialScale) < scaleCheck;

        if (posMatch && rotMatch && scaleMatch)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = maxTimeUntilClosed;
        }
    }
}
