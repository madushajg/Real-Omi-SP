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
    public GameObject[] slcdCards;
    public Canvas roundOver;

    public Sprite[] CardFace;
    public Sprite cardBack;
    public GameObject[] Cards;
    private bool _init = false;
    public bool canNow = true;

    public Text MaxText;
    public Text TrumpText;

    public Text trickText1;
    public Text trickText2;
    public Text trickText3;
    public Text trickText4;

    public Text pointAText;
    public Text pointBText;

    public int _tricksP1 = 0;
    public int _tricksP2 = 0;
    public int _tricksP3 = 0;
    public int _tricksP4 = 0;

    public static int _tt = 0;

    public int pointsA=0;
    public int pointsB=0;

    public int roundId = 0;

    public int first=0;
    public int chanceId;

    List<int> c = new List<int>();

    List<int> pl1 = new List<int>();
    List<int> pl2 = new List<int>();
    List<int> pl3 = new List<int>();
    List<int> pl4 = new List<int>();

    // Use this for initialization
    void Start()
    {
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


        if (c.Count == 1)
        {
            first = c[0];
            if ((Cards[c[0]].GetComponent<Card>().cType == 1) || (Cards[c[0]].GetComponent<Card>().cType == 2) || (Cards[c[0]].GetComponent<Card>().cType == 4)) { 

            lockPeers(first);
            cardSlced();
        }
        }



        if (chanceId == 4 && canNow)
        { 
            if(first==99)
                StartCoroutine(putP4CardFirst());
            
            else
                StartCoroutine(putP4Card());

        }

         if (chanceId == 2 && canNow)
        {
            if (first == 99)
                StartCoroutine(putP2CardFirst());
            
            else
                StartCoroutine(putP2Card());
        }

         if (chanceId == 3 && canNow)
         {
            if (first == 99)
                StartCoroutine(putP3CardFirst());
            
            else
                StartCoroutine(putP3Card());
        }


        




        if (c.Count == 4)
        {

            cardComparison(first);

            for (int i = 0; i < 4; i++)
            {
                slcdCards[i] = GameObject.Find(Cards[c[i]].GetComponent<Card>().name).gameObject;
            }
            c.Clear();
            Debug.Log(c.Count);
            StartCoroutine(cardWipe());
            
        }


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


        foreach (GameObject go in Cards) {
            if (go.GetComponent<Card>().cType == 1)
                go.GetComponent<Card>().setupGraphics();
            else if((go.GetComponent<Card>().cType != 1))               
                go.GetComponent<Card>().enabled = false;
        }

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

    void askTrump()
    {
        

        roundId++;

        if (roundId % 4 == 1)
        {


            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
            }
            buttons[5].gameObject.SetActive(false);
        }
        else {
            buttons[5].gameObject.SetActive(true);
            StartCoroutine(generateTrump());

        }

    }


    IEnumerator generateTrump() {
        yield return new WaitForSeconds(3);

        int tChoice = 0;

        
        tChoice = Random.Range(0, 4);

        switch (tChoice)
        {
            case (0):
                trumpHearts();
                break;

            case (1):
                trumpSpeades();
                break;

            case (2):
                trumpClubs();
                break;

            case (3):
                trumpDiamonds();
                break;

            default:
                break;



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



    
    public void checkCards(int cVal)
    {
        

        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].GetComponent<Card>().cardValue == cVal)
            {
                c.Add(i);

            }
        }   
               
    }



    IEnumerator putP2Card()
    {
        canNow = false;
        yield return new WaitForSeconds(1f);
        int t2;

        for (int p2 = 8; p2 < 16; p2++)
        {
            if (Cards[p2].GetComponent<Card>().allowable == 1 && Cards[p2].GetComponent<Card>().state == 0)
                pl2.Add(p2);

        }


        t2 = Random.Range(0, pl2.Count);
        c.Add(pl2[t2]);
        Cards[pl2[t2]].GetComponent<Card>().state = 1;
        
        Cards[pl2[t2]].GetComponent<Card>().moveCard();
        Cards[pl2[t2]].GetComponent<Card>().allowToPerform();
        Cards[pl2[t2]].GetComponent<Card>().setupGraphics();

        canNow = true;
    }

    IEnumerator putP3Card()
    {
        canNow = false;
        yield return new WaitForSeconds(1.2f);
        int t3;

        for (int p3 = 16; p3 < 24; p3++)
        {
            if (Cards[p3].GetComponent<Card>().allowable == 1 && Cards[p3].GetComponent<Card>().state == 0)
                pl3.Add(p3);

        }


        t3 = Random.Range(0, pl3.Count);
        c.Add(pl3[t3]);
        
        Cards[pl3[t3]].GetComponent<Card>().state = 1;
        
        Cards[pl3[t3]].GetComponent<Card>().moveCard();
        Cards[pl3[t3]].GetComponent<Card>().allowToPerform();
        Cards[pl3[t3]].GetComponent<Card>().setupGraphics();

        foreach (Button button in p1)
        {
            button.GetComponent<Button>().enabled = true;
            button.GetComponent<Button>().interactable = false; 

        }

        for (int p1 = 0; p1 < 8; p1++)
        {
            Cards[p1].GetComponent<Button>().interactable = false; 

            if (Cards[p1].GetComponent<Card>().allowable == 1 && Cards[p1].GetComponent<Card>().state == 0)
                Cards[p1].GetComponent<Button>().interactable = true;

        }

        canNow = true;
    }

    IEnumerator putP4Card()
    {
        canNow = false;
        yield return new WaitForSeconds(0.5f);
        int t4;


        for (int p4 = 24; p4 < 32; p4++)
        {
            if (Cards[p4].GetComponent<Card>().allowable == 1 && Cards[p4].GetComponent<Card>().state == 0)
                pl4.Add(p4);

        }

        t4 = Random.Range(0, pl4.Count);
        c.Add(pl4[t4]);
        Cards[pl4[t4]].GetComponent<Card>().state = 1;
       
        Cards[pl4[t4]].GetComponent<Card>().moveCard();
        Cards[pl4[t4]].GetComponent<Card>().allowToPerform();
        Cards[pl4[t4]].GetComponent<Card>().setupGraphics();



        canNow = true; 
    }

    IEnumerator putP2CardFirst()
    {
        canNow = false;
        yield return new WaitForSeconds(2.5f);
        int tf2;
              

        for (int p2 = 8; p2 < 16; p2++)
        {
            if (Cards[p2].GetComponent<Card>().state == 0)
                pl2.Add(p2);

        }
        tf2 = Random.Range(0, pl2.Count);
        c.Add(pl2[tf2]);
        Cards[pl2[tf2]].GetComponent<Card>().state = 1;

        Cards[pl2[tf2]].GetComponent<Card>().moveCard();
        Cards[pl2[tf2]].GetComponent<Card>().allowToPerform();
        Cards[pl2[tf2]].GetComponent<Card>().setupGraphics();

        canNow = true;
    }

    IEnumerator putP3CardFirst()
    {
        canNow = false;

        yield return new WaitForSeconds(3.0f);
        int tf3;


        for (int p3 = 16; p3 < 24; p3++)
        {
            if (Cards[p3].GetComponent<Card>().state == 0)
                pl3.Add(p3);

        }
        tf3 = Random.Range(0, pl3.Count);
        c.Add(pl3[tf3]);
        Cards[pl3[tf3]].GetComponent<Card>().state = 1;

        lockPeers(pl3[tf3]);
        cardSlced();

        Cards[pl3[tf3]].GetComponent<Card>().moveCard();
        Cards[pl3[tf3]].GetComponent<Card>().allowToPerform();
        Cards[pl3[tf3]].GetComponent<Card>().setupGraphics();

        foreach (Button button in p1)
        {
            button.GetComponent<Button>().enabled = true;
            button.GetComponent<Button>().interactable = false;

        }

        

        for (int p1 = 0; p1 < 8; p1++)
        {
            Cards[p1].GetComponent<Button>().interactable = false;

            if (Cards[p1].GetComponent<Card>().allowable == 1 && Cards[p1].GetComponent<Card>().state == 0)
                Cards[p1].GetComponent<Button>().interactable = true;

        }

        canNow = true;
    }


    IEnumerator putP4CardFirst()
    {
        canNow = false;

        yield return new WaitForSeconds(1.5f);
        int tf4;


        for (int p4 = 24; p4 < 32; p4++)
        {
            if (Cards[p4].GetComponent<Card>().state == 0)
                pl4.Add(p4);

        }
        tf4 = Random.Range(0, pl3.Count);
        c.Add(pl4[tf4]);
        Cards[pl4[tf4]].GetComponent<Card>().state = 1;

        Cards[pl4[tf4]].GetComponent<Card>().moveCard();
        Cards[pl4[tf4]].GetComponent<Card>().allowToPerform();
        Cards[pl4[tf4]].GetComponent<Card>().setupGraphics();

        canNow = true;
    }





    void cardSlced()
    {
        int p=0;

        for (int t = 0; t < Cards.Length; t++) {
            if (Cards[t].GetComponent<Card>().state == 1)
            {

                for (int j = 0; j < Cards.Length; j++)
                {
                    if (Cards[j].GetComponent<Card>().cType == Cards[t].GetComponent<Card>().cType)
                    {
                        Cards[j].GetComponent<Button>().interactable = false;
                        p = j;
                    }
                }
            }
    }
       

    }



    IEnumerator cardWipe() {

       

        yield return new WaitForSeconds(2.5f);

        for (int j = 0; j < Cards.Length; j++)
        {
            if (Cards[j].GetComponent<Card>().state == 2)
            {
                foreach (GameObject g in slcdCards)
                {
                    g.gameObject.SetActive(false);
                    g.GetComponent<Card>().setInitialState();

                }
                Cards[j].GetComponent<Card>().state = 3;
            }
        }
    }


    public void nextCardSet(int z)
    {
        dissableAll();

        // int ct = Cards[z].GetComponent<Card>().cType;
        int ct = z;

        if (ct == 1) {
            foreach (Button button in p4)
            {
                if (button.GetComponent<Card>().state == 0)
                    button.GetComponent<Card>().chance = 1;
               
                

            }
            chanceId = 4;
        }

        else if (ct==2) {
            foreach (Button button in p3)
            {
                if (button.GetComponent<Card>().state == 0)
                    button.GetComponent<Card>().chance = 1;
                chanceId = 3;


            }
            chanceId = 3;
        }

        else if (ct == 3)
        {
            foreach (Button button in p1)
            {
                if (button.GetComponent<Card>().state == 0) { 
                button.GetComponent<Card>().chance = 1;
                    button.GetComponent<Button>().enabled = true;
                }
                


            }
            chanceId = 1;
        }

        else if (ct == 4)
        {
            foreach (Button button in p2)
            {
                if (button.GetComponent<Card>().state == 0)
                    button.GetComponent<Card>().chance = 1;

                
            }
            chanceId = 2;
        }


        
    }



    void cardComparison(int f)
    {
        

        int[] temp = new int[4];
        int cIndex = 0;
        int fv = Cards[f].GetComponent<Card>().cardValue;
        int cv=-1;
        bool putTrump = false;
        int x = 0;

        for (int i = 0; i < 4; i++)
        {
            if (Cards[c[i]].GetComponent<Card>().trumpValue > 32)
            {
                temp[i] = Cards[c[i]].GetComponent<Card>().trumpValue;
                putTrump = true;
            }
            else {
                cv = Cards[c[i]].GetComponent<Card>().cardValue;

                if ((fv >= 1 && fv <= 8) && (cv >= 1 && cv <= 8))
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


            updateTricks(cIndex, c);
            MaxText.text = "Max Card: " + maxValue;


        }
        else if (putTrump)
        {
            x = 2;
            for (int j = 0; j < 4; j++)
            {

                if (Cards[c[j]].GetComponent<Card>().trumpValue == maxValue)
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
        resetChance();
        first = 99;

        if (0 <= c[cIndex] && c[cIndex] < 8)
        {
            _tricksP1++;
            trickText1.text = "Tricks :" + _tricksP1;

            foreach (Button button in p1)
            {
                button.GetComponent<Card>().chance = 1;
                button.GetComponent<Button>().enabled = true;
                button.GetComponent<Button>().interactable = true;
                

            }

            chanceId = 1;

        }

        else if (8 <= c[cIndex] && c[cIndex] < 16)
        {
            _tricksP2++;
            trickText2.text = "Tricks :" + _tricksP2;

            foreach (Button button in p2)
            {
                button.GetComponent<Card>().chance = 1;


            }

            chanceId = 2;
        }

        else if (16 <= c[cIndex] && c[cIndex] < 24)
        {
            _tricksP3++;
            trickText3.text = "Tricks :" + _tricksP3;

            foreach (Button button in p3)
            {
                button.GetComponent<Card>().chance = 1;

            }

            chanceId = 3;
        }

        else if (24 <= c[cIndex] && c[cIndex] < 32)
        {
            _tricksP4++;
            trickText4.text = "Tricks :" + _tricksP4;
            foreach (Button button in p4)
            {
                button.GetComponent<Card>().chance = 1;

            }

            chanceId = 4;
        }

        _tt = _tricksP1 + _tricksP2 + _tricksP3 + _tricksP4;
        if (_tt==8) {

            StartCoroutine(waitForPoints(_tricksP1, _tricksP2, _tricksP3, _tricksP4));
        }

        resetAllowable();
        pl1.Clear();
        pl2.Clear();
        pl3.Clear();
        pl4.Clear();
        canNow = true;
        

    }


    void resetAllowable()
    {
        for (int k=0;k<Cards.Length;k++) {
            Cards[k].GetComponent<Card>().allowable = 0;
        }
    }

    void resetChance()
    {
        for (int k = 0; k < Cards.Length; k++)
        {
            Cards[k].GetComponent<Card>().chance = 0;
        }
        chanceId = 0;
    }


    void unlockCards() {
        foreach (Button button in p1)
        {
            button.GetComponent<Button>().interactable = true;
            
        }
    foreach (Button button in p2)
        {
            button.GetComponent<Button>().enabled = false;

        }
        foreach (Button button in p3)
        {
            button.GetComponent<Button>().enabled = false;

        }
        foreach (Button button in p4)
        {
            button.GetComponent<Button>().enabled = false;

        }


    }  




    void dissableAll()
    {
        foreach (Button button in p1)
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponent<Card>().chance = 0;
            chanceId = 0;

        }
        foreach (Button button in p2)
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponent<Card>().chance = 0;
            chanceId = 0;

        }
        foreach (Button button in p3)
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponent<Card>().chance = 0;
            chanceId = 0;

        }
        foreach (Button button in p4)
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponent<Card>().chance = 0;
            chanceId = 0;

        }


    }

    IEnumerator waitForPoints(int p1, int p2, int p3, int p4) {
        yield return new WaitForSeconds(2.6f);
        calculatePoints(p1, p2, p3, p4);
    }



    void calculatePoints(int p1, int p2, int p3, int p4) {
        
       StartCoroutine(cardWipe());

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
            Cards[i].GetComponent<Button>().enabled = true;
            Cards[i].GetComponent<Card>().state = 0;
            Cards[i].GetComponent<Card>().trumpValue = 0;
            Cards[i].GetComponent<Card>().initialized = false;
            Cards[i].GetComponent<Card>().flip();
        }

        canNow = true;
        initializeCards();
        clearTricks();




    }


    void clearTricks() {
        trickText1.text = "Tricks : 00";
        trickText2.text = "Tricks : 00";
        trickText3.text = "Tricks : 00";
        trickText4.text = "Tricks : 00";

    }
    
    

    void lockPeers(int i)
    {
        int val = Cards[i].GetComponent<Card>().cardValue;

        for (int x = 0; x < Cards.Length; x++)
        {                        

            if (Cards[x].GetComponent<Card>().state == 0) {
                

                if (1 <= val && val < 9)
                {
                    if (Cards[x].GetComponent<Card>().cardValue >= 1 && Cards[x].GetComponent<Card>().cardValue < 9 )
                    {
                        Cards[x].GetComponent<Card>().allowable = 1;
                        
                       
                    }
                }




                else if (9 <= val && val < 17)
                {
                    if (Cards[x].GetComponent<Card>().cardValue >= 9 && Cards[x].GetComponent<Card>().cardValue < 17)
                    {
                        Cards[x].GetComponent<Card>().allowable = 1;
                        
                    }
                }


                else if (17 <= val && val < 25)
                {
                    if (Cards[x].GetComponent<Card>().cardValue >= 17 && Cards[x].GetComponent<Card>().cardValue < 25)
                    {                                              
                        Cards[x].GetComponent<Card>().allowable = 1;
                        
                    }
                }

                else if (25 <= val && val < 33)
                {
                    if (Cards[x].GetComponent<Card>().cardValue >= 25 && Cards[x].GetComponent<Card>().cardValue < 33) { 
                   
                    Cards[x].GetComponent<Card>().allowable = 1;

                        
                    }
                }

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
            if (Cards[a].GetComponent<Card>().allowable==1 && Cards[a].GetComponent<Card>().state == 0)
            {
                count1++;
            }
        }
        if (count1 == -1)
        {
            for (int a = 0; a < 8; a++)
            {
               
                Cards[a].GetComponent<Card>().allowable = 1;
                
                
                
            }
        }

        for (int b = 8; b < 16; b++)
        {
            if (Cards[b].GetComponent<Card>().allowable == 1 && Cards[b].GetComponent<Card>().state == 0)
            {
                count2++;
            }
        }

        if (count2 == -1)
        {
            for (int b = 8; b < 16; b++)
            {
                
                Cards[b].GetComponent<Card>().allowable = 1;
                
            }
        }

        for (int c = 16; c < 24; c++)
        {
            if (Cards[c].GetComponent<Card>().allowable == 1 && Cards[c].GetComponent<Card>().state == 0)
            {
                count3++;

            }

        }

        if (count3 == -1)
        {
            for (int c = 16; c < 24; c++)
            {
                
                Cards[c].GetComponent<Card>().allowable = 1;

                
            }
        }

        for (int d = 24; d < 32; d++)
        {
            if (Cards[d].GetComponent<Card>().allowable == 1 && Cards[d].GetComponent<Card>().state == 0)
            {
                count4++;

            }

        }

        if (count4 == -1)
        {
            for (int d = 24; d < 32; d++)
            {
              
                Cards[d].GetComponent<Card>().allowable = 1;
                
                
                    
            }
        }
    }








}







    







