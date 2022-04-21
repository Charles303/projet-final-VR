using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public static Joueur Instance { get; private set; }
    public float hp_joueur = 50;
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
        print(hp_joueur);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hp_joueur -= 5;
    }
}
