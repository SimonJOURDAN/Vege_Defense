using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int damage = 10;    //Dégats infligés  à la cible

    private Transform target;   //Cible

    public float flight_speed = 20f;    //Vitesse de vol

    private Ennemy ennemy;  //Ennemi ciblé

    public void Seek(Transform set_target){ //Méthode pour assigner une cible
        target = set_target;    //Initialisation de la cible
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)  //Si la cible n'existe plus
        {
            Destroy(gameObject);    //Destruction du projectile
            return;
        }
        
        Vector2 dir = target.position - transform.position; //Calcul de la direction de la cible
        float distanceThisFrame = flight_speed * Time.deltaTime;    //Calcul du déplacement vers la cible

        if(dir.magnitude <= distanceThisFrame){ //Si le projectile doit toucher pendant cette frame
            HitTarget();    //Toucher la cible
            return;
        }else{
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);   //Déplacement vers la cible
        }   
    }

    void HitTarget(){   //Méthode pour toucher la cible
        ennemy = target.GetComponent<Ennemy>(); //Récupération de la partie script de la cible
        ennemy.TakeDamage(damage);  //Appel de la méthode prendre des dégats de la cible
        Destroy(gameObject);    //Destruction du projectile
        return;
    }
}
