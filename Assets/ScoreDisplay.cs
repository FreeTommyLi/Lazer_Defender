using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class ScoreDisplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI guessText;

    // Use this for initialization
    void Start () {
        guessText.text = ScoreKeepper.score.ToString(); ;
        ScoreKeepper.Reset();


    }
	
}
