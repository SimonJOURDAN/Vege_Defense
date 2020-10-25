using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrevert : MonoBehaviour
{

    private node node; //La node à laquelle le carré vert est lié

    public void SetNode(node n){ //Méthode permettant de définir la node à laquelle le carré vert est lié
        node = n;
    }

    public node GetNode(){ //Méthode permettant de récupérer la node à laquelle le carré vert est lié
        return node;
    }
}
