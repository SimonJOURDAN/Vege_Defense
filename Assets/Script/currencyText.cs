using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencyText : MonoBehaviour
{
    public Text rewardtext; //Texte qui contient le compteur

    public Image currencyImage; //Image à afficher avec le compteur

    public GameObject canvas; //Canvas qui contient l'ensemble

    private Vector2 up = new Vector2(0, 1); //Vecteur pointant vers le "haut"


    public void display(int nombre){ //Méthode permettant d'afficher un entier et d'ajouter sa valeur à la somme totale du joueur
        rewardtext.text = "+"+(nombre.ToString()); //Change le contenu du texte
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().earn(nombre); //Ajoute la valeur nombre à la somme totale disponible pour le joueur
        fade();
    }

    public void fade(){ //Méthode faisant disparaitre l'affichage
        rewardtext.CrossFadeAlpha(0f, 1f, false); //Changement de l'alpha du texte vers 0 en 1 seconde
        currencyImage.CrossFadeAlpha(0f, 1f, false); //Changement de l'alpha de l'image vers 0 en 1 seconde
    }

    public void Update(){ //Méthode appelée 1 fois par frame
        transform.Translate(up * Time.fixedDeltaTime * 0.5f, Space.World); //Déplacement de l'ensemble vers le "haut"
        if(rewardtext.canvasRenderer.GetAlpha() == 0){ //Si l'apha du text est égal à 0
            Destroy(canvas); //Destruction de l'ensemble
        }
    }
}
