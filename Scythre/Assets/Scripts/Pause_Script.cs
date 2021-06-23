using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Script : MonoBehaviour
{
    public GameObject restart;
    public GameObject exit;
    public GameObject title_screen;
    public GameObject Player;

    public bool isPaused;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        isPaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Player.GetComponent<Player_Movement>().isDead == false)
            {
                if (isPaused == false)
                {

                    pauseGame();
                }
                if (isPaused == true)
                {
                    continueGame();
                }
            }
        }
    }

    private void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        restart.gameObject.SetActive(true);
        title_screen.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);

    }
    private void continueGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        restart.gameObject.SetActive(false);
        title_screen.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);

    }
}
