using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player" &&  !other.GetComponent<PlayerStats>().shielded){
            other.GetComponent<PlayerStats>().GetShot(10);
            Debug.Log("Player Got Shot!");
        }
    }
}
