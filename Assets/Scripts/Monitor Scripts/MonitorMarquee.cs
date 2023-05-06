using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorMarquee : MonoBehaviour
{
    [SerializeField] VisualValues _vis_vals;

    void Start()
    {
        if (_vis_vals is null)
            _vis_vals = AssetManager.Instance.VisualValues;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.x <= _vis_vals.MarqueeDespawn)
        {
            transform.localPosition = new Vector2(_vis_vals.MarqueeRespawn, 0);
        }

        moveMarquee(_vis_vals.MarqueeMoveSpeed, _vis_vals.MarqueeDespawn);

    }

    private void moveMarquee(float moveSpeed, float targetPos)
    {
        transform.localPosition = new Vector2(Mathf.MoveTowards(transform.localPosition.x, targetPos, moveSpeed * Time.deltaTime), 0); 
    }
}
