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
    private Queue<GameObject> filMunition = new Queue<GameObject>();
    
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
        /**
         * Code utilisé du projet 1 en Jeux3D pour tirer une munition avec un offset - Charles Lavoie
         */
        Vector3 offsetArme = origine.transform.forward * 0.2f;
        Vector3 positionDepart = origine.transform.position + origine.transform.forward + offsetArme;
        if (tirerButton.action.triggered)
        {
            GameObject balles = Instantiate(balle);
            filMunition.Enqueue(balles);
            if (filMunition.Count > 3)
            {
                GameObject peek = filMunition.Peek();
                Destroy(peek);
                filMunition.Dequeue();
            }
            balles.transform.position = positionDepart;
            balles.transform.rotation = origine.rotation;
            balles.GetComponent<Rigidbody>().velocity = vitesse * origine.forward;
            source.PlayOneShot(audioTir);
           
        }
            
    }
}
