using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    //[SerializeField] private Manager ringManager;
	private Stack<Ring> poleStack;

    private int polePosition;

    [SerializeField] GameObject towerPole;
    private float poleSize;

    //bool startingPole; //all rings start where this is true
    bool endingPole; //rings must try ending, but pole
	/*
	[SerializeField] Ring templateRing3;
	[SerializeField] Ring templateRing2;
	[SerializeField] Ring templateRing;*/

	
    void Awake()
    {
        poleStack = new Stack<Ring>();
        polePosition = (int)transform.position.z;


      /*  if (polePosition == 6) { 
            endingPole = true;
        }*/

    }
	public void poleConstructor(int polePosition){
		this.polePosition = polePosition;
        endingPole = false;

        if (polePosition == -1)
        {
            endingPole = true;
        }


        transform.localPosition = new Vector3(0, -2.2f, 3.8f*polePosition);
        transform.rotation = Quaternion.Euler(0, 0, 180);

        poleSize = .6f + (.08f * (GameManager.Instance.getRingAmount() - 3));
        towerPole.transform.localScale = new Vector3(towerPole.transform.localScale.x, poleSize, towerPole.transform.localScale.z);
        

        gameObject.SetActive(true);
        /*if(polePosition == 0){
			startingPole = true;
		}
		if(polePosition == poleAmount-1){
			endingPole = true;
		}*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnMouseOver(){
		poleHovered();
	}

	public void poleHovered(){
		GameManager.Instance.hoveredPole(transform.position);
	}

	void OnMouseDown(){
		poleTouched();
	}
	
	public void poleTouched(){
        //Debug.Log("clicked at pole:" + polePosition );
        GameManager.Instance.selectedPole(this);
	
	}
	
	public void addRing(Ring newRing){
		//Debug.Log("adding ring at pole:" + polePosition );
		newRing.setPole(this); // set new ring's pole to new(this) one
		poleStack.Push(newRing);

        /*if(endingPole && poleStack.Count == GameManager.Instance.getRingAmount())
        {
            GameManager.Instance.resetGame();
        }*/
	}

	public Ring removeTopRing(){
		return poleStack.Pop();
	}

	public Ring getTopRing(){
		return poleStack.Peek();
	}

	public int getTopRingWidth(){
		return poleStack.Peek().getRingWidth();
	}

	public int getPoleCount(){
		return poleStack.Count;
	}

	public int getPolePosition(){
		return polePosition;
	}
    public bool isEndingPole()
    {
        return endingPole;
    }

    public void depleteStack()
    {
        poleStack.Clear();
    }
    public void resetPole()
    {
        depleteStack();
        gameObject.SetActive(false);
    }
}
