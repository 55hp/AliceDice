using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCPU : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    public bool ping;
    public bool collidoColPlayer;

    int faccia;
    public int mosseContatore;
    bool canGoUp;
    bool canGoDown;
    bool canGoLeft;
    bool canGoRight;

    public bool movementPermission;

    public string casellaAttuale;
    

    private void Start()
    {
        collidoColPlayer = false;
        movementPermission = false;
        canGoDown = false;
        canGoLeft = false;
        canGoRight = false;
        canGoUp = false;
    }

    public void Reset()
    {
        collidoColPlayer = false;
        movementPermission = false;
        canGoDown = false;
        canGoLeft = false;
        canGoRight = false;
        canGoUp = false;
        gameObject.transform.SetPositionAndRotation(new Vector3(3, 1, 3), new Quaternion(0, 0, 90, 1));
        gameObject.transform.eulerAngles = PickRandomFace();

        AggiornaMosse();
    }

    private void Update()
    {
        
        if ( mosseContatore > 0 && movementPermission)
        {
            CheckMovimenti();
            var dir = RandomDirectionGenerator();

            if (dir != Vector3.zero && !gameObject.GetComponent<DiceStep>().isTumbling )
            {
                StartCoroutine(gameObject.GetComponent<DiceStep>().Tumble(dir));
                mosseContatore--;
                dir = Vector3.zero;
            }

            if (mosseContatore == 0)
            {
                movementPermission = false;
                ping = false;
                StartCoroutine(gameManager.GetComponent<GameManager>().HoFinito(casellaAttuale, "CPU"));
            }
        }

    }

    public void CanGo(bool yesOrNot)
    {
        movementPermission = yesOrNot;
    }

    public void CheckMovimenti()
    {
        canGoDown = true;
        canGoLeft = true;
        canGoRight = true;
        canGoUp = true;

        if (casellaAttuale == "A1" || casellaAttuale == "A2" || casellaAttuale == "A3" || casellaAttuale == "A4")
        {
            canGoDown = false;
        }

        if (casellaAttuale == "D1" || casellaAttuale == "D2" || casellaAttuale == "D3" || casellaAttuale == "D4")
        {
            canGoUp = false;
        }

        if (casellaAttuale == "A1" || casellaAttuale == "B1" || casellaAttuale == "C1" || casellaAttuale == "D1")
        {
            canGoRight = false;
        }

        if (casellaAttuale == "A4" || casellaAttuale == "B4" || casellaAttuale == "C4" || casellaAttuale == "D4")
        {
            canGoLeft = false;
        }
    }

    public void AggiornaMosse()
    {
        mosseContatore = faccia;
    }

    public void SetCasellaAttuale(string nuovaCasella)
    {
        casellaAttuale = nuovaCasella;
    }

    public void SetFaccia(int attualeFaccia)
    {
        faccia = attualeFaccia;
    }
    

    public Vector3 RandomDirectionGenerator()
    {
        var dir = Vector3.zero;
        int x = Random.Range(0, 4);
        switch (x)
        {
            case 0:
                if (canGoUp) dir = Vector3.forward;
                break;
            case 1:
                if (canGoDown) dir = Vector3.back;
                break;
            case 2:
                if (canGoLeft) dir = Vector3.left;
                break;
            case 3:
                if (canGoRight) dir = Vector3.right;
                break;
            default:
                dir = Vector3.zero;
                break;
        }
        return dir;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            collidoColPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            collidoColPlayer = false;
        }
    }

    Vector3 one = new Vector3(0, 0, 0);
    Vector3 two = new Vector3(0, 0, 90);
    Vector3 three = new Vector3(-90, 0, 0);
    Vector3 four = new Vector3(90, 0, 0);
    Vector3 five = new Vector3(0, 0, -90);

    public Vector3 PickRandomFace()
    {
        int x = Random.Range(1, 6);
        switch (x)
        {
            case 1:
                return one;
            case 2:
                return two;
            case 3:
                return three;
            case 4:
                return four;
            case 5:
                return five;
            default:
                return one;
        }
    }
}
