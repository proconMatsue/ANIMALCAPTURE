using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitBar : MonoBehaviour
{
    [SerializeField] private GameObject waitbar;
    private timecontroller time;

    private Slider _slider;
    private float _bar;

    private void Start()
    {
        _slider = this.gameObject.GetComponent<Slider>();
        _bar = 0.0f;
        time = this.gameObject.GetComponent<timecontroller>();
    }

    public void generateBar() {
        GameObject pro_bar = Instantiate<GameObject>(
            waitbar,
            this.gameObject.transform
            );
        this.gameObject.transform.parent = pro_bar.transform;
    }

    private void Update()
    {
        _bar += 1.0f/15.0f;
        _slider.value = _bar;
    }
}

