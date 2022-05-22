using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceGenerator : MonoBehaviour
{
    private GameObject _interface = null;
    private GameObject _eventText;
    private List<EventText> _events = new List<EventText>();
    private int _count_of_event_text = 0;
    void Start()
    {
        _interface = GameObject.Find("Interface");
        _eventText = GameObject.Find("Text");
    }

    public EventText addEventText(string text, string name = "unidentified") {
        if (!_events.Exists(txt => txt.text == text)) {
            EventText tmp = Instantiate(_eventText, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), _interface.transform).GetComponent<EventText>();
            tmp.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 150 - _eventText.GetComponent<RectTransform>().rect.height * _events.Count, 0);
            tmp.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
            tmp.changeText(text);
            tmp.txt_name = name;
            tmp.show();
            _events.Add(tmp);
            return tmp;
        } else {
            return _events.Find(txt => txt.text == text);
        }
    }
    public void removeEventText(EventText text) {
        int i = 0;
        for (; i < _events.Count; ++i) {
            if (_events[i] == text) {
                break;
            }
        }
        for (i += 1; i < _events.Count; ++i) {
            _events[i].GetComponent<RectTransform>().localPosition +=  Vector3.up * _eventText.GetComponent<RectTransform>().rect.height;
        }
        _events.Remove(text);
        Destroy(text.gameObject);
    }
    public bool isEventActive(string name) {
        return _events.Exists(txt => txt.txt_name == name);
    }
}
