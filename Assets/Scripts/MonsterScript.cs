using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{   
    public Transform Player;
    public GameObject bullet;

    public ParticleSystem deathExplosion;

    public ParticleSystem flameCrown;

    public bool isDead;

    public float speed = 1f; 
    Vector3 newPosition;

    public PlayerStats playerStats;
    
 
    void PositionChange()
    {
        newPosition = new Vector2(Random.Range(Player.position.x -5.0f, Player.position.x + 5.0f), Random.Range(-5.0f, 5.0f));
    }
   
    void Start()
    {   
        playerStats = Player.GetComponent<PlayerStats>();
        PositionChange();
        StartCoroutine(ShootPlayer());  
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, newPosition) < 1)
            PositionChange();
 
        transform.position=Vector3.Lerp(transform.position,newPosition,Time.deltaTime*speed);
    }

    public IEnumerator ShootPlayer(){
        if (!isDead){
            if (playerStats.alive){
                GameObject instanceBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                instanceBullet.GetComponent<Rigidbody2D>().velocity = (Player.position - transform.position).normalized * 15;
            }
            yield return new WaitForSeconds(1f);
            StartCoroutine(ShootPlayer());
        }
    }
    
    public void Die(){
        deathExplosion.Play();
        isDead = true;
        SpriteRenderer[] array = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in array){
            sprite.enabled = false;
        }
        flameCrown.Stop();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
