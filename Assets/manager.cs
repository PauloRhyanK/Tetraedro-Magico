using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class manager : MonoBehaviour {
    public GameObject planePrefab;
    public GameObject tetrahedron;
    public List<GameObject> tetrahedrons = new List<GameObject>();
    public Dictionary<string, GameObject> rotationPlanes = new();
    private bool isRotating = false;
    
	void Start () {
		// Instanciando os objetos
        CreateTetrahedrons();

        // Movimentando, Rotacionando e Invertendo escalas para forcar o tetraedro maior
        SetPositions();
        SetRotations();
        SetScales();

        //FindPlanesInScene();
        InstantiatePlanes();
    }

	void Update () {
        // Para realizar as rotações com os Botões
        StartCoroutines();
	}
    void CreateTetrahedrons() {
        // Criar todos os Tetraedros
        for (int i = 0; i < 22; i++) {
            GameObject newTetra = Instantiate(tetrahedron, Vector3.zero, Quaternion.identity);
            newTetra.name = "Tetrahedron_" + i;
            tetrahedrons.Add(newTetra);

            // Adicionar arestas pretas
            AddEdges(newTetra);
        }
    }

    void AddEdges(GameObject tetra) {
        Mesh mesh = tetra.GetComponent<MeshFilter>().sharedMesh;
        if (mesh == null) return;

        Vector3[] vertices = mesh.vertices;

        // Obtem pares de arestas únicos
        HashSet<(int, int)> edges = new();

        int[] triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3) {
            AddEdge(edges, triangles[i], triangles[i + 1]);
            AddEdge(edges, triangles[i + 1], triangles[i + 2]);
            AddEdge(edges, triangles[i + 2], triangles[i]);
        }

        foreach (var (a, b) in edges) {
            GameObject edgeObj = new GameObject("Edge");
            edgeObj.transform.parent = tetra.transform;
            edgeObj.transform.localPosition = Vector3.zero;
            edgeObj.transform.localRotation = Quaternion.identity;
            edgeObj.transform.localScale = Vector3.one;

            LineRenderer lr = edgeObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = 0.01f;
            lr.endWidth = 0.01f;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = Color.black;
            lr.endColor = Color.black;

            lr.useWorldSpace = false;
            lr.SetPosition(0, vertices[a]);
            lr.SetPosition(1, vertices[b]);
        }
    }

    void AddEdge(HashSet<(int, int)> edgeSet, int i, int j) {
        if (i < j)
            edgeSet.Add((i, j));
        else
            edgeSet.Add((j, i));
    }

    void SetPositions() {
        // Definição manual de posições para formar a estrutura
        tetrahedrons[1].transform.position = new Vector3(1, 0, 0);
        tetrahedrons[2].transform.position = new Vector3(2, 0, 0);
        tetrahedrons[3].transform.position = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3f);
        tetrahedrons[4].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3f);
        tetrahedrons[5].transform.position = new Vector3(1f, Mathf.Sqrt(0.75f) * 2, Mathf.Sqrt(0.75f) / 3f * 2);
        tetrahedrons[6].transform.position = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        tetrahedrons[7].transform.position = new Vector3(1.5f, 0, Mathf.Sqrt(0.75f));
        tetrahedrons[8].transform.position = new Vector3(1f, Mathf.Sqrt(0.75f), (Mathf.Sqrt(0.75f) / 3f) + Mathf.Sqrt(0.75f));
        tetrahedrons[9].transform.position = new Vector3(1f, 0, Mathf.Sqrt(0.75f) * 2);

        // De cabeça para baixo
        // Rosa
        tetrahedrons[10].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3f);
        tetrahedrons[11].transform.position = new Vector3(2f, Mathf.Sqrt(0.75f) * 2, Mathf.Sqrt(0.75f) / 3f * 2);
        tetrahedrons[12].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3f);

        // Amarelo
        tetrahedrons[13].transform.position = new Vector3(1.15f, 0.347f, 1.244025f);
        tetrahedrons[14].transform.position = new Vector3(1.649f, 0.347f, 0.378f);
        tetrahedrons[15].transform.position = new Vector3(1.15f, 1.215f, 0.663f);

        // Azul
        tetrahedrons[16].transform.position = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        tetrahedrons[17].transform.position = new Vector3(1.5f, 0, Mathf.Sqrt(0.75f));
        tetrahedrons[18].transform.position = new Vector3(1, 0, Mathf.Sqrt(0.75f) * 2);

        // Vermelho
        tetrahedrons[19].transform.position = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3f);
        tetrahedrons[20].transform.position = new Vector3(1f, Mathf.Sqrt(0.75f) * 2, Mathf.Sqrt(0.75f) / 3f * 2);
        tetrahedrons[21].transform.position = new Vector3(1f, Mathf.Sqrt(0.75f), (Mathf.Sqrt(0.75f) / 3f) + Mathf.Sqrt(0.75f));
    }

    void SetRotations() {
        // Ajustando rotações específicas
        //Rosa
        tetrahedrons[10].transform.Rotate(36.9f,0f,180f);
        tetrahedrons[11].transform.Rotate(36.9f,0f,180f);
        tetrahedrons[12].transform.Rotate(36.9f,0f,180f);

        // Amarelo
        tetrahedrons[13].transform.Rotate(-17.43f,-5.2f,33f);
        tetrahedrons[14].transform.Rotate(-17.43f,-5.2f,33f);
        tetrahedrons[15].transform.Rotate(-17.43f,-5.2f,33f);

        // Vermelho
        tetrahedrons[19].transform.Rotate(-17.43f,5.2f,-33f);
        tetrahedrons[20].transform.Rotate(-17.43f,5.2f,-33f);
        tetrahedrons[21].transform.Rotate(-17.43f,5.2f,-33f);

        // Azul
        tetrahedrons[16].transform.Rotate(0,60,0);
        tetrahedrons[17].transform.Rotate(0,60,0);
        tetrahedrons[18].transform.Rotate(0,60,0);
    }

    void SetScales() {
        // Invertendo escalas para certos tetraedros
        Vector3 invertedScale = new Vector3(1, -1, 1);
        tetrahedrons[13].transform.localScale = invertedScale;
        tetrahedrons[14].transform.localScale = invertedScale;
        tetrahedrons[15].transform.localScale = invertedScale;
        tetrahedrons[19].transform.localScale = invertedScale;
        tetrahedrons[20].transform.localScale = invertedScale;
        tetrahedrons[21].transform.localScale = invertedScale;
    }

    // Instanciar os planos (definido como prefab para realizar as rotações)
    void InstantiatePlanes()
    {
        AddPlane("Base", new Vector3(1.5f, 0, Mathf.Sqrt(0.75f)), new Vector3(0, 0, 0));

        // Para os angulos foi feito o Calculo do Coeficiente angular e transformado em Graus
        AddPlane("Face_Frontal", new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f)/3), new Vector3(71.56505f, 180, 0));
        AddPlane("Face_Esquerda", new Vector3(1f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f)*4 /3), new Vector3(0, 30, 71.56505f));
        AddPlane("Face_Direita", new Vector3(2f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f)*4/3), new Vector3(0, -30, -71.56505f));

        // Preciso dos Angulos para fazer as Diagonais
        AddPlane("Diagonal_Esquerda", new Vector3(1.036f, 0.47f, 0.6f), new Vector3(0, -30, -71.56505f));
        AddPlane("Diagonal_Direita", new Vector3(2f, 0.481f, 0.578f), new Vector3(71.56505f, -60, 0));
        AddPlane("Diagonal_Costas", new Vector3(1.5f, 0.461f, 1.505f), new Vector3(-71.56505f, 0, 0));
        AddPlane("Diagonal_Base", new Vector3(1.5f, Mathf.Sqrt(0.75f)*1.5f, Mathf.Sqrt(0.75f)), new Vector3(0, 0, 0));

        AddPlane("Tetra_Topo", new Vector3(1.5f, Mathf.Sqrt(0.75f)*3, Mathf.Sqrt(0.75f)), new Vector3(0, 0, 0));
        AddPlane("Tetra_Esquerda", new Vector3(0f, 0f, 0f), new Vector3(71.56505f, 60, 0));
        AddPlane("Tetra_Direita", new Vector3(3f, 0f, 0f), new Vector3(71.56505f, -60, 0));
        AddPlane("Tetra_Costas", new Vector3(1.5f, 0, Mathf.Sqrt(0.75f) * 3), new Vector3(-71.56505f, 0, -180));
    }

    // Inserir o Plano na Posição e Rotação desejada
    void AddPlane(string name, Vector3 position, Vector3 rotationEuler)
    {
        GameObject plane = Instantiate(planePrefab, position, Quaternion.identity);
        plane.transform.eulerAngles = rotationEuler;
        plane.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        plane.name = name;

        rotationPlanes[name] = plane;
    }


    void StartCoroutines(){
        // Faces
        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(RotateByPlane("Base", Vector3.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(RotateByPlane("Face_Frontal", rotationPlanes["Face_Frontal"].transform.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(RotateByPlane("Face_Esquerda", rotationPlanes["Face_Esquerda"].transform.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(RotateByPlane("Face_Direita", rotationPlanes["Face_Direita"].transform.up, 120.00f, 0.5f));

        // Diagonais
        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(RotateByPlane("Diagonal_Base", rotationPlanes["Diagonal_Base"].transform.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(RotateByPlane("Diagonal_Costas", rotationPlanes["Diagonal_Costas"].transform.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(RotateByPlane("Diagonal_Esquerda", rotationPlanes["Diagonal_Esquerda"].transform.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(RotateByPlane("Diagonal_Direita", rotationPlanes["Diagonal_Direita"].transform.up, 120.00f, 0.5f));

        // Pontas
        if (Input.GetKeyDown(KeyCode.Z))
            StartCoroutine(RotateByPlane("Tetra_Topo", rotationPlanes["Tetra_Topo"].transform.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.X))
            StartCoroutine(RotateByPlane("Tetra_Costas", rotationPlanes["Tetra_Costas"].transform.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(RotateByPlane("Tetra_Esquerda", rotationPlanes["Tetra_Esquerda"].transform.up, 120.00f, 0.5f));
        if (Input.GetKeyDown(KeyCode.V))
            StartCoroutine(RotateByPlane("Tetra_Direita", rotationPlanes["Tetra_Direita"].transform.up, 120.00f, 0.5f));
    }

    IEnumerator RotateByPlane(string groupName, Vector3 axis, float angle, float duration) {
        if (isRotating) yield break; // Bloqueia se já estiver rodando
        isRotating = true;

        if (!rotationPlanes.ContainsKey(groupName)) { // Verficar a existencia do plano
            isRotating = false;
            yield break;
        }

        GameObject plane = rotationPlanes[groupName];
        Collider planeCollider = plane.GetComponent<Collider>();
        if (planeCollider == null) {
            isRotating = false;
            yield break;
        }

        // Captura os tetraedros que estao transpassando o plano
        List<GameObject> group = new();
        foreach (var t in tetrahedrons) {
            Collider tetraCollider = t.GetComponent<Collider>();
            if (tetraCollider == null) continue;

            bool isTouching = Physics.ComputePenetration(
                tetraCollider, t.transform.position, t.transform.rotation,
                planeCollider, plane.transform.position, plane.transform.rotation,
                out Vector3 dir, out float dist
            );

            if (isTouching) {
                group.Add(t);
            }
        }

        if (group.Count == 0) {
            isRotating = false;
            yield break;
        }

        // Rotaciona os tetraedros com base no centro do plano
        Vector3 pivot = plane.transform.position;

        float time = 0f;
        float speed = angle / duration;

        float totalRotated = 0f;

        while (time < duration) {
            float step = speed * Time.deltaTime;

            // Corrigir se o último passo passar do ângulo alvo
            if (totalRotated + step > angle) {
                step = angle - totalRotated;
            }

            foreach (GameObject t in group) {
                t.transform.RotateAround(pivot, axis, step);
            }

            totalRotated += step;
            time += Time.deltaTime;
            yield return null;
        }

        isRotating = false;
    }

}