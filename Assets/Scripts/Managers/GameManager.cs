using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventReference creak;
    public static GameManager instance;

    [Header("Player Values")]
    [HideInInspector] public bool hasVignette = false;
    [HideInInspector] public float MusicVolume = 0.5f;
    [HideInInspector] public float SFXVolume = 0.5f;

    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus soundsBus;

    private void Awake()
    {
        // if the current instance hasn't been assigned, that means this object is the first instance of this class.
        if (instance == null)
        {
            // Set the instance to this object.
            instance = this;

            // Make this object persistent until the game ends.
            DontDestroyOnLoad(gameObject);
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

        ToggleVignette();

        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/music thing");
        soundsBus = FMODUnity.RuntimeManager.GetBus("bus:/sounds thing");

        musicBus.setVolume(MusicVolume);
        soundsBus.setVolume(SFXVolume);
    }

    public void GoToStartScreen()
    {
        StartCoroutine(SwitchSceneWithFade("StartScene"));
    }
    public void GoToBasement1()
    {
        StartCoroutine(SwitchSceneWithFade("Basement Scene 1"));
    }
    public IEnumerator StartGameWithDoorAnim()
    {
        GameObject door = GameObject.FindWithTag("DoorAnimator");
        door.GetComponent<Animator>().SetBool("HasStarted", true);
        RuntimeManager.PlayOneShot(creak);

        yield return new WaitForSeconds(1.5f);

        GoToHouseScene();
    }
    public void GoToEndScene()
    {
        StartCoroutine(SwitchSceneWithFade("EndScene"));
    }
    public void EnterCredits()
    {
        StartCoroutine(SwitchSceneWithFade("Credit Scene"));
    }
    public void GoToBasement2()
    {
        StartCoroutine(SwitchSceneWithFade("Basement 2"));
    }
    public void GoToBasementFinal()
    {
        StartCoroutine(SwitchSceneWithFade("Basement Scene Final"));
    }
    public void GoToHouseScene()
    {
        StartCoroutine(SwitchSceneWithFade("HouseScene"));
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }



    IEnumerator SwitchSceneWithFade(string sceneName)
    {
        FadeManager fadeManager = FindFadeManager();
        if (fadeManager != null)
        {
            Debug.Log("Starting");
            yield return StartCoroutine(fadeManager.Fade(false));
            Debug.Log("Ending");
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        fadeManager = FindFadeManager();
        fadeManager.SetAlpha(1);

        if (fadeManager != null)
        {
            yield return StartCoroutine(fadeManager.Fade(true));
        }

        Camera.main.transform.GetChild(0).gameObject.SetActive(hasVignette);
    }

    public void ToggleVignette()
    {
        Camera.main.transform.GetChild(0).gameObject.SetActive(hasVignette);
    }
    FadeManager FindFadeManager()
    {
        Camera cam = Camera.main;

        return cam.GetComponentInChildren<FadeManager>();
    }
}
