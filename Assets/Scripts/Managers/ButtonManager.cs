using UnityEngine;
public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject vignetteMark;

    public void GoToStartScreen()
    {
        GameManager.instance.GoToStartScreen();
    }
    public void EneterBasement1()
    {
        GameManager.instance.GoToBasement1();
    }
    public void EnterGameWithDoorAnim()
    {
        StartCoroutine(GameManager.instance.StartGameWithDoorAnim());
    }
    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
    public void EnterEndGame()
    {
        GameManager.instance.GoToEndScene();
    }
    public void EnterBasement2()
    {
        GameManager.instance.GoToBasement2();
    }
    public void GoToCredits()
    {
        GameManager.instance.EnterCredits();
    }
    public void GoToHouseScene()
    {
        GameManager.instance.GoToHouseScene();
    }
    public void GoToMagnumOpus()
    {
        GameManager.instance.GoToBasementFinal();
    }
    public void SetVignette()
    {
        GameManager.instance.hasVignette = !GameManager.instance.hasVignette;

        vignetteMark.SetActive(GameManager.instance.hasVignette);

        GameManager.instance.ToggleVignette();
    }
}
