using UnityEngine;
using System.Collections.Generic;
using FMODUnity;

public class TaskHandler : MonoBehaviour
{

    //-- Game Objects --\\
    public GameObject TaskBoard;

    //-- Task Related Variables --\\
    private bool CompletedBowledApples;
    private bool VanishedBowledApples;
    private bool CompletedTutorial;
    private bool VanishedFood;
    [SerializeField] EventReference itemplace;

    //-- Task Related Tables --\\
    private List<GameObject> BowledApples = new List<GameObject> { }; //This is for placing the apples inside
    private List<GameObject> TrashedFood = new List<GameObject> { };

    //-- Materials --\\
    public Material[] MaterialArray = new Material[7];
    public Dictionary<string, Material> Materials = new Dictionary<string, Material>();

    private List<string> ValidBowledFood = new List<string> { "HalfApple", "Apple", "QuarterApple", "Strawberry" };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; MaterialArray.Length > i; i++)
        {
            Materials.Add(MaterialArray[i].name, MaterialArray[i]);
        }

        CompletedBowledApples = false;
        VanishedBowledApples = false;
        CompletedTutorial = false;
        VanishedFood = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VanishFood()
    {
        if (CompletedBowledApples && !VanishedBowledApples)
        {
            for (int i = 0; BowledApples.Count > i; i++)
            {
                Debug.Log("Spoiled " + BowledApples[i].name);



                GameObject RottenFood = BowledApples[i].transform.Find(BowledApples[i].name.Split(' ')[0] + "_Rotten").gameObject;
                GameObject FreshFood = BowledApples[i].transform.Find(BowledApples[i].name.Split(' ')[0] + "_Fresh").gameObject;

                RottenFood.SetActive(true);
                FreshFood.SetActive(false);
            }
            VanishedFood = true;
        }
    }

    public void FoodtoPlate(GameObject FoodItem)
    {
        string ObjectName = FoodItem.name.Split(' ')[0];
        Debug.Log(FoodItem.name);
        if (ValidBowledFood.Contains(ObjectName) && !BowledApples.Contains(FoodItem))
        {
            BowledApples.Add(FoodItem);
            FMODUnity.RuntimeManager.PlayOneShot(itemplace);
            Debug.Log(BowledApples.Count);
            if (BowledApples.Count >= 12 && !CompletedBowledApples)
            {
                Debug.Log("Complete");
                CompletedBowledApples = true;
                Renderer TaskRend = TaskBoard.GetComponent<Renderer>();
                TaskRend.material = Materials["BlueMaterial"];
            }
        }
        else
        {
        }
    }

    public void FoodtoTrash(GameObject FoodItem)
    {
        string ObjectName = FoodItem.name.Split(' ')[0];
        Debug.Log(FoodItem.name);
        if (VanishedFood && ValidBowledFood.Contains(ObjectName) && !TrashedFood.Contains(FoodItem))
        {
            TrashedFood.Add(FoodItem);
            Debug.Log(TrashedFood.Count);
            if (TrashedFood.Count >= 12 && !CompletedTutorial)
            {
                Debug.Log("Complete");
                CompletedTutorial = true;
                Renderer TaskRend = TaskBoard.GetComponent<Renderer>();
                TaskRend.material = Materials["RedMaterial"];
            }
        }
        else
        {
        }
    }
}
