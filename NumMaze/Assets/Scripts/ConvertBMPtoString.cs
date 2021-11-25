using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertBMPtoString : MonoBehaviour
{

    public Texture2D mazeBMP;
    private string mazeString;

    // Start is called before the first frame update
    void Start()
    {
        ConvertBitmap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConvertBitmap()
    {
        for (int iii = 0; iii < mazeBMP.height; iii++)
        {
            for (int jjj = 0; jjj < mazeBMP.width; jjj++)
            {
                string pixel = mazeBMP.GetPixel(jjj, iii).grayscale.ToString();
                mazeString += (pixel == "1") ? "0" : "1" ;
            }
        }
        print(mazeString);
    }

    private void GenerateVerticies(string mazeString, int width, int height)
    {
        // Calculate the amount of vertices 
        int length = 0;
        for (int i = 0; i < mazeString.Length; i++)
        {
            length += (mazeString[i] == '1') ? 8 : 0;
        }

        Mesh mazeMesh = new Mesh();
        Vector3[] vertices = new Vector3[length];
        //Vector3[] triangles = new Vector3[length];
        //Vector3[] normals = new Vector3[length];
        for (int iii = 0; iii < mazeBMP.height; iii++)
        {
            for (int jjj = 0; jjj < mazeBMP.width; jjj++)
            {
                int S = iii * 15 + jjj;
                for (int i = 0; i < 2; i++)
                {
                    int j = i * 2;
                    vertices[S + 0] = new Vector3(jjj, iii, j);
                    vertices[S + 1] = new Vector3(++jjj, iii, j);
                    vertices[S + 2] = new Vector3(jjj, ++iii, j);
                    vertices[S + 3] = new Vector3(++jjj, ++iii, j);
                }
            }
        }
        mazeMesh.vertices = vertices;
        //mazeMesh.SetTriangles(triangles, 0);

    }
}



