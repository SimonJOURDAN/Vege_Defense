using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage : MonoBehaviour
{

    [Header ("Attributes")]
    public float range = 3f;            //Portée d'attaque du mage
    public float fireRate = 1f;         //Vitesse d'attaque du mage
    private float fireCountdown = 0f;   //Temps avant que la prochaine attaque soit prête

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";   //String servant à chercher les ennemis
    public Transform target;            //Ennemi cible
    public Image attackBar;             //Barre d'attaque pour montrer le chargement de l'attaque
    public GameObject normal_projectilePrefab;  //Prefab de projectile à instancier quand une attaque est lancée
    Animator anim;                      //Animation permettant de gérer les transitions entre différentes "animation": états visuels de la tourelle

    private Vector2 down = new Vector2(0, -1);  //Vecteur pointant vers le "bas"
    private float angle;                 //Angle utilisé pour passer l'information à l'animator
    private bool attackReady;           //Booléen utilisé pour savoir si l'attaque est prête

    void Start()        //Méthode appelée une fois au début du cycle de vie de l'objet avant la première frame 
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);  //Appel en boucle de "UpdateTarget" toutes les 0.5 secondes
        anim = GetComponent<Animator>();            //Initialistion de variables
        updateSpriteRotation(down);                 //Initialisation du visuel du mage
    }

    // Update is called once per frame
    void Update()       //Méthode appelée une fois par frame (!)dépend donc des performances de la machine
    {
        StartCoroutine("ChargeAttack");     //Lancement d'un script en parallèle
        if(target == null){     //Si aucun ennemi n'est ciblé, arrêt de l'update
            return;
        }

        Vector2 dir = target.position - transform.position; //Vecteur direction ("regard du mage")
        updateSpriteRotation(dir);                          //Changement du visuel en fonction de la direction
        if(attackReady){                //Si l'attaque est prête
            Shoot(dir);                 //Attaquer dans la direction de la cible
            attackReady = false;        //Passage de l'attaque à "non prête"
            fireCountdown = 1f / fireRate;  //"Remise à 0" du décompte d'attaque
        }

    }

    void UpdateTarget(){    //Méthode permettant de cibler l'ennemi le plus proche

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Creation d'une liste d'ennemis

        float shortestDistance = Mathf.Infinity;    //Initialisation de la distance
        GameObject nearestEnemy = null;             //Initialisation de l'ennemi le plus proche

        foreach(GameObject enemy in enemies){       //Pour chaque ennemi (recherche de l'ennemi le plus proche)
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position); //Calcul de la distance à cet ennemi
            if(distanceToEnemy < shortestDistance){ //Si la distance est plus petite que la plus petite jusqu'à présent
                shortestDistance = distanceToEnemy; //Mise à jour de la plus petite distance
                nearestEnemy = enemy;               //Mise à jour de l'ennemi le plus proche
            }
        }

        if(nearestEnemy != null && shortestDistance <= range){  //Si un ennemi existe et qu'il est à portée
            target = nearestEnemy.transform;    //Mise à jour de la cible
        }else{
            target = null;  //Sinon, pas de cible trouvée
        }
    }

    void OnDrawGizmosSelected(){    //Affichage de la portée du mage si l'option Gizmos est séléctionnée
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void updateSpriteRotation(Vector2 dir){ //Changement du sprite grâce à l'animator basé sur la direction de "regard" du mage
        angle = Vector2.SignedAngle(dir, down); //Angle entre le regard du mage et le bas
        anim.SetInteger("direction", (int)((angle + 180 + 45) / 90)%4); //Changement de l'entier "direction" de l'animator qui occasionne un changement d'état
    }
    
    void Shoot(Vector2 dir){    //Méthode qui gère le tir
        dir.Normalize();    //Normalisation de dir
        GameObject projectileGO = Instantiate(normal_projectilePrefab, (Vector2)transform.position + dir, Quaternion.Euler(0,0,0)); //Instanciation du projectile dans la direction dir et sans rotation
        Projectile projectile = projectileGO.GetComponent<Projectile>(); //Récupération de la partie script du projectile
        if(projectile != null){ //Si un script est trouvé
            projectile.Seek(target);    //Ordre de chasser la cible
        }
    }

    void ChargeAttack(){            //Méthode qui charge l'attaque
        if (fireCountdown <= 0f)    //Si le décompte est terminé
        {
            attackReady = true;     //Attaque prête
        }

        if (!attackReady){  //Si l'attaque n'est pas prête
            fireCountdown -= Time.deltaTime;    //Décompte (Time.deltaTime étant le temps entre les appels de update)
        }
        
        attackBar.fillAmount = 1f - fireCountdown * fireRate; //Mise à jour de la barre visuelle
    }


}
