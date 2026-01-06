using UnityEngine;
using FMODUnity;

public class SoundManager : MonoBehaviour
{  
    [SerializeField] EventReference attemptopenlockeddoor;
    [SerializeField] EventReference backwardsalert;
    [SerializeField] EventReference forwardsalert;
    [SerializeField] EventReference footstep;
    [SerializeField] EventReference closeinventory;
    [SerializeField] EventReference openinventory;
    [SerializeField] EventReference death;
    [SerializeField] EventReference opendoor;
    [SerializeField] EventReference doorunlockedping;
    [SerializeField] EventReference generatoroff;
    [SerializeField] EventReference generatoron;
    [SerializeField] EventReference grabitem;
    [SerializeField] EventReference heartbeat;
    [SerializeField] EventReference keypickup;
    [SerializeField] EventReference lightbulbcrack;
    [SerializeField] EventReference openlockeddoor;
    [SerializeField] EventReference tasklistinteraction;

    public void attemptopenlockeddoorsound()
    {
        RuntimeManager.PlayOneShot(attemptopenlockeddoor);
    }
    public void backwardsalertsound()
    {
        RuntimeManager.PlayOneShot(backwardsalert);
    }
    public void forwardsalertsound()
    {
        RuntimeManager.PlayOneShot(forwardsalert);
    }public void footstepsound()
    {
        RuntimeManager.PlayOneShot(footstep);
    }public void closeinventorysound()
    {
        RuntimeManager.PlayOneShot(closeinventory);
    }public void openinventorysound()
    {
        RuntimeManager.PlayOneShot(openinventory);
    }public void deathsound()
    {
        RuntimeManager.PlayOneShot(death);
    }public void opendoorsound()
    {
        RuntimeManager.PlayOneShot(opendoor);
    }public void doorunlockedpingsound()
    {
        RuntimeManager.PlayOneShot(doorunlockedping);
    }public void generatoroffsound()
    {
        RuntimeManager.PlayOneShot(generatoroff);
    }public void generatoronsound()
    {
        RuntimeManager.PlayOneShot(generatoron);
    }public void grabitemsound()
    {
        RuntimeManager.PlayOneShot(grabitem);
    }public void heartbeatsound()
    {
        RuntimeManager.PlayOneShot(heartbeat);
    }public void keypickupsound()
    {
        RuntimeManager.PlayOneShot(keypickup);
    }public void lightbulbcracksound()
    {
        RuntimeManager.PlayOneShot(lightbulbcrack);
    }public void tasklistinteractionsound()
    {
        RuntimeManager.PlayOneShot(tasklistinteraction);
    }
}
