using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    [SerializeField] float speed;
    [SerializeField] float paddleSize;
    [SerializeField] float lazerSpeed;
    [SerializeField] float firingRate;
    [SerializeField] float health;
    [SerializeField] GameObject playerLazer;
    [SerializeField] AudioClip fire;
    private SceneLoader scene;

    float xmin;
    float xmax;

    void Fire() {
        
        GameObject Lazer = Instantiate(playerLazer, transform.position, Quaternion.identity) as GameObject;
        Lazer.GetComponent<Rigidbody2D>().velocity = new Vector3(0, lazerSpeed, 0);
        AudioSource.PlayClipAtPoint(fire, transform.position, 5f);
    }
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + paddleSize;
        xmax = rightmost.x - paddleSize;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if(Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("Fire");
        }

        
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile) {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0) {
                Die();
            }
        }
    }
    void Die() {
        scene = GameObject.Find("Scene Loader").GetComponent<SceneLoader>();
        Destroy(gameObject);
        scene.LoadNextScene("Lose Game");
    }
}
