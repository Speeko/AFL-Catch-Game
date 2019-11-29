using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject footy;
    public GameObject meth;
    public float maxWaitSeconds;
    public float minWaitSeconds;
    public float timeLeft;
    public Text timerText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public Camera cam;

    private int rand;
    private float maxWidth;

    public AudioSource bgm;

    void Awake()
    {
        GameObject currentBGM = GameObject.FindGameObjectWithTag("Music");
        if (currentBGM == null)
        {
            AudioSource spawned = Instantiate(bgm);
            spawned.Play();
            DontDestroyOnLoad(spawned);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        maxWidth = targetWidth.x;

        StartCoroutine(Spawn());
        UpdateText();
    }

    void FixedUpdate()
    {

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
        UpdateText();
        
    }

    //Ball spawner
    IEnumerator Spawn()
    {

        while (timeLeft > 0)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                transform.position.z
                );


            //Vector3 spawnRotation = new Vector3(
            //    transform.position.x,
            //    transform.position.y,
            //    Random.Range(-3.0f, 3.0f)
            //);

            //Quaternion spawnRotation = Random.rotation;
            Quaternion spawnRotation = Quaternion.identity;

            rand = Random.Range(1, 4);

            if (rand < 3)
            {
                Instantiate(footy, spawnPosition, spawnRotation);
            }
            else
            {
                Instantiate(meth, spawnPosition, spawnRotation);
            }

            
            yield return new WaitForSeconds(Random.Range(minWaitSeconds, maxWaitSeconds));
        }

        yield return new WaitForSeconds(1.0f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        restartButton.SetActive(true);

    }

     void UpdateText()
        {
          timerText.text = "Time Left: " + Mathf.RoundToInt(timeLeft);
        }

    }
