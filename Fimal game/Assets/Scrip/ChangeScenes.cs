using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GotoGameOver()
    {
        SceneManager.LoadScene("Gameover");
    }
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void GotoHowtoPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void GoToLevel1()
    {
        SceneManager.LoadScene("level1");
    }
    public void GoToEnd()
    {
        SceneManager.LoadScene("End");
    }
    public void GoToGameover()
    {
        SceneManager.LoadScene("Gameover");
    }
    public void GoToWinScene()
    {
        StartCoroutine(Cooldown());
    }
    public void GoToSouceCode()
    {
        Application.OpenURL("https://github.com/EmmanuelKeka/Final-Game");
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WinScene");
    }
}
