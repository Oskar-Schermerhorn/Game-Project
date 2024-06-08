using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSlide : MonoBehaviour
{
    [SerializeField] private ContactFilter2D wallFilter;
    public void calculateDropPosition()
    {
        wallFilter.useTriggers = true;
        wallFilter.useLayerMask = true;
        LayerMask mask = new LayerMask();
        mask |= (1 << 3);
        mask |= (1 << 6);
        wallFilter.SetLayerMask(mask);
        Vector2 offset;
        offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        print(offset);
        //item.transform.position = new Vector3(this.transform.position.x + offset.x, this.transform.position.y + offset.y, this.transform.position.z);
        StartCoroutine(MoveItem(offset));
    }
    IEnumerator MoveItem(Vector2 offset)
    {
        Vector2 destination = new Vector3(this.transform.position.x + offset.x, this.transform.position.y + offset.y, this.transform.position.z);
        while (Vector2.Distance(this.transform.position, destination) > 0.01f)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, destination, 0.01f);
            yield return null;
            if (this.gameObject.GetComponent<Rigidbody2D>().IsTouching(wallFilter))
            {
                print("stop");
                break;
            }
        }
        Destroy(this);
    }
}
