using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obama : MonoBehaviour
{
    GameObject obamamove;
    public Text obamatext;
    string tagenter;
    void Start()
    {
        obamamove = this.gameObject;
        obamatext.enabled=false;
    }

    public void surprise(string tag){
        tagenter=tag;
        if(tagenter=="obama"){
            obamaa();
        }

    }

    public void obamaa(){
        
        obamamove.transform.position=new Vector3(-2,-1,10);
        obamatext.enabled=true;
        StartCoroutine(waiter());
        Debug.Log("yo");
        
    }

    IEnumerator waiter(){
        Debug.Log("hey");
        yield return new WaitForSeconds(4);
        obamatext.enabled=false;
    }
}
