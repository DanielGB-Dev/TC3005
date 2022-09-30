using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    List<GameObject> cubes;
    List<Vector3> positions;
    List<Matrix4x4> matrices;
    List<Matrix4x4> mOriginales;
    Vector3[] originales;

    float rotZ;
    float rotY;
    // Start is called before the first frame update
    void Start()
    {
        rotZ = 0;
        rotY = 0;
        cubes = new List<GameObject>();
        positions = new List<Vector3>();
        matrices = new List<Matrix4x4>();
        mOriginales = new List<Matrix4x4>();
        for (int i = 0; i < 8; i++)
        {
            cubes.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        }
        float s = 1.0f;
        float h = s / 2.0f;
        positions.Add(new Vector3(-h, -h, h));
        positions.Add(new Vector3(h, -h, h));
        positions.Add(new Vector3(h, h, h));
        positions.Add(new Vector3(-h, h, h));
        positions.Add(new Vector3(-h, -h, -h));
        positions.Add(new Vector3(h, -h, -h));
        positions.Add(new Vector3(h, h, -h));
        positions.Add(new Vector3(-h, h, -h));

        originales = cubes[0].GetComponent<MeshFilter>().mesh.vertices;

        for (int i = 0; i < cubes.Count; i++)
        {
            mOriginales.Add(Transformaciones.Translate(positions[i].x, positions[i].y,  positions[i].z));
            matrices.Add(mOriginales[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rotZ < 360.0f)
        {
            rotZ += 0.1f;
            for (int i = 4; i < 8; i++)
            {
                matrices[i] = Transformaciones.RotateZ(rotZ) * mOriginales[i];
            }
        }
        else if (rotY < 360.0f)
        {
            rotY += 0.1f;
            matrices[2] = Transformaciones.RotateY(rotY) * mOriginales[2];
            matrices[3] = Transformaciones.RotateY(rotY) * mOriginales[3];
            matrices[6] = Transformaciones.RotateY(rotY) * mOriginales[6];
            matrices[7] = Transformaciones.RotateY(rotY) * mOriginales[7];
        }
        else
        {
            rotZ = rotY = 0f;
            for (int i = 4; i < 8; i++)
            {
                matrices[i] = mOriginales[i];
            }
        }
        for (int i = 0; i < 8; i++)
        {
            cubes[i].GetComponent<MeshFilter>().mesh.vertices = 
                Transformaciones.Transform(matrices[i], originales);
        }
    }

}
