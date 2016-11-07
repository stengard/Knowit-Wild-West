using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class AdjustSize : MonoBehaviour
{
    public float StepSize;
    // Use this for initialization

    public void MakeLarger()
    {
        Vector3 newSize = new Vector3(gameObject.transform.localScale.x * StepSize, gameObject.transform.localScale.y* StepSize, gameObject.transform.localScale.z * StepSize);

        StartCoroutine(ResizeObject(gameObject.transform, gameObject.transform.localScale, newSize, 1));
    }

    public void MakeSmaller()
    {
        Vector3 newSize = new Vector3(gameObject.transform.localScale.x / StepSize, gameObject.transform.localScale.y / StepSize, gameObject.transform.localScale.z / StepSize);

        StartCoroutine(ResizeObject(gameObject.transform, gameObject.transform.localScale, newSize, 1));

    }

    IEnumerator ResizeObject(Transform thisTransform, Vector3 startSize, Vector3 endSize, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.localScale = Vector3.Lerp(startSize, endSize, i);
            yield return 0;
        }
    }

}
