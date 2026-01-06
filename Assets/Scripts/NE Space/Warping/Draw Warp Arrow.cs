using UnityEngine;

public class DrawWarpArrow : MonoBehaviour
{
    [SerializeField] Color color = Color.yellow;
    [SerializeField] float length = 3;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);
    }
}
