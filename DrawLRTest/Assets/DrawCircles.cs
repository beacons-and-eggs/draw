using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCircles : MonoBehaviour {

	IEnumerator removeLine(){
		yield return new WaitForSeconds (2f);
		for (int i = pointcount; i > 0; i--) {
			yield return new WaitForSeconds (0.002f);
			//			lineRenderer.SetVertexCount (i);
			points.RemoveAt(0);
		}
		pointcount = 0;
	}

	//public GameObject drawCircle;
	public AnimationCurve lineWidthCurve; //a width curve that you set in the inspector
	public Material lineRendererMaterial; //a material of your choice that you can set in the inspector
	GameObject line; //this is the GameObject that will have the LineRenderer component
	LineRenderer lineRenderer; //our LineRenderer component
	int pointcount = 0; //current number of line positions
	bool drawing = false;

	List<Vector3> points;

	void  Start (){
		line = new GameObject("Line");
		line.AddComponent<LineRenderer>();

		lineRenderer = line.GetComponent<LineRenderer>();
		points = new List<Vector3> ();
		setupLine ();

	}

	void setupLine(){
		//below we set the necessary properties
		lineRenderer.material = lineRendererMaterial;
		lineRenderer.widthMultiplier = 0.25f; //THICKNESS
		lineRenderer.widthCurve = lineWidthCurve;
		lineRenderer.positionCount = 0; //positionCount is the number of positions (the points that make our line)

		lineRenderer.startColor = Color.white;
		lineRenderer.endColor = Color.cyan;
	}

	void startLine(){

	}

	void  Update (){
		var ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 5));
		var destination = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 10));

		if (Input.GetMouseButton (0) && transform.hasChanged && !Physics.Raycast (ray)) { 
			if (!drawing) {
				startLine ();
			} 
			AddLinePoint (destination);
			drawing = true;

		} else if(drawing && !Input.GetMouseButton (0)) {
			drawing = false;
			StopAllCoroutines ();
			StartCoroutine (removeLine ());
		}
		lineRenderer.SetPositions (points.ToArray());
	}

	void AddLinePoint(Vector3 destination){
		//add point and increment pointcount
		lineRenderer.positionCount = pointcount + 1; //set the number of positions
		//the line below is used to get neat round corners, we set the number of the corners to the number of the points
		lineRenderer.numCornerVertices = lineRenderer.positionCount;
		//		lineRenderer.SetPosition (pointcount, destination); //set the position of the point (or vertex) that is at the n index

		points.Add (destination);


		pointcount++; //increase number of points
		transform.hasChanged = false;

	}
}