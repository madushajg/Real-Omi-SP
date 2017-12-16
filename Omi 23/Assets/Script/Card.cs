using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour {

 
    [SerializeField]
    public int _state;
    [SerializeField]
    public int _cType;
    [SerializeField]
    public int _allowable;
    [SerializeField]
    public int _chance;

    [SerializeField]
    private int _cardValue;
    [SerializeField]
    private int _trumpValue;

  

    Vector3 positionStart;
   

    Vector3 position1 = new Vector3(-39, -29, 0);
     Vector3 position2 = new Vector3(-39, 31, 0);
     Vector3 position3 = new Vector3(-69, 24, 0);
     Vector3 position4 = new Vector3(42, 24, 0);



    private bool _initialized = false;

    private Sprite _cardBack;
    private Sprite _cardFace;

    private GameObject _manager;

    // Use this for initialization
    void Start () {
        positionStart = transform.position;
        _state = 0;
        _manager = GameObject.FindWithTag("Manager");
       
    }

    public void setupGraphics()
    {
        _cardBack = _manager.GetComponent<GameManager>().getCardBack();
        _cardFace = _manager.GetComponent<GameManager>().getCardFace(_cardValue);

        flipCard();
    }

    public void flipCard()
    {
                  GetComponent<Image>().sprite = _cardFace;
    }

    public void flip()
    {
        GetComponent<Image>().sprite = _cardBack;
    }

    public void chooseCard()
    {
        if (_state == 0)
            _state = 1;
        else if (_state == 1)
            _state = 0;

        
        

        _manager.GetComponent<GameManager>().checkCards(cardValue);
        moveCard();

        allowToPerform();
        
      

    }

   public void moveCard() {
        if (_cType == 1)
        {
            gameObject.transform.localPosition = position1;
            
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Button>().enabled = false;
        }


        if (_cType == 2)
        {
           gameObject.transform.localPosition = position2;
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Button>().enabled = false;
        }

        if (_cType == 3)
        {
            transform.localPosition = position3;
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Button>().enabled = false;
        }

        if (_cType == 4)
        {
            transform.localPosition = position4;
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Button>().enabled = false;
        }
    }


    public void setInitialState() {
        gameObject.transform.position = positionStart;
    }


    public void allowToPerform() {
        _manager.GetComponent<GameManager>().nextCardSet(cType);
        
    }




    // Update is called once per frame
    void Update () {
	
	}


    public int cardValue
    {
        get { return _cardValue; }
        set { _cardValue = value; }
    }


    public int trumpValue
    {
        get { return _trumpValue; }
        set { _trumpValue = value; }
    }

    public int state
    {
        get { return _state; }
        set { _state = value; }
    }

    public int cType
    {
        get { return _cType; }
        set { _cType = value; }
    }

    public int allowable
    {
        get { return _allowable; }
        set { _allowable = value; }
    }

    public int chance
    {
        get { return _chance; }
        set { _chance = value; }
    }

    public bool initialized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }

    
}
