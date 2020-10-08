using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencyText : MonoBehaviour
{
    public Text rewardtext;

    public Image currencyImage;

    public GameObject canvas;

    private Vector2 up = new Vector2(0, 1);


    public void display(int nombre){
        rewardtext.text = "+"+(nombre.ToString());
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().earn(nombre);
        fade();
    }

    public void fade(){
        rewardtext.CrossFadeAlpha(0f, 1f, false);
        currencyImage.CrossFadeAlpha(0f, 1f, false);
    }

    public void Update(){
        transform.Translate(up * Time.fixedDeltaTime * 0.5f, Space.World);
        if(rewardtext.canvasRenderer.GetAlpha() == 0){
            Destroy(canvas);
        }
    }
}
