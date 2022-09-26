using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejercicio2 : MonoBehaviour
{
    /*
    // The various categories the editor will display the variables in
    public enum DisplayCategory
    {
        Sun, Mercury
    }
    // The enum field that will determine what variables to display in the Inspector
    public DisplayCategory categoryToDisplay;
    */

    [Header("Sun")]

    GameObject sun;
    Vector3[] vSun;
    Matrix4x4 trSun;
    float rotYSun;
    public float rotSpeedSun;
    public float scaleSun;
    public Color colorSun;

    [Header("Mercury")]
    GameObject mercury;
    Vector3[] vMercury;
    Matrix4x4 trMercury;
    Matrix4x4 ttMercury;
    Matrix4x4 tsMercury;
    float rotYMercury;
    public float translateMercury;
    public float rotSpeedMercury;
    public float scaleMercury;
    public Color colorMercury;

    // Start is called before the first frame update
    void Start()
    {
        rotYSun = 0;
        rotYMercury = 0;

        sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        vSun = sun.GetComponent<MeshFilter>().mesh.vertices;
        sun.GetComponent<MeshRenderer>().material.color = colorSun;

        mercury = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        vMercury = mercury.GetComponent<MeshFilter>().mesh.vertices;
        mercury.GetComponent<MeshRenderer>().material.color = colorMercury;
    }

    // Update is called once per frame
    void Update()
    {
        rotYSun += rotSpeedSun;
        trSun = Transformaciones.RotateY(rotYSun);
        sun.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(trSun, vSun);
        sun.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        planetMovement(trMercury, rotSpeedMercury, ttMercury, translateMercury, tsMercury, scaleMercury, vMercury, mercury);      
    }

    void planetMovement(Matrix4x4 tr, float rotSpeed, Matrix4x4 tt, float tSpeed, Matrix4x4 ts, float scale, Vector3[] v, GameObject planet)
    {
        tr = Transformaciones.RotateY(rotYSun * rotSpeed);
        tt = Transformaciones.Translate(tSpeed, 0, 0);
        ts = Transformaciones.Scale(scale, scale, scale);
        planet.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(tr * tt * ts, v);
        planet.GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }

}
