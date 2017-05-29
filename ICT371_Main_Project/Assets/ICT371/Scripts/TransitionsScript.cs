using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsScript : MonoBehaviour {
    bool open = true, gsFound = false, safeFound = false, knifeFound = false, corpseFound = false;
    bool inter1 = false, inter2 = false;
    int flag = 0;
    int Score = 0, ScoreTotal = 0;
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
        transform.rotation = new Quaternion(0.0f, -0.7f, 0.0f, 0.7f);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = transform.position;
        
        //for Closing Dialogue windows and Advancing plot flags
        if (DialogManager.showingDialog && Input.GetMouseButtonDown(0) && flag < 12)
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
        if (!DialogManager.showingDialog && !open && Input.GetMouseButtonUp(0) && flag == 12)
        {
            //Debug.Log("yay?"); //Testing
            transform.position = quiz1Pos;
            transform.rotation = quizFace;
            string text = "Question 1: Where was the Witness when the robber opened the safe?\nOptions: 1) In the room. 2) At the door.\nPress the correct answer on the Keyboard.";
            DialogManager.PopUpDialog("The Paperwork: ", text, DialogManager.DialogType.OkDialog);
            open = true;
        }

        if (DialogManager.showingDialog && flag == 12 && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)))
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Score += 150;
            }
            flag++;
            DialogManager.instance.DismissDialog(true);
            open = false;
        }

        if (!DialogManager.showingDialog && flag == 13 && ((Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2))))
        {
            flag++;
        }

        if (!DialogManager.showingDialog && flag == 14)
        {
            string text = "Question 2: What was the Witness doing?\nOptions: 1) Held hostage. 2) Getting Newspaper. 3) Responding to a sound.\nPress the correct answer on the Keyboard.";
            DialogManager.PopUpDialog("The Paperwork: ", text, DialogManager.DialogType.OkDialog);
            open = true;
        }

        if (DialogManager.showingDialog && flag == 14 && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)))
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Score += 150;
            }
            flag++;
            DialogManager.instance.DismissDialog(true);
            open = false;
        }

        if (!DialogManager.showingDialog && flag == 15 && (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha3)))
        {
            flag++;
        }

        if (!DialogManager.showingDialog && flag == 16)
        {
            string text = "Question 3: What sort of clothing was the Robber wearing?\nOptions: 1) Black Clothes. 2) White Clothes. 3) Tie-Dye Clothing. 4) Shirtless\nPress the correct answer on the Keyboard.";
            DialogManager.PopUpDialog("The Paperwork: ", text, DialogManager.DialogType.OkDialog);
            open = true;
        }

        if (DialogManager.showingDialog && flag == 16 && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Score += 150;
            }
            flag++;
            DialogManager.instance.DismissDialog(true);
            open = false;
        }

        if (!DialogManager.showingDialog && flag == 17 && (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Alpha4)))
        {
            flag++;
            ScoreTotal += Score;
            Score = 0;
        }

        if (!DialogManager.showingDialog && flag == 18)
        {
            string text = "That's the paperwork done for now, congratulations on graduating.";
            DialogManager.PopUpDialog("The Paperwork: ", text, DialogManager.DialogType.OkDialog);
            open = true;
        }

        //for Closing Dialogue windows and Advancing plot flags
        if (DialogManager.showingDialog && Input.GetMouseButtonDown(0) && flag >= 18 && flag <= 31)
        {
            if (open)
            {
                DialogManager.instance.DismissDialog(true);
                flag++;
                open = false;
            }
            else
            {
                open = true;
            }
        }

        if (!DialogManager.showingDialog && flag == 19)
        {
            transform.position = new Vector3(-3.1f, 0.5f, -0.3f);
            transform.rotation = new Quaternion(0.0f, 0.7f, 0.0f, 0.7f);
            string text = "Okay, this time, you’ll be going out solo. There’s been a murder, and I need you to find out which of the two suspects did it. You won’t have anyone watching over you this time, so you’d better get it right. Got it? Okay, here’s the address…";
            DialogManager.PopUpDialog("Commissioner: ", text, DialogManager.DialogType.OkDialog);
        }

        if (!DialogManager.showingDialog && flag == 20)
        {
            transform.position = murderScene;
            transform.rotation = startFace;
            string text = "Objective: Investigate the Murder Scene!";
            DialogManager.PopUpDialog("Objective: ", text, DialogManager.DialogType.OkDialog);
        }

        if (Vector3.Distance(playerPos, GameObject.Find("Knife").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag > 20)
        {
            //Debug.Log("clicked!"); // for testing
            string text = "A knife covered in blood has been hidden under the chair.\nIt matches the size of the wounds in the victim, and is probably the murder weapon.";
            if (!knifeFound)
            {
                knifeFound = true;
                text += "\nScore +150.";
                Score += 150;
            }
            DialogManager.PopUpDialog("Evidence!", text, DialogManager.DialogType.OkDialog);
        }

        if (Vector3.Distance(playerPos, GameObject.Find("Person").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag > 20)
        {
            //Debug.Log("clicked!"); // for testing
            string text = "The corpse of the Victim. The victim has been stabbed in the chest from the front.";
            if (!corpseFound)
            {
                corpseFound = true;
                text += "\nScore +50.";
                Score += 50;
            }
            DialogManager.PopUpDialog("Evidence!", text, DialogManager.DialogType.OkDialog);
        }

        if (Vector3.Distance(playerPos, GameObject.Find("Person (4)").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag >= 4)
        {
            //Debug.Log("clicked!"); // for testing
            string text = "Sorry, kid, not allowed to help you here.";
            DialogManager.PopUpDialog("Inspector:", text, DialogManager.DialogType.OkDialog);
        }

        if (Vector3.Distance(playerPos, GameObject.Find("Person (1)").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag >= 4)
        {
            //Debug.Log("clicked!"); // for testing
            string text;
            if (!inter1)
            {
                text = "Witness 1: [Well, I was out in front of the house when it happened, but I ran in there as soon as I could when I heard the guy shout in pain. I got there first, but there was no-one else inside the room… I- I don’t know how it could have happened...]";
                inter1 = true;
            }
            else
            {
                text = "Witness 1: [Actually, now that I think about it, I think I heard someone coming down the stairs just before I started running. It might have been the killer…]";
            }
            
            DialogManager.PopUpDialog("Witness 1:", text, DialogManager.DialogType.OkDialog);
        }

        if (Vector3.Distance(playerPos, GameObject.Find("Person (3)").transform.position) < 3.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag >= 4)
        {
            //Debug.Log("clicked!"); // for testing
            string text;
            if (!inter2)
            {
                text = "Witness 2: [I was in my room, behind the stairs, for most of the day, but I didn’t hear anything until the other guy screamed for someone to call an ambulance. Kind of suspicious, I think…]";
                inter2 = true;
            }
            else
            {
                text = "Witness 2: [Look, I’ve told you it before, I was in my room, behind the stairs, for most of the day, but I didn’t hear anything until the other guy screamed for someone to call an ambulance. Do you have any other dumb questions?]";
            }

            DialogManager.PopUpDialog("Witness 2:", text, DialogManager.DialogType.OkDialog);
        }

        if (Vector3.Distance(playerPos, GameObject.Find("Door2").transform.position) < 2.0f && Input.GetMouseButtonDown(1) && !DialogManager.showingDialog && flag >= 20)
        {
            flag = 30;
            DialogManager.PopUpDialog("Score: ", "Current Score: " + Score.ToString(), DialogManager.DialogType.OkDialog);
        }

        //Goto Quiz here
        if (!DialogManager.showingDialog && !open && Input.GetMouseButtonUp(0) && flag == 31)
        {
            //Debug.Log("yay?"); //Testing
            transform.position = quiz1Pos;
            transform.rotation = quizFace;
            DialogManager.PopUpDialog("The Paperwork: ", "And now, for the paperwork: ", DialogManager.DialogType.OkDialog);
        }

        //Goto Quiz here
        if (!DialogManager.showingDialog && !open && Input.GetMouseButtonUp(0) && flag == 32)
        {
            //Debug.Log("yay?"); //Testing
            transform.position = quiz1Pos;
            transform.rotation = quizFace;
            string text = "Question 1: Where was Witness 1 when the murder occurred?\nOptions: 1) Outside the house. 2) In his room, behind the stairs.\nPress the correct answer on the Keyboard.";
            DialogManager.PopUpDialog("The Paperwork: ", text, DialogManager.DialogType.OkDialog);
            open = true;
        }

        if (DialogManager.showingDialog && flag == 32 && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Score += 150;
            }
            flag++;
            DialogManager.instance.DismissDialog(true);
            open = false;
        }

        if (!DialogManager.showingDialog && flag == 33 && ((Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2))))
        {
            flag++;
        }

        if (!DialogManager.showingDialog && flag == 34)
        {
            string text = "Question 2: How did the victim die?\nOptions: 1) Shot. 2) Stabbed. 3) Choked.\nPress the correct answer on the Keyboard.";
            DialogManager.PopUpDialog("The Paperwork: ", text, DialogManager.DialogType.OkDialog);
            open = true;
        }

        if (DialogManager.showingDialog && flag == 34 && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)))
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Score += 150;
            }
            flag++;
            DialogManager.instance.DismissDialog(true);
            open = false;
        }

        if (!DialogManager.showingDialog && flag == 35 && (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)))
        {
            flag++;
        }

        if (!DialogManager.showingDialog && flag == 36)
        {
            string text = "Question 3: Who found the victim first?\nOptions: 1) Witness 1. 2) Witness 2.\nPress the correct answer on the Keyboard.";
            DialogManager.PopUpDialog("The Paperwork: ", text, DialogManager.DialogType.OkDialog);
            open = true;
        }

        if (DialogManager.showingDialog && flag == 36 && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Score += 150;
            }
            flag++;
            DialogManager.instance.DismissDialog(true);
            open = false;
        }

        if (!DialogManager.showingDialog && flag == 37 && (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2)))
        {
            flag++;
            ScoreTotal += Score;
        }

        if (!DialogManager.showingDialog && flag == 38)
        {
            string text = "And that's the end of the Game.\nYour Score for this stage was: " + Score + "\n And your Final Score total Score was: " + ScoreTotal + "\nHope You had Fun! (^_^)";
            DialogManager.PopUpDialog("The End", text, DialogManager.DialogType.OkDialog);
        }

        if (DialogManager.showingDialog && flag == 38 && Input.GetMouseButtonDown(0))
        {
            
            flag++;
            DialogManager.instance.DismissDialog(true);
            open = false;
            Application.Quit();
        }
	}
}
