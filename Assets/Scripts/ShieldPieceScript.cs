using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPieceScript : MonoBehaviour
{   
    public ParticleSystem glow;
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            other.GetComponent<PlayerStats>().AddShieldPiece(1);
            GetComponent<ParticleSystem>().Play();
            SpriteRenderer[] array = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in array){
                sprite.enabled = false;
            }
            GetComponent<CircleCollider2D>().enabled = false;
            glow.Stop();
        }
    }
}
