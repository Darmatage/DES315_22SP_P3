using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownHandler : MonoBehaviour{

	private GameHandler gameHandlerObj;
	private string playerChoice;
	private string playerChoiceName;
	private int pNum;
	public bool isPlayer1 = false;
	public bool isNPC = false;

	void Awake(){
		if (GameObject.FindWithTag("GameHandler") != null){
				gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
			}
			
		if (isPlayer1 == true){pNum = 1;}
		else {pNum = 2;}
	}

    void Start(){
        var dropdown = transform.GetComponent<Dropdown>();
		dropdown.options.Clear();
		
		List<string> items1 = new List<string>();
		if (isNPC == false){
			items1.Add("");
			items1.Add("BotA00 basic");
			
			items1.Add("BotA01 Taher Kagzi");
			items1.Add("BotA02 Mac Roshak");
			items1.Add("BotA03 Hermie Choi");
			items1.Add("BotA04 Kiara Santiago");
			items1.Add("BotA05 Jakob Shumway");
			items1.Add("BotA06 Ben Mowry");
			items1.Add("BotA07 Matthew Mikhin");
			items1.Add("BotA08 Kenny Mecham");
			items1.Add("BotA09 Daniel Nunes");
			items1.Add("BotA10 Jirakit Jarusiripipat");
			items1.Add("BotA11 Taro Kumagai");
			items1.Add("BotA12 Scott Fado-Bristow");
			items1.Add("BotA13 Kai Kawashima");
			items1.Add("BotA14 Fredy Martin");
			items1.Add("BotA15 Christian Wookey");
			items1.Add("BotA16 Ben Thompson");
			items1.Add("BotA17 Kelson Wysocki");
			items1.Add("BotA18 Jason Hunt");
			items1.Add("BotA19 Kobe Dennis");
			items1.Add("BotA20 Cobe Colson");
			items1.Add("BotA21 Deante James");
			items1.Add("BotA22 Gary Chen");
			items1.Add("BotA23 Hyungseob Kim");
			items1.Add("BotA24 Damian Rouse");
			
			items1.Add("BotB01 Charles Osberg");
			items1.Add("BotB02 Jonathan Hamling");
			items1.Add("BotB03 Jessica Gramer");
			items1.Add("BotB04 Julian Blackstone");
			items1.Add("BotB05 Bar Ben-zvi");
			items1.Add("BotB06 Chris Dowell");
			items1.Add("BotB07 Wonju Jo");
			items1.Add("BotB08 Frankie Camarillo");
			items1.Add("BotB09 Lilyan Monroy");
			items1.Add("BotB10 Jackson Fischer");
			items1.Add("BotB11 Erin Scribner");
			items1.Add("BotB12 Daeun Jeong");
			items1.Add("BotB13 Braedan Nevers");
			items1.Add("BotB14 Grant Wu");
			items1.Add("BotB15 Eunjin Hong");
			items1.Add("BotB16 Seth Merrill");
			items1.Add("BotB17 Matt Walker");
			items1.Add("BotB18 Alex Jalonen");
			items1.Add("BotB19 Jacob Burke");
			items1.Add("BotB20 Cole Schwinghammer");
			items1.Add("BotB21 Carlos Fernandez");
			items1.Add("BotB22 Ty Janis");
			items1.Add("BotB23 Riley Bird");
		}else if (isNPC == true){
			items1.Add("");
			items1.Add("NPC_A00");
			items1.Add("NPC_A10 Jirakit Jarusiripipat");
			items1.Add("NPC_A24 Damian Rouse");
			//items1.Add("NPC_A03 Jiwon Jung");
			//items1.Add("NPC_A19 Zacary Brown");
			//items1.Add("NPC_B05 Carlos Garcia-Perez");
			//items1.Add("NPC_B13 Skyler Powers");
			//items1.Add("NPC_B16 Chase Graves");
		}
		
		List<string> items2 = new List<string>();
		if (isNPC == false){
			items2.Add("");
			items2.Add("BotB00 basic");
			
			items2.Add("BotB01 Charles Osberg");
			items2.Add("BotB02 Jonathan Hamling");
			items2.Add("BotB03 Jessica Gramer");
			items2.Add("BotB04 Julian Blackstone");
			items2.Add("BotB05 Bar Ben-zvi");
			items2.Add("BotB06 Chris Dowell");
			items2.Add("BotB07 Wonju Jo");
			items2.Add("BotB08 Frankie Camarillo");
			items2.Add("BotB09 Lilyan Monroy");
			items2.Add("BotB10 Jackson Fischer");
			items2.Add("BotB11 Erin Scribner");
			items2.Add("BotB12 Daeun Jeong");
			items2.Add("BotB13 Braedan Nevers");
			items2.Add("BotB14 Grant Wu");
			items2.Add("BotB15 Eunjin Hong");
			items2.Add("BotB16 Seth Merrill");
			items2.Add("BotB17 Matt Walker");
			items2.Add("BotB18 Alex Jalonen");
			items2.Add("BotB19 Jacob Burke");
			items2.Add("BotB20 Cole Schwinghammer");
			items2.Add("BotB21 Carlos Fernandez");
			items2.Add("BotB22 Ty Janis");
			items2.Add("BotB23 Riley Bird");
			
			items2.Add("BotA01 Taher Kagzi");
			items2.Add("BotA02 Mac Roshak");
			items2.Add("BotA03 Hermie Choi");
			items2.Add("BotA04 Kiara Santiago");
			items2.Add("BotA05 Jakob Shumway");
			items2.Add("BotA06 Ben Mowry");
			items2.Add("BotA07 Matthew Mikhin");
			items2.Add("BotA08 Kenny Mecham");
			items2.Add("BotA09 Daniel Nunes");
			items2.Add("BotA10 Jirakit Jarusiripipat");
			items2.Add("BotA11 Taro Kumagai");
			items2.Add("BotA12 Scott Fado-Bristow");
			items2.Add("BotA13 Kai Kawashima");
			items2.Add("BotA14 Fredy Martin");
			items2.Add("BotA15 Christian Wookey");
			items2.Add("BotA16 Ben Thompson");
			items2.Add("BotA17 Kelson Wysocki");
			items2.Add("BotA18 Jason Hunt");
			items2.Add("BotA19 Kobe Dennis");
			items2.Add("BotA20 Cobe Colson");
			items2.Add("BotA21 Deante James");
			items2.Add("BotA22 Gary Chen");
			items2.Add("BotA23 Hyungseob Kim");
			items2.Add("BotA24 Damian Rouse");
		}else if (isNPC == true){
			items2.Add("");
			items2.Add("NPC_A00");
			items2.Add("NPC_A10 Jirakit Jarusiripipat");
			items2.Add("NPC_A24 Damian Rouse");
			//items2.Add("NPC_B16 Chase Graves");
			//items2.Add("NPC_A01 Oussama Khalaf");
			//items2.Add("NPC_A02 Ryan Heath");
			//items2.Add("NPC_A03 Jiwon Jung");
			//items2.Add("NPC_A19 Zacary Brown");
		}

		//fill dropdown with items
		if (pNum == 1){
			foreach(var item in items1){
				dropdown.options.Add(new Dropdown.OptionData(){text = item});
			}
		}
		else if (pNum == 2){
			foreach(var item in items2){
				dropdown.options.Add(new Dropdown.OptionData(){text = item});
			}
		}
		
		DropdownItemSelected(dropdown);
		dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
		
		 //This script should be attached to Item
		 Toggle toggle = gameObject.GetComponent<Toggle>();
		 Debug.Log(toggle);
		 if (toggle != null && toggle.name == "Item 1: Option B"){
		 		toggle.interactable = false;
		 }
    }

    void DropdownItemSelected(Dropdown dropdown){
        int index = dropdown.value;
		//string firstWord = item.Split(' ')[0]; //the .Split part IDs the first word, so names can be added
		playerChoice = dropdown.options[index].text.ToString().Split(' ')[0];
		playerChoiceName = dropdown.options[index].text.ToString();
		
		Debug.Log("Player " + pNum + " Choice: " + playerChoiceName);
		
		if (pNum == 1){
			gameHandlerObj.p1PrefabName = playerChoice;
			gameHandlerObj.p1PlayerChoiceName = playerChoiceName;
			
			if (playerChoice.Length > 1){
				if (playerChoice.Substring(0, 1) == "N"){
					gameHandlerObj.isP1_NPC = true;
				} else {gameHandlerObj.isP1_NPC = false;}
			}
		}
		else if (pNum == 2){
			gameHandlerObj.p2PrefabName = playerChoice;
			gameHandlerObj.p2PlayerChoiceName = playerChoiceName;
			
			if (playerChoice.Length > 1){
				if (playerChoice.Substring(0, 1) == "N"){
					gameHandlerObj.isP2_NPC = true;
				} else {gameHandlerObj.isP2_NPC = false;}
			}
		}	
    }
}
