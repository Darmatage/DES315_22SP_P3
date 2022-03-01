using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotBasic_displayID : MonoBehaviour{
	private bool isPlayer1;
	private Transform cam;
	public Transform playerLabel;
	public GameObject imageP1;
	public GameObject imageP2;
	
	private GameHandler gameHandlerObj;
	Scene thisScene;
	
	void Awake(){
		// check for endscene
		thisScene = SceneManager.GetActiveScene();
		if (thisScene.name == "EndScene"){
			gameObject.GetComponent<BotBasic_displayID>().enabled = false;
		}
		
		if (GameObject.FindWithTag("GameHandler") != null){
			gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
		}
	}
	
    void Start(){		
		if (gameHandlerObj.isShowcase == true){
			//Debug.Log("Hey, ID has discover this is a showcase scene!");
			gameObject.GetComponent<BotBasic_displayID>().enabled = false;
		}
		
		isPlayer1 = gameObject.transform.root.GetComponent<playerParent>().isPlayer1;
		
        if ((isPlayer1 == true)&&(GameObject.FindWithTag("camP1") != null)){
			cam = GameObject.FindWithTag("camP1").transform;
			imageP1.SetActive(true);
			imageP2.SetActive(false);
			playerLabel = imageP1.transform;
		}   else if (GameObject.FindWithTag("camP2") != null){
			cam = GameObject.FindWithTag("camP2").transform;
			imageP1.SetActive(false);
			imageP2.SetActive(true);
			playerLabel = imageP2.transform;
		}
    }
	
	void Update(){
		playerLabel.LookAt(cam);	
	}	
}
