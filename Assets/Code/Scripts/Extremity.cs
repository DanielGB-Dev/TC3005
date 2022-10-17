using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Robot;

public class Extremity : MonoBehaviour
{
    string type;
    string side;

    public void Create(string _type, string _side)
    {
        type = _type;
        side = _side;
        if(type == "ARM" && side == "LEFT")
        {
            // LSHOULDER
            InstantiateRobotPart((int)PartsBody.LSHOUDLER, Color.red, "LSHOULDER", new Vector3(0.4f, 0.4f, 0.4f), new Vector3(-1.2f/2f - 0.4f/2f, 0f, 0f));
            // LUPPERARM
            InstantiateRobotPart((int)PartsBody.LUPPERARM, Color.white, "LUPPERARM", new Vector3(0.4f, 0.5f, 0.4f), new Vector3(0f, -0.4f/2f - 0.5f/2f, 0f));
            // LELBOW
            InstantiateRobotPart((int)PartsBody.LELBOW, Color.red, "LELBOW", new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0f, -0.5f/2f - 0.4f/2f, 0f));
            // LFOREARM
            InstantiateRobotPart((int)PartsBody.LFOREARM, Color.red, "LFOREARM", new Vector3(0.4f, 0.3f, 0.4f), new Vector3(0f, -0.4f/2f - 0.3f/2f, 0f));
            // LHAND
            InstantiateRobotPart((int)PartsBody.LHAND, Color.blue, "LHAND", new Vector3(0.5f, 0.3f, 0.4f), new Vector3(0f, -0.3f/2f - 0.3f/2f, 0f));
        }
        else if(type == "ARM" && side == "RIGHT")
        {
            // RSHOULDER
            InstantiateRobotPart((int)PartsBody.RSHOUDLER, Color.red, "RSHOULDER", new Vector3(0.4f, 0.4f, 0.4f), new Vector3(1.2f/2f + 0.4f/2f, 0f, 0f));
            // RUPPERARM
            InstantiateRobotPart((int)PartsBody.RUPPERARM, Color.white, "RUPPERARM", new Vector3(0.4f, 0.5f, 0.4f), new Vector3(0f, -0.4f/2f - 0.5f/2f, 0f));
            // RELBOW
            InstantiateRobotPart((int)PartsBody.RELBOW, Color.red, "RELBOW", new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0f, -0.5f/2f - 0.4f/2f, 0f));
            // RFOREARM
            InstantiateRobotPart((int)PartsBody.RFOREARM, Color.red, "RFOREARM", new Vector3(0.4f, 0.3f, 0.4f), new Vector3(0f, -0.4f/2f - 0.3f/2f, 0f));
            // RHAND
            InstantiateRobotPart((int)PartsBody.RHAND, Color.blue, "RHAND", new Vector3(0.5f, 0.3f, 0.4f), new Vector3(0f, -0.3f/2f - 0.3f/2f, 0f));
        }
        else if(type == "LEG" && side == "LEFT")
        {
            // LTHIGH
            InstantiateRobotPart((int)PartsBody.LTHIGH, Color.white, "LTHIGH", new Vector3(0.4f, 0.8f, 0.4f), new Vector3(-1f/2f + 0.4f/2f, -0.5f/2f - 0.8f/2f, 0f));
            // LKNEE
            InstantiateRobotPart((int)PartsBody.LKNEE, Color.blue, "LKNEE", new Vector3(0.45f, 0.3f, 0.45f), new Vector3(0f, -0.8f/2f - 0.3f/2f, 0f));
            // LLEG
            InstantiateRobotPart((int)PartsBody.LLEG, Color.blue, "LLEG", new Vector3(0.45f, 0.5f, 0.45f), new Vector3(0f, -0.3f/2f - 0.5f/2f, 0f));
            // LFOOT
            InstantiateRobotPart((int)PartsBody.LFOOT, Color.blue, "LFOOT", new Vector3(0.45f, 0.3f, 0.75f), new Vector3(0f, -0.5f/2f - 0.3f/2f, 0.3f/2f));
        }
        else if(type == "LEG" && side == "RIGHT")
        {
            // RTHIGH
            InstantiateRobotPart((int)PartsBody.RTHIGH, Color.white, "RTHIGH", new Vector3(0.4f, 0.8f, 0.4f), new Vector3(1f/2f - 0.4f/2f, -0.5f/2f - 0.8f/2f, 0f));
            // RKNEE
            InstantiateRobotPart((int)PartsBody.RKNEE, Color.blue, "RKNEE", new Vector3(0.45f, 0.3f, 0.45f), new Vector3(0f, -0.8f/2f - 0.3f/2f, 0f));
            // RLEG
            InstantiateRobotPart((int)PartsBody.RLEG, Color.blue, "RLEG", new Vector3(0.45f, 0.5f, 0.45f), new Vector3(0f, -0.3f/2f - 0.5f/2f, 0f));
            // RFOOT
            InstantiateRobotPart((int)PartsBody.RFOOT, Color.blue, "RFOOT", new Vector3(0.45f, 0.3f, 0.75f), new Vector3(0f, -0.5f/2f - 0.3f/2f, 0.3f/2f));
        }
    }

    public void Draw(   ref Matrix4x4 accumulative, 
                        ref List<GameObject> partsBody,
                        ref List<Matrix4x4> locations,
                        ref List<Matrix4x4> scales,
                        BackForth rX,
                        Vector3[] vOriginals)
    {
        Matrix4x4 accumT = Matrix4x4.identity;
        if(type == "ARM" && side == "LEFT")
        {
            for (int i = (int)PartsBody.LSHOUDLER; i <= (int)PartsBody.LHAND; i++)
            {
                Matrix4x4 m = accumT * locations[i] * scales[i];
                if(i == (int)PartsBody.LSHOUDLER)
                {
                    accumT = accumulative;
                    Matrix4x4 r = Transformaciones.RotateZ(-5f);
                    m = accumT * locations[i] * r * scales[i];
                    accumT *= locations[i] * r;
                }
                else if(i == (int)PartsBody.LELBOW)
                {
                    Matrix4x4 t1 = Transformaciones.Translate(0f, -0.5f / 2f, 0f);
                    Matrix4x4 t2 = Transformaciones.Translate(0f, -0.4f / 2f, 0f);
                    Matrix4x4 r = Transformaciones.RotateX(-rX.val);
                    m = accumT * t1 * r * t2 * scales[i];
                    accumT *= t1 * r * t2;
                }
                else
                    accumT *= locations[i];
                
                partsBody[i].GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(m, vOriginals);
            }
        }
        else if(type == "ARM" && side == "RIGHT")
        {
            for (int i = (int)PartsBody.RSHOUDLER; i <= (int)PartsBody.RHAND; i++)
            {
                Matrix4x4 m = accumT * locations[i] * scales[i];
                if(i == (int)PartsBody.RSHOUDLER)
                {
                    accumT = accumulative;
                    Matrix4x4 r = Transformaciones.RotateZ(5f);
                    m = accumT * locations[i] * r * scales[i];
                    accumT *= locations[i] * r;
                }
                else if(i == (int)PartsBody.RELBOW)
                {
                    Matrix4x4 t1 = Transformaciones.Translate(0f, -0.5f / 2f, 0f);
                    Matrix4x4 t2 = Transformaciones.Translate(0f, -0.4f / 2f, 0f);
                    Matrix4x4 r = Transformaciones.RotateX(rX.val);
                    m = accumT * t1 * r * t2 * scales[i];
                    accumT *= t1 * r * t2;
                }
                else
                    accumT *= locations[i];
                
                partsBody[i].GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(m, vOriginals);
            }
        }
        else if(type == "LEG" && side == "LEFT")
        {
            for (int i = (int)PartsBody.LTHIGH; i <= (int)PartsBody.LFOOT; i++)
            {
                Matrix4x4 m = accumT * locations[i] * scales[i];
                if(i == (int)PartsBody.LTHIGH)
                {
                    accumT = accumulative;
                    Matrix4x4 t1 = Transformaciones.Translate(-1f/2f, -0.5f/2f, 0f);
                    Matrix4x4 t2 = Transformaciones.Translate(0.4f/2f, -0.8f/2f, 0f);
                    Matrix4x4 r = Transformaciones.RotateX(rX.val);
                    m = accumT * t1 * r * t2 * scales[i];
                    accumT *= t1 * r * t2;
                }
                else if(i == (int)PartsBody.LKNEE)
                {
                    Matrix4x4 t1 = Transformaciones.Translate(0f, -0.8f / 2f, 0f);
                    Matrix4x4 t2 = Transformaciones.Translate(0f, -0.3f / 2f, 0f);
                    Matrix4x4 r = Transformaciones.RotateX(rX.val);
                    m = accumT * t1 * r * t2 * scales[i];
                    accumT *= t1 * r * t2;
                }
                else
                    accumT *= locations[i];
                
                partsBody[i].GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(m, vOriginals);
            }
        }
        else if(type == "LEG" && side == "RIGHT")
        {
            for (int i = (int)PartsBody.RTHIGH; i <= (int)PartsBody.RFOOT; i++)
            {
                Matrix4x4 m = accumT * locations[i] * scales[i];
                if(i == (int)PartsBody.RTHIGH)
                {
                    accumT = accumulative;
                    Matrix4x4 t1 = Transformaciones.Translate(1f/2f, -0.5f/2f, 0f);
                    Matrix4x4 t2 = Transformaciones.Translate(-0.4f/2f, -0.8f/2f, 0f);
                    Matrix4x4 r = Transformaciones.RotateX(-rX.val);
                    m = accumT * t1 * r * t2 * scales[i];
                    accumT *= t1 * r * t2;
                }
                else if(i == (int)PartsBody.RKNEE)
                {
                    Matrix4x4 t1 = Transformaciones.Translate(0f, -0.8f / 2f, 0f);
                    Matrix4x4 t2 = Transformaciones.Translate(0f, -0.3f / 2f, 0f);
                    Matrix4x4 r = Transformaciones.RotateX(-rX.val);
                    m = accumT * t1 * r * t2 * scales[i];
                    accumT *= t1 * r * t2;
                }
                else
                    accumT *= locations[i];
                
                partsBody[i].GetComponent<MeshFilter>().mesh.vertices = Transformaciones.Transform(m, vOriginals);
            }
        }
    }
}
