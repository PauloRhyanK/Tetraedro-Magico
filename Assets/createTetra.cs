using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esse script cria um tetraedro proceduralmente no Unity

[RequireComponent(typeof(MeshCollider))] // Garante que o GameObject tenha um collider
[RequireComponent(typeof(MeshFilter))]   // Garante que tenha um filtro de malha (MeshFilter)
[RequireComponent(typeof(MeshRenderer))] // Garante que tenha um renderizador de malha (MeshRenderer)
public class createTetra : MonoBehaviour {

    public bool sharedVertices = false; // Define se os vértices serão compartilhados entre as faces

    // Definição dos vértices do tetraedro
    Vector3 p0 = new Vector3(0, 0, 0);
    Vector3 p1 = new Vector3(1, 0, 0);
    Vector3 p2 = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
    Vector3 p3 = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
    
    Mesh mesh; // Variável para armazenar a malha do tetraedro

    // Método para obter os vetores dos vértices do tetraedro
    public Vector3[] getVectors()
    {
        Vector3[] vertex = new Vector3[] { p0, p1, p2, p3 };
        return vertex;
    }

    // Método responsável por reconstruir a malha do tetraedro
    public void Rebuild()
    {
        // Obtém o componente MeshFilter do GameObject
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter not found!");
            return;
        }

        // Obtém ou cria uma nova malha
        mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            meshFilter.mesh = new Mesh();
            mesh = meshFilter.sharedMesh;
        }
        mesh.Clear(); // Limpa a malha atual para evitar sobreposição de vértices

        // Define os vértices e triângulos
        if (sharedVertices)
        {
            // Define os vértices compartilhados
            mesh.vertices = new Vector3[] { p0, p1, p2, p3 };
            mesh.triangles = new int[]{
                0,1,2, // Base
                0,2,3, // Face lateral 1
                2,1,3, // Face lateral 2
                0,3,1  // Face lateral 3
            };
        }
        else
        {
            // Define os vértices sem compartilhamento
            mesh.vertices = new Vector3[]{
                p0,p1,p2, // Base
                p0,p2,p3, // Face lateral 1
                p2,p1,p3, // Face lateral 2
                p0,p3,p1  // Face lateral 3
            };
            
            mesh.triangles = new int[]{ // Cada face do tetraedro é composta por 3 vértices
                0,1,2,  // Base
                3,4,5,  // Face lateral 1
                6,7,8,  // Face lateral 2
                9,10,11 // Face lateral 3
            };
        }

        // Define as cores de cada face do tetraedro
        Color[] color = new Color[mesh.vertices.Length];
        color[0] = Color.blue;
        color[1] = Color.blue;
        color[2] = Color.blue;

        color[3] = Color.red;
        color[4] = Color.red;
        color[5] = Color.red;

        color[6] = Color.yellow;
        color[7] = Color.yellow;
        color[8] = Color.yellow;

        color[9] = Color.magenta;
        color[10] = Color.magenta;
        color[11] = Color.magenta;

        mesh.colors = color; // Aplica as cores aos vértices da malha

        // Recalcula as normais e os limites da malha para otimização
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        MeshCollider collider = GetComponent<MeshCollider>();
        if (collider != null) {
            collider.sharedMesh = mesh;
            collider.convex = true; // Importante para que ComputePenetration funcione corretamente
        }
    }

    // Método chamado ao iniciar o GameObject
    void Start () {
        Rebuild(); // Gera a malha do tetraedro ao iniciar
    }
    
    // Método chamado a cada frame
    void Update () {
        // Pode ser utilizado para futuras modificações dinâmicas
    }
}
