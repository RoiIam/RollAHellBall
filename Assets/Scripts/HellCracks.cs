using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class HellCracks : MonoBehaviour
{
    public Material material;
    public List<Transform> cracks;
    public MeshCollider mCollider;
    public float startOffset;

    public AnimationClip ac;
    public Animator anim;
    public ParticleSystem ps;
    public Color burnt = new Color(0f, 0f, 0f, 1f);
    void Start()
    {
        /*
         we cant use this
         List<Vector4> list = new List<Vector4>();
        foreach (var c in cracks)
        {
            list.Add(c.position);
        }
        Shader.SetGlobalVectorArray("_hellCracks", list);
        */
        material = GetComponent<MeshRenderer>().material;
        mCollider = GetComponent<MeshCollider>();
        //float cutoff = Random.Range(0.2f, 10f);
        //material.SetFloat("_CutoutSize",cutoff);
        material.SetFloat("_FalloffSize",1);
        //startOffset =Random.Range(0.2f, 8f);
        anim = GetComponent<Animator>();
        //ps = GetComponent<ParticleSystem>();
        
        float randomStartTime = Random.Range(0.2f, 4);
        
        anim.Play("FireAnim",0,randomStartTime);
        anim.Play("FireAnim",0,randomStartTime);
    }

    public void StartAnim()
    {
        ps.Play();
    }
    public void EndAnim()
    {
        ps.Stop();
        
    }
    private void Update()
    {
        //we dont need to deltatime it... :)
        if (startOffset > 0)
        {
            startOffset -= 0.1f;
            return;
        }
        
        //else grow/shrink the cutoff + collider
        //5.1 - 0.4 cuttout size
        //spher collider radii 1.6 -0.1 + off
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //fall and die
            mCollider.enabled = false;
            other.GetComponent<MeshRenderer>().material.SetColor("_BaseColor",burnt);
            StartCoroutine(StartFail(other));
        }
        
    }

    IEnumerator StartFail(Collider other)
    {
        yield return new WaitForSeconds(1);
        other.GetComponent<PlayerController>().Failed();
    }
}
