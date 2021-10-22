using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{
    public void startGame()
    {
      SceneManager.LoadScene("mainGame");
    }

    public void exitGame()
    {
      Debug.Log("Exit working");
      Application.Quit();
    }
}
