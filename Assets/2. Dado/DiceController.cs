using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] Swipe swipeController;

    public bool ping;
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
        movementPermission = false;
        canGoDown = false;
        canGoLeft = false;
        canGoRight = false;
        canGoUp = false;
        AggiornaMosse();
    }

    public void Reset()
    {
        movementPermission = false;
        canGoDown = false;
        canGoLeft = false;
        canGoRight = false;
        canGoUp = false;

        
        gameObject.transform.SetPositionAndRotation(new Vector3(0,1,0) , new Quaternion(0,0,0,1));
        gameObject.transform.eulerAngles = PickRandomFace();
        AggiornaMosse();
    }


    public Vector3 lastDirection = new Vector3(1, 1, 1);

    // Update is called once per frame
    void Update()
    {
        #region Inputs


        if (mosseContatore > 0)
        {
            var dir = Vector3.zero;

            if ((Input.GetKey(KeyCode.UpArrow) || swipeController.SwipeUp) && lastDirection != Vector3.back)
            {
                CheckMovimenti();
                if (canGoUp)
                {
                    dir = Vector3.forward;
                    lastDirection = dir;
                }
            }

            if ((Input.GetKey(KeyCode.DownArrow) || swipeController.SwipeDown) && lastDirection != Vector3.forward)
            {
                CheckMovimenti();
                if (canGoDown) {
                    dir = Vector3.back;
                lastDirection = dir;
            }
        }

            if ((Input.GetKey(KeyCode.LeftArrow) || swipeController.SwipeLeft) && lastDirection != Vector3.right)
            {
                CheckMovimenti();
                if (canGoLeft){
                    dir = Vector3.left; 
                    lastDirection = dir;
            }
        }

            if ((Input.GetKey(KeyCode.RightArrow) || swipeController.SwipeRight) && lastDirection != Vector3.left)
            {
                CheckMovimenti();
                if (canGoRight) {
                    dir = Vector3.right;
                lastDirection = dir;
            }
        }

            if (dir != Vector3.zero && !gameObject.GetComponent<DiceStep>().isTumbling && movementPermission)
            {
                StartCoroutine(gameObject.GetComponent<DiceStep>().Tumble(dir));
                mosseContatore--;
                UIManager.Instance.RefreshMosse(mosseContatore);
                dir = Vector3.zero;
                lastDirection = dir;

            }

            if (mosseContatore == 0)
            {
                ping = false;
                UIManager.Instance.RefreshMosse(mosseContatore);
                StartCoroutine(gameManager.GetComponent<GameManager>().HoFinito(casellaAttuale, "Player"));
                movementPermission = false;
                lastDirection = Vector3.zero;
            }
        }

        #endregion
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

        #region SE SEI SUL BORDO -> NON PUOI USCIRE DAL BORDO
        if (casellaAttuale == "A1" || casellaAttuale == "A2" || casellaAttuale == "A3" || casellaAttuale == "A4" )
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
        #endregion

    }

    public void AggiornaMosse()
    {
        mosseContatore = faccia;
        UIManager.Instance.RefreshMosse(mosseContatore);
    }

    public void SetCasellaAttuale(string nuovaCasella)
    {
        casellaAttuale = nuovaCasella;
    }

    public void SetFaccia(int attualeFaccia)
    {
        faccia = attualeFaccia;
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
