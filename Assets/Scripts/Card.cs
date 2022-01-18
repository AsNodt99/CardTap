using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    public Text BoxHead;
    public Text BoxDesc;
    public Text timer;
    public float time=0;
    public bool lok = false;
    public bool open=false;
    public int ID=1;    
    public bool flip = false;
    public string modelAdress;
    public string head;
    public string description;
    public bool center = false;
    public string modelA;
    public GameObject model;
    public MaskObjects[] mask;
    public GameObject test;
    public GameObject[] textMask;
    public Canvas can;
    tapCards parent;
    private void OnMouseDown()
    {
        if (lok == false)
        {
            //flip status
            if (center)
                flip = !flip;
            //check flip condition and ui hide/show on flip
            if (flip)
            {
                open = true;
                BoxHead.transform.gameObject.SetActive(true);
                BoxDesc.transform.gameObject.SetActive(true);
                
            }
            else
            {
                BoxHead.transform.gameObject.SetActive(false);
                BoxDesc.transform.gameObject.SetActive(false);
            }
        }
    }
  
    // Start is called before the first frame update
    void Start()
    {
        //Timer load 
        time = PlayerPrefs.GetFloat("Timer");
        //Canvas settings
        can.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
        can.transform.localPosition = new Vector3(0.55f,-0.93f,-0.01f);
      // 3D Model load form address stored in struct
        Addressables.LoadAssetAsync<GameObject>(""+modelA).Completed += (onLoad) => {
            model = onLoad.Result;
            //instantiate loaded model into world
            model = Instantiate(model, transform);
            //randomize appearing of model for better mask effect
           float x= Random.Range(-2,2);
            float y = Random.Range(-1, 1);
            model.transform.localPosition = new Vector3(model.transform.localPosition.x+x, model.transform.localPosition.y+y, model.transform.localPosition.z);
           /* for (int i = 0; i < 4; i++)
            {
                mask[i].maskOb[0] = model.transform.GetChild(0).gameObject;
                mask[i].maskOb[1] = model.transform.GetChild(1).gameObject;
            }*/
        };
        //loading UI text form struct
        BoxHead.text = head;
        BoxDesc.text = description;


    }

    // Update is called once per frame
    void Update()
    {
        //Timer for first only card while closed
        if (ID == 1)//first card
        {
            if (flip==false)//when closed
            {          
               
                lok = true; // lock card flip for time 
                time += Time.deltaTime;
                int i = (int)time;
                timer.text = i.ToString();//converting float to string rounded and show on UI
                PlayerPrefs.SetFloat("Timer", time);// save corrent timer
                if (time > 5) //timer=5 seconds then closing timer
                {
                    timer.text = null;
                    flip = true;
                    lok = false;
                    time = 0;
                }
            }

        }
    }
}
