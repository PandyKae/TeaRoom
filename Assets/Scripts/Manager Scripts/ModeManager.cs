using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public string mode;
    private GameManager gm;
    public List<GameObject> gameOnUI;
    public List<GameObject> gameOffUI;
    public List<GameObject> chatModeUI;
    public List<GameObject> convoOffUI;
    public List<GameObject> convoOnUI;
    public List<GameObject> people;
    public List<Camera> cameras;
    public List<GameObject> responseButtons;
    public List<GameObject> responseButtonTexts;
    public Character curPerson;
    public CharCatman catman;
    public Intro intro;
    public int pplTalkedTo;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        StartIntro();
        responseButtons[0].SetActive(false);
        responseButtons[1].SetActive(false);
        responseButtons[2].SetActive(false);
    }

    private void StartIntro(){
        intro.StartIntro();
    }

    public void StartRound(){
        mode = "play";
        // Switch to main cam
        foreach(Camera cam in cameras){
            cam.gameObject.SetActive(false);
        }
        cameras[6].gameObject.SetActive(true);
        // Turn off chat UI
        foreach(GameObject ui_element in chatModeUI){
            ui_element.SetActive(false);
        }
        // Turn on game UI
        foreach(GameObject ui_element in gameOnUI){
            ui_element.SetActive(true);
        }
        // Turn off hitboxes
        foreach(GameObject person in people){
            person.GetComponent<BoxCollider2D>().enabled = false;
        }
        gm.GetComponent<GameManager>().enabled = true;
        gm.SetupMatch();
    }

    public void StartChatMode(){
        mode = "chat";
        pplTalkedTo = 0;
        cameras[0].gameObject.SetActive(true);
        cameras[6].gameObject.SetActive(false);
        foreach(GameObject ui_element in gameOffUI){
            ui_element.SetActive(false);
        }
        gm.GetComponent<GameManager>().enabled = false;
        chatModeUI[4].SetActive(true); // click ppl text

        // Turn on hitboxes
        foreach(GameObject person in people){
            person.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void StartConversation(){
        foreach(GameObject ui_element in convoOnUI){
            ui_element.SetActive(true);
        }

        // Turn off hitboxes
        foreach(GameObject person in people){
            person.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void EndConversation(){
        pplTalkedTo++;
        foreach(GameObject ui_element in convoOffUI){
            ui_element.SetActive(false);
        }

        // Turn on hitboxes
        foreach(GameObject person in people){
            person.GetComponent<BoxCollider2D>().enabled = true;
        }
        // Except for who you just talked to
        curPerson.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        if (pplTalkedTo == 2){
            //cat interrupt
            catman.StartTalk();
        }
    }

    public void Response1(){
        curPerson.Response1();
    }
    public void Response2(){
        curPerson.Response2();
    }
    public void Response3(){
        curPerson.Response3();
    }

    public void SkipDialogue(){
        curPerson.SkipText();
    }
}
