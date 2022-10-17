using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public float angleChestMax = 45f;
    public float angleThighMax = 45f;
    public float angleKneeMax = 45f;
    public float translateMax = 0.5f;
    public static List<Matrix4x4> locations;    // --
    public static List<Matrix4x4> scales;       // --
    public static List<GameObject> partsBody;   // --
    Vector3[] v_originals;                      // --
    BackForth rY;                               // --
    BackForth rX;                               // --
    BackForth Jump;                             // --
    Vector3[] v_chest;
    Vector3 pivoteHeap;
    Vector3 pivoteChest;

    float deltaRotY;
    float deltaY;
    float dirRotChestY;
    float dirRotThighY;
    float dirRotKneeY;
    float rotChestY;
    float rotShoulderY;
    float rotThighY;
    float rotKneeY;
    float dirTransY;
    float transY;

    enum PartsBody
    {
        // Body
        HEAP, TORSO, CHEST, NECK, HEAD,
        // Left ARM
        LSHOUDLER, LUPPERARM, LELBOW, LFOREARM, LHAND, 
        // Right ARM
        RSHOUDLER, RUPPERARM, RELBOW, RFOREARM, RHAND,
        // Left Leg
        LTHIGH, LKNEE, LLEG, LFOOT,
        // Right Leg
        RTHIGH, RKNEE, RLEG, RFOOT
    };

    public struct BackForth
    {
        public float delta, dir, val, min, max;
        public BackForth(float _delta, float _dir, float _val, float _min, float _max)
        {
            delta = _delta;
            dir = _dir;
            val = _val;
            min = _min;
            max = _max;
        }
        public void Update()
        {
            val += delta * dir;
            if(val <= min || val >= max) dir = -dir;
        }
    };

    void Start()
    {
        rotChestY = 0f;
        rotThighY = 0f;
        rotKneeY = 0f;
        dirRotChestY = 1f;
        dirRotThighY = 1f;
        dirRotKneeY = 1f;
        deltaRotY = 0.065f;
        transY = 0f;
        dirTransY = -1f;
        deltaY = 0.0025f;

        partsBody = new List<GameObject>();
        scales = new List<Matrix4x4>();
        locations = new List<Matrix4x4>();
        
        //HEAP
        InstantiateRobotPart((int)PartsBody.HEAP, Color.gray, "HEAP", new Vector3(1f, 0.5f, 1f), new Vector3(0f, 0f, 0f), 0f);
        v_originals = partsBody[(int)PartsBody.HEAP].GetComponent<MeshFilter>().mesh.vertices;
        //TORSO
        InstantiateRobotPart((int)PartsBody.TORSO, Color.white, "TORSO", new Vector3(.85f, 0.75f, .85f), new Vector3(0f, 0.5f/2f + 0.4f/2f, 0f), 0f);
        //CHEST
        InstantiateRobotPart((int)PartsBody.CHEST, Color.red, "CHEST", new Vector3(1.2f, 0.75f, 1.2f), new Vector3(0f, 0.4f/2f + 0.75f/2f, 0f), 15f);
        //NECK
        InstantiateRobotPart((int)PartsBody.NECK, Color.white, "NECK", new Vector3(0.2f, 0.1f, 0.2f), new Vector3(0f, 0.75f/2f + 0.1f/2f, 0f), 0f);
        //HEAD
        InstantiateRobotPart((int)PartsBody.HEAD, Color.blue, "HEAD", new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0f, 0.1f/2f + 0.5f/2f, 0f), 0f);
        // LSHOULDER
        InstantiateRobotPart((int)PartsBody.LSHOUDLER, Color.red, "LSHOULDER", new Vector3(0.6f, 0.6f, 0.6f), new Vector3(-1.2f/2f - 0.6f/2f, 0.75f/4f - 0.1f/2f, 0f), 0f);
        // rotations[(int)PartsBody.LSHOUDLER] *= Transformaciones.RotateZ(-5f);

        // LUPPERARM
        InstantiateRobotPart((int)PartsBody.LUPPERARM, Color.white, "LUPPERARM", new Vector3(0.4f, 0.5f, 0.4f), new Vector3(0f, -0.4f/2f - 0.5f/2f, 0f), 0f);
        // LELBOW
        InstantiateRobotPart((int)PartsBody.LELBOW, Color.red, "LELBOW", new Vector3(0.6f, 0.4f, 0.6f), new Vector3(0f, -0.5f/2f - 0.4f/2f, 0f), 0f);
        // LFOREARM
        InstantiateRobotPart((int)PartsBody.LFOREARM, Color.red, "LFOREARM", new Vector3(0.6f, 0.3f, 0.6f), new Vector3(0f, -0.4f/2f - 0.3f/2f, 0f), 0f);
        // LHAND
        InstantiateRobotPart((int)PartsBody.LHAND, Color.blue, "LHAND", new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0f, -0.3f/2f - 0.4f/2f, 0f), 0f);
        // RSHOUDLER
        InstantiateRobotPart((int)PartsBody.RSHOUDLER, Color.red, "RSHOUDLER", new Vector3(0.6f, 0.6f, 0.6f), new Vector3(1.2f/2f + 0.6f/2f, 0.75f/4f - 0.1f/2f, 0f), 0f);
        // rotations[(int)PartsBody.RSHOUDLER] *= Transformaciones.RotateZ(5f);
        // RUPPERARM
        InstantiateRobotPart((int)PartsBody.RUPPERARM, Color.white, "RUPPERARM", new Vector3(0.4f, 0.5f, 0.4f), new Vector3(0f, -0.4f/2f - 0.5f/2f, 0f), 0f);
        // RELBOW
        InstantiateRobotPart((int)PartsBody.RELBOW, Color.red, "RELBOW", new Vector3(0.6f, 0.4f, 0.6f), new Vector3(0f, -0.5f/2f - 0.4f/2f, 0f), 0f);
        // RFOREARM
        InstantiateRobotPart((int)PartsBody.RFOREARM, Color.red, "RFOREARM", new Vector3(0.6f, 0.3f, 0.6f), new Vector3(0f, -0.4f/2f - 0.3f/2f, 0f), 0f);
        // RHAND
        InstantiateRobotPart((int)PartsBody.RHAND, Color.blue, "RHAND", new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0f, -0.3f/2f - 0.4f/2f, 0f), 0f);
        // LTHIGH
        InstantiateRobotPart((int)PartsBody.LTHIGH, Color.white, "LTHIGH", new Vector3(0.4f, 0.8f, 0.4f), new Vector3(-1f/2f + 0.4f/2f, -0.5f/2f - 0.8f/2f, 0f), 0f);
        // LKNEE
        InstantiateRobotPart((int)PartsBody.LKNEE, Color.blue, "LKNEE", new Vector3(0.45f, 0.3f, 0.45f), new Vector3(0f, -0.8f/2f - 0.3f/2f, 0f), 0f);
        // LLEG
        InstantiateRobotPart((int)PartsBody.LLEG, Color.blue, "LLEG", new Vector3(0.45f, 0.5f, 0.45f), new Vector3(0f, -0.3f/2f - 0.5f/2f, 0f), 0f);
        // LFOOT
        InstantiateRobotPart((int)PartsBody.LFOOT, Color.blue, "LFOOT", new Vector3(0.45f, 0.3f, 0.75f), new Vector3(0f, -0.5f/2f - 0.3f/2f, 0.3f/2f), 0f);
        // RTHIGH
        InstantiateRobotPart((int)PartsBody.RTHIGH, Color.white, "RTHIGH", new Vector3(0.4f, 0.8f, 0.4f), new Vector3(1f/2f - 0.4f/2f, -0.5f/2f - 0.8f/2f, 0f), 0f);
        // RKNEE
        InstantiateRobotPart((int)PartsBody.RKNEE, Color.blue, "RKNEE", new Vector3(0.45f, 0.3f, 0.45f), new Vector3(0f, -0.8f/2f - 0.3f/2f, 0f), 0f);
        // RLEG
        InstantiateRobotPart((int)PartsBody.RLEG, Color.blue, "RLEG", new Vector3(0.45f, 0.5f, 0.45f), new Vector3(0f, -0.3f/2f - 0.5f/2f, 0f), 0f);
        // RFOOT
        InstantiateRobotPart((int)PartsBody.RFOOT, Color.blue, "RFOOT", new Vector3(0.45f, 0.3f, 0.75f), new Vector3(0f, -0.5f/2f - 0.3f/2f, 0.3f/2f), 0f);
    }

    void Update()
    {
        rotChestY += deltaRotY * dirRotChestY;
        rotThighY += deltaRotY * dirRotThighY;
        rotKneeY += deltaRotY * dirRotKneeY;
        transY += deltaY * dirTransY;

        if(rotChestY <= -angleChestMax || rotChestY >= angleChestMax) dirRotChestY = -dirRotChestY;
        if(rotThighY <= -angleThighMax || rotThighY >= angleThighMax) dirRotThighY = -dirRotThighY;
        if(rotKneeY <= -angleKneeMax || rotKneeY >= angleKneeMax) dirRotKneeY = -dirRotKneeY;
        if(transY <= -translateMax || transY >= translateMax) dirTransY = -dirTransY;

        Matrix4x4 accumulative = Matrix4x4.identity;
        Matrix4x4 accumulativeHeap = Matrix4x4.identity;
        Matrix4x4 accumulativeChest = Matrix4x4.identity;
        Matrix4x4 accumulativeL_Arm = Matrix4x4.identity;
        Matrix4x4 accumulativeL_Leg = Matrix4x4.identity;
        Matrix4x4 accumulativeR_Arm = Matrix4x4.identity;
        Matrix4x4 accumulativeR_Leg = Matrix4x4.identity;
        pivoteHeap = new Vector3(0f, partsBody[(int)PartsBody.HEAP].transform.position.y + 0.5f / 2f, 0f);
        pivoteChest = new Vector3(0f, partsBody[(int)PartsBody.CHEST].transform.position.y + 0.75f / 2f, 0f);
        
        for (int i = 0; i < partsBody.Count; i++)
        {
            if(     i == (int)PartsBody.LSHOUDLER
                    || i == (int)PartsBody.RSHOUDLER)
                accumulative = accumulativeChest;

            if(     i == (int)PartsBody.LTHIGH
                    || i == (int)PartsBody.RTHIGH)
                accumulative = accumulativeHeap;

            if(     i == (int)PartsBody.LUPPERARM
                    || i == (int)PartsBody.LELBOW
                    || i == (int)PartsBody.LFOREARM
                    || i == (int)PartsBody.LHAND)
                accumulative = accumulativeL_Arm;

            if(     i == (int)PartsBody.LKNEE
                    || i == (int)PartsBody.LLEG
                    || i == (int)PartsBody.LFOOT)
                accumulative = accumulativeL_Leg;

            if(     i == (int)PartsBody.RUPPERARM
                    || i == (int)PartsBody.RELBOW
                    || i == (int)PartsBody.RFOREARM
                    || i == (int)PartsBody.RHAND)
                accumulative = accumulativeR_Arm;

            if(     i == (int)PartsBody.RKNEE
                    || i == (int)PartsBody.RLEG
                    || i == (int)PartsBody.RFOOT)
                accumulative = accumulativeR_Leg;
            
            Matrix4x4 m = accumulative * locations[i] 
            // * rotations[i] 
            * scales[i];
            // locations[i] = Transformaciones.Translate(0f, transY, 0f);
            
            if(i == (int)PartsBody.CHEST)
            {
                // rotations[i] = Transformaciones.RotateY(rotChestY);
                Matrix4x4 pivotePosicion2 = Transformaciones.Translate(pivoteHeap.x, 0.5f / 2f, pivoteHeap.z);
                Matrix4x4 newPosicion2 = Transformaciones.Translate(pivoteHeap.x, 0.75f / 2f, pivoteHeap.z);

                m = accumulative * pivotePosicion2 
                // * rotations[i] 
                * newPosicion2 * scales[i];
                accumulative *= pivotePosicion2 
                // * rotations[i] 
                * newPosicion2;
            }
            else if(i == (int)PartsBody.LSHOUDLER)
            {
                // rotations[i] = Transformaciones.RotateY(rotShoulderY);
                Matrix4x4 pivotePosicion2 = Transformaciones.Translate(1.2f / 2f, pivoteChest.y, pivoteChest.z);
                Matrix4x4 newPosicion2 = Transformaciones.Translate(0.6f / 2f, pivoteChest.y, pivoteChest.z);

                m = accumulativeL_Arm * pivotePosicion2 
                // * rotations[i] 
                * newPosicion2 * scales[i];
                accumulativeL_Arm *= pivotePosicion2 
                // * rotations[i] 
                * newPosicion2;
            }
            else
                accumulative *= locations[i] 
                // * rotations[i]
                ;
            
            if(i == (int)PartsBody.CHEST)
                accumulativeChest = accumulative;

            if(i == (int)PartsBody.HEAP)
                accumulativeHeap = accumulative;
            
            if(i == (int)PartsBody.LSHOUDLER
                    || i == (int)PartsBody.LUPPERARM
                    || i == (int)PartsBody.LELBOW
                    || i == (int)PartsBody.LFOREARM
                    || i == (int)PartsBody.LHAND)
                accumulativeL_Arm = accumulative;
            
            if(i == (int)PartsBody.LTHIGH
                    || i == (int)PartsBody.LKNEE
                    || i == (int)PartsBody.LLEG
                    || i == (int)PartsBody.LFOOT)
                accumulativeL_Leg = accumulative;
            
            if(i == (int)PartsBody.RSHOUDLER
                    || i == (int)PartsBody.RUPPERARM
                    || i == (int)PartsBody.RELBOW
                    || i == (int)PartsBody.RFOREARM
                    || i == (int)PartsBody.RHAND)
                accumulativeR_Arm = accumulative;
            
            if(i == (int)PartsBody.RTHIGH
                    || i == (int)PartsBody.RKNEE
                    || i == (int)PartsBody.RLEG
                    || i == (int)PartsBody.RFOOT)
                accumulativeR_Leg = accumulative;

            partsBody[i].GetComponent<MeshFilter>().mesh.vertices = 
                Transformaciones.Transform(m, v_originals);
        }
    }

    public static void InstantiateRobotPart(int index, Color color, string name, Vector3 scale, Vector3 translate, float rot)
    {
        partsBody.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        partsBody[index].GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        partsBody[index].name = name;
        partsBody[index].GetComponent<BoxCollider>().enabled = false;
        scales.Add(Transformaciones.Scale(scale.x,scale.y,scale.z));
        locations.Add(Transformaciones.Translate(translate.x,translate.y,translate.z));
    }
}
