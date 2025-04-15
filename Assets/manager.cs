using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject tetrahedron;
    public GameObject[] vetGameObj = new GameObject[24];

    struct TetraInfo
    {
        public Vector3 position;
        public Vector3 rotation;

        public TetraInfo(Vector3 pos, Vector3 rot)
        {
            position = pos;
            rotation = rot;
        }
    }

    void Start()
    {
        List<TetraInfo> infos = new List<TetraInfo>
        {
            new TetraInfo(new Vector3(0,0,0), Vector3.zero),
            new TetraInfo(new Vector3(1,0,0), Vector3.zero),
            new TetraInfo(new Vector3(2,0,0), Vector3.zero),
            new TetraInfo(new Vector3(1.5f, 0.86603f, 0.28868f), Vector3.zero),
            new TetraInfo(new Vector3(0.5f, 0.86603f, 0.28868f), Vector3.zero),
            new TetraInfo(new Vector3(1f, 1.73206f, 0.57736f), Vector3.zero),
            new TetraInfo(new Vector3(1.5f, 0f, 0.86603f), Vector3.zero),
            new TetraInfo(new Vector3(0.5f, 0f, 0.86603f), Vector3.zero),
            new TetraInfo(new Vector3(1f, 0f, 1.73206f), Vector3.zero),
            new TetraInfo(new Vector3(1f, 0.86603f, 1.15472f), Vector3.zero),
            new TetraInfo(new Vector3(2.5f, 0f, 0.86603f), new Vector3(0f,180f,0f)),
            new TetraInfo(new Vector3(1.5f, 0f, 0.86603f), new Vector3(0f,180f,0f)),
            new TetraInfo(new Vector3(2f, 0f, 1.73206f), new Vector3(0f,180f,0f)),
            new TetraInfo(new Vector3(1.5f, 0.86603f, 0.28868f), new Vector3(143.14f,180f,0f)),
            new TetraInfo(new Vector3(2.5f, 0.86603f, 0.28868f), new Vector3(143.14f,180f,0f)),
            new TetraInfo(new Vector3(2f, 1.73206f, 0.57736f), new Vector3(143.14f,180f,0f)),
            new TetraInfo(new Vector3(1f, 0.86603f, 1.15472f), new Vector3(-162.4815f, 54.7f, 33.02f)), // serão definidos com base nas faces
            new TetraInfo(new Vector3(1.5f, 0.86603f, 2.02076f), new Vector3(-162.4815f, 54.7f, 33.02f)),
            new TetraInfo(new Vector3(1.5f, 1.73206f, 1.4434f), new Vector3(-162.4815f, 54.7f, 33.02f)),
            new TetraInfo(Vector3.zero, Vector3.zero),
            new TetraInfo(Vector3.zero, Vector3.zero),
            new TetraInfo(Vector3.zero, Vector3.zero),
            new TetraInfo(Vector3.zero, Vector3.zero),
            new TetraInfo(Vector3.zero, Vector3.zero)
        };

        for (int i = 0; i < infos.Count; i++)
        {
            vetGameObj[i] = Instantiate(tetrahedron, infos[i].position, Quaternion.Euler(infos[i].rotation));

            GameObject pivot = new GameObject("Pivot_" + i);
            pivot.transform.position = infos[i].position;
            vetGameObj[i].transform.SetParent(pivot.transform);
        }

    }

    //Vector3(342.5,125.300003,146.899994)
    /*void OnDrawGizmos()
    {
        if (vetGameObj == null) return;

        foreach (GameObject obj in vetGameObj)
        {
            if (obj == null) continue;

            Vector3 pos = obj.transform.position;

            Gizmos.color = Color.red; // forward
            Gizmos.DrawLine(pos, pos + obj.transform.forward * 0.5f);

            Gizmos.color = Color.green; // up
            Gizmos.DrawLine(pos, pos + obj.transform.up * 0.5f);

            Gizmos.color = Color.blue; // right
            Gizmos.DrawLine(pos, pos + obj.transform.right * 0.5f);
        }
    }*/
}