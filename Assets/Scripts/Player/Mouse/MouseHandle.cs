
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class MouseHandle : MonoBehaviour
{
    
    private Vector3 mouseWorld;
    private Vector3Int cellPos;
    private Vector3 mouseCenterPos;

    [SerializeField] private GameObject highLightBlockObject;
    private HighLightBlock highLightBlockcript;
    [SerializeField] private GameObject holdingItem;

    [SerializeField] private Tilemap tilemap;

    [SerializeField] private Transform items;
    
    void Start()
    {
        if (!highLightBlockObject)
        {
            highLightBlockObject = GameObject.FindGameObjectWithTag("highlightblock");
        }
        if (!items)
            items = GameObject.FindGameObjectWithTag("items").transform;
        
        highLightBlockcript = highLightBlockObject.GetComponent<HighLightBlock>();
        highLightBlockcript.PickBlock(holdingItem);
    }

    void Update()
    {
        MousePosHandle();

        highLightBlockcript.HighLight(mouseWorld, mouseCenterPos, transform, tilemap, holdingItem);

        Click();
    }

    private void PlaceBlock()
    {
        if(!highLightBlockcript.getEnable()) return;
        Vector3 pos = highLightBlockcript.PlaceBlock(holdingItem).position;
        GameObject newBlock = Instantiate(holdingItem,pos, Quaternion.identity,items);
    }

    private void Click()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (holdingItem)
            {
                PlaceBlock();
            }
        }
    }

    private void MousePosHandle()
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        mouseWorld =  Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorld.z = 0;
        cellPos = tilemap.WorldToCell(mouseWorld);
        mouseCenterPos = tilemap.GetCellCenterWorld(cellPos);
    }
}
