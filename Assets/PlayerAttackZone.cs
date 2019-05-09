using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackZone : MonoBehaviour
{
    public Animator playerAnim;
    public PlayerStats playerStats;
    public void OnTriggerStay2D(Collider2D other){
        if (other.tag == "Monster"){
            if (playerAnim.GetBool("Attack")){
                other.GetComponent<MonsterScript>().Die();
                playerStats.UpdateKillStreak();
                playerStats.GetSouls((playerStats.killStreak + 1) * 10);
            }
        }
    }
}
