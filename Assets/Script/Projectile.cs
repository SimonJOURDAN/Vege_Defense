using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int damage = 10;

    private Transform target;

    public float flight_speed = 20f;

    private Ennemy ennemy;

    public void Seek(Transform set_target){
        target = set_target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = flight_speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }else{
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    void HitTarget(){
        ennemy = target.GetComponent<Ennemy>();
        ennemy.TakeDamage(damage);
        Destroy(gameObject);
        return;
    }
}
