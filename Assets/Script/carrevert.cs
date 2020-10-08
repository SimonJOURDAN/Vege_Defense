using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrevert : MonoBehaviour
{

    private node node;

    public void SetNode(node n){
        node = n;
    }

    public node GetNode(){
        return node;
    }
}
