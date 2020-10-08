using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Text moneyText;
    public int startMoney;
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    public bool affordable(int cost){
        return(cost<=money);
    }

    public void spend(int cost){
        money-=cost;
        moneyText.text = money.ToString();
    }

    public void earn(int amount){
        money+=amount;
        moneyText.text = money.ToString();
    }
}
