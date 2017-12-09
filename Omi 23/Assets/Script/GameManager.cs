using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Button[] buttons;
    public Button[] p1;
    public Button[] p2;
    public Button[] p3;
    public Button[] p4;
    public Button[] slcdCards;
    public Canvas roundOver;

    public Sprite[] CardFace;
    public Sprite cardBack;
    public GameObject[] Cards;
    private bool _init = false;
  

    private Text MaxText;
    private Text TrumpText;

    private Text trickText1;
    private Text trickText2;
    private Text trickText3;
    private Text trickText4;

    public Text pointAText;
    public Text pointBText;

    public int _tricksP1 = 0;
    public int _tricksP2 = 0;
    public int _tricksP3 = 0;
    public int _tricksP4 = 0;

    public static int _tt = 0;

    public int pointsA=0;
    public int pointsB=0;

    List<int> findSt = new List<int>();

    // Use this for initialization
    void Start()
    {

        MaxText = GameObject.Find("MaxText").GetComponent<Text>();
        TrumpText = GameObject.Find("TrumpText").GetComponent<Text>();


        trickText1 = GameObject.Find("Player1").transform.FindChild("TrickText").GetComponent<Text>();
        trickText2 = GameObject.Find("Player2").transform.FindChild("TrickText").GetComponent<Text>();
        trickText3 = GameObject.Find("Player3").transform.FindChild("TrickText").GetComponent<Text>();
        trickText4 = GameObject.Find("Player4").transform.FindChild("TrickText").GetComponent<Text>();
    }

    public void exit()
    {

        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_init)
        {
           
            initializeCards();
        }


        


        if (Input.GetMouseButtonUp(0))
               checkCards();
        

        

    }


    void initializeCards()
    {

        for (int i = 1; i < 33; i++)
        {

            bool test = false;
            int choice = 0;
            while (!test)
            {
                choice = Random.Range(0, Cards.Length);
                test = !(Cards[choice].GetComponent<Card>().initialized);
            }
            Cards[choice].GetComponent<Card>().cardValue = i;
            
            Cards[choice].GetComponent<Card>().initialized = true;

        }


        foreach (GameObject c in Cards)
            c.GetComponent<Card>().setupGraphics();

        if (!_init)
            _init = true;

        lockall();
        askTrump();
    }

   void lockall()
    {
        foreach (Button button in p1)
        {
            button.GetComponent<Button>().interactable = false;

        }
        foreach (Button button in p2)
        {
            button.GetComponent<Button>().interactable = false;

        }
        foreach (Button button in p3)
        {
            button.GetComponent<Button>().interactable = false;

        }
        foreach (Button button in p4)
        {
            button.GetComponent<Button>().interactable = false;

        }

    }

    void askTrump() {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
    

    public Sprite getCardBack()
    {
        return cardBack;
    }

    public Sprite getCardFace(int i)
    {
        return CardFace[i - 1];
    }

    
    public void trumpHearts() {
        for (int q1 = 0; q1 < 32; q1++)
        {
            //Cards[q1].GetComponent<Card>().trumpValue = Cards[q1].GetComponent<Card>().cardValue;
            if (Cards[q1].GetComponent<Card>().cardValue >= 1 && Cards[q1].GetComponent<Card>().cardValue <=8)
                Cards[q1].GetComponent<Card>().trumpValue = Cards[q1].GetComponent<Card>().cardValue + 32;


        }
        trumpSelected();
        TrumpText.text = "Trumps : Hearts ";
    }

    public void trumpSpeades()
    {
        for (int q2 = 0; q2 < 32; q2++)
        {
            //Cards[q2].GetComponent<Card>().trumpValue = Cards[q2].GetComponent<Card>().cardValue;
            if (Cards[q2].GetComponent<Card>().cardValue >= 9 && Cards[q2].GetComponent<Card>().cardValue <=16)
                Cards[q2].GetComponent<Card>().trumpValue = Cards[q2].GetComponent<Card>().cardValue + 32;


        }
        trumpSelected();
        TrumpText.text = "Trumps : Speades ";
    }

    public void trumpClubs()
    {
        for (int q3 = 0; q3 < 32; q3++)
        {
            //Cards[q3].GetComponent<Card>().trumpValue = Cards[q3].GetComponent<Card>().cardValue;
            if (Cards[q3].GetComponent<Card>().cardValue >= 17 && Cards[q3].GetComponent<Card>().cardValue <=24)
                Cards[q3].GetComponent<Card>().trumpValue = Cards[q3].GetComponent<Card>().cardValue + 32;


        }
        trumpSelected();
        TrumpText.text = "Trumps : Clubs ";
    }

    public void trumpDiamonds()
    {
        for (int q4 = 0; q4 < 32; q4++)
        {
            //Cards[q4].GetComponent<Card>().trumpValue = Cards[q4].GetComponent<Card>().cardValue;
            if (Cards[q4].GetComponent<Card>().cardValue >= 25 && Cards[q4].GetComponent<Card>().cardValue <=32)
                Cards[q4].GetComponent<Card>().trumpValue = Cards[q4].GetComponent<Card>().cardValue + 32;


        }
        trumpSelected();
        TrumpText.text = "Trumps : Diamonds ";
    }


    public void trumpSelected() {

        unlockCards();

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }







    






    void checkCards()
    {
        findState();

        List<int> c = new List<int>();
        int first = 0;
        int count=-1;
       


        


        for (int i = 0; i < Cards.Length; i++)
        {           
            
            if (findSt[i]==1)
            {
                c.Add(i);
                count++;

                if (c.Count == 1)
                {
                    first = i;
                    lockPeers(first);
                }

               
               // slcdCards[i].GetComponent<Card>().cardValue = i;
                //slcdCards[i].GetComponent<Card>().setupGraphics();
                

                cardSelected(i);
            }
        }



        if (c.Count == 4)
        {
            cardComparison(c,first);
            c.Clear();
            
        }                


    }



    void findState() {       

        for (int q=0;q<8;q++) {
            findSt.Add(GameObject.Find("Player_1").gameObject.GetComponent<Player>().state[q]);
        }

        for (int q = 8; q < 16; q++)
        {
            findSt.Add(GameObject.Find("Player_2").gameObject.GetComponent<Player>().state[q]);
        }

        for (int q = 16; q < 24; q++)
        {
            findSt.Add(GameObject.Find("Player_3").gameObject.GetComponent<Player>().state[q]);
        }

        for (int q = 24; q < 32; q++)
        {
            findSt.Add(GameObject.Find("Player_4").gameObject.GetComponent<Player>().state[q]);
        }


    }




    public void cardSelected(int z)
    {
        int ct = Cards[z].GetComponent<Card>().cType;


        if (ct == 1) {
            foreach (Button button in p1)
            {
                button.GetComponent<Button>().interactable = false;
                //button.gameObject.SetActive(false);

            }

        }

        else if (ct==2) {
            foreach (Button button in p2)
            {
                button.GetComponent<Button>().interactable = false;
                //button.gameObject.SetActive(false);

            }

        }

        else if (ct == 3)
        {
            foreach (Button button in p3)
            {
                button.GetComponent<Button>().interactable = false;
                //button.gameObject.SetActive(false);

            }

        }

        else if (ct == 4)
        {
            foreach (Button button in p4)
            {
                button.GetComponent<Button>().interactable = false;
                //button.gameObject.SetActive(false);

            }

        }


        
    }



    void cardComparison(List<int> c,int f)
    {
        int[] temp = new int[4];
        int cIndex = 0;
        int fv = Cards[f].GetComponent<Card>().cardValue;
        int cv=-1;
        bool putTrump = false;
        int x = 0;


        for (int i = 0; i < 4; i++)
        {
            if (GameObject.Find("Player_1").gameObject.GetComponent<Player>().trumpValue[i]>32) 
            {
                temp[i] = GameObject.Find("Player_1").gameObject.GetComponent<Player>().trumpValue[i];
                putTrump = true;
            }
            else {
                cv = Cards[c[i]].GetComponent<Card>().cardValue;

                if ((fv >= 0 && fv <= 8) && (cv >= 0 && cv <= 8))
                    temp[i] = Cards[c[i]].GetComponent<Card>().cardValue;

                else if ((fv >= 9 && fv <= 16) && (cv >= 9 && cv <= 16))
                    temp[i] = Cards[c[i]].GetComponent<Card>().cardValue;

                else if ((fv >= 17 && fv <= 24) && (cv >= 17 && cv <= 24))
                    temp[i] = Cards[c[i]].GetComponent<Card>().cardValue;

                else if ((fv >= 25 && fv <= 32) && (cv >= 25 && cv <= 32))
                    temp[i] = Cards[c[i]].GetComponent<Card>().cardValue;

                else {
                    temp[i] = 0;
                }


            }
           
        }



        var maxValue = Mathf.Max(temp);


        if (!putTrump) {
            x = 2;
            for (int j = 0; j < 4; j++)
            {

                if (Cards[c[j]].GetComponent<Card>().cardValue == maxValue)
                {
                    cIndex = j;
                }
            }
            updateTricks(cIndex,c);    

            MaxText.text = "Max Card: " + maxValue;


        }
        else if (putTrump)
        {
            x = 2;
            for (int j = 0; j < 4; j++)
            {

                if (GameObject.Find("Player_1").gameObject.GetComponent<Player>().trumpValue[c[j]] == maxValue) 
                {
                    cIndex = j;
                }
            }

            updateTricks(cIndex, c);
            MaxText.text = "Max Card: " + maxValue;


        }

        for (int i = 0; i < c.Count; i++)
        {
            Cards[c[i]].GetComponent<Card>().state = x;

        }

    }


    void updateTricks(int cIndex, List<int> c) {
        if (0 <= c[cIndex] && c[cIndex] < 8)
        {
            _tricksP1++;
            trickText1.text = "Tricks :" + _tricksP1;
            unlockCards();
        }

        else if (8 <= c[cIndex] && c[cIndex] < 16)
        {
            _tricksP2++;
            trickText2.text = "Tricks :" + _tricksP2;
            unlockCards();
        }

        else if (16 <= c[cIndex] && c[cIndex] < 24)
        {
            _tricksP3++;
            trickText3.text = "Tricks :" + _tricksP3;
            unlockCards();
        }

        else if (24 <= c[cIndex] && c[cIndex] < 32)
        {
            _tricksP4++;
            trickText4.text = "Tricks :" + _tricksP4;
            unlockCards();
        }

        _tt = _tricksP1 + _tricksP2 + _tricksP3 + _tricksP4;
        if (_tt==8) {
            
            calculatePoints(_tricksP1, _tricksP2, _tricksP3, _tricksP4);
        }

    }


    void unlockCards() {
        foreach (Button button in p1)
        {
            button.GetComponent<Button>().interactable = true;
            
        }
        foreach (Button button in p2)
        {
            button.GetComponent<Button>().interactable = true;

        }
        foreach (Button button in p3)
        {
            button.GetComponent<Button>().interactable = true;

        }
        foreach (Button button in p4)
        {
            button.GetComponent<Button>().interactable = true;

        }


    }



    void calculatePoints(int p1, int p2, int p3, int p4) {
        if ((p1+p2)>4) {
            if ((p1 + p2) == 8)
                pointsA = pointsA + 3;

            pointsA = pointsA + 2;
        }
        else if((p3 + p4) >4) {
            if ((p3 + p4) == 8)
                pointsB = pointsB + 3;

            pointsB = pointsB + 2;
        }
       
        roundOver.gameObject.SetActive(true);

        pointAText.text = "Team A Points : " + pointsA;
        pointBText.text = "Team B Points : " + pointsB;

        

    }


    public void nextRound() {
        _tricksP1 = 0;
        _tricksP2 = 0;
        _tricksP3 = 0;
        _tricksP4 = 0;

        _tt = 0;
        roundOver.gameObject.SetActive(false);

        for (int i=0;i<Cards.Length;i++) {
            Cards[i].GetComponent<Card>().cardValue = 0;
            Cards[i].gameObject.SetActive(true);
            Cards[i].GetComponent<Card>().state = 0;
            Cards[i].GetComponent<Card>().trumpValue = 0;
            Cards[i].GetComponent<Card>().initialized = false;
        }
        initializeCards();
        clearTricks();




    }


    void clearTricks() {
        trickText1.text = "Tricks : 00";
        trickText2.text = "Tricks : 00";
        trickText3.text = "Tricks : 00";
        trickText4.text = "Tricks : 00";

    }




    void lockCards()
    {
        int cindex1 = -1;

        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].GetComponent<Card>().state == 1)
                cindex1 = i;


        }
        // Debug.Log(cindex1);





        if (0 <= cindex1 && cindex1 < 8)
        {
            for (int x = 0; x < 8; x++)
            {
                Cards[x].GetComponent<Button>().interactable = false;
            }
            Cards[cindex1].GetComponent<Button>().interactable = true;
            Cards[cindex1].GetComponent<Card>().state = 2;
        }

        if (8 <= cindex1 && cindex1 < 16)
        {
            for (int x = 8; x < 16; x++)
            {
                Cards[x].GetComponent<Button>().interactable = false;
            }
            Cards[cindex1].GetComponent<Button>().interactable = true;
            Cards[cindex1].GetComponent<Card>().state = 2;
        }

        if (16 <= cindex1 && cindex1 < 24)
        {
            for (int x = 16; x < 24; x++)
            {
                Cards[x].GetComponent<Button>().interactable = false;
            }
            Cards[cindex1].GetComponent<Button>().interactable = true;
            Cards[cindex1].GetComponent<Card>().state = 2;
        }

        if (24 <= cindex1 && cindex1 < 32)
        {
            for (int x = 24; x < 32; x++)
            {
                Cards[x].GetComponent<Button>().interactable = false;
            }
            Cards[cindex1].GetComponent<Button>().interactable = true;
            Cards[cindex1].GetComponent<Card>().state = 2;
        }


    }



    void lockPeers(int i)
    {
        int val = Cards[i].GetComponent<Card>().cardValue;

        for (int x = 0; x < Cards.Length; x++)
        {
            Cards[x].GetComponent<Button>().interactable = false;




            if (1 <= val && val < 9)
            {
                if (Cards[x].GetComponent<Card>().cardValue >= 1 && Cards[x].GetComponent<Card>().cardValue < 9)
                    Cards[x].GetComponent<Button>().interactable = true;
            }




            else if (9 <= val && val < 17)
            {
                if (Cards[x].GetComponent<Card>().cardValue >= 9 && Cards[x].GetComponent<Card>().cardValue < 17)
                    Cards[x].GetComponent<Button>().interactable = true;
            }


            else if (17 <= val && val < 25)
            {
                if (Cards[x].GetComponent<Card>().cardValue >= 17 && Cards[x].GetComponent<Card>().cardValue < 25)
                    Cards[x].GetComponent<Button>().interactable = true;
            }

            else if (25 <= val && val < 33)
            {
                if (Cards[x].GetComponent<Card>().cardValue >= 25 && Cards[x].GetComponent<Card>().cardValue < 33)
                    Cards[x].GetComponent<Button>().interactable = true;
            }


        }

        noCard();
    }



    void noCard()
    {

        int count1 = -1;
        int count2 = -1;
        int count3 = -1;
        int count4 = -1;

        for (int a = 0; a < 8; a++)
        {
            if (Cards[a].GetComponent<Button>().interactable && Cards[a].GetComponent<Card>().state == 0)
            {
                count1++;
            }
        }
        if (count1 == -1)
        {
            for (int a = 0; a < 8; a++)
            {
                Cards[a].GetComponent<Button>().interactable = true;
            }
        }

        for (int b = 8; b < 16; b++)
        {
            if (Cards[b].GetComponent<Button>().interactable && Cards[b].GetComponent<Card>().state == 0)
            {
                count2++;
            }
        }

        if (count2 == -1)
        {
            for (int b = 8; b < 16; b++)
            {
                Cards[b].GetComponent<Button>().interactable = true;
            }
        }

        for (int c = 16; c < 24; c++)
        {
            if (Cards[c].GetComponent<Button>().interactable && Cards[c].GetComponent<Card>().state == 0)
            {
                count3++;

            }

        }

        if (count3 == -1)
        {
            for (int c = 16; c < 24; c++)
            {
                Cards[c].GetComponent<Button>().interactable = true;
            }
        }

        for (int d = 24; d < 32; d++)
        {
            if (Cards[d].GetComponent<Button>().interactable && Cards[d].GetComponent<Card>().state == 0)
            {
                count4++;

            }

        }

        if (count4 == -1)
        {
            for (int d = 24; d < 32; d++)
            {
                Cards[d].GetComponent<Button>().interactable = true;
            }
        }
    }








}







    







