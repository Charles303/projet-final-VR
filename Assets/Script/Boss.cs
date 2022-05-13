using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    Animator animator;
    float hpBoss = 600;
    public float vitesse;
    bool aAttaque = false;   //Le joueur s'est fait attaqué récemment
    int boucleTouche = 0;
    bool isInvincible;

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
            VictoireFinale();
            print("Boss mort");

        }
        else if (dist <= minDist)
        {
            //permet a l'ennemi de se deplacer en direction du joueur tout seul.
            var trajectoire = Vector3.MoveTowards(transform.position, Joueur.Instance.transform.position, 0 * Time.deltaTime);
            transform.position = trajectoire;
            // Active l'animation
            animator.SetBool("attack", true);
            animator.SetBool("walk", false);
            LoseHealth();
        }
        else if (dist > minDist)
        {
            //permet a l'ennemi de se deplacer en direction du joueur tout seul.
            var trajectoire = Vector3.MoveTowards(transform.position, Joueur.Instance.transform.position, vitesse * Time.deltaTime);
            transform.position = trajectoire;
            //Desactive l'animation
            animator.SetBool("attack", false);
            animator.SetBool("walk", true);
            boucleTouche = 0;
        }
    }
    public void GetDommage(int dommage)
    {
        hpBoss -= dommage;
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        vitesse = vitesse + 0.1f;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "joueur")
        {
            GetDommage(2);
        }
        else if (collision.gameObject.tag == "munition")
        {
            GetDommage(10);
        }
    }
    public void VictoireFinale()
    {
        print("Victoire");
        //Game Over
        SceneManager.LoadScene("Victory", LoadSceneMode.Single);
    }
    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        yield return new WaitForSeconds(1);

        isInvincible = false;
        Debug.Log("Player is no longer invincible!");
    }
    public void LoseHealth()
    {
        if (isInvincible) return;

        Joueur.Instance.hp_joueur -= 20;

        StartCoroutine(BecomeTemporarilyInvincible());
    }
}
