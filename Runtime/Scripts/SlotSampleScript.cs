using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSampleScript : MonoBehaviour
{
    [SerializeField] private Types type;
    public Types Type => type;
    Vector2 TargetScale;

    bool StartScaling;

    public float ScalingSpeed;

    IEnumerator WaitToScale()
    {

        yield return new WaitForSeconds(0.4f);

        StartScaling = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        TargetScale = new Vector2(transform.localScale.x, transform.localScale.y);
        transform.localScale = new Vector2(0, 0); 

        StartCoroutine(WaitToScale());
    }

    // Update is called once per frame
    void Update()
    {
        if (StartScaling)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, TargetScale, ScalingSpeed * Time.deltaTime);
        }

        if(GameManagerScript.CurrentSlotTag != type.ToString())
        {
            Destroy(gameObject);
        }
    }
}
