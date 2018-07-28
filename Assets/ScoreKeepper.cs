using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeepper : MonoBehaviour {

    [SerializeField] Text scoreKeepper;
    public static int score = 0; 
    

	// Use this for initialization
	void Start () {
        scoreKeepper = GetComponent<Text>();
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Score(int points) {
        score += points;
        scoreKeepper.text = score.ToString();
    }

    public static void Reset() {
        score = 0;
    }


}
