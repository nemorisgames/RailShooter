using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {
    public enum PointPosition { BottomOutLeft, BottomOutCenter, BottomOutRight, BottomLeftOut, BottomLeft, BottomCenter, BottomRight, BottomRightOut, MiddleLeftOut, MiddleLeftBorder, MiddleLeft, MiddleCenter, MiddleRight, MiddleRightBorder, MiddleRightOut, UpperLeftOut, UpperLeft, UpperCenter, UpperRight, UpperRightOut, UpperOutLeft, UpperOutCenter, UpperOutRight};
    public PointPosition pointPosition;
    public Transform point;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
