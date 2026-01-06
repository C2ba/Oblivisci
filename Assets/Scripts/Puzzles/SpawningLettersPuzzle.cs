using System.Collections.Generic;
using UnityEngine;

public class SpawningLettersPuzzle : MonoBehaviour
{
    [SerializeField] List<GameObject> letters;
    [SerializeField] GameObject completeGameObject; // GameObject spawned when all letters spawned

    public void SpawnRandomLetter()
    {
        if (letters.Count > 0)
        {
            GameObject curLetter = letters[Random.Range(0, letters.Count)];

            curLetter.SetActive(true);
            letters.Remove(curLetter);
        }
        else
        {
            if (completeGameObject != null && completeGameObject.activeSelf == false)
            {
                completeGameObject.SetActive(true);
            }
            return;
        }
    }
}
