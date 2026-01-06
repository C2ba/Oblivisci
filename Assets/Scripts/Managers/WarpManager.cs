using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public static WarpManager instance;

    private Camera cam;
    private Plane[] frustumPlanes;

    private void Awake()
    {
        // if the current instance hasn't been assigned, that means this object is the first instance of this class.
        if (instance == null)
        {
            // Set the instance to this object.
            instance = this;
        }
        else
        {
            // if the instance already exist and it's not this object
            if (instance != this)
            {
                // destroy this one
                Destroy(gameObject);
            }
        }

        cam = FindCam();
    }

    private Camera FindCam()
    {
        foreach (Camera cam in Camera.allCameras)
        {
            if (cam.stereoTargetEye == StereoTargetEyeMask.Both)
            {
                return cam;
            }
        }

        if (Camera.main != null)
        {
            return Camera.main;
        }

        //If neither of these are the real cam then I shall cry
        return null;
    }

    private static List<AbstractWarp> warps = new List<AbstractWarp>();

    public static void AddWarp(AbstractWarp warp)
    {
        if (!warps.Contains(warp))
            warps.Add(warp);
    }

    public static void RemoveWarp(AbstractWarp warp)
    {
        warps.Remove(warp);
    }

    void LateUpdate()
    {
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        for (int i = 0; i < warps.Count; i++)
        {
            if (warps[i] != null)
            {
                warps[i].TryWarp(frustumPlanes, cam);
            }
        }
    }
}
