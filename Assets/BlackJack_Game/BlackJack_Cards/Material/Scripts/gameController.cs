using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{

    public static gameController instance;
    public GameObject betMaxText;

    float counter;
    float interval;
    float iterate;

    public Sprite[] allSprite;

    public int[] deck;


    public GameObject prefabCard;
    public GameObject posDeck;
    public GameObject textHand;
    public GameObject iaTextHand;
    public GameObject iaHAND;
    public GameObject myHand;
    GameObject currentGo;

    List<GameObject> allCard;

    Vector3 positionToGo;
    Vector3 positionToGo2;
    Vector3 cardGoHere;

    bool fly;
    int nbTake;
   
    bool beginGame;
    int textHandValue;
    int iaTextHandValue;
    bool stay;
    bool endGame;
    int earlyGame;
    bool playerWin;
    bool oneTimeAction;

    bool startTheGame;

    public GameObject holderCanvas;
    public GameObject holderObject;
    public GameObject textBet;
    public int valBet;
    public int playerCoin;
    public Text totalCoin;
    public GameObject holderEarly;
    public GameObject holderBet;
    public GameObject GoHereWinner;
    public Text textWinner;
    public GameObject holderWinner;
    bool translateWinner;
    bool equalWin;
    public GameObject holderBackButton;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {


        //showAds.instance.ShowInterstitial();
        equalWin = false;
        translateWinner = false;


        
       // PlayerPrefs.DeleteKey("playerCoin");//Comment this lane when launching

        playerCoin = PlayerPrefs.GetInt("playerCoin", 5000);
        totalCoin.text = playerCoin + "$";
     
        startTheGame = false;
        oneTimeAction = false;
        playerWin = false;
        earlyGame = 0;
        endGame = false;
        stay = false;
        beginGame = false;
        counter = 0;
        interval = 1f;
        nbTake = 0;
        textHandValue = 0;
        iaTextHandValue = 0;
        textHand.GetComponent<TextMesh>().text = textHandValue + "";
        iaTextHand.GetComponent<TextMesh>().text = iaTextHandValue + "";
        positionToGo = iaHAND.transform.position;
        positionToGo2 = myHand.transform.position;
        positionToGo.z -= 1;
        positionToGo2.z -= 1;
        cardGoHere = positionToGo;
        fly = false;
        allCard = new List<GameObject>();
        int xVal = 1;

        for (int i = 0; i < allSprite.Length; i++)
        {
            GameObject card = (GameObject)Instantiate(prefabCard, posDeck.transform.position, Quaternion.identity);
            card.GetComponent<SpriteRenderer>().sprite = allSprite[i];
            card.GetComponent<cardScript>().value = xVal;
            card.SetActive(false);
            xVal++;
            if (xVal > 13)
                xVal = 1;
            allCard.Add(card);
        }
       
    }


    public void validBet()
    {
        Debug.Log(valBet + " =====valbet=======");
      betMaxText.SetActive(false);

        if (valBet > 0)
        {
            holderEarly.SetActive(false);
            holderBet.SetActive(false);
            holderBackButton.SetActive(false);
            startTheGame = true;
        }
        else
        {
            print("BET MORE");
        }
    }


    public void addBet(int val)
    {
        if (startTheGame == false)
        {
            playerCoin -= val;
            PlayerPrefs.SetInt("playerCoin", playerCoin);
            totalCoin.text = playerCoin + "$";
            valBet += val;
            textBet.GetComponent<TextMesh>().text = valBet + " $";
        }
    }

    void drawCard()
    {
        fly = true;
        bool ok = false;
        cardGoHere = positionToGo;
        while (ok == false)
        {
            currentGo = allCard[Random.Range(0, allCard.Count)];
            if (currentGo.activeSelf == false)
            {
                nbTake++;
                currentGo.SetActive(true);
                ok = true;
            }
        }

    }

    public void playerDraw()
    {
        print("player Draw");
        if (fly == false && nbTake < 15 && stay == false && endGame == false)
        {
            cardGoHere = positionToGo2;
            bool ok = false;
            while (ok == false)
            {
                currentGo = allCard[Random.Range(0, allCard.Count)];
                if (currentGo.activeSelf == false)
                {
                    nbTake++;
                    currentGo.SetActive(true);
                    ok = true;
                }
            }

            fly = true;
        }
    }

    public void playerStay()
    {
      
        if (endGame == false && earlyGame >= 3)
        {
            print("player Stay");
            stay = true;

            playerStayDraw();
        }
    }

    void playerStayDraw()
    {
        bool ok = false;
        cardGoHere = positionToGo;
        while (ok == false)
        {
            currentGo = allCard[Random.Range(0, allCard.Count)];
            if (currentGo.activeSelf == false)
            {
                nbTake++;
                currentGo.SetActive(true);
                ok = true;
            }
        }
    }


    public void backButton()
    {
        playerCoin += valBet;
        PlayerPrefs.SetInt("playerCoin", playerCoin);
        totalCoin.text = playerCoin + "$";
        Application.LoadLevel("homeScene");
    }

    IEnumerator delayRestart()
    {
        yield return new WaitForSeconds(2.5f);
        Application.LoadLevel(Application.loadedLevel);
    }


    // Update is called once per frame
    void Update()
    {
        //print winner animation
        if(translateWinner == true)
        {
            holderWinner.transform.position = Vector3.MoveTowards(holderWinner.transform.position, GoHereWinner.transform.position, 4000f * Time.deltaTime);
        }

        //wait before start the game after Bet
        if (startTheGame == true)
        {
            counter += Time.deltaTime;
            if (counter >= interval && beginGame == false)
            {
                beginGame = true;
                drawCard();
            }

        }
        //Give Reward
        if (endGame == true && oneTimeAction == false)
        {
            translateWinner = true;
            if(equalWin == true)
            {
                textWinner.text = "Equality";
                print("equality");

                playerCoin += valBet;
                PlayerPrefs.SetInt("playerCoin", playerCoin);
                totalCoin.text = playerCoin + "$";
            }
            else if(playerWin == true)
            {
                playerCoin += valBet * 2;
                PlayerPrefs.SetInt("playerCoin", playerCoin);
                totalCoin.text = playerCoin + "$";
                textWinner.text = "Player Win";
                print("GIVE TO PLAYER");
            }
            else
            {
                textWinner.text = "Bank Win";
                print("GIVE TO IA");
            }
            oneTimeAction = true;
            StartCoroutine(delayRestart());
            
        }

        //Draw card animation
        if (beginGame == true && endGame == false)
        {
            if (fly == true)
            {

                currentGo.transform.position = Vector3.MoveTowards(currentGo.transform.position, cardGoHere, 10f * Time.deltaTime);

                if(Vector3.Distance(currentGo.transform.position, cardGoHere) <= 0.01f)
                {
                    fly = false;
                    earlyGame++;
                    if (cardGoHere == positionToGo2)
                    {
                        positionToGo2.x += 0.8f;
                        positionToGo2.z -= 1;

                        int valueCard = currentGo.GetComponent<cardScript>().value;
                        if (valueCard == 1)
                        {
                            if (textHandValue + 11 < 22)
                            {
                                valueCard = 11;
                            }
                            else
                            {
                                valueCard = 1;
                            }
                        }
                        else if (valueCard > 10)
                            valueCard = 10;

                        textHandValue += valueCard;
                        textHand.GetComponent<TextMesh>().text = textHandValue + "";
                        //Winning or loosing
                        if (textHandValue > 21)
                        {
                            playerWin = false;
                            print("IA WIN");
                            endGame = true;
                        }else if (textHandValue == 21)
                        {
                            print("Player WIN");
                            playerWin = true;
                            endGame = true;
                        }
                    }  
                    else
                    {
                        positionToGo.x += 0.8f;
                        positionToGo.z -= 1;

                        int valueCard = currentGo.GetComponent<cardScript>().value;
                        if (valueCard == 1)
                        {
                            if (iaTextHandValue + 11 < 22)
                            {
                                valueCard = 11;
                            }
                            else
                            {
                                valueCard = 1;
                            }
                        }
                        else if (valueCard > 10)
                            valueCard = 10;

                        iaTextHandValue += valueCard;
                        iaTextHand.GetComponent<TextMesh>().text = iaTextHandValue + "";
                    }
                    //Early auto draw
                    if(earlyGame < 3)
                    {
                        playerDraw();
                    }
                    if(earlyGame == 3)
                    {
                        holderCanvas.SetActive(true);
                        holderObject.SetActive(true);
                    }

                }
            }

            if (stay == true && endGame == false)
            {
                currentGo.transform.position = Vector3.MoveTowards(currentGo.transform.position, cardGoHere, 10f * Time.deltaTime);

                if (Vector3.Distance(currentGo.transform.position, cardGoHere) <= 0.01f)
                {
                    positionToGo.x += 0.8f;
                    positionToGo.z -= 1;

                    int valueCard = currentGo.GetComponent<cardScript>().value;

                    if(valueCard == 1)
                    {
                        if(iaTextHandValue + 11 < 22)
                        {
                            valueCard = 11;
                        }
                        else
                        {
                            valueCard = 1;
                        }
                    }
                   else if (valueCard > 10)
                        valueCard = 10;

                    iaTextHandValue += valueCard;
                    iaTextHand.GetComponent<TextMesh>().text = iaTextHandValue + "";

                    //ia loose or win ?
                    if(iaTextHandValue > 21)
                    {
                        endGame = true;
                        print("PlayerWin");
                        playerWin = true;
                    }
                    else if (iaTextHandValue > textHandValue)
                    {
                        endGame = true;
                        print("IAWIN");
                        playerWin = false;
                    }
                    else if(iaTextHandValue < 17)
                    {
                        playerStayDraw();
                    }
                    else if(iaTextHandValue == textHandValue)
                    {
                        equalWin = true;
                        endGame = true;
                    }
                    else
                    {
                        endGame = true;
                        print("PlayerWin");
                        playerWin = true;
                    }
                         
                }

            }
        }
    }
}
