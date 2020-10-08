using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour
{
    
    [Header("Variables")]
    private Transform target;
    private int health;
    private int waypointIndex = 1;
    private Animator anim;
    private Vector2 down = new Vector2(0, -1);

    [Header("Attributes")]
    public float speed;
    public int maxHealth;
    public int worth;

    [Header("Unity stuff")]
    public Image healthBar;

    public GameObject currencytextprefab;

    void Start()
    {
        target = waypoints.points[1];
        anim = GetComponent<Animator>();
        health = maxHealth; 
    }

    void Update(){
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World);
        float angle = Vector2.SignedAngle(dir, down);
        anim.SetInteger("direction", (int)((angle + 180 + 45) / 90)%4);

        if(Vector2.Distance(transform.position, target.position) <= 0.2f){
            GetNextWaypoint();
        }

        
    }

    void GetNextWaypoint(){
        if(waypointIndex >= waypoints.points.Length - 1)
        {
            Finish();
            return;
        }
        waypointIndex++;
        target = waypoints.points[waypointIndex];
    }

    void Finish(){
        Destroy(gameObject);
    }

    public void Die(){
        GameObject currencytextGO = Instantiate(currencytextprefab, transform.position, Quaternion.Euler(0,0,0));
        currencyText currencytext = currencytextGO.GetComponent<currencyText>();
        if(currencytext != null){
            currencytext.display(worth);
        }
        Destroy(gameObject);
        return;
    }

    public void TakeDamage(int damage){
        health -= damage;
        healthBar.fillAmount = ((float)health)/maxHealth;
        if(health <= 0){
            Die();
        }
    }
}
