using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LevelMessageHolder;
    public GameObject ControlMessageHolder;
    public GameObject MessageHolder;
    public TMPro.TextMeshProUGUI ObjectiveText;
    public TMPro.TextMeshProUGUI CurrentLevelText;
    public TMPro.TextMeshProUGUI AccomplishedObjectiveText;
    public GameObject minimap;
    public TMPro.TextMeshProUGUI ResetText;
    public bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject Pause;
    public GameObject Settings;
    public GameObject Exit;
    public static bool isPassingMessage = false;
    public GameObject MessagePassHolder;
    public TMPro.TextMeshProUGUI MessagePass;
    public static string MessageText = "";
    private Vector3 OPos;
    
    void Start()
    {
        //OPos = current player position
        OPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //if restarting level, reset everthing
        AccomplishedObjectiveText.text = SetAccomplishedObjectiveText(LevelManager.GetCurrentLevelIndex());
        ObjectiveText.text = SetObjectiveText(LevelManager.GetCurrentLevelIndex());
        CurrentLevelText.text = SetCurrentLevelText(LevelManager.GetCurrentLevelIndex());
        Invoke("KillLevelStartMessages", 5f);
        
        //DontDestroyOnLoad(gameObject);
    }
    void KillLevelStartMessages(){
        LevelMessageHolder.SetActive(false);
        ControlMessageHolder.SetActive(false);
        minimap.SetActive(true);
        //Destroy(LevelMessageHolder);
    }
    // Update is called once per frame
    void Update()
    {
        if(LevelManager.checkCurrentLevelAccomplished(LevelManager.GetCurrentLevelIndex())){
            Debug.Log("Level Objective Done");
            callOutMessage(); 
            // StartCoroutine(FadeOutTransition());
            LevelManager.SetCurrentLevelAccomplished(LevelManager.GetCurrentLevelIndex(), false);
            //Invoke("returnMain",3f);

            Invoke("LoadNextLevel",3f);
            
        }
        if(PlayerMovement.playerPosY < -10){
            ResetText.text = "You died...restarting level in 3 sec...";
            PromptManager.enableMouseInput = false;
            Invoke("Restartinglevel", 3f);
        }
        else{
            ResetText.text = "";
        }
        if(!isGamePaused && Input.GetKeyDown(KeyCode.P)){
            isGamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            ActivePauseMenu();
        }
        if(isPassingMessage){
            LeanTween.moveLocalX(MessagePassHolder, -400, 1f).setEase(LeanTweenType.easeInOutSine);
            MessagePass.text = MessageText;
            isPassingMessage = false;
            Invoke("returnMessage",5f);
        }
    }
    void returnMessage(){
        LeanTween.moveLocalX(MessagePassHolder, 0, 1f).setEase(LeanTweenType.easeInOutSine);
    }

    void LoadNextLevel(){
        if(SceneManager.GetActiveScene().buildIndex == 5){
            SceneTransition.LoadLevel=false;
            OnClickReturnMain();
            Invoke("NextLevelTrans",1.5f);

        }
        else if(SceneManager.GetActiveScene().buildIndex == 4){
                SceneTransition.LoadLevel=false;
                OnClickReturnMain();
                Invoke("NextLevelTrans",1.5f);
        }
        else{SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneTransition.LoadLevel=false;}
        
    }


    void Restartinglevel(){
        LevelManager.reloadLevel = true;
    }

    private void callOutMessage(){
        MessageHolder.SetActive(true);
        Invoke("killMessage", 2.0f);
    }
    // IEnumerator FadeOutTransition(){
    //     float ft = 0.0f;
    //     while(ft < 1.0f){
    //         ft += Time.deltaTime;
    //         Exit.GetComponent<Image>().color.a = Mathf.Lerp(Exit.GetComponent<Image>().color.a, 255f, ft);
    //     }
    //     yield return null;
    // }

    void killMessage(){
        //Destroy(MessageHolder);
        MessageHolder.SetActive(false);
    }
    string SetObjectiveText(int CurrentLevelIndex){
        switch(CurrentLevelIndex){
            case 1:
                return "Get the jump force and ..";
            case 2:
                return "Find out what happened here(Get info by touching books, reach doors to open them)";
            case 3:
                return "Find the white books and palette knife then Finish painting";
            case 4:
                return "Find the keys, escape from this house";
            case 5:
                return "TRY TO MOVE YOUR CHARACTER, and Find the Red Flower";
            default:
                return "";
        }
    }
    string SetCurrentLevelText(int CurrentLevelIndex){
        switch(CurrentLevelIndex){
            case 1:
                return "Day 1";
            case 2:
                return "Day 2";
            case 3:
                return "Day 3";
            case 4:
                return "Day 4";
            case 5:
                return "Tutorial";
            default:
                return "";
        }
    }
    string SetAccomplishedObjectiveText(int CurrentLevelIndex){
        switch(CurrentLevelIndex){
            case 1:
                return "You passed out after feeling dizzy.." + " Return to Main in 3 sec...";
            case 2:
                return "You passed out after feeling dizzy.." + " Return to Main in 3 sec...";
            case 3:
                return "You feel like everything is confused and you wake up.." + " Return to Main in 3 sec...";
            case 4:
                return "You escaped..."+ " Return to Main in 3 sec...";
            case 5:
                return "Tutorial Done!" + " Return to Main in 3 sec...";
            default:
                return "";
        }
    }
    public void OnClickTutorialDone(){
        LevelManager.CurrentLevelAccomplished(LevelManager.GetCurrentLevelIndex());
        Invoke("returnMain",3f);
    }
    public void onClickRestart(){
        
        SceneManager.LoadScene(LevelManager.GetCurrentLevelIndex());
    }
    public void onClickTestObjectiveAccomplished(){
        LevelManager.CurrentLevelAccomplished(LevelManager.GetCurrentLevelIndex());
        Invoke("returnMain",3f);
    }
    void returnMain(){
        SceneManager.LoadScene(0);
    }
    public void OnClickReturnMain(){
        
        if(!StartSceneManager.isReturningToMain){
            StartSceneManager.isReturningToMain = true;
        }
        SceneManager.LoadScene(0);
    }
    void ActivePauseMenu(){
        
        pauseMenu.SetActive(true);
        LeanTween.moveLocalX(Pause, 0, 1f);
        PlayerMovement.isFreeze = true;
        Cursor.lockState = CursorLockMode.None;
        print("Now Pause");
    }
    public void OnClickResume(){
        pauseMenu.SetActive(false);
        LeanTween.moveLocalX(Pause, -1300, 1f);
        isGamePaused = false;
        PlayerMovement.isFreeze = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnClickSetting(){
        LeanTween.moveLocalX(Settings, 0, 1f);
        LeanTween.moveLocalX(Pause, -450, 1f);
    }
    public void OnClickSettingsBack(){
        LeanTween.moveLocalX(Settings, -450, 1f);
        LeanTween.moveLocalX(Pause, 0, 1f);
    }


}
