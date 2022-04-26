using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangerScene()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void ChangerSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void ChangerSceneGameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
    public void ChangerSceneVictory()
    {
        SceneManager.LoadScene("Victory", LoadSceneMode.Single);
    }
}