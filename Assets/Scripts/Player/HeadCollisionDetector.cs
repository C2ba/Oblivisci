using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeadCollisionDetector : MonoBehaviour
{
    [SerializeField] float detectionDelay;
    [SerializeField] float detectionDistance = 0.2f;

    public List<RaycastHit> detectedCollidersHit { get; private set; }

    [SerializeField] LayerMask detectionLayermask;

    float currentTime = 0;

    private List<RaycastHit> performDetection(Vector3 position, float distance, LayerMask layerMask)
    {
        List<RaycastHit> detectedHits = new();

        List<Vector3> directions = new() { transform.forward, transform.right, -transform.right };

        RaycastHit hit;
        foreach (var dir in directions)
        {
            if (Physics.Raycast(position, dir, out hit, distance, layerMask))
            {
                detectedHits.Add(hit);
            }
        }

        return detectedHits;
    }

    void Start()
    {
        detectedCollidersHit = performDetection(transform.position, detectionDistance, detectionLayermask);
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > detectionDelay)
        {
            currentTime = 0;

            detectedCollidersHit = performDetection(transform.position, detectionDistance, detectionLayermask);
        }
    }
}
