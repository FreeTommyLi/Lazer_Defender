using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    [SerializeField] public float health;
    [SerializeField] public float speed;
    [SerializeField] float shotsPerSeconds;
    [SerializeField] GameObject enemyLazer;
    [SerializeField] int scoreValue;
    private ScoreKeepper scoreKeeper;
    [SerializeField] AudioClip fire;
    [SerializeField] AudioClip die;






    void OnTriggerEnter2D(Collider2D collision) {

        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if(missile) {
            health -= missile.GetDamage();
            missile.Hit();
            if(health <= 0) {
                Destroy(gameObject);
                scoreKeeper.Score(scoreValue);
                AudioSource.PlayClipAtPoint(die, transform.position, 5f);
            }
        }
    }

    void Shot() {
        
        GameObject shot = Instantiate(enemyLazer, transform.position , Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed);
        
    }



    void Start() {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeepper>();
     
    }


    void Update() {
        float probability = Time.deltaTime * shotsPerSeconds;
        if(Random.value < probability) {
            Shot();
            AudioSource.PlayClipAtPoint(fire, transform.position, 5f);
        }
    }






}
