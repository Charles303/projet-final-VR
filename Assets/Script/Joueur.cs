using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Joueur : MonoBehaviour
{
    public static Joueur Instance { get; private set; }
    public float hp_joueur = 150;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp_joueur <= 0)
        {
            Mourir();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hp_joueur -= 5;
    }
    public void Mourir()
    {
        print("GameOver");
        //Game Over
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
