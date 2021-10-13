using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Bird[] BirdPreFabs;
    public float spawnTime;

    public int timeLimit;

    public GameObject Infor;
    public GameObject Pause;


    int m_curTimelimit;
    int birdKilled;
    bool isGameOver;

    public int BirdKilled { get => birdKilled; set => birdKilled = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    public override void Awake()
    {
        MakeSingleton(false);
        m_curTimelimit = timeLimit;
    }
    public override void Start()
    {

        GameGUIManager.Ins.ShowGameGUI(false);
        GameGUIManager.Ins.UpdateKilledCounting(birdKilled);
    }

  public void PlayGame()
    {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGameGUI(true);
    }
    IEnumerator TimeCountDown()
    {
        while (m_curTimelimit > 0)
        {
            yield return new  WaitForSeconds(1f);
            m_curTimelimit--;
            if (m_curTimelimit <= 0)
            {
                isGameOver = true;
                Time.timeScale = 0f;

                Infor.SetActive(false);
                Pause.SetActive(false);
                

                GameGUIManager.Ins.gameDiaLog.UpdateDiaLog("YOUR BEST", "BEST KILLED : X" + birdKilled);
                GameGUIManager.Ins.gameDiaLog.Show(true);
                GameGUIManager.Ins.CurDiaLog = GameGUIManager.Ins.gameDiaLog;
            }
            GameGUIManager.Ins.UpdateTimer(IntoTime(m_curTimelimit));
        }
    }

    IEnumerator GameSpawn()
    {
        while (!isGameOver)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }
    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;
        float ranDomChk = Random.Range(0f, 1f);
        if (ranDomChk >= 0.5f)
        {
            spawnPos = new Vector3(12, Random.Range(-2.5f, 4f));
            

        }
        else
        {
            spawnPos = new Vector3(-12, Random.Range(-2.5f, 4f));
        }
        if (BirdPreFabs != null && BirdPreFabs.Length > 0)
        {
            int  randDomIndx = Random.Range(0, BirdPreFabs.Length);
            if (BirdPreFabs[randDomIndx] != null)
            {
                Bird birdClone = Instantiate(BirdPreFabs[randDomIndx], spawnPos, Quaternion.identity);
            }
        }
    }
    string IntoTime(int time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time%60);
        return minutes.ToString("00") + " : " + seconds.ToString("00");
    }
}
