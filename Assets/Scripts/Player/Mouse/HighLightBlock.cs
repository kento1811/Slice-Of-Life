using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class HighLightBlock : MonoBehaviour
{
    private const float highlightMaxDistance = 3f;
    private bool isEnable = true;
    [SerializeField]private SpriteRenderer sr;
    [SerializeField] private Sprite sprite;
    private Vector3 defaultBlockSize = new Vector3(1,1,1);
    void Start()
    {
        if (!sr)
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void PickBlock(GameObject block = null)
    {
        if (isEnable && block)
        {
            Debug.Log(1);
            ChangeBlockSize(block.transform.localScale);
            MakeBlockTrans(block);
        } else
        {
            Debug.Log(2);
            MakeBlockNormal();
        }
    }

    // Update is called once per frame
    public void HighLight(Vector3 mouseWorld, Vector3 mouseCenterPos,  Transform player, Tilemap tilemap, GameObject block = null)
    {
        float distance = (mouseWorld - player.position).magnitude;
        if(distance > highlightMaxDistance)
        {
            sr.enabled = false;
            isEnable = sr.enabled;
            return;
        }
        sr.enabled = true;
        gameObject.transform.position = mouseCenterPos;
        isEnable = sr.enabled;
    }

    private void ChangeBlockSize(Vector3 newSize)
    {
        transform.localScale  = newSize;
    }

    private void ChangeBlockSize()
    {
        transform.localScale = defaultBlockSize;
    }

    private void MakeBlockNormal()
    {

        sr.sprite = sprite;
        Color color = sr.color;
        color.a = 1f;
        sr.color = color;
    }

    private void MakeBlockTrans(GameObject block)
    {
        SpriteRenderer sr_b = block.GetComponentInChildren<SpriteRenderer>();

        if (block)
        {
            Debug.Log(3);
            sr.sprite = sr_b.sprite;
        }
        Color color = sr.color;
        color.a = 0.5f;
        sr.color = color;
    }

    public Transform PlaceBlock(GameObject block)
    {
        if(!isEnable)
        MakeBlockNormal(); 
        return getHighLightTransform();
    }

    public Transform getHighLightTransform()
    {
        if (isEnable)
        {
            return gameObject.transform;
        }

        return null;
    }

    public bool getEnable()
    {
        return isEnable;
    }
}
    