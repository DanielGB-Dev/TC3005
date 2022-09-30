using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejercicio3 : MonoBehaviour
{
    GameObject sun;
    Vector3[] vSun;
    public Color colorSun;

    GameObject mer;
    Vector3[] vMer;
    public Color colorMer;

    float rotZ;
    // Start is called before the first frame update
    void Start()
    {
        rotZ = 0;

        sun = GameObject.CreatePrimitive(PrimitiveType.Cube);
        vSun = sun.GetComponent<MeshFilter>().mesh.vertices;
        sun.GetComponent<MeshRenderer>().material.color = colorSun;

        mer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        vMer = mer.GetComponent<MeshFilter>().mesh.vertices;
        mer.GetComponent<MeshRenderer>().material.color = colorMer;
    }

    // Update is called once per frame
    void Update()
    {
        rotZ += 0.1f;
        Matrix4x4 c1Pos = Transformaciones.Translate(1.84f, 1.659f, 0);
        Matrix4x4 c2Pos =  Transformaciones.Translate(3.83f, 1.659f, 0);
        sun.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(c1Pos, vSun);
        mer.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(c2Pos, vMer);

        Matrix4x4 tmenosP = Transformaciones.Translate(-1.84f, -1.659f, 0);
        Matrix4x4 rotarZ = Transformaciones.RotateZ(rotZ);
        Matrix4x4 tmasP = c1Pos;

        Matrix4x4 rotarPivote =  tmenosP * rotarZ * tmasP * c2Pos;
        mer.GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(rotarPivote, vMer);
    }
}
