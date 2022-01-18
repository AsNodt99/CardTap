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


    //Structure for saving data
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
        //array for saving Card objects
        ObjCards = new GameObject[nCards];
        c = new DBcard[nCards]; //struct

        //loading information about flipping cards
        if (PlayerPrefs.GetInt("1") == 0)
            load = false;
        else
            load = true;
        // Data in struct for all cards
        c[0].ID = 1;
        c[0].flip = load;  //add loaded card status information to the structure (open/close)
        c[0].modelAdr = "";
        c[0].head = "Ivan"; //name  
        c[0].descr = "Nimble Troll Cannibal";  //description
        c[0].modelA = "Assets/Recources/Troll_cannibal 1.prefab";  //adsress of 3D model
                                                                   //same for all cards
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

        // instantiate all cards (add objects to the world)-----------------------------------------------------------------------------------------------------

        float dist = 0;
        for (int i = 0; i < nCards; i++)
        {

            var tC = Instantiate(Cpref, new Vector3(dist, -8.3f, 0), Cpref.transform.rotation);  //instantiate
            dist += 5;         // distance between cards
            // add all information about cards from struct 
            tC.GetComponent<Card>().ID = c[i].ID;
            tC.GetComponent<Card>().flip = c[i].flip;//flip status

            float yFlp = 0;
            if (tC.GetComponent<Card>().flip == false)  // if the status of the card is "closed", then flip the card(rotate y axis 180 degrees)
                yFlp = 180;
            tC.transform.rotation = Quaternion.Euler(new Vector3(tC.transform.rotation.x, yFlp, 0));// actual rotation

            tC.GetComponent<Card>().head = c[i].head;
            tC.GetComponent<Card>().modelAdress = c[i].modelAdr;
            tC.GetComponent<Card>().description = c[i].descr;
            tC.GetComponent<Card>().modelA = c[i].modelA;
            ObjCards[i] = tC; //saving all Card objects to an array for easier future use
            tC.transform.SetParent(content.transform);
        }
        //---------------------------------------------------------------------------------------------------------------------------
        Center = ObjCards[0].GetComponent<Card>();         // in the start active center card it is the first one
        ObjCards[0].GetComponent<Card>().center = true;   // adding center status to this card
    }



    public void BGallery() // after pressing the "Gallery" button, this function is called
    {
        moveG = !moveG; // change status open-> close, close-> open
    }


    // Update is called once per frame
    void Update()
    {
        //scrolling through the gallery and correct the position to the center card
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        pos = new float[content.transform.childCount]; // number of cards (objects of all Cards belong to the parent object "content"). Content is related to Scroll View. Scroll View is the basis of the gallery 

        float distance = 1f / (pos.Length - 1f); //calculate distance between cards (form 0 to 1). This is importante because scrollbar working range is between 0-1 value

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;          // posicion of each card
        }
        // Debug.Log(scrollbar.GetComponent<Scrollbar>().value);
        if (Input.GetMouseButton(0))
        {

            scroll_pos = scrollbar.GetComponent<Scrollbar>().value; // we get the value of the position of the scrollbar which we move with the mouse


        }
        else // after we stopping to drag the Gallery
        {
            for (int i = 0; i < pos.Length; i++) // for each card positions
            {



                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) // if we stop drag and we stop before or after that card
                {
                    if (lok == false) // lock this movements (this is usually needed when, after opening the Card, the gallery smoothly transitions to the first closed Card)
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f); //move the cards so that the card in position pos[i] is in the center (this is necessary to correct the position to the center after flipping through the gallery)    

                }

                if (scrollbar.GetComponent<Scrollbar>().value < pos[i] + (distance / 3) && scrollbar.GetComponent<Scrollbar>().value > pos[i] - (distance / 3)) //detect if new card become new center card
                {
                    if (lok == false)// same lock as before
                    {
                        //last card lose center status and the new cards become center card
                        Center.center = false;
                        Center = ObjCards[i].GetComponent<Card>();
                        ObjCards[i].GetComponent<Card>().center = true;
                    }
                }

            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //actual close/open effect(face down, face up) and  slow transition effect to first face-down(closed) card

        if (Center != null) 
            if (Center.flip == true) //when center card is open (face up)
            {
                PlayerPrefs.SetInt("" + Center.ID, 1); //save status of the card (open/close)
                PlayerPrefs.Save();
             
                Center.transform.rotation = Quaternion.Slerp(Center.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * tapSpd);// rotate the card face up relatively slowly (not instant)
                if (Center.open)  //"open" status is needed for the effect of a slow transition to the first closed card after opening this card
                {
                    lok = true; // lock the correcting center function (due to the conflict of two translate funcion. One is a slow translate effect and the other is a fast correct center function)

                    for (int i = 0; i <= pos.Length; i++) //search first closed(face down) card
                    {
                        if (i == pos.Length) //if all cards is open then centered card it is the first one
                        {
                            nextP = 0; //first card
                            break;
                        }
                        if (ObjCards[i].GetComponent<Card>().flip == false) //check status of corrent card (open/close)
                        {
                            nextP = i; //if this card is closed then it is the first closed card and our next position to move
                            ObjCards[i].GetComponent<Card>().center = false; //the current card loses its center status. We are moving on to a new face-down card that will become the central
                            break;
                        }
                    }
                    // Center = ObjCards[Center.ID].GetComponent<Card>();
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[nextP], Time.deltaTime * 2f); //slowly translate to that first closed card
                    if (scrollbar.GetComponent<Scrollbar>().value > pos[nextP] - (distance / 4) && scrollbar.GetComponent<Scrollbar>().value < pos[nextP] + (distance / 4)) //when we already there on a closed card 
                    {
                        //prepare to end of this function (slow translate effect)
                        scroll_pos = scrollbar.GetComponent<Scrollbar>().value; // the correction center function needs to know what happened to the current position
                        Center.open = false; //now the last card no more have this "open effect" 
                        lok = false; // remove lock (now correct center function may work)

                        Center.center = false; //remove last card center status
                        Center = ObjCards[nextP].GetComponent<Card>(); // our new closed card is the centered card 
                        ObjCards[nextP].GetComponent<Card>().center = true;// change center status



                    }



                }

            }
            else //when our card is face down. 
            {
                PlayerPrefs.SetInt("" + Center.ID, 0);//saving status "face down" (close card)
                PlayerPrefs.Save();
               
                Center.transform.rotation = Quaternion.Slerp(Center.transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * tapSpd);// rotate card face down
            }
  //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // open/close gallery
        if (moveG) //close 
        {
            for (int i = 1; i < ObjCards.Length; i++) // put all the cards in a pile
            {
                ObjCards[i].transform.position = Vector3.MoveTowards(ObjCards[i].transform.position, ObjCards[0].transform.position, 20 * Time.deltaTime);
            }
          //  if(ObjCards[ObjCards.Length-1].transform.position==ObjCards[0].transform.position)
            Gallery.position = Vector3.MoveTowards(Gallery.position, new Vector3(-18, 0, 0), 15 * Time.deltaTime); // remove gallery with cards
        }
        else // open
        {
            for (int i = 1; i < ObjCards.Length; i++) //from the deck, return all the cards to the correct position with the distance between them
            {
                ObjCards[i].transform.position = Vector3.MoveTowards(ObjCards[i].transform.position, new Vector3(ObjCards[i-1].transform.position.x+5, ObjCards[i].transform.position.y, ObjCards[i].transform.position.z), 20 * Time.deltaTime);
            }
            Gallery.position = Vector3.MoveTowards(Gallery.position, new Vector3(0, 0, 0), 15 * Time.deltaTime); // return gallery with cards
        }

    }
}
