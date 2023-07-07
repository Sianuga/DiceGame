using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DesintegrationControl : MonoBehaviour
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material desintegrationMaterial;
    [SerializeField] public float desintegrationTime { get; private set; } = 1f;
    
    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void desintegrate()
    {
       /* meshRenderer.material = desintegrationMaterial;*/
        StartCoroutine(DesintegrationProcess());
    }

    private IEnumerator DesintegrationProcess()
    {
/*        float elapsedTime = 0f;
        float fadeValue = 0f;
        float startFadeValue = 1f;
        float targetFadeValue = -1f;


        while (elapsedTime < desintegrationTime)
        {
            fadeValue = Mathf.Lerp(startFadeValue, targetFadeValue, elapsedTime / desintegrationTime);
            meshRenderer.material.SetFloat("_Fade", fadeValue);

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }


        meshRenderer.material.SetFloat("_Fade", targetFadeValue);*/

        yield return new WaitForSeconds(0.01f);
    }

}
