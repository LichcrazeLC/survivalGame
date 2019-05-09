using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAttackZone : MonoBehaviour
{

    public Animator playerAnim;
    public PlayerStats playerStats;
    public Demo demo;
    public void OnTriggerStay2D(Collider2D other){
          if (demo.bigSlash){
                demo.bigSlash = false;
                if (other.tag == "Monster"){     
                other.GetComponent<MonsterScript>().Die();
                playerStats.UpdateKillStreak();
                playerStats.GetSouls((playerStats.killStreak + 1) * 10);
            }
        }
    }
}
