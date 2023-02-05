using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;
using System.Text.RegularExpressions;
public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;
    
    [SerializeField]
    private GameObject _input_slot_1;

    [SerializeField]
    private GameObject _input_slot_2;

    private PlayerInventory _inventory;
    private GameObject _player;

    private Boolean _has_been_placed = false;
    private Boolean _has_been_removed = false;
    private Vector3 _in_inventory_position;
    private Vector3 _start_position;
    
    private Vector3 _input_slot_position;

    private Vector3 _input_slot_1_position;
    private Vector3 _input_slot_2_position;


    private string _state;

    public void DragHandler(BaseEventData data) {
        // PointerEventData pointerData = (PointerEventData)data;
        // Vector2 position;
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //     (RectTransform)canvas.transform,
        //     pointerData.position,
        //     canvas.worldCamera,
        //     out position);
        
        // string parent_name = this.gameObject.transform.parent.name;
        // string resultString = Regex.Match(parent_name, @"\d+").Value;
        // if(_inventory.GetPickupAtIndex(Convert.ToInt32(resultString)) != null) {
        //     transform.position = canvas.transform.TransformPoint(position);
        // }
    }

    private void Awake() {
        _player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        _inventory = _player.GetComponent<PlayerInventory>();
        _in_inventory_position = this.transform.position;
        _has_been_placed = false;
        _input_slot_1_position = _input_slot_1.transform.position ;
        _input_slot_2_position = _input_slot_2.transform.position ;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {   
        _state = "Started Dragging " + this.transform.name;
  

        if (_has_been_placed) _has_been_removed = true;
        _start_position = this.transform.position;
        // Debug.Log(_start_position);
        // Debug.Log("OnBeginDrag:");
    }

    // Drag the selected item.
    public void OnDrag(PointerEventData data)
    {
        string parent_name = this.gameObject.transform.parent.name;
        string resultString = Regex.Match(parent_name, @"\d+").Value;
        if (data.dragging && _inventory.GetPickupAtIndex(Convert.ToInt32(resultString)) != null)
        {
            _state = "Dragging " + this.transform.name;
            if (_has_been_removed) {
                transform.position = data.position;
            }
            if (_has_been_placed && !_has_been_removed) {
                data.pointerDrag = (null);
                _state = "Snapped " + this.transform.name;
            } else {
                transform.position = data.position;
            }
            
        } 
    }

    public void ResetDrag()
    {
        _has_been_placed = false;
        this.transform.position = _in_inventory_position;
        _input_slot_1.GetComponent<IbridatorInputManager>().is_empty = true;
        _input_slot_2.GetComponent<IbridatorInputManager>().is_empty = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!(_input_slot_1.GetComponent<IbridatorInputManager>().is_empty && _input_slot_2.GetComponent<IbridatorInputManager>().is_empty))
        {
            transform.position = _in_inventory_position;
        }
    }

    private void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == Tags.IBRIDATOR_TAG && target != this.transform.parent)
            {
                if(target.gameObject == _input_slot_1 &&  _input_slot_1.GetComponent<IbridatorInputManager>().is_empty)
                    {
                        _input_slot_1.GetComponent<IbridatorInputManager>().is_empty = false;
                        _input_slot_position = target.transform.position;
                        _has_been_placed = true;
                        string parent_name = this.gameObject.transform.parent.name;
                        string resultString = Regex.Match(parent_name, @"\d+").Value;
                        transform.position = _input_slot_1_position;
                        PickupScriptableObject inventory_picked_object_1 = _inventory.GetPickupAtIndex(Convert.ToInt32(resultString));
                }
                else if (target.gameObject == _input_slot_2 &&  _input_slot_2.GetComponent<IbridatorInputManager>().is_empty) {
                        _input_slot_2.GetComponent<IbridatorInputManager>().is_empty = false;
                        _input_slot_position = target.transform.position;
                        _has_been_placed = true;
                        string parent_name = this.gameObject.transform.parent.name;
                        string resultString = Regex.Match(parent_name, @"\d+").Value;
                        transform.position = _input_slot_2_position;
                        PickupScriptableObject inventory_picked_object_2 = _inventory.GetPickupAtIndex(Convert.ToInt32(resultString));
                }
        }
    } 

    // private void OnTriggerExit2D(Collider2D target) {
    //     if(target.tag == Tags.I && target != this.transform.parent)
    //         {
    //             if(target.gameObject == _input_slot_1 && _has_been_placed && !_input_slot_1.GetComponent<IbridatorInputManager>().is_empty)
    //                 {
    //                     _input_slot_1.GetComponent<IbridatorInputManager>().is_empty = true;
    //                     this.transform.position = _start_position;
    //                     _has_been_removed = true;
    //                 }
    //             else if (target.gameObject == _input_slot_2 && _has_been_placed &&  !_input_slot_2.GetComponent<IbridatorInputManager>().is_empty) {

    //                     _input_slot_2.GetComponent<IbridatorInputManager>().is_empty = true;
    //                     this.transform.position = _start_position;
    //                     _has_been_removed = true;

    //             }
    //     }
    // } 
}
