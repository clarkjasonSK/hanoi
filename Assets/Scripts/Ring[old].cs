using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOld : MonoBehaviour
{
	private Pole residingPole;
	private int ringWidth;
	
	Rigidbody rb;
	float floatHeight;
	private bool floating = false;
	[SerializeField] float floatSpeed;
	[SerializeField] float travelSpeed;
	private Vector3 targetPosition;

    // Start is called before the first frame update
    void Awake()
    {
        //targetPosition = transform.position;
		rb = gameObject.GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        rb.angularVelocity = Vector3.zero;
        floatHeight = 3.25f;
    }
	public void ringConstructor(int width){
        ringWidth = 5-width;
        transform.position = new Vector3(0, 5+width, -6);
        transform.localScale = new Vector3(1f -(.15f*width), 1f-(.15f*width), 1f-(.15f*width) );
	}

    // Update is called once per frame
    void Update()
    {
        if(floating){
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			transform.rotation = Quaternion.Euler(-90, 0, 0);

			if(transform.position.y <floatHeight){
				transform.position = transform.position + new Vector3(0, 
					floatSpeed * Time.fixedDeltaTime,
					0);
					//Mathf.MoveTowards(targetPosition.y, 3.25f, floatSpeed * Time.fixedDeltaTime), 
			}
			else if (transform.position.y > floatHeight-.5f){
				transform.position = new Vector3(transform.position.x, floatHeight, transform.position.z);
				
			}
			/*else{
				transform.position = targetPosition;
				
			}*/

			
			float temp = travelSpeed * Time.fixedDeltaTime;

			if(transform.position.x < targetPosition.x){

				if(transform.position.x + temp > targetPosition.x-.5f){
					targetPosition = new Vector3(targetPosition.x , transform.position.y, transform.position.z);
					transform.position = targetPosition;
				}
				else{
					transform.position = transform.position + new Vector3(temp, 0, 0);
				}
			}
			else if(transform.position.x > targetPosition.x){

				if(transform.position.x - temp < targetPosition.x+.5f){
					targetPosition = new Vector3(targetPosition.x , transform.position.y, transform.position.z);
					transform.position = targetPosition;
					
				}
				else{
					transform.position = transform.position - new Vector3(temp, 0, 0);
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
	
	void OnMouseDown(){
		residingPole.poleTouched();
	}
	void OnMouseOver(){
		residingPole.poleHovered();
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag=="Ring" && ringWidth == 1 ){
			//Debug.Log(" huhu" + residingPole.getPolePosition() + " and " + residingPole.getPoleCount() );
			if(residingPole.getPolePosition()==2  && residingPole.getPoleCount()==3){
				/*Destroy(residingPole.removeTopRing());
				Destroy(residingPole.removeTopRing());
				Destroy(residingPole.removeTopRing());*/
			}
		}
	}
	public int getRingWidth(){
		return ringWidth;
	}

	public void floatRing(){
	//	setPosition(new Vector3(targetPosition.x, 3.25f, targetPosition.z));
		rb.useGravity = false;
		floating = true;
	}
	private void dropRing(){
		rb.useGravity = true;
		floating = false;
	
	}
	public void dropRing(Vector3 newPos){
		floating = false;
		rb.useGravity = true;

		setPosition(new Vector3(newPos.x, floatHeight, newPos.z));
		transform.position = targetPosition;
	}

	public void setPosition(Vector3 newPos){
		targetPosition = newPos;
	}

	public void setPole(Pole newPole){
		residingPole = newPole;
	}

	public int getPolePosition(){
		return residingPole.getPolePosition();
	}

    public void resetRing()
    {
        dropRing();
        residingPole = null;
        targetPosition = Vector3.zero;
    }
}

