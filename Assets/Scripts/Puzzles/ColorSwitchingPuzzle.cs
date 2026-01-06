using UnityEngine;
using UnityEngine.Events;

public class ColorSwitchingpuzzle : MonoBehaviour
{
    [SerializeField] Renderer[] renderers;
    [SerializeField] Material winningMat;
    public UnityEvent puzzleSolved;

    public void checkColors()
    {
        foreach (Renderer rend in renderers)
        {
            if (rend.material.color != winningMat.color)
            {
                return;
            }
        }

        puzzleSolved?.Invoke();
    }
}
