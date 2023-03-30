using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Pole residingPole;
    private int ringWidth;
    private bool topRing;

    Rigidbody rb;
    float floatHeight;
    private bool floating = false;
    [SerializeField] float floatSpeed;
    [SerializeField] float travelSpeed;
    //float startingPolePos;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Awake()
    {
        //targetPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        floatHeight = 5.5f;
    }
    public void ringConstructor(int order, Vector3 startPolePosition)
    {
        ringWidth = order;
        if(ringWidth == GameManager.Instance.getRingAmount())
        {
            topRing = true;
        }
        else
        {
            topRing = false;
        }
        transform.position = startPolePosition;
        transform.position += new Vector3(0, floatHeight + (order*.5f), 0);

        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // transform.localScale = new Vector3(1f - (.15f * width), 1f - (.15f * width), 1f - (.15f * width));
        //  Debug.Log("rings constructor:");
        //Debug.Log("ring " + order + " coords: y:" + transform.localPosition.y + " z: " + transform.localPosition.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (floating)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (transform.position.y < floatHeight)
            {
                transform.position = transform.position + new Vector3(0,
                    floatSpeed * Time.fixedDeltaTime,
                    0);
                //Mathf.MoveTowards(targetPosition.y, 3.25f, floatSpeed * Time.fixedDeltaTime), 
            }
            else if (transform.position.y > floatHeight - .5f)
            {
                transform.position = new Vector3(transform.position.x, floatHeight, transform.position.z);

            }
            /*else{
				transform.position = targetPosition;
				
			}*/


            float temp = travelSpeed * Time.fixedDeltaTime;

            if (transform.position.z < targetPosition.z)
            {

                if (transform.position.z + temp > targetPosition.z - .5f)
                {
                    targetPosition = new Vector3(transform.position.x, transform.position.y, targetPosition.z);
                    transform.position = targetPosition;
                }
                else
                {
                    transform.position = transform.position + new Vector3(0, 0, temp);
                }
            }
            else if (transform.position.z > targetPosition.z)
            {

                if (transform.position.z - temp < targetPosition.z + .5f)
                {
                    targetPosition = new Vector3(transform.position.x, transform.position.y, targetPosition.z);
                    transform.position = targetPosition;

                }
                else
                {
                    transform.position = transform.position - new Vector3(0, 0, temp);
                }
            }
            /*else{
				transform.position = targetPosition;
			}*/
        }
        /*else{
			transform.position = targetPosition;
		}*/

    }

    void OnMouseDown()
    {
        residingPole.poleTouched();
    }
    void OnMouseOver()
    {
        residingPole.poleHovered();
    }

    public int getRingWidth()
    {
        return ringWidth;
    }

    public void floatRing()
    {
        //	setPosition(new Vector3(targetPosition.x, 3.25f, targetPosition.z));
        rb.useGravity = false;
        floating = true;
    }
    private void dropRing()
    {
        rb.useGravity = true;
        floating = false;

    }
    public void dropRing(Vector3 newPos)
    {
        floating = false;
        rb.useGravity = true;

        setPosition(new Vector3(newPos.x, floatHeight, newPos.z));
        transform.position = targetPosition;
    }

    public void setPosition(Vector3 newPos)
    {
        targetPosition = newPos;
    }

    public void setPole(Pole newPole)
    {
        residingPole = newPole;
    }

    public int getPolePosition()
    {
        return residingPole.getPolePosition();
    }

    public void resetRing()
    {
        dropRing();
        //residingPole = null;
        //targetPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("collided " + ringWidth + " and " + GameManager.Instance.getRingAmount());
        if (col.gameObject.tag == "Ring" && topRing)
        {
            //Debug.Log("inside collided " );
            //Debug.Log(" huhu" + residingPole.getPolePosition() + " and " + residingPole.getPoleCount() );
            if (residingPole.isEndingPole() && residingPole.getPoleCount() == GameManager.Instance.getRingAmount())
            {
                Invoke("endRound", .5f);
                /*Destroy(residingPole.removeTopRing());
				Destroy(residingPole.removeTopRing());
				Destroy(residingPole.removeTopRing());*/
            }
        }
    }

    private void endRound()
    {
        GameManager.Instance.resetGame();
    }
}

