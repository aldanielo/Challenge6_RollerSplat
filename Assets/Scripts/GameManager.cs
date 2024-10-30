using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    //private AudioSource gameSound;
   // public AudioClip swipeSound;
    
    

    private GroundPiece[] allGroundPieces;
  //  private RandomGradient randomGradient;


    // Start is called before the first frame update
    void Start()
    {
       // gameSound = GetComponent<AudioSource>();
        SetupNewLevel();
        
        //gameSound.Play();
      //  randomGradient = GetComponent<RandomGradient>();
    }

    private void SetupNewLevel()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();
    }


    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        } else if(singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(gameSound);
        }
    }

    private void OnEnable()
    {
        //gameSound.Play();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }


    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();

    }

    public void CheckComplete()
    {
        bool isFinished = true;

        for (int i = 0; i < allGroundPieces.Length; i++)
        {
            if (allGroundPieces[i].isColored == false)
            {
                isFinished = false; 
                break;
            }
        }

        if (isFinished)
        {
            // Next Level
            NextLevel();
        }
    }

    private void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(0);
            
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
