using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public AudioClip CorrectSound;
    public AudioClip WrongSound;
    public AudioClip geSound;

    private AudioSource audioSource;
    private AudioSource generalAudio;


    private List<Button> btnList= new List<Button>();
    [SerializeField]
    private Sprite backgroundImg;

    [SerializeField]
    public Sprite[] SourceSprites;

    public List<Sprite> GameSprites = new List<Sprite>();

    private bool firstGuess, secondGuess;
    private int firstIndex, secondIndex;
    private int totalGuess, noOfGuess, correctGuess;
    private string firstName, secondName;

    public Text Total, NoGuess, CorrectG;





    void Awake()
    {
        SourceSprites = Resources.LoadAll<Sprite>("Spirtes/GameImage");
        ReplayControl.prevIndexscene = SceneManager.GetActiveScene().buildIndex;
        generalAudio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetButtons();
        totalGuess = btnList.Count / 2;
        AddListener();
        AddSprites();
        Shuffle(GameSprites);
        //Total.text = totalGuess.ToString();
        audioSource = GetComponent<AudioSource>();
        generalAudio.clip = geSound;
        generalAudio.Play();
    }

    private void Update()
    {
        CorrectG.text ="Score: " +correctGuess.ToString();

    }



    void GetButtons()
    {
        //Lay het cac Button them vao List
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzuleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btnList.Add(objects[i].GetComponent<Button>());
            btnList[i].image.sprite = backgroundImg;

        }
    }

    void AddSprites()
    {
        int size = btnList.Count;
        int index = 0;
        for(int i = 0; i < size; i++)
        {
            if (i == size / 2)
            {
                index = 0;
            }

            GameSprites.Add(SourceSprites[index]);
            index++;
        }
    }

    void AddListener()
    {
        foreach(Button btn in btnList)
        {
            btn.onClick.AddListener( () => PickPuzzle() );
        }
    }

    void PickPuzzle()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstName = GameSprites[firstIndex].name;
            btnList[firstIndex].image.sprite = GameSprites[firstIndex];
            Debug.Log("1st Index: " + firstIndex + " 1st Name: " + firstName);
            
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondName = GameSprites[secondIndex].name;
            btnList[secondIndex].image.sprite = GameSprites[secondIndex];
            Debug.Log("2nd Index: " + secondIndex + " 2nd Name: " + secondName);

            noOfGuess++;
            StartCoroutine(CheckIFPuzzleMatch());
        }
    }

    IEnumerator CheckIFPuzzleMatch()
    {
        yield return new WaitForSeconds(1);

        if (firstName == secondName && firstIndex != secondIndex)
        {
            
            audioSource.PlayOneShot(CorrectSound);
          
            
            btnList[firstIndex].interactable = false;
            btnList[secondIndex].interactable = false;
            
            btnList[firstIndex].image.color = new Color(0,0,0,0);
            btnList[secondIndex].image.color =  new Color(0, 0, 0, 0);
            correctGuess++;
            CheckFinished();
        }
        else
        {

            audioSource.PlayOneShot(WrongSound);

            btnList[firstIndex].image.sprite = backgroundImg;
            btnList[secondIndex].image.sprite = backgroundImg;
        }
        firstGuess = secondGuess = false;
    }

    void CheckFinished()
    {
        if (correctGuess == totalGuess)
        {
            CorrectG.text = correctGuess.ToString();
            Debug.Log("You Win!" + noOfGuess);
            SceneManager.LoadScene("ReplayScene");
            Debug.Log("CorG: " + correctGuess);
            ReplayControl.Score = correctGuess;

        }
    }

    void Shuffle(List<Sprite> listsp)
    {
        Sprite temp;
        for(int i =0; i < listsp.Count; i++)
        {
            temp = listsp[i];
            int randomindex = Random.Range(i, listsp.Count);
            listsp[i] = listsp[randomindex];
            listsp[randomindex] = temp;
        }
    }

    public void QuitScene()
    {
        Application.Quit();
    }

}
