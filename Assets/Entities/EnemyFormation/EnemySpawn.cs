using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] float speed;
    [SerializeField] float spawnDelay;

    private bool movingRight = true;

    float xmin;
    float xmax;


    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + 0.5f * width;
        xmax = rightmost.x - 0.5f * width;

        SpawnFullUnity();
        
	}

    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {
		if(movingRight) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }


        if(transform.position.x >= xmax) {
            movingRight = false;
        } 
        else if(transform.position.x <= xmin) {
            movingRight = true;
        }
        
        if(AllMembersDead()) {
            SpawnFullUnity();
        }
    }

    void Respawn() {
        

        foreach (Transform child in transform) {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void SpawnFullUnity() {
        Debug.Log("1 times");
        Transform freePosition = NextFreePosition();
        if(freePosition) {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition()) {
            Invoke("SpawnFullUnity", spawnDelay);
        }
        
    }

    Transform NextFreePosition() {
        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount <= 0) {
                return childPositionGameObject;
            }
            
        }
        return null;
    }

    bool AllMembersDead() {
        foreach(Transform childPositionGameObject in transform) {
            if(childPositionGameObject.childCount > 0) {
                return false;
            }
        }
        return true;
    }

}
