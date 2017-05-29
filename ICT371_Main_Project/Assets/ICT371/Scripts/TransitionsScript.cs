using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsScript : MonoBehaviour {
    bool open = true, gsFound = false, safeFound = false, knifeFound = false;
    int flag = 0;
    int Score = 0;
    Vector3 murderScene = new Vector3(-29.4f, 0.4f, -48.6f);
    Vector3 startPos = new Vector3(-5.0f, 0.5f, 2.0f);
    Vector3 quiz1Pos = new Vector3(-3.0f, 0.5f, 2.8f);
    Quaternion quizFace = new Quaternion(0.0f, 0.7f, 0.0f, 0.7f);
    Quaternion startFace = new Quaternion(0.0f, -0.7f, 0.0f, 0.7f);

	// Use this for initialization
	void Start () {
        open = true;
        string text = "Use the WASD keys to move. \n Right click to interact with objects and people. \n When you're done with a crime scene, interact with the door to exit.";
        DialogManager.PopUpDialog("New Game! Instructions: ", text, DialogManager.DialogType.OkDialog);
        transform.position = startPos;
        transform.rotation = startFace;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = transform.position;
        
        //for Closing Dialogue windows and Advancing plot flags
        if (DialogManager.showingDialog && Input.GetMouseButtonDown(0) && (flag != 11))
        {
            if(open){
                DialogManager.instance.DismissDialog(true);
                flag++;
                open = false;
            } else {
                open = true;
            }
        }

        //Starting Dialogues
        if (!DialogManager.showingDialog && !open && Input.GetMouseButtonUp(0) && flag == 1)
        {
            //Debug.Log("yay?"); //Testing
            DialogManager.PopUpDialog("Objective 1: ", "Objective 1: Speak to the Inspector", DialogManager.DialogType.OkDialog);
        }

        //Displays current location
        Debug.Log(transform.position + " " + transform.rotation); // For debugging

        //Talk to Inspector dude
        if (Vector3.Distance(playerPos, GameObject.Find("Person (5)").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog)
        {
            string text = "Welcome, recruit. This is your last test before you are sent out into the real world. Just try your best to find all the evidence you can and take your time better to be thorough than miss something, note down what you find and what the witness tells you and after this is done get ready to fill in some paperwork, we will be grading you according to that";
            DialogManager.PopUpDialog("Inspector:", text, DialogManager.DialogType.OkDialog);

        }

        if (!DialogManager.showingDialog && !open && Input.GetMouseButtonUp(0) && flag == 3)
        {
            //Debug.Log("yay?"); //Testing
            DialogManager.PopUpDialog("Objective 2: ", "Objective 2: Explore the crime scene and take notes to solve the crime!", DialogManager.DialogType.OkDialog);
        }

        //Talk to Robbery Witness
        if (Vector3.Distance(playerPos, GameObject.Find("Person (2)").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag >= 4)
        {
            //Debug.Log("clicked!"); // for testing
            string text = "Uh, well, there was a knock on the door, and when I went to answer it, I heard the window break, so I ran back inside to see what it was. When I got back to my study, the safe was open, and some guy in black was going out the window. I tried to chase him, but he got away on a black motorbike, and before you ask, no, I don’t remember the licence plate.";
            DialogManager.PopUpDialog("Witness:", text, DialogManager.DialogType.OkDialog);
        }

        if (Vector3.Distance(playerPos, GameObject.Find("GlassShards").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag >= 4)
        {
            //Debug.Log("clicked!"); // for testing
            string text = "Glass shards from the Broken Window.\n Scatter pattern suggests it was broken from the outside, and the lack of blood means the perpetrator was covered or used something to break it.";
            if (!gsFound)
            {
                gsFound = true;
                text += "\nScore +100.";
                Score += 100;
            }
            DialogManager.PopUpDialog("Witness:", text, DialogManager.DialogType.OkDialog);
        }

        if (Vector3.Distance(playerPos, GameObject.Find("Safe").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag >= 4)
        {
            //Debug.Log("clicked!"); // for testing
            string text = "The safe that was broken into. Currently Empty.\nNo signs of forced entry, but there are fingerprints on the door, suggesting that the thief knew the code.";
            if (!safeFound)
            {
                safeFound = true;
                text += "\nScore +100.";
                Score += 100;
            }
            DialogManager.PopUpDialog("Witness:", text, DialogManager.DialogType.OkDialog);
        }

        //show Score
        if (Vector3.Distance(playerPos, GameObject.Find("Door").transform.position) < 2.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag >= 3)
        {
            flag = 10;
            DialogManager.PopUpDialog("Score: ", "Current Score: " + Score.ToString(), DialogManager.DialogType.OkDialog);
        }

        //Goto Quiz here
        if (!DialogManager.showingDialog && !open && Input.GetMouseButtonUp(0) && flag == 11)
        {
            //Debug.Log("yay?"); //Testing
            transform.position = quiz1Pos;
            transform.rotation = quizFace;
            DialogManager.PopUpDialog("The Paperwork: ", "And now, for the paperwork: ", DialogManager.DialogType.OkDialog);
        }

        //Goto Quiz here
        if (!DialogManager.showingDialog && !open && Input.GetMouseButtonUp(0) && flag == 11)
        {
            //Debug.Log("yay?"); //Testing
            transform.position = quiz1Pos;
            transform.rotation = quizFace;
            string text = "Question 1: Where was the Witness when the robber opened the safe?\nOptions: 1) In the room. 2) At the door.\nPress the correct answer on the Keyboard.";
            DialogManager.PopUpDialog("The Paperwork: ", text, DialogManager.DialogType.OkDialog);
            open = true;
        }

        if (DialogManager.showingDialog && flag == 11 && Input.GetKeyDown(KeyCode.Alpha1))
        {
            flag++;
            Score += 150;
            DialogManager.instance.DismissDialog(true);
            open = false;
        }
	}
}
