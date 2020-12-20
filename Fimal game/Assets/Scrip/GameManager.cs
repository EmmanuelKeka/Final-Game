using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerContreller player;
    ship ship1;
    ChangeScenes change;
    void Start()
    {
        player = GameObject.Find("PolyartCharacter").GetComponent<PlayerContreller>();
        ship1 = GameObject.Find("F3_Red Variant").GetComponent<ship>();
        change = GameObject.Find("manageScene").GetComponent<ChangeScenes>();
    }
    // Update is called once per frame
    void Update()
    {
        if (player.gameOver == true)
        {
            StartCoroutine(endGame(2f));
        }
        else if (ship1.gameWin == true)
        {
            change.GoToWinScene();
        }
    }
    IEnumerator endGame(float time)
    {
        yield return new WaitForSeconds(time);
        change.GoToGameover();
    }
}
