using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tap : MonoBehaviour
{
    public Spawner spawner;

    public GameObject startButton;
    public GameObject pauseButton;
    public GameObject restartButton;
    public Text finalScore;
    public GameObject pauseText;

    public int points = 0;
    public Text pointsText;
    public GameObject popUpPoints;

    bool paused;
    BoxCollider bc;
    public GameObject tapParticle;

    float startTime = 60f;
    float curTime;
    public Text curTimeText;

    private void Start()
    {
        curTime = 60f;
        curTimeText.text = curTime.ToString();
    }
    void Update()
    {
        if (spawner.stop) return;
        if(curTime > 0)
        {
            curTime -= Time.deltaTime;
        }
        else
        {
            curTime = 0;
            GameOver();
        }
        curTimeText.text = Mathf.Round(curTime).ToString();
        pointsText.text = points.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                bc = hit.collider as BoxCollider;
                if (bc != null)
                {
                    if(hit.collider.tag == "GreenCube")
                    {
                        points += 25;
                        PopUpPoints("+25", hit.collider.transform.position, Color.green);
                    }else
                    if (hit.collider.tag == "RedCube")
                    {
                        points -= 25;
                        PopUpPoints("-25", hit.collider.transform.position, Color.red);
                    }
                    Instantiate(tapParticle, hit.collider.transform.position, Quaternion.identity);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void PopUpPoints(string pointsText, Vector3 pos, Color color)
    {
        GameObject popUpText = Instantiate(popUpPoints, pos + new Vector3(0,1,0), Quaternion.identity);
        popUpText.GetComponent<TextMesh>().text = pointsText;
        popUpText.GetComponent<TextMesh>().color = color;
    }
    public void StartGame()
    {
        curTime = startTime;

        startButton.SetActive(false);
        pauseButton.SetActive(true);
        spawner.StartingGame();
    }

    public void PauseGame()
    {
        if (!paused)
        {
            pauseText.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            pauseText.SetActive(false);
        }

        paused = !paused;
    }

    void GameOver()
    {
        spawner.stop = true;
        pauseButton.SetActive(false);
        restartButton.SetActive(true);
        finalScore.gameObject.SetActive(true);
        finalScore.text = "YourScore: " + points;
    }

    public void Restart()
    {
        points = 0;
        spawner.ClearObjList();
        curTime = startTime;

        restartButton.SetActive(false);
        finalScore.gameObject.SetActive(false);
        pauseButton.SetActive(true);
        spawner.StartingGame();
    }
}
