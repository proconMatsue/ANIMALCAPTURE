using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class FeedBar : MonoBehaviour
{
    [SerializeField] private GameObject feedInventoryManager;
    private FeedInventory feedInventory;

    private Slider _slider;
    private string myname;
    private float _bar;

    private void Start()
    {
        _slider = this.gameObject.GetComponent<Slider>();
        myname = this.gameObject.tag;
        feedInventory = feedInventoryManager.GetComponent<FeedInventory>();
        _bar = 0.0f;
    }

    private void Update()
    {
        //if (_bar == 0) { }
        //else if (_bar == 1) { }
        //else
        //{
            _bar = 0.1f * feedInventory.GetFeedNum(myname); ;
            _slider.value = _bar;
        //}
    }
}
