using UnityEngine;
using UnityEngine.UI;

public class SliderChangeValue : MonoBehaviour
{
    private Slider slider;
    public Text text;
    private void Awake()
    {
        slider = GetComponentInParent<Slider>();
        //text = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateText(float val)
    {
        text.text = slider.value.ToString();
    }
}