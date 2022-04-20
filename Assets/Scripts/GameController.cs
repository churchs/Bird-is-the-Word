using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Typing Variables
    [SerializeField] private TextAsset file;
    public GameObject bird;
    public GameObject birdSpawns;


    public PlayfabManager playfabManager;

    public TMP_Text textToCopy;
    public TMP_InputField textToEnter;
    public int textPosition;
    public AudioSource click;

    public List<string> listOfSentences;



    public bool paused = true;

    bool roundover = false;

    public char enter;
    public char copy;
    public bool typing;

    public Color enterTextCurrentColor;
    public Color enterTextPastColor;

    protected float Timer;
    private int inputPos;
    private Vector3 birdLoc;

    #endregion
    #region Art and Game Variables
    public float cockSpeed = 0;
    public float bgSpeed = 0;
    public float cockSpeedMultiplier = 0;
    public float bgSpeedMultiplier = 0;
    public GameObject[] mountainBG;
    public GameObject fog;
    [SerializeField] float cockSpeedCap;
    [SerializeField] float bgSpeedCap;
    [SerializeField] GameObject endGameUI;
    [SerializeField] float distanceTravelled;
    [SerializeField] TMP_Text distanceText;
    [SerializeField] GameObject copyTxtBG;
    public TMP_Text pointsTxt;
    public float points;
    public TMP_Text accuracyTxt;
    public float accuracy;
    public float totalCorrect;
    public float totalEntries;
    public TMP_Text wpmTxt;
    public float wpm;
    public TMP_Text timerUI;
    public float gameTime;
    [SerializeField] AudioSource bgMusic;
    [SerializeField] AudioSource chirpSound;
    [SerializeField] bool soundOff;
    [SerializeField] bool musicoff;
    [SerializeField] TMP_Text titleUI;

    public GameObject startButton;
    //   public GameObject peaks;
    //   public GameObject fogs;
    #endregion
    void Start()
    {
        bgMusic.Play();
        PauseGame();
        var content = file.text;
        string[] stringSep = new string[] { "\n" };
        var AllWords = content.Split(stringSep,System.StringSplitOptions.None);
        listOfSentences = new List<string>(AllWords);
        textToEnter.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    public void ToggleMusic()
    {
        if (musicoff)
        {
            bgMusic.volume = 1;
            musicoff = false;
        }
        else
        {
            bgMusic.volume = 0;
            musicoff = true;
        }

    }
    public void ToggleSound()
    {
        if (soundOff)
        {
            soundOff = false;
        }
        else
        {
            soundOff = true;
        }
    }

    public void FixedUpdate()
    {   
        SpeedCalcs();
      //  if (Input.GetKeyDown(KeyCode.Escape) && roundover == false)
      //  {
     //       PauseGame();
     //   }
        if (!paused)
        {
            TimerCount();
            wpm = Mathf.Round((totalCorrect / 4.7f) / (gameTime / 60f));
        }
        textToEnter.caretPosition = textToEnter.text.Length;
        if (textToEnter.isFocused == false && paused == false)
        {
            textToEnter.Select();
        }
        if (paused == true)
        {
            textToEnter.OnDeselect(null);
        }

    }
   
    public void SpeedCalcs()
    {
        // calc even if paused 
         cockSpeedCap = (wpm * (.01f * accuracy)) * cockSpeedMultiplier;
         bgSpeedCap = (wpm * (.01f * accuracy)) * bgSpeedMultiplier;

        if (cockSpeed < wpm)
        {
            if(cockSpeed < cockSpeedCap)
            {
                cockSpeed += 1.45f * Time.deltaTime;
            }
            if (cockSpeed > cockSpeedCap)
            {
                cockSpeed += -1.45f * Time.deltaTime;
            }
        }
        if (bgSpeed < wpm)
        {
            if (bgSpeed < bgSpeedCap)
            {
                bgSpeed += .55f * Time.deltaTime;
            }
            if (bgSpeed > bgSpeedCap)
            {
                bgSpeed += -.55f * Time.deltaTime;
            }
        }
        // Dont calc these if paused
        if (!paused)
        {
            distanceTravelled += cockSpeed * Time.deltaTime * 3.54f;
            distanceText.text = "Travelled " + Mathf.Round(distanceTravelled) + " ft";
        }
    }

    void BirdRenew()
    {
        if(bird != null)
        {
            bird.gameObject.GetComponent<BirdController>().BirdDone(bird.transform.localPosition.y + 200);
        }
        bird = null;
        bird = GameObject.Instantiate(birdSpawns, textToCopy.gameObject.transform.parent, false);
        birdLoc = textToCopy.textInfo.characterInfo[0].topLeft;
        birdLoc = new Vector3(birdLoc.x -1750, birdLoc.y - 360, 0);
        bird.transform.localPosition = birdLoc;
    }
    public void BeginGame(GameObject button)
    {

        paused = false;
        startButton = button.gameObject;
        startButton.SetActive(false);
        NewSentence();
        endGameUI.SetActive(false);
        copyTxtBG.SetActive(true);
        timerUI.gameObject.SetActive(true);
        distanceText.gameObject.SetActive(true);
        titleUI.gameObject.SetActive(false);
    }
    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
        }
        else
        {
            textToEnter.Select();
            paused = false;
        }
    }
    public void RoundEnd()
    {
        timerUI.gameObject.SetActive(false);
        distanceText.gameObject.SetActive(false);
        copyTxtBG.SetActive(false);
        playfabManager.SendACC(accuracy);
        playfabManager.SendWPMLB(wpm);
        playfabManager.SendPointsLB(distanceTravelled);
        playfabManager.GetLeaderboard();
        startButton.SetActive(true);
        accuracyTxt.text = ("Accuracy: %" + accuracy);
        wpmTxt.text = ("WPM: " + wpm);
        pointsTxt.text = ("Distance Flown: " + Mathf.Round(distanceTravelled) + " ft.");
        distanceTravelled = 0;
        accuracy = 0;
        wpm = 0;
        points = 0;
        totalEntries = 0;
        totalCorrect = 0;
        textToCopy.text = "";
        textToEnter.text = "";
        cockSpeed = 1;
        bgSpeed = 1;
        gameTime = 0;
        PauseGame();
        Destroy(bird);
        endGameUI.SetActive(true);
        titleUI.gameObject.SetActive(true);
    }
    public void TimerCount()
    {
        Timer += Time.deltaTime;
        timerUI.text = "Time remaining: " + (30 - gameTime);
        if (Timer >= 1)
        {
            Timer = 0f;
            gameTime++;
            if(gameTime == 30f)
            {
                RoundEnd();
            }
        }
        #region Bird Mechanics

        if(textToEnter.text.Length > 0)
        {
            if (Vector3.Distance(bird.transform.localPosition, birdLoc) > 1200 && textToEnter.text.Length > 10)
            {
                BirdRenew();
            }
            if (!bird.gameObject.GetComponent<BirdController>().birdDone)
            {
                float birdSpeed = wpm * 1.2f;
                bird.transform.localPosition = Vector3.MoveTowards(bird.transform.localPosition, birdLoc, 999);
            }
        }
        #endregion
    }
    public void FetchBirdLoc(int charPos)
    {
        if(charPos == 0)
        {
            birdLoc = textToCopy.textInfo.characterInfo[0].topLeft;
            birdLoc = new Vector3(birdLoc.x + 1, birdLoc.y - 360, 0);
        }
        else
        {
            birdLoc = textToCopy.textInfo.characterInfo[charPos].topLeft;
            birdLoc = new Vector3(birdLoc.x + 1, birdLoc.y - 360, 0);
        }
    }
    public void NewSentence()
    {
        //
        int randomSentence = Random.Range(0, listOfSentences.Count);
        textToCopy.text = listOfSentences[randomSentence];
        listOfSentences.RemoveAt(randomSentence);
        if (!soundOff)
        {
            chirpSound.Play();
        }
        BirdRenew();
    }
    public void EnterTextColor(int charnumber)
    {
        int newcharnumber;
        if (charnumber > 0)
        {
            newcharnumber = (charnumber * 24);
        }
        else
        {
            newcharnumber = charnumber;
        }
        string textReplace = textToCopy.text;
        textReplace = textReplace.Remove(newcharnumber, 1).Insert(newcharnumber, "<color=#3EB01C>" + textToCopy.text[newcharnumber].ToString() + "</color>");
        textToCopy.text = textReplace;
    }
    public void NextLetter()
    {

        typing = true;
        string textToEnterString = textToEnter.text;
        if (textToEnterString.Length != 0)
        {
            enter = textToEnterString[textToEnterString.Length -1];
            string textToCopyString = textToCopy.text;
            copy = textToCopyString[((textToEnterString.Length - 1)* 24)];
            if (enter == copy)
            {
                FetchBirdLoc(((textToEnterString.Length)));
                totalEntries++;
                totalCorrect++;
                accuracy = Mathf.Round(((totalCorrect / totalEntries) * 100) * 100f) / 100f;
                accuracyTxt.text = ("Accuracy: %" + accuracy);
                wpmTxt.text = ("WPM: " + wpm);
                typing = false;
                if ((textToCopy.GetParsedText().Length - 1) == textToEnter.text.Length)
                {
                    NewSentence();
                    textToEnter.text = "";
                    points = points + (textToEnterString.Length * (accuracy * .01f) * (wpm * .01f));
                    pointsTxt.text = "Points: " + points;
                }
                EnterTextColor(textToEnter.text.Length - 1);
                return;
            }
            else 
            { 
                string errorTextString = textToEnter.text;
                errorTextString = errorTextString.Substring(0, errorTextString.Length - 1);
                textToEnter.text = errorTextString;
                click.Play();
                totalEntries++;
                accuracy = Mathf.Round(((totalCorrect / totalEntries) * 100) * 100f) / 100f;
                accuracyTxt.text = ("Accuracy: %" + accuracy);
             //   wpm = ((totalCorrect / 4.7f) / (gameTime/60f));
                wpmTxt.text = ("WPM: " + wpm);
                typing = false;
                return;
            }
        }
        else
        {
            typing = false;
            return;
        }
    }
    public void ValueChangeCheck()
    {
        if (!typing && !paused)
        {
            NextLetter();
        }
    }
    public void AppClose()
    {
        //Application.Quit();
    }
}
