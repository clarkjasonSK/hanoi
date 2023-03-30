using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    private List<Pole> poleList;
    [SerializeField] private Transform poleParent;
    [SerializeField] private Pole poleTemplate;
    [SerializeField] private int poleMax;


    List<Ring> ringListA;
    List<Ring> ringListB;
    [SerializeField] private Transform ringParent;
    [SerializeField] private Ring ringA;
    [SerializeField] private Ring ringB;
    [SerializeField] private Ring ringC;
    [SerializeField] private Ring ringD;
    [SerializeField] private Ring ringE;
    [SerializeField] private Ring ringF;
    [SerializeField] private Ring ringG;

    void Awake()
    {
        Instance = this;

        Pole tempPole;
        poleList = new List<Pole>();

        for (int i = 0; i < poleMax; i++)
        {
            tempPole = Instantiate(poleTemplate);
            tempPole.transform.parent = poleParent.transform;
            tempPole.gameObject.SetActive(false);
            poleList.Add(tempPole);
        }

        ringListA = new List<Ring>();
        ringListA.Add(Instantiate(ringA));
        ringListA.Add(Instantiate(ringB));
        ringListA.Add(Instantiate(ringC));
        ringListA.Add(Instantiate(ringD));
        ringListA.Add(Instantiate(ringE));
        ringListA.Add(Instantiate(ringF));
        ringListA.Add(Instantiate(ringG));


        ringListB = new List<Ring>();
        ringListB.Add(Instantiate(ringA));
        ringListB.Add(Instantiate(ringB));
        ringListB.Add(Instantiate(ringC));
        ringListB.Add(Instantiate(ringD));
        ringListB.Add(Instantiate(ringE));
        ringListB.Add(Instantiate(ringF));
        ringListB.Add(Instantiate(ringG));

        for(int i=0; i<ringListA.Count; i++)
        {
            ringListA[i].transform.parent = ringParent.transform;
            ringListB[i].transform.parent = ringParent.transform;
        }
    }

    // Update is called once per frame
    public Pole getPole()
    {
        for (int i = 0; i < poleMax; i++)
        {
            if (!poleList[i].gameObject.activeInHierarchy)
            {
                //Debug.Log("Returning ,,,");
                return poleList[i];
            }
        }
        return null;
    }

    public List<Ring> getRings()
    {
        List<Ring> tempRingList = new List<Ring>();
        // Debug.Log("asset manager:");
        if (!ringListA[0].gameObject.activeInHierarchy)
        {
            for (int i=0; i< GameManager.Instance.getRingAmount(); i++)
            {
                tempRingList.Add(ringListA[i]);
            }
        
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.getRingAmount(); i++)
            {
                tempRingList.Add(ringListB[i]);
            }
        }

        return tempRingList;
    }
}
