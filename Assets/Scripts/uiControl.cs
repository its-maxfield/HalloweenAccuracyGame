using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiControl : MonoBehaviour
{
    public Text timerText;
    public GameObject gameOver;
    public Text gameOverInfo;

    private float timer = 20.0f;

    void Start()
    {
      Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
      float seconds = timer % 60;
      timerText.text = "Time Left: " + seconds.ToString("n2");

      if(timer < 0.0f)
      {
        timerText.text = "";
        endGame();
      }
      timer -= Time.deltaTime;
    }

    private void endGame()
    {
      Debug.Log("Ending game");

      gameOver.SetActive(true);
      Time.timeScale = 0f;

      float misses = (float)gameObject.GetComponent<hitControl>().returnMisses();
      float hits = (float)gameObject.GetComponent<hitControl>().returnTargets();

      float totalShots = misses + hits;

      float percentMiss = (misses / totalShots) * 100f;
      float percentHit = (hits /totalShots) * 100f;

      string p1 = "You clicked a total of " + totalShots.ToString() + " times.\n";
      string p2 = "You missed " + misses.ToString() + " of the pumpkins you tried to click.\n";
      string p3 = "You clicked a pumpkin without missing " + hits.ToString() + " times.\n\n";
      string p4 = "Your miss percentage was: " + percentMiss.ToString("n2") + " percent\n";
      string p5 = "Your click percentage was " + percentHit.ToString("n2") + " percent.";

      gameOverInfo.text = p1 + p2 + p3 + p4 + p5;
    }

    public void returnToMenu()
    {
      SceneManager.LoadScene("mainMenu");
    }
}
