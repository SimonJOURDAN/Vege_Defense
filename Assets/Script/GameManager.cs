using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Text moneyText;
    public int startMoney;
    public int money;

    void Start()    //Méthode appelée avant la première update
    {
        money = startMoney; //Initialisation de l'argent disponible
        moneyText.text = money.ToString();  //Initialisation de l'affichage de l'argent
    }

    // Update is called once per frame
    public bool affordable(int cost){   //Méthode qui permet de déterminer si le joueur pas dépenser un certain montant
        return(cost<=money);
    }

    public void spend(int cost){    //Méthode qui fait dépenser un certain montant
        money-=cost;    //Mise à jour de l'argent disponible
        moneyText.text = money.ToString();  //Mise à jour de l'affichage
    }

    public void earn(int amount){   //Méthode qui permet de gagner de l'argent
        money+=amount;  //Mise à jour de l'argent disponible
        moneyText.text = money.ToString();  //Mise à jour de l'affichage
    }   
}
