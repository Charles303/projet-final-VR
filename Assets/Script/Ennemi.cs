using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ennemi : MonoBehaviour
{
    Animator animator;
    float hpEnnemi = 20;
    public float vitesse;

    void Start()
    {
        //Animation normale
        animator = GetComponent<Animator>();
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

    private void OnCollisionEnter(Collision collision)
    {
        print("touche");
        if (hpEnnemi < 1)
        {
            animator.SetBool("dying", true);
        }
    }
}
