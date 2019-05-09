using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public int currentHealth;
    public Image healthSlider;
    public ParticleSystem gotHit;
    public int killStreak;
    public Text killStreakCount;
    public Animator killStreakCountAnim;
    public GameObject killStreakObject;
    public float currentSouls;
    public Image soulSlider;
    public bool alive;
    public GameObject Monster;
    public Demo playerMovement;
    public ParticleSystem rampage;
    public GameObject rampageText;
    public ParticleSystem shield;
    public int shieldPieces;
    public bool shielded;
    public Text shieldPiecesAmount;
    public GameObject shieldPiece;
    public Text slashPiecesAmount;
    public GameObject slashPiece;
    public int slashPieces;
    public bool bigSlash;

    void Start()
    {   
        alive = true;
        currentHealth = 100;
        currentSouls = 10;
        GetSouls(0);
        StartCoroutine(spawnMonster());
        StartCoroutine(spawnShieldPiece());
        StartCoroutine(spawnSlashPiece());
    }

    public IEnumerator spawnShieldPiece(){
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        Instantiate(shieldPiece, new Vector2(Random.Range(transform.position.x -5.0f, transform.position.x + 5.0f), Random.Range(-4f, 5.0f)), Quaternion.identity);
        if (alive)
        StartCoroutine(spawnShieldPiece());
    }

    public IEnumerator spawnSlashPiece(){
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        Instantiate(slashPiece, new Vector2(Random.Range(transform.position.x -5.0f, transform.position.x + 5.0f), Random.Range(-4f, 5.0f)), Quaternion.identity);
        if (alive)
        StartCoroutine(spawnSlashPiece());
    }

    public IEnumerator spawnMonster(){
        yield return new WaitForSeconds(5f);
        GameObject monster = Instantiate(Monster, new Vector2(Random.Range(transform.position.x -5.0f, transform.position.x + 5.0f), Random.Range(-5.0f, 5.0f)), Quaternion.identity);
        monster.GetComponent<MonsterScript>().Player = this.transform;
        monster.GetComponent<MonsterScript>().playerStats = this;
        if (alive)
        StartCoroutine(spawnMonster());
    }

    public void GetShot(int damage){
        currentHealth -= damage;
        healthSlider.fillAmount = currentHealth / 100f;
        gotHit.Play();

        if (currentHealth <= 0){
            alive = false;
            playerMovement.Die();
        }
    }  

    public void AddShieldPiece(int amount){
        shieldPieces += amount;
        shieldPiecesAmount.text = "x" + shieldPieces;
    }

     public void AddSlashPiece(int amount){
        slashPieces += amount;
        slashPiecesAmount.text = "x" + slashPieces;
    }

    public void GetSouls(float amount){
        currentSouls += amount;
        soulSlider.fillAmount = currentSouls / 100f;
    }

    public void UpdateKillStreak(){
        killStreakObject.SetActive(true);
        killStreak++;
        killStreakCount.text = "x" + killStreak;
        killStreakCountAnim.SetTrigger("Inc");
        if (killStreak >= 3){
            rampage.Play();
        }
        StopCoroutine("resetKillStreak");
        StartCoroutine("resetKillStreak");
    }

    IEnumerator resetKillStreak(){
        yield return new WaitForSeconds(5);
        killStreak = 0;
        rampage.Stop();
        killStreakObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && shieldPieces > 0 && !shielded){
                AddShieldPiece(-1);
                if (currentSouls > 0){
                    shield.Play();
                    shielded = true;
                }
        }
        
        if (shielded){
            GetSouls(-5f * Time.deltaTime);
            if (currentSouls <= 0){
                currentSouls = 0;
                shielded = false;
                shield.Stop();
            }
        }
        
    }
}
