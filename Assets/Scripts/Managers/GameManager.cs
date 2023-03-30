using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    Queue<Pole> poleQueue = new Queue<Pole>(); // queu of 3 poles
    private Pole lastPole; // last pole in queue
    private Pole tempPole;


    //[Range(3, 5)] [SerializeField] int ringAmount;
    private int ringAmount;
    //private List<Pole> poleList;

    private List<Ring> ringList; // contains all rings
    private Ring selectedRing; // ring currently selected by player
    private bool selected; // whether player has selected a ring or not
    private Pole originPole; // pole where the selected ring originated from


    private void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        ringAmount = 3; // default amount

        for(int i=1; i>=-1; i--)
        {
            tempPole = AssetManager.Instance.getPole();
            tempPole.poleConstructor(i);

            poleQueue.Enqueue(tempPole);
            if (i == 1)
            {
                lastPole = tempPole;
            }
        }

        ringList = new List<Ring>();

        instantiateRings();

        selected = false;
    }
    public void resetGame()
    {

        while (poleQueue.Count > 0)
        {
            poleQueue.Dequeue().resetPole();
        }

        for (int i = -1; i <= 1; i++)
        {
            tempPole = AssetManager.Instance.getPole();
            tempPole.poleConstructor(i);
            poleQueue.Enqueue(tempPole);
            if (i == 1)
            {
                lastPole = tempPole;
            }
        }

        selected = false;
        selectedRing = null;

        foreach (Ring r in ringList)
        {
            r.resetRing();
        }

        ringList.Clear();

        UIManager.Instance.resetCounter();

        instantiateRings();

    }

    public int getRingAmount()
    {
        return ringAmount;
    }
    public void setRingAmount(int ringCount)
    {
        ringAmount = ringCount;
    }
    public void selectRing(Ring newRing)
    {
        selectedRing = newRing;
        //Debug.Log("Selecting ring width: " + selectedRing.getRingWidth());
        selectedRing.floatRing();
        selected = true;
    }
    public void hoveredPole(Vector3 polePosition)
    {
        if (selected)
        {
            selectedRing.setPosition(polePosition);
        }/*
        else
        {
            ;
        }*/
    }

    public void selectedPole(Pole touchedPole)
    {
        if (selected)
        {
            if ( (touchedPole.getPoleCount() == 0) || (touchedPole.getTopRingWidth() < selectedRing.getRingWidth()) || (touchedPole.getPolePosition() == originPole.getPolePosition()) )
            {
                if(touchedPole.getPolePosition() != originPole.getPolePosition())
                {
                    UIManager.Instance.incrementCounter();
                }
                unselectRing(touchedPole);
                // Debug.Log("droppin on no ring");

            }
            /*else if(touchedPole.getPolePosition() == originPole.getPolePosition()){
				unselectRing();
			}*/
           /* else if (touchedPole.getTopRingWidth() > selectedRing.getRingWidth() || touchedPole.getPolePosition() == originPole.getPolePosition())
            {
                //Debug.Log("selected ring is: " + selectedRing.getRingWidth() + " topringwidth is " + touchedPole.getTopRingWidth());
                unselectRing(touchedPole);
                UIManager.Instance.incrementCounter();

            }
            else
            {
                //Debug.Log("can't put it down there boss " + touchedPole.getTopRingWidth() + " and " + selectedRing.getRingWidth());

            }*/
        }
        else
        {
            if (touchedPole.getPoleCount() != 0)
            {
                //Debug.Log("do we got poles? " + touchedPole.getPoleCount());
                originPole = touchedPole;
                //Debug.Log("topringwidth is " + touchedPole.getTopRingWidth() );
                selectRing(originPole.getTopRing());
            }
            else
            {
                //Debug.Log("no ring there b0ss");
            }

        }

    }

    public void unselectRing(Pole destinationPole)
    {
        //selectedRing = null;

        selectedRing.dropRing(destinationPole.transform.position);
        selected = false;
        destinationPole.addRing(originPole.removeTopRing());
    }

    private void instantiateRings()
    {

        ringList = AssetManager.Instance.getRings();
       
        for (int i = 0; i < ringAmount; i++)
        {
            ringList[i].gameObject.SetActive(true);
            ringList[i].ringConstructor(i+1, lastPole.transform.position);

            lastPole.addRing(ringList[i]);
        }
        /*
        for (int i = 0; i < rings; i++)
        {

            Debug.Log("instantiating ,,,");
        }*/
    }
}
