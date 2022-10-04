using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    List<GameObject> go_parts;
    List<Matrix4x4> m_locations;
    List<Matrix4x4> m_scales;
    Vector3[] v3_originals;

    enum PARTS
    {
        RP_HEAP, RP_TORSO, RP_CHEST, RP_NECK, RP_HEAD
    }
    // Start is called before the first frame update
    void Start()
    {
        go_parts = new List<GameObject>();
        m_scales = new List<Matrix4x4>();
        m_locations = new List<Matrix4x4>();
        //HEAP
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        v3_originals = go_parts[(int)PARTS.RP_HEAP].GetComponent<MeshFilter>().mesh.vertices;
        m_scales.Add(Transformaciones.Scale(1f, 0.5f, 1f));
        m_locations.Add(Transformaciones.Translate(0f,0f,0f));
        //TORSO
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        m_scales.Add(Transformaciones.Scale(1f, 0.75f, 1f));
        m_locations.Add(Transformaciones.Translate(0f,0.75f/2f,0f));
        //CHEST
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        m_scales.Add(Transformaciones.Scale(1.2f, 0.4f, 1.2f));
        m_locations.Add(Transformaciones.Translate(0f,0.2f + 0.25f + 0.75f,0f));
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 accumT = Matrix4x4.identity;
        for (int i = 0; i < go_parts.Count; i++)
        {
            Matrix4x4 m = m_locations[i] * m_scales[i];
            go_parts[i].GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(m, v3_originals);
            go_parts[i].GetComponent<MeshFilter>().mesh.RecalculateBounds();
        }
    }
}
