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
    bool lok = false;
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
            if (center)
                flip = !flip;
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
        time = PlayerPrefs.GetFloat("Timer");
        can.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
        can.transform.localPosition = new Vector3(0.55f,-0.93f,-0.01f);
       
        Addressables.LoadAssetAsync<GameObject>(""+modelA).Completed += (onLoad) => {
            model = onLoad.Result;
          model=  Instantiate(model, transform);
           float x= Random.Range(-2,2);
            float y = Random.Range(-1, 1);
            model.transform.localPosition = new Vector3(model.transform.localPosition.x+x, model.transform.localPosition.y+y, model.transform.localPosition.z);
           /* for (int i = 0; i < 4; i++)
            {
                mask[i].maskOb[0] = model.transform.GetChild(0).gameObject;
                mask[i].maskOb[1] = model.transform.GetChild(1).gameObject;
            }*/
        };

        BoxHead.text = head;
        BoxDesc.text = description;


    }

    // Update is called once per frame
    void Update()
    {
        if (ID == 1)
        {
            if (flip==false)
            {
               
                int i = (int)time;
                timer.text = i.ToString();
                PlayerPrefs.SetFloat("Timer", time);
                lok = true;
                time += Time.deltaTime;
                if (time > 5)
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
