using System.Collections.Generic;
using UnityEngine;

public class HeadDetectionManager : MonoBehaviour
{
    [SerializeField] HeadCollisionDetector detector;
    [SerializeField] CharacterController characterController;
    public float pushStrength = 1.0f;
    [SerializeField] FadeManager fade;

    private Vector3 calculatePushbackDirection(List<RaycastHit> hits)
    {
        Vector3 normal = Vector3.zero;

        foreach (RaycastHit hit in hits)
        {
            normal += new Vector3(hit.normal.x, 0, hit.normal.z);
        }

        return normal;
    }

    void Update()
    {
        if (detector.detectedCollidersHit.Count == 0)
        {
            fade.Fade(false);
            return;
        }
        else
        {
            fade.Fade(true); //Idk why this isnt working but as of right now the player basically cant even put their head into the wall so its ok
        }

        Vector3 pushbackDir = calculatePushbackDirection(detector.detectedCollidersHit);

        characterController.Move(pushbackDir.normalized * pushStrength * Time.deltaTime);
    }
}
