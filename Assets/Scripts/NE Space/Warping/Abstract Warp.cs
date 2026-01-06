using System;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class GameObjectArrayWrapper
{
    public GameObject[] innerList;
}
public class RendererArrayWrapper
{
    public Renderer[] innerList;
}


public abstract class AbstractWarp : MonoBehaviour
{
    [Tooltip("This is the point that the player cannot see to be able to warp")]
    [SerializeField] protected GameObjectArrayWrapper[] pointsToView;
    protected RendererArrayWrapper[] renderers;
    private Collider playerInside;
    private bool isPlayerWarpable;
    [Tooltip("The amount of extra space offscreen the game will not allow for the warp to happen -> it gives more room to look before warping; ex: 0.5 = 50% margin")]
    [SerializeField] protected float margin;
    private bool hasWarped = false;
    [Tooltip("If the warp can happen more than once")]
    [SerializeField] bool canRedo = true;
    protected int maxSwitchVersions = 0;
    int currentSwitch = 0;
    public UnityEvent onWarp;

    [Space]

    [Tooltip("This is the door to check if its closed. If it is then you can do the warp")]
    [SerializeField] GameObject checkDoor;

    protected enum WarpType
    {
        ChangeMaterial,
        ChangeObject,
        MoveObject,
        PlayerWarp,
        SetActive,
        ChangeDecal,
        UnityEvent
    }
    protected WarpType wType;

    protected virtual void Awake()
    {
        SetLookRenderers();
    }

    void OnTriggerEnter(Collider other)
    {
        BeginWarp(other);
    }
    protected void BeginWarp(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerWarpable = true;
            playerInside = other;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndWarp();
        }
    }

    void OnEnable()
    {
        WarpManager.AddWarp(this);
    }
    void OnDisable()
    {
        EndWarp();

        WarpManager.RemoveWarp(this);
    }
    void OnDestroy()//I dont think this is neccessary but ill keep it here for now just in case
    {
        EndWarp();

        WarpManager.RemoveWarp(this);
    }

    protected void EndWarp()
    {
        isPlayerWarpable = false;
        playerInside = null;
        if (canRedo) //If you can redo the warp, then set hasWarped to false when the player isnt looking
        {
            hasWarped = false;
        }
    }
    const float threshold = 0.01f;

    /// <summary>
    /// This checks to see if the player is looking at the point they can't look at, and if not, then warp them to the location seamlessly
    /// </summary>
    /// <param name="other">Its just the other object that the object collides with</param>
    public void TryWarp(Plane[] frustumPlanes, Camera cam)
    {
        if (!isPlayerWarpable || playerInside == null)
        {
            if (canRedo)
            {
                hasWarped = false;
            }

            return;
        }

        if (checkDoor != null)
        {
            DoorState doorScript = checkDoor.GetComponent<DoorState>();
            if (doorScript != null)
            {
                if (!doorScript.IsInClosedPosition())
                    return;
            }
        }

        int clampRendIndex = Mathf.Clamp(currentSwitch, 0, renderers.Length - 1);
        int clampGameObjectIndex = Mathf.Clamp(currentSwitch, 0, pointsToView.Length - 1);

        for (int i = 0; i < renderers[clampRendIndex].innerList.Length; i++)
        {
            Renderer rend = renderers[clampRendIndex].innerList[i];

            if (rend != null)
            {
                if (GeometryUtility.TestPlanesAABB(frustumPlanes, rend.bounds))
                {
                    if (canRedo) //If you can redo the warp, then set hasWarped to false when the player isnt looking
                    {
                        hasWarped = false;
                    }

                    return;
                }
            }
            else
            {
                //Gets the screenpoint of the player and checks to see if they are looking at the view point
                Vector3 screenPoint = cam.WorldToViewportPoint(pointsToView[clampGameObjectIndex].innerList[i].transform.position);
                if (screenPoint.z > 0 && screenPoint.x > -margin && screenPoint.x < 1 + margin && screenPoint.y > -margin && screenPoint.y < 1 + margin)
                {
                    if (canRedo) //If you can redo the warp, then set hasWarped to false when the player isnt looking
                    {
                        hasWarped = false;
                    }

                    return;
                }
            }
        }

        startWarp(playerInside);
    }

    void startWarp(Collider other)
    {
        if (!hasWarped)
        {
            hasWarped = true;

            if (wType == WarpType.SetActive)
            {
                onWarp?.Invoke();
                PerformWarp(other, currentSwitch);
            }
            else
            {
                PerformWarp(other, currentSwitch);
                onWarp?.Invoke();
            }

            currentSwitch++;
            if (currentSwitch >= maxSwitchVersions)
            {
                currentSwitch = 0;
            }
        }
    }

    void SetLookRenderers()
    {
        renderers = new RendererArrayWrapper[pointsToView.Length];
        for (int i = 0; i < pointsToView.Length; i++)
        {
            renderers[i] = new RendererArrayWrapper();
            renderers[i].innerList = new Renderer[pointsToView[i].innerList.Length];
            for (int j = 0; j < pointsToView[i].innerList.Length; j++)
            {
                renderers[i].innerList[j] = pointsToView[i].innerList[j].GetComponent<Renderer>();
            }
        }
    }

    protected void ChangeWarpObject(GameObject oldObj, GameObject newObj)
    {
        for (int i = 0; i < pointsToView.Length; i++)
        {
            for (int j = 0; j < pointsToView[i].innerList.Length; j++)
            {
                if (pointsToView[i].innerList[j] == oldObj)
                {
                    pointsToView[i].innerList[j] = newObj;
                }
            }
        }

        SetLookRenderers();
    }

    protected abstract void PerformWarp(Collider other, int numOfSwitch);
}
