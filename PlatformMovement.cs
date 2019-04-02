using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour 
{
    public GameObject platform;

    public float moveSpeed;
    public Transform currentPoint;
    public Transform[] points;
    public int pointSelection;
    public bool flag = false;

	// Use this for initialization
	void Start () {
        currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update () {
        if(flag){
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

            if (platform.transform.position == currentPoint.position)
            {
                pointSelection++;
                if (pointSelection == points.Length)
                {
                    pointSelection = 0;
                }
                currentPoint = points[pointSelection];
            }
        }

	}

    public void setFlag(bool fVal){
        flag = fVal;
    }
    //Reference:
    //https://www.youtube.com/watch?v=HMZnSZswTmU

}