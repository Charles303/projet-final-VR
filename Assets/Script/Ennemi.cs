using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// fortement inspiré de mon code de jeux 3d https://github.com/Cours-Alexandre-Ouellet/jeu-1-jeuneprince
public class Ennemi : MonoBehaviour
{
    Animator animator;
    float hpEnnemi = 50;
    public float vitesse = 2f;
    public AudioClip audioTir;
    private AudioSource source;

    void Start()
    {
        //Animation normale
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //permet a l'ennemi de se deplacer en direction du joueur tout seul.
        var trajectoire = Vector3.MoveTowards(transform.position, Joueur.Instance.transform.position, vitesse * Time.deltaTime);
        transform.position = trajectoire;
        animator.SetBool("walking", true);

        // Mettre les regards de mes ennemis en face du joueur.
        Vector3 lookVector = Joueur.Instance.transform.position - transform.position;
        lookVector.y = transform.position.y;

        // Toujours regarder le joueur
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);

        //Calcul la distance entre le joueur et l'ennemi puis fait une attaque si la distance minimum est atteinte
        float minDist = 3;
        float dist = Vector3.Distance(Joueur.Instance.transform.position, transform.position);
        if (dist < minDist)
        {
            // Active l'animation
            animator.SetBool("attack", true);
            animator.SetBool("walking", false);
        }
        else
        {
            //Desactive l'animation
            animator.SetBool("attack", false);
            animator.SetBool("walking", true);
        }

    }
    public void GetDommage(int dommage)
    {
        hpEnnemi -= dommage;
        source.PlayOneShot(audioTir);

        //Si les points de vie sont inférieurs à 1, la cible est detruite.
        if (hpEnnemi < 1)
        {
            source.PlayOneShot(audioTir);
            CreationEnnemi.Instance.Mort(gameObject);
          
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "joueur")
        {
            GetDommage(2);
            Joueur.Instance.hp_joueur -= 10;
        }
        else if (collision.gameObject.tag == "munition")
        {
            GetDommage(10);           
        }

    }
}
