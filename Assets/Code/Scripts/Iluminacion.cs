using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iluminacion : MonoBehaviour
{
    public List<MonoBehaviour> Scripts;
    public Vector3 ka;
    public Vector3 kd;
    public Vector3 ks;
    public Vector3 Ia;
    public Vector3 Id;
    public Vector3 Is;
    public Vector3 PoI;
    public Vector3 n;
    public Vector3 LIGHT;
    public Vector3 CAMERA;
    public float ALPHA;
    public float SR;
    public Vector3 SC;

    public Vector3 contact;
   

    Vector3 Cast()
    {
        Camera cam = Camera.main;

        float frusttumHeight = 2.0f * cam.nearClipPlane * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frusttumHeight * cam.aspect;
        float pixelWidth = frustumWidth / 480;
        float pixelHeight = frusttumHeight / 640;
        Vector3 center = FindTopLeftFrusrtumNear();
        center += +(pixelWidth / 2f) * cam.transform.right; Debug.Log(pixelWidth.ToString("F5"));
        center -= (pixelWidth / 2f) * cam.transform.up; Debug.Log(pixelWidth.ToString("F5"));

     
        return center;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 i = Illumination();
        Debug.Log(i.ToString("F5"));

        GameObject sph = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sph.transform.position = SC;
        sph.transform.localScale = new Vector3(SR*2f, SR*2f, SR*2f);
        Renderer rend = sph.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", new Color(kd.x, kd.y, kd.z));
        rend.material.SetColor("_SpecColor", new Color(ks.x, ks.y, ks.z));

        GameObject pointLight = new GameObject("ThePointLight");
        Light lightComp = pointLight.AddComponent<Light>();
        lightComp.type = LightType.Point;
        lightComp.color = new Color(Id.x, Id.y, Id.z);
        lightComp.intensity = 20;
        Camera.main.transform.position = CAMERA;
        Camera.main.transform.LookAt(PoI);

        //comprovar frustum
        GameObject sph2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sph2.transform.position = FindTopLeftFrusrtumNear();
        sph2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Renderer rend2 = sph2.GetComponent<Renderer>();
        rend2.material.shader = Shader.Find("Specular");
        rend2.material.SetColor("_Color", new Color(1, 0, 1));
        rend2.material.SetColor("_SpecColor", new Color(1, 1, 1));

        Cast();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 c = Camera.main.transform.position;
        Debug.DrawLine(c, contact, Color.blue);

        Vector3 l = LIGHT - PoI;
        Vector3 lp = n * Vector3.Dot(n.normalized, l);
        Vector3 lo = l - lp;
        Vector3 r = lp - lo;
        Vector3 v = CAMERA-PoI;

        Debug.DrawLine(PoI, l + PoI, Color.red);
        Debug.DrawLine(PoI, r + PoI, Color.magenta);
        Debug.DrawLine(PoI, n + PoI, Color.green);
        Debug.DrawLine(PoI, v + PoI, Color.blue);
        
    }

    Vector3 Illumination()
    {
        Vector3 A = new Vector3(ka.x * Ia.x, ka.y * Ia.y, ka.z * Ia.z);
        Vector3 D = new Vector3(ka.x * Id.x, kd.y * Id.y, kd.z * Id.z);
        Vector3 S = new Vector3(ks.x * Is.x, ks.y * Is.y, ks.z * Is.z);

        Vector3 l = LIGHT - PoI;
        Vector3 v = CAMERA - PoI;
        float dotNuLu = Vector3.Dot(n.normalized, l.normalized);
        float dotNuL = Vector3.Dot(n.normalized, l);

        Vector3 lp = n * dotNuL;
        Vector3 lo = l - lp;
        Vector3 r = lp-lo;
        D *= dotNuLu;
        S *= Mathf.Pow(Vector3.Dot(v.normalized,r.normalized),ALPHA);
        return A + D + S;
    }

    Vector3 FindTopLeftFrusrtumNear()
    {
        Camera cam = Camera.main;
        //localizar camara
        Vector3 o = cam.transform.position;
        //mover hacia adelante
        Vector3 p = o + cam.transform.forward * cam.nearClipPlane;
        //obtener dimenciones del frustum
        float frusttumHeight = 2.0f * cam.nearClipPlane * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frusttumHeight * cam.aspect;
        //mover hacia arriba, media altura
        p += cam.transform.up * frustumWidth / 2.0f;
        //mover a la izquierda, medio ancho
        p += cam.transform.right * frustumWidth / 2.0f;
        return p;

    }
}