using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour {

 
    [SerializeField]
    public int _state;
    [SerializeField]
    public int _cType;
    [SerializeField]
    private int _cardValue;
    [SerializeField]
    private int _trumpValue;


    private bool _initialized = false;

    private Sprite _cardBack;
    private Sprite _cardFace;

    private GameObject _manager;

    // Use this for initialization
    void Start () {
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

    public void chooseCard()
    {
        if (_state == 0)
            _state = 1;
        else if (_state == 1)
            _state = 0;

        this.gameObject.SetActive(false);
        //this.gameObject.transform{ get};
      

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

    public bool initialized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }

    
}
