using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DrawCircles : MonoBehaviour {
	IEnumerator removeLine(){
		yield return new WaitForSeconds (5);
		for (int i = pointcount; i > 0; i--) {
			lineRenderer.SetVertexCount (i);
		}
		pointcount = 0;
	}
	//public GameObject drawCircle;
	public AnimationCurve lineWidthCurve; //a width curve that you set in the inspector
	public Material lineRendererMaterial; //a material of your choice that you can set in the inspector
	GameObject line; //this is the GameObject that will have the LineRenderer component
	LineRenderer lineRenderer; //our LineRenderer component
	int pointcount = 0; //current number of line positions
	void  Start (){
		line = new GameObject("Line");
		line.AddComponent<LineRenderer>();

		lineRenderer = line.GetComponent<LineRenderer>();

		//below we set the necessary properties
		lineRenderer.material = lineRendererMaterial;
		lineRenderer.widthMultiplier = 0.25f; //THICKNESS
		lineRenderer.widthCurve = lineWidthCurve;
		lineRenderer.positionCount = 0; //positionCount is the number of positions (the points that make our line)

		lineRenderer.startColor = Color.white;
		lineRenderer.endColor = Color.cyan;
	}

	void  Update (){
		Vector3 fwd= transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		int distance= 100; //max distance from camera you can draw
		Vector3 destination= Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 10));

		if (Input.GetMouseButton (0) && Physics.Raycast (transform.position, fwd, out hit, distance) && transform.hasChanged) { 
			//Instantiate (drawCircle, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal));
			//add point and increment pointcount
			lineRenderer.positionCount = pointcount + 1; //set the number of positions
			//the line below is used to get neat round corners, we set the number of the corners to the number of the points
			lineRenderer.numCornerVertices = lineRenderer.positionCount;
			lineRenderer.SetPosition (pointcount, destination); //set the position of the point (or vertex) that is at the n index
			pointcount++; //increase number of points
			transform.hasChanged = false;
			StopAllCoroutines ();
			StartCoroutine (removeLine ());
		}
			

//		if (Input.GetMouseButton(0) && transform.hasChanged){ 
//			Instantiate(drawCircle, destination, Quaternion.identity);
//			transform.LookAt(Camera.main.transform);
//	
//			transform.hasChanged = false;
//		}
	}

}
