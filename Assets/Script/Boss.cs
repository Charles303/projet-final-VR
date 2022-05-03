using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    Animator animator;
    float hpBoss = 50;
    public float vitesse;

    void Start()
    {
        //Animation normale
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("walk", true);

        // Mettre les regards de mes ennemis en face du joueur.
        Vector3 lookVector = Joueur.Instance.transform.position - transform.position;
        lookVector.y = transform.position.y;
        

        // Toujours regarder le joueur
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);

        //Calcul la distance entre le joueur et l'ennemi puis fait une attaque si la distance minimum est atteinte
        float minDist = 2;
        float dist = Vector3.Distance(Joueur.Instance.transform.position, transform.position);
        if (hpBoss <= 0)
        {
            Victoire();
        }
        else if (dist <= minDist)
        {
            //permet a l'ennemi de se deplacer en direction du joueur tout seul.
            var trajectoire = Vector3.MoveTowards(transform.position, Joueur.Instance.transform.position, 0 * Time.deltaTime);
            transform.position = trajectoire;
            // Active l'animation
            animator.SetBool("attack", true);
            animator.SetBool("walk", false);
        }
        else if (dist > minDist)
        {
            //permet a l'ennemi de se deplacer en direction du joueur tout seul.
            var trajectoire = Vector3.MoveTowards(transform.position, Joueur.Instance.transform.position, vitesse * Time.deltaTime);
            transform.position = trajectoire;
            //Desactive l'animation
            animator.SetBool("attack", false);
            animator.SetBool("walk", true);
        }
    }
    public void GetDommage(int dommage)
    {
        hpBoss -= dommage;

        //Si les points de vie sont inférieurs à 1, la cible est detruite.
        if (hpBoss < 1)
        {
            CreationEnnemi.Instance.Mort(gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "joueur")
        {
            print("test");
            GetDommage(2);
            Joueur.Instance.hp_joueur -= 10;
        }
        else if (collision.gameObject.tag == "munition")
        {
            GetDommage(10);
           
        }
    }
    public void Victoire()
    {
        print("Victoire");
        //Game Over
        SceneManager.LoadScene("Victory", LoadSceneMode.Single);
    }
}
