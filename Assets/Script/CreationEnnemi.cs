using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// inspiré de mon code de jeux 3d https://github.com/Cours-Alexandre-Ouellet/jeu-1-jeuneprince
public class CreationEnnemi : MonoBehaviour
{
    public static CreationEnnemi Instance { get; private set; }
    // Modèle de l'ennemi
    public GameObject prototypeEnnemi;
    // Référence vers les ennemis créés
    public List<GameObject> ennemis;
    //positions des ennemis
    public int posX;
    public int posZ;
    //nombre d'ennemis
    private int nombreEnnemi= 6;


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

    void Start()
    {
        ennemis = new List<GameObject>();
        CreerEnnemis();
    }

    public void CreerEnnemis()
    {
        //Creation des ennemis et placement sur le terrain de jeu
        for (int i = 0; i < nombreEnnemi; i++)
        {

            GameObject ennemi = Instantiate(prototypeEnnemi);
            posX = Random.Range(-30, 30);
            posZ = Random.Range(-30, 30);
            ennemi.transform.position = new Vector3(posX + 5f, 0, posZ + 5f);

            ennemis.Add(ennemi);
        }
    }
    public void Mort(GameObject ennemi)
    {
        ennemis.Remove(ennemi);
            
        Destroy(ennemi);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
