using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPool : MonoBehaviour
{
    public static RingPool Instance;

    private List<Ring> ringList;
    [SerializeField] private Ring ringTemplate;

    [SerializeField] private int poolMax;
    void Awake()
    {
        Instance = this;

        Ring tempRing;
        ringList = new List<Ring>();

        for (int i =0; i<poolMax; i++)
        {
            tempRing = Instantiate(ringTemplate);
            tempRing.gameObject.SetActive(false);
            ringList.Add(tempRing);
        }

    }

    // Update is called once per frame
    public Ring getRing()
    {
        for (int i = 0; i < poolMax; i++)
        {
            if (!ringList[i].gameObject.activeInHierarchy)
            {
                //Debug.Log("Returning ,,,");
                return ringList[i];
            }
        }
        return null;
    }
}
