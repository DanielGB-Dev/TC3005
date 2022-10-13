using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    List<GameObject> go_parts;
    List<Matrix4x4> m_locations;
    List<Matrix4x4> m_scales;
    List<Matrix4x4> m_rotations;
    Vector3[] v3_originals;

    float deltaY;
    float dirY;
    float rotY;

    enum PARTS
    {
        RP_HEAP, RP_TORSO, RP_CHEST, RP_NECK, RP_HEAD,

        RP_SHOULDER_L, RP_SHOULDER_R
    }
    // Start is called before the first frame update
    void Start()
    {
        rotY = 0f;
        dirY = 1f;
        deltaY = 0.1f;
        go_parts = new List<GameObject>();
        m_scales = new List<Matrix4x4>();
        m_locations = new List<Matrix4x4>();
        m_rotations = new List<Matrix4x4>();
        //HEAP
        InstantiateRobotPart(go_parts, (int)PARTS.RP_HEAP, Color.gray, "HEAP", new Vector3(1f, 0.5f, 1f), new Vector3(0f,0f,0f), 0f);
        v3_originals = go_parts[(int)PARTS.RP_HEAP].GetComponent<MeshFilter>().mesh.vertices;
        //TORSO
        InstantiateRobotPart(go_parts, (int)PARTS.RP_TORSO, Color.white, "TORSO", new Vector3(1f, 0.75f, 1f), new Vector3(0f,0.25f + 0.75f/2f,0f), 15f);
        //CHEST
        InstantiateRobotPart(go_parts, (int)PARTS.RP_CHEST, Color.red, "CHEST", new Vector3(1.5f, 0.4f, 1.5f), new Vector3(0f,0.75f/2f + 0.2f,0f), 0f);
        //NECK
        InstantiateRobotPart(go_parts, (int)PARTS.RP_NECK, Color.white, "NECK", new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0f, 0.2f + 0.1f/2f,0f), 0f);
        //HEAD
        InstantiateRobotPart(go_parts, (int)PARTS.RP_HEAD, Color.red, "HEAD", new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0f,0.1f/2f + 0.5f/2f,0f), 0f);
        //SHOULDER LEFT
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        go_parts[(int)PARTS.RP_SHOULDER_L].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        go_parts[(int)PARTS.RP_SHOULDER_L].name = "SHOULDER LEFT";
        go_parts[(int)PARTS.RP_SHOULDER_L].GetComponent<BoxCollider>().enabled = false;
        m_scales.Add(Transformaciones.Scale(0.4f, 0.4f, 0.4f));
        m_locations.Add(Transformaciones.Translate(-1.2f/2f - 0.4f/2f,-0.75f/2f - 0.2f,0f));
        m_rotations.Add(Transformaciones.RotateY(0f));
        //SHOULDER RIGHT
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        go_parts[(int)PARTS.RP_SHOULDER_R].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        go_parts[(int)PARTS.RP_SHOULDER_R].name = "SHOULDER RIGHT";
        go_parts[(int)PARTS.RP_SHOULDER_R].GetComponent<BoxCollider>().enabled = false;
        m_scales.Add(Transformaciones.Scale(0.4f, 0.4f, 0.4f));
        m_locations.Add(Transformaciones.Translate(1.2f/2f + 0.4f/2f,-0.75f/2f - 0.2f,0f));
        m_rotations.Add(Transformaciones.RotateY(0f));
        /*
        //UPPER ARM LEFT
        go_parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        go_parts[(int)PARTS.RP_UPPER_ARM_L].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
        go_parts[(int)PARTS.RP_UPPER_ARM_L].name = "UPPER ARM LEFT";
        go_parts[(int)PARTS.RP_UPPER_ARM_L].GetComponent<BoxCollider>().enabled = false;
        m_scales.Add(Transformaciones.Scale(0.4f, 0.5f, 0.4f));
        m_locations.Add(Transformaciones.Translate(0f,-0.4f,0f));
        m_rotations.Add(Transformaciones.RotateY(0f));
        */
    }

    // Update is called once per frame
    void Update()
    {
        
        //rotY += deltaY * dirY;
        if (rotY <= -25f || rotY >= 25f)
        {
            dirY = -dirY;
        }
        Matrix4x4 accumT = Matrix4x4.identity;
        for (int i = 0; i < go_parts.Count; i++)
        {
            Matrix4x4 m = accumT * m_locations[i] * m_rotations[i] * m_scales[i];
            if (i == (int)PARTS.RP_TORSO)
            {
                m = accumT * Transformaciones.Translate(0, 0.5f / 2f, 0) * Transformaciones.RotateX(rotY) *
                    Transformaciones.Translate(0, 0.75f / 2f, 0) * m_scales[i];
                accumT *= Transformaciones.Translate(0, 0.5f / 2f, 0) * Transformaciones.RotateX(rotY) *
                    Transformaciones.Translate(0, 0.75f / 2f, 0);
            }
            else
            {
                accumT *= m_locations[i] * m_rotations[i];
            }
            
            go_parts[i].GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(m, v3_originals);
        }
        
    }

    void InstantiateRobotPart(List<GameObject> parts, int index, Color color, string name, Vector3 scale, Vector3 translate, float rot)
    {
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        parts[index].GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        parts[index].name = name;
        parts[index].GetComponent<BoxCollider>().enabled = false;
        m_scales.Add(Transformaciones.Scale(scale.x,scale.y,scale.z));
        m_locations.Add(Transformaciones.Translate(translate.x,translate.y,translate.z));
        m_rotations.Add(Transformaciones.RotateY(rot));
    }
}
