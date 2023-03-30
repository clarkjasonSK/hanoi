using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public static Manager Instance;
    
	[SerializeField] Pole startPole;
    [SerializeField] Pole midPole;
    [SerializeField] Pole endPole;

    [SerializeField] Ring ringPrefab;

    //[Range(3, 5)] [SerializeField] int ringAmount;
    int ringAmount;
    //private List<Pole> poleList;

    private List<Ring> ringList;
	private Ring selectedRing; 
	private bool selected;
	private Pole originPole;
    //private Pole destinationPole;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
		//poleList = new List<Pole>();
		ringList = new List<Ring>();
		ringAmount = 3;

		for(int i=0; i<ringAmount; i++){
			//Debug.Log("instantiating thangs " + ringAmount + " at " + i);
			
			/*if(i<3){
				poleList.Add(Instantiate(polePrefab, new Vector3( i*3, 1, -6), Quaternion.identity));
				poleList[i].poleConstructor(this, i, ringAmount);
			}*/

			ringList.Add(Instantiate(ringPrefab, new Vector3(0, 5+i, -6),  Quaternion.Euler(-90, 0, 0)));
		}
		for(int i=0; i<ringAmount; i++){
			//Debug.Log("constructing rings to add to the Polish " +ringList.Count + " at " + i);
			//ringList[i].ringConstructor(i);
            startPole.addRing(ringList[i]);
		}

		//Debug.Log("we outta there chief");
        selected = false;
    }

    public void resetGame(int ringAmount)
    {
        startPole.depleteStack();
        midPole.depleteStack();
        endPole.depleteStack();

        foreach(Ring r in ringList)
        {
            r.gameObject.SetActive(false);
        }

        ringList.Clear();

        this.ringAmount = ringAmount;

        for (int i = 0; i < ringAmount; i++)
        {
            ringList.Add(Instantiate(ringPrefab, new Vector3(0, 5 + i, -6), Quaternion.Euler(-90, 0, 0)));
        }
        for (int i = 0; i < ringAmount; i++)
        {
           // ringList[i].ringConstructor(i);
            startPole.addRing(ringList[i]);
        }

        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void selectRing(Ring newRing){
		selectedRing = newRing;
		//Debug.Log("Selecting ring width: " + selectedRing.getRingWidth());
		selectedRing.floatRing();
		selected = true;
	}
	
	/*public void unselectRing(){
		selectedRing.dropRing();
		selected = false;
	}*/

	public void unselectRing(Pole destinationPole){
		//selectedRing = null;
		
		selectedRing.dropRing(destinationPole.transform.position);
		selected = false;
		destinationPole.addRing(originPole.removeTopRing());
	}
	
	/*
	public Ring getSelectedRing(){
		return selectedRing;
	}

	public bool hasSelectedRing(){
		return selected;
	}*/

	public void hoveredPole(Vector3 polePosition){
		if(selected){
			selectedRing.setPosition(polePosition);
		}
		else{
			;
		}
	}

	public void selectedPole(Pole touchedPole){
		if(selected){
			if(touchedPole.getPoleCount()==0){
				unselectRing(touchedPole);
                UIManager.Instance.incrementCounter();

            }
			/*else if(touchedPole.getPolePosition() == originPole.getPolePosition()){
				unselectRing();
			}*/
			else if(touchedPole.getTopRingWidth() > selectedRing.getRingWidth() || touchedPole.getPolePosition() == originPole.getPolePosition()){
				unselectRing(touchedPole);
                UIManager.Instance.incrementCounter();
            }
			else{
				//Debug.Log("can't put it down there boss " + touchedPole.getTopRingWidth() + " and " + selectedRing.getRingWidth());
			
			}
		}
		else{
			if(touchedPole.getPoleCount()!=0){
				//Debug.Log("do we got poles? " + touchedPole.getPoleCount());
				originPole = touchedPole;
				//Debug.Log("topringwidth is " + touchedPole.getTopRingWidth() );
				selectRing(originPole.getTopRing());
			}
			else{
				//Debug.Log("no ring there b0ss");
			}

		}

	}
	
}
