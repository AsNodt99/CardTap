using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class tapCards : MonoBehaviour
{
    bool moveG = true;
    public Transform Gallery;
    public GameObject content;
    public int nCards = 6;
    public Card Center;
    public float tapSpd = 10;
    public GameObject Cpref;
    public GameObject[] ObjCards;
    public DBcard[] c;
    bool load = false;
    int nextP = 0;
    public GameObject scrollbar;
    private float scroll_pos = 0;
    float[] pos;
   public bool lok=false;

    public struct DBcard
        {
        public int ID;
        public bool flip;
        public string modelAdr;
        public string head;
        public string descr;
        public string modelA;

        }
    /*private void onLoad(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        
    }*/
    // Start is called before the first frame update
    void Start()
    {
        ObjCards = new GameObject[nCards];
        c = new DBcard[nCards];
       
        
        if (PlayerPrefs.GetInt("1") == 0)
            load = false;
        else
            load = true;

            c[0].ID = 1;       
            c[0].flip = load;
            c[0].modelAdr = "";
            c[0].head = "Ivan";
            c[0].descr = "Nimble Troll Cannibal";
            c[0].modelA = "Assets/Recources/Troll_cannibal 1.prefab";

        if (PlayerPrefs.GetInt("2") == 0)
            load = false;
        else
            load = true;

            c[1].ID = 2;
            c[1].flip = load;
            c[1].modelAdr = "";
            c[1].head = "Petr";
            c[1].descr = "Mannerly Troll Cannibal";
            c[1].modelA = "Assets/Recources/Troll_cannibal 1.prefab";

        if (PlayerPrefs.GetInt("3") == 0)
            load = false;
        else
            load = true;

            c[2].ID = 3;
            c[2].flip = load;
            c[2].modelAdr = "";
            c[2].head = "Egor";
            c[2].descr = "Sublime Troll Cannibal";
            c[2].modelA = "Assets/Recources/Troll_cannibal 1.prefab";

        if (PlayerPrefs.GetInt("4") == 0)
            load = false;
        else
            load = true;

            c[3].ID = 4;
            c[3].flip = load;
            c[3].modelAdr = "";
            c[3].head = "Alexandr";
            c[3].descr = "Gentle Troll Cannibal";
            c[3].modelA = "Assets/Recources/Troll_cannibal 1.prefab";


        if (PlayerPrefs.GetInt("5") == 0)
            load = false;
        else
            load = true;

            c[4].ID = 5;
            c[4].flip = load;
            c[4].modelAdr = "";
            c[4].head = "Stepan";
            c[4].descr = "Insolent Troll Cannibal";
            c[4].modelA = "Assets/Recources/Troll_cannibal 1.prefab";

        if (PlayerPrefs.GetInt("6") == 0)
            load = false;
        else
            load = true;

            c[5].ID = 6;
            c[5].flip = load;
            c[5].modelAdr = "";
            c[5].head = "Nikolai";
            c[5].descr = "A little mad Troll Cannibal";
            c[5].modelA = "Assets/Recources/Troll_cannibal 1.prefab";


        float dist = 0;
        for (int i = 0; i < nCards; i++)
        {
            
           var tC = Instantiate(Cpref,new Vector3(dist,-8.3f,0),Cpref.transform.rotation);
            dist += 5;
            tC.GetComponent<Card>().ID= c[i].ID;
            tC.GetComponent<Card>().flip = c[i].flip;
            float yFlp=0;
            if (tC.GetComponent<Card>().flip == false)
                yFlp = 180;
            tC.transform.rotation = Quaternion.Euler(new Vector3(tC.transform.rotation.x, yFlp, 0));
            tC.GetComponent<Card>().head = c[i].head;
            tC.GetComponent<Card>().modelAdress = c[i].modelAdr;
            tC.GetComponent<Card>().description = c[i].descr;
            tC.GetComponent<Card>().modelA = c[i].modelA;
            ObjCards[i] = tC;
            tC.transform.SetParent(content.transform);
        }
        Center = ObjCards[0].GetComponent<Card>();
        ObjCards[0].GetComponent<Card>().center = true;
    }
    public void BGallery()
    {
        moveG = !moveG;
    }
    // Update is called once per frame
    void Update()
    {



        // slide 
        pos = new float[content.transform.childCount];
        float distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        // Debug.Log(scrollbar.GetComponent<Scrollbar>().value);
        if (Input.GetMouseButton(0))
        {

            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;


        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {



                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    if (lok == false)
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);

                }

                if (scrollbar.GetComponent<Scrollbar>().value < pos[i] + (distance / 3) && scrollbar.GetComponent<Scrollbar>().value > pos[i] - (distance / 3))
                {
                    if (lok == false)
                    {
                        Center.center = false;
                        Center = ObjCards[i].GetComponent<Card>();
                        ObjCards[i].GetComponent<Card>().center = true;
                    }
                }

            }
        }

        // Flip card
        if (Center != null)
            if (Center.flip == true)
            {
                PlayerPrefs.SetInt("" + Center.ID, 1);
                PlayerPrefs.Save();
                Debug.Log(PlayerPrefs.GetInt("" + Center.ID));
                Center.transform.rotation = Quaternion.Slerp(Center.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * tapSpd);
                if (Center.open)
                {
                    lok = true;

                    for (int i = 0; i <= pos.Length; i++)
                    {
                        if (i == pos.Length)
                        {
                            nextP = 0;
                            break;
                        }
                        if (ObjCards[i].GetComponent<Card>().flip == false)
                        {
                            nextP = i;
                            ObjCards[i].GetComponent<Card>().center = false;
                            break;
                        }
                    }
                    // Center = ObjCards[Center.ID].GetComponent<Card>();
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[nextP], Time.deltaTime * 2f);
                    if (scrollbar.GetComponent<Scrollbar>().value > pos[nextP] - (distance / 4) && scrollbar.GetComponent<Scrollbar>().value < pos[nextP] + (distance / 4))
                    {
                        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
                        Center.open = false;
                        lok = false;

                        Center.center = false;
                        Center = ObjCards[nextP].GetComponent<Card>();
                        ObjCards[nextP].GetComponent<Card>().center = true;



                    }



                }

            }
            else
            {
                PlayerPrefs.SetInt("" + Center.ID, 0);
                PlayerPrefs.Save();
                Debug.Log(PlayerPrefs.GetInt("" + Center.ID));
                Center.transform.rotation = Quaternion.Slerp(Center.transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * tapSpd);
            }
        if (moveG)
        {
            for (int i = 1; i < ObjCards.Length; i++)
            {
                ObjCards[i].transform.position = Vector3.MoveTowards(ObjCards[i].transform.position, ObjCards[0].transform.position, 20 * Time.deltaTime);
            }
          //  if(ObjCards[ObjCards.Length-1].transform.position==ObjCards[0].transform.position)
            Gallery.position = Vector3.MoveTowards(Gallery.position, new Vector3(-18, 0, 0), 15 * Time.deltaTime);
        }
        else
        {
            for (int i = 1; i < ObjCards.Length; i++)
            {
                ObjCards[i].transform.position = Vector3.MoveTowards(ObjCards[i].transform.position, new Vector3(ObjCards[i-1].transform.position.x+5, ObjCards[i].transform.position.y, ObjCards[i].transform.position.z), 20 * Time.deltaTime);
            }
            Gallery.position = Vector3.MoveTowards(Gallery.position, new Vector3(0, 0, 0), 15 * Time.deltaTime);
        }

    }
}
