using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage : MonoBehaviour
{

    

    [Header ("Attributes")]
    public float range = 3f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform target;
    public Image attackBar;

    public GameObject normal_projectilePrefab;
    public Transform firePoint;
    Animator anim;
    private Vector2 down = new Vector2(0, -1);

    public float angle;

    private bool attackReady;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        anim = GetComponent<Animator>();
        updateSpriteRotation(down);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("ChargeAttack");
        if(target == null){
            return;
        }

        Vector2 dir = target.position - transform.position;
        updateSpriteRotation(dir);
        if(attackReady){
            Shoot(dir);
            attackReady = false;
            fireCountdown = 1f / fireRate;
        }

    }

    void UpdateTarget(){

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies){
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance){
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range){
            target = nearestEnemy.transform;
        }else{
            target = null;
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void updateSpriteRotation(Vector2 dir){
        angle = Vector2.SignedAngle(dir, down);
        //float angle = Vector2.SignedAngle(dir, down);
        anim.SetInteger("direction", (int)((angle + 180 + 45) / 90)%4);
    }
    
    void Shoot(Vector2 dir){
        dir.Normalize();
        GameObject projectileGO = Instantiate(normal_projectilePrefab, (Vector2)transform.position + dir, Quaternion.Euler(0,0,0));
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if(projectile != null){
            projectile.Seek(target);
        }
    }

    void ChargeAttack(){
        if (fireCountdown <= 0f)
        {
            attackReady = true;
        }

        if (!attackReady){
            fireCountdown -= Time.deltaTime;
        }
        
        attackBar.fillAmount = 1f - fireCountdown * fireRate;
    }


}
