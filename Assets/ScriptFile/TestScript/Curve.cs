using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Curve : MonoBehaviour
{
    [Range(0,1)]
    public float test= 0f;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;
    private void OnEnable()
    {
        p4 = Camera.main.transform.position;
    }
    public void BeziaMove(GameObject rock)
    {
        StartCoroutine(CorutineBezia(rock));
    }
    IEnumerator CorutineBezia(GameObject rock)
    {
        while (true)
        {
            p1= rock.transform.position;
            transform.position = BezierTest(p1, p2, p3, p4, test);
            test += 0.005f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public Vector3 BezierTest(
        Vector3 p_1,
        Vector3 p_2,
        Vector3 p_3,
        Vector3 p_4,
        float Value)
    {
        Vector3 a = Vector3.Lerp(p_1, p_2, Value);
        Vector3 b = Vector3.Lerp(p_2, p_3, Value);
        Vector3 c = Vector3.Lerp(p_3, p_4, Value);

        Vector3 d = Vector3.Lerp(a, b, Value);
        Vector3 e = Vector3.Lerp(b, c, Value);

        Vector3 f = Vector3.Lerp(d, e, Value);
        return f;
    }
}

