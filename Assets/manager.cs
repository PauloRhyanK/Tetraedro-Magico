using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{

    public GameObject tetrahedron; // prefab da camrera
    //public GameObject[] vetGameObj = new GameObject[24];
    private Dictionary<string, Vector3> pontos = new Dictionary<string, Vector3>();
    private List<GameObject> tetraHedrons = new List<GameObject>();

    GameObject pai;
    Vector3 m_Center;
    // Use this for initialization
    void Start()
    {
        // Coordenadas dos pontos A, B e C
        pontos.Add("A1", new Vector3(0f, 0f, 0f));
        pontos.Add("A2", new Vector3(1f, 0f, 0f));
        pontos.Add("A3", new Vector3(2f, 0f, 0f));
        pontos.Add("A4", new Vector3(0.5f, 0f, 0.865f));
        pontos.Add("A5", new Vector3(1.5f, 0f, 0.865f));
        pontos.Add("A6", new Vector3(1f, 0f, 1.73f));

        pontos.Add("B1", new Vector3(0.5f, 0.865f, 0.286f));
        pontos.Add("B2", new Vector3(1.5f, 0.865f, 0.286f));
        pontos.Add("B3", new Vector3(1f, 0.865f, 1.151f));

        pontos.Add("C", new Vector3(1f, 1.74f, 0.582f));

        pontos.Add("FA1", new Vector3(0.5f, 0.865f, 0.278f));
        pontos.Add("FA2", new Vector3(1.5f, 0.865f, 0.278f));
        pontos.Add("FA3", new Vector3(1f, 1.734f, 0.556f));

        pontos.Add("FB1", new Vector3(1.5f, 0.866f, 2f));
        pontos.Add("FB2", new Vector3(1f, 0.864f, 1.148f));
        pontos.Add("FB3", new Vector3(1.5f, 1.73f, 1.44f));

        pontos.Add("FC1", new Vector3(2.5f, 0.865f, 0.287f));
        pontos.Add("FC2", new Vector3(2f, 0.865f, 1.154f));
        pontos.Add("FC3", new Vector3(2f, 1.73f, 0.574f));

        pontos.Add("FD1", new Vector3(1.5f, 0, 0.863f));
        pontos.Add("FD2", new Vector3(2.5f, 0, 0.863f));
        pontos.Add("FD3", new Vector3(2f, 0, 1.72f));

        // GameObject tetrahedronF1A = Instantiate(tetrahedron, pontos["F1A"], Quaternion.identity);
        // tetrahedronF1A.transform.Rotate(108f, 0f, 0f);
        // Criar os tetraedros nas posições indicadas
        foreach (KeyValuePair<string, Vector3> ponto in pontos)
        {
            GameObject obj = Instantiate(tetrahedron, ponto.Value, Quaternion.identity);
            obj.name = ponto.Key;

            // Rotate based on name
            if (obj.name.Contains("FA"))
            {
                obj.transform.Rotate(108f, 0f, 0f);
            }
            else if (obj.name.Contains("FB"))
            {
                obj.transform.Rotate(108f, 121.29f, 1.557f);
            }
            else if (obj.name.Contains("FC"))
            {
                obj.transform.Rotate(108, 240, 0);
            }
            else if (obj.name.Contains("FD"))
            {
                obj.transform.Rotate(362, 180, 0);
            }
            // Adjust position
            obj.transform.position += new Vector3(0, 3, 0);
            tetraHedrons.Add(obj);
        }

        // 	for(int i=0; i < 24; i++)
        //     {



        //         if(i == 0)
        //         {
        //             vetGameObj[i] = Instantiate(tetrahedron, new Vector3(0, 0, 0), Quaternion.identity); // tetraedro base
        //         }
        //         else
        //             vetGameObj[i]= Instantiate(tetrahedron, new Vector3(vetGameObj[i-1].transform.position.x + 1, 0, 0), vetGameObj[i - 1].transform.rotation);
        //         //i-1 posicao anterior
        //     }


        //     //pegar tetra da posicao 3 e transladar
        //     vetGameObj[3].transform.position = new Vector3(0.5f, 0.86603f, 0.28868f);
        //     //vetGameObj[3].transform.Rotate(110f,0f,0); // 90f
        //    // vetGameObj[3].transform.RotateAround(transform.position, Vector3.forward, 5f);

        //     pai = new GameObject();
        //     //pai.transform.position = new Vector3(0,1,0); //pivo
        //     pai.transform.position = new Vector3(0, 1, 0); //pivo
        //     vetGameObj[3].transform.parent = pai.transform;
        //     //vetGameObj[3].transform.bounds

        // Criar uma pirâmide entre A1, A2 e B1
    }

    void LogObjectAttributes(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("Objeto é nulo!");
            return;
        }

        Debug.Log($"Face: {obj.name}");
        // Acessar o Mesh e suas cores
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();

        if (meshFilter != null && meshFilter.mesh != null)
        {
            Mesh mesh = meshFilter.mesh;
            Color[] colors = mesh.colors;
            Debug.Log($"Mesh {mesh}:");
            if (colors.Length > 0)
            {
                Debug.Log($"Cores das faces do objeto {obj.name}:");
                for (int i = 0; i < colors.Length; i++)
                {
                    Debug.Log($"Face {i}: {colors[i]}");
                }
            }
            else
            {
                Debug.Log($"O objeto {obj.name} não possui cores definidas no mesh.");
            }
        }
        else
        {
            Debug.Log($"O objeto {obj.name} não possui um MeshFilter ou o mesh está vazio.");
        }

        // Verificar a cor do material
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Debug.Log($"Cor do material: {renderer.material.color}");
        }
        else
        {
            Debug.Log("O objeto não possui um Renderer.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //vetGameObj[3].transform.RotateAround(transform.position, Vector3.forward, 5f);
        //cria um gameobject: Pai. Tem eixo de rotacao
        //por o objeto como filho deste gameobject
        //rotaciona o gameObjet(pai): consequencia o filho rotaciona
        //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
        //pai.transform.Rotate(Vector3.right * 5);
        // vetGameObj[4].transform.Rotate((Vector3.right + Vector3.up) * 5);
    }
}
