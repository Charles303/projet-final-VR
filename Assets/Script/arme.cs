using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class arme : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InputActionReference tirerButton;
    //Audio de tirs et recharge 
    public AudioClip audioTir;
    private AudioSource source;
    public Text Score;
    public float score = 0;

    public GameObject balle;

    public Transform origine;

    public float vitesse = 40;
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
     void Update()
    {
        if (tirerButton.action.triggered)
        {
            score += 10;
            Score.text = "SCORE: " + score.ToString();
            GameObject balles = Instantiate(balle, origine.position, origine.rotation);
            balles.GetComponent<Rigidbody>().velocity = vitesse * origine.forward;
            source.PlayOneShot(audioTir);
            score += 10;
        }
            
    }
}
