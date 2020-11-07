﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedGrass : MonoBehaviour
{
    public GeneratedBranch[] generatedBranches;
    public GameObject mergedMesh;
    float grassBrachRandomOffset = 0.55f;

    void Start()
    {
        GenerateGrass();
        MergeGrass();
        AddLODs();
    }

    void GenerateGrass()
    {
        int count = generatedBranches.Length;
        for (int i = 0; i < count; i++)
        {
            generatedBranches[i].Generate(4, 3, 1, null, 0.75f ,0.85f);
            generatedBranches[i].VerySlowlyConvertToFlatShading();
            generatedBranches[i].transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.075f, 0.2f, 0.075f));
            generatedBranches[i].transform.position += new Vector3(Random.Range(0.1f, grassBrachRandomOffset), 0, Random.Range(0.1f, grassBrachRandomOffset));
        }
    }
    void AddLODs()
    {
        if (GetComponent<LODGroup>())
        {
            LOD[] lods = new LOD[1];
            Renderer[] renderers = new Renderer[1];
            renderers[0] = mergedMesh.GetComponent<Renderer>();

            lods[0] = new LOD(0.0045f, renderers);

            GetComponent<LODGroup>().SetLODs(lods);
            GetComponent<LODGroup>().RecalculateBounds();
        }
    }
    
    void MergeGrass()
    {
        //Merge objects to one mesh
        int count = generatedBranches.Length;
        MeshFilter[] meshFilters = new MeshFilter[count];
        for (int i = 0; i < count; i++)
        {
            meshFilters[i] = generatedBranches[i].GetComponent<MeshFilter>();
        }

        mergedMesh = GeneratedMesh.CombineMeshes(transform, meshFilters);
    }

}