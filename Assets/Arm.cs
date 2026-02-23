using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] private GameObject upperArm;
    [SerializeField] private GameObject lowerArm;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject target;

    //Hand length
    [SerializeField] private float c;

    //Length of the two parts of the arm
    [SerializeField] private float l;
    //X claw position
    [Range(0f, 12f)]
    [SerializeField] private float x;
    //Y claw position
    [Range(0f, 6f)]
    [SerializeField] private float y;
    //Z claw position
    [Range(0f, 12f)]
    [SerializeField] private float z;
    //X Angle of the hand
    [Range(0f, 180f)]
    [SerializeField] private float Xangle;
    //Z Angle of the hand
    [Range(0f, 180f)]
    [SerializeField] private float Zangle;

    //x, y and z claw position
    private Vector3 position;
    //Ground vector for distance
    private Vector2 v;
    //Hand vector to add its length to the equasion
    private Vector3 hv;

    //Distance of the claw from the base
    private float m;
    //height of the triangle formed by the two arms and the m
    private float h;

    //Angle between the m and the upper arm
    private float a1;
    //Angle between y and the ground vector
    private float a2;
    //Angle between x and y
    private float a3;
    
    //Angle for the upper arm
    private float q1;
    //Angle for the lower arm
    private float q2;
    //Angle for the hand
    private float q3;
    //Angle of the base
    private float q4;

    void Update()
    {
        // position = new Vector3(x, y, z);
        position = target.transform.position;
        goToPosition(position, Xangle);
    }

    void goToPosition(Vector3 pos, float Xangle)
    {
        //takes the direction the arm should rotate in y to (in radians)
        a3 = Mathf.Atan2(pos.x, pos.z);
    
        Vector2 hv2d = new Vector2(c*Mathf.Sin(Xangle*Mathf.Deg2Rad),c*Mathf.Cos(Xangle*Mathf.Deg2Rad));
        // 0 [0, -1]
        // PI/4 [-1, 0]
        // PI/2 [0, 1]
        
        //Vector to consider the hand length in the m
        hv = new Vector3 (Mathf.Sin(a3) * hv2d.x,
                          hv2d.y,
                          Mathf.Cos(a3) * hv2d.x);

        //Distance the claw is from the base in the ground plane
        pos = pos-hv;
        v = new Vector2(pos.x, pos.z);
        //Distance of the claw from the base in 3d
        m = Mathf.Sqrt(Mathf.Pow(pos.x, 2) + Mathf.Pow(pos.y, 2) + Mathf.Pow(pos.z, 2));
        m = Mathf.Clamp(m, 0, l);
        //Height of the triangle formed by the arms and m
        h = Mathf.Sqrt(Mathf.Pow(l/2, 2) - Mathf.Pow(m/2, 2));

        //Angle between m and upper arm
        a1 = Mathf.Atan2(m/2, h) * Mathf.Rad2Deg;
        //Angle between y and v
        a2 = Mathf.Atan2(-pos.y, v.magnitude) * Mathf.Rad2Deg;

        //Final angles with offsets
        q1 = (-a1 -a2);
        q2 = (a1*2) -180;
        q3 = (-a1 +a2 +90) - Xangle + 90;
        q4 = (a3 * Mathf.Rad2Deg) -90;

        upperArm.transform.localRotation =  Quaternion.Euler(0, q4, q1);
        lowerArm.transform.localRotation =  Quaternion.Euler(0, 0, q2);
        hand.transform.localRotation =  Quaternion.Euler(0, 0, q3);
    }
}