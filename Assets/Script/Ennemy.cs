using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour
{
    
    [Header("Variables")]
    private Transform target;   //Transform pour le waypoint cible
    private int health; //Vie de l'ennemi
    private int waypointIndex = 1;  //Numéro du waypoint ciblé
    private Animator anim;  //Animator pour cet ennemi
    private Vector2 down = new Vector2(0, -1);  //Vecteur pointant vers le "bas"

    [Header("Attributes")]
    public float speed; //Vitesse
    public int maxHealth;   //Vie maximum
    public int worth;   //Argent gagné quand vaincu

    [Header("Unity stuff")]
    public Image healthBar; //Barre de vie

    public GameObject currencytextprefab;   //Affichage texte gain d'argent

    void Start()    //Méthode appelée à la création de l'objet
    {
        target = waypoints.points[1];   //Ciblage du premier waypoint
        anim = GetComponent<Animator>();    //Récupération de l'animator
        health = maxHealth; //Initialisation de la vie
    }

    void Update(){  //Méthode appelée une fois par frame
        Vector2 dir = target.position - transform.position; //Mise à jour de la direction vers le waypoint cible
        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World);  //Déplacement vers le waypoint
        float angle = Vector2.SignedAngle(dir, down);   //Calcul de l'angle entre le 'regard' et le vecteur down
        anim.SetInteger("direction", (int)((angle + 180 + 45) / 90)%4); //Mise à jour de 'direction' pour l'affichage de l'ennemi

        if(Vector2.Distance(transform.position, target.position) <= 0.2f){  //Si suffisament proche du waypoint cible
            GetNextWaypoint();  //Récupérer le prochain waypoint
        }

        
    }

    void GetNextWaypoint(){ //Récupération du prochain waypoint
        if(waypointIndex >= waypoints.points.Length - 1)    //Si il n'existe pas d'autre waypoints
        {
            Finish();   //Finir
            return; 
        }
        waypointIndex++;    //Sinon
        target = waypoints.points[waypointIndex];   //Récupération du prochain waypoint
    }

    void Finish(){  //Méthode appelée quand l'ennemi atteint la fin
        Destroy(gameObject);    //Destuction de l'objet
    }

    public void Die(){  //Méthode appelée quand l'ennemi est détruit par une tour
        GameObject currencytextGO = Instantiate(currencytextprefab, transform.position, Quaternion.Euler(0,0,0));   //Creation du texte de gain d'argent
        currencyText currencytext = currencytextGO.GetComponent<currencyText>();    //Récupération de la partie script
        if(currencytext != null){   //Si l'objet existe
            currencytext.display(worth);    //Affichage de l'argent gagné
        }
        Destroy(gameObject);    //Destruction de l'objet
        return;
    }

    public void TakeDamage(int damage){ //Méthode appelée quand l'ennemi prends des dégats
        health -= damage;   //Mise à jour de la vie
        healthBar.fillAmount = ((float)health)/maxHealth;   //Mise à jour du visuel de la barre de vie
        if(health <= 0){    //Si il ne reste plus de vie
            Die();  //Appel de la Méthode
        }
    }
}
