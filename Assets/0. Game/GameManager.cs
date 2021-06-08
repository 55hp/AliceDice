using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{


    public GameObject Player;
    public GameObject CPU;
    
    public string casellaPlayer;
    public string casellaCPU;

    bool stop;

    float timer = 0;

    private void Start()
    {
        UIManager.Instance.TurnoDiNessuno();
        UIManager.Instance.SetWinner("none");
        stop = true;

        casellaCPU = "aa";
        casellaPlayer = "bb";
    }

    public void StartGame()
    {
        StartCoroutine(TossACoin());
    }

    public void RestartGame()
    {
        timer = 0;
        stop = true;
        UIManager.Instance.TurnoDiNessuno();
        UIManager.Instance.SetWinner("none");

        casellaCPU = "aa";
        casellaPlayer = "bb";


        //Resetta i dadi.
        Player.GetComponent<DiceController>().Reset();
        CPU.GetComponent<DiceCPU>().Reset();

        StartCoroutine(TossACoin());
    }

    private void Update()
    {
        if (!stop)
        {
            timer -= Time.deltaTime;
            UIManager.Instance.RefreshTimerText(timer);
        }


        if (Input.GetKeyDown(KeyCode.Space) && stop )
        {
            RestartGame();
        }
    }


    public void GoPlayer()
    {
        UIManager.Instance.TurnoPlayer();

        Player.GetComponent<DiceController>().CanGo(true);
        Player.GetComponent<DiceController>().AggiornaMosse();
    }

    public void GoCPU()
    {
        UIManager.Instance.TurnoCPU();

        CPU.GetComponent<DiceCPU>().CanGo(true);
        CPU.GetComponent<DiceCPU>().AggiornaMosse();

    }
    
    public IEnumerator HoFinito(string casella , string chiSono)
    {

        yield return new WaitForSeconds(1f);
        if (chiSono == "Player")
        {
            casellaPlayer = casella;
        }
        else if(chiSono == "CPU")
        {
            casellaCPU = casella;
        }

        CheckWinner(chiSono);

        if (!stop)
        {
            if (chiSono == "Player")
            {
                GoCPU();
            }
            else if (chiSono == "CPU")
            {
                GoPlayer();
            }
        }
    }
    

    public void CheckWinner(string chiSono)
    {
        if (CPU.GetComponent<DiceCPU>().collidoColPlayer)
        {
            stop = true;

            if (chiSono == "Player")
            {
                UIManager.Instance.SetWinner("Player");
            }
            else
            {
                UIManager.Instance.SetWinner("CPU");
            }
            UIManager.Instance.TurnoDiNessuno();
        }
    }

    IEnumerator TossACoin()
    {

        yield return new WaitForSeconds(2f);
        int x = Random.Range(0, 2);

        if (x == 0)
        {
            GoPlayer();
        }else if (x == 1)
        {
            GoCPU();
        }
        stop = false;
    }



    

}
