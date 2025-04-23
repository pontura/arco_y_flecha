using UnityEngine;
using extOSC;
using static UnityEngine.Rendering.DebugUI;

public class OSCExampleReceiver : MonoBehaviour
{
    InputManager inputManager;
    [SerializeField] TMPro.TMP_Text field;
    public OSCReceiver receiver;

    float delayTimer = 1;

    int offset = 10;

    string key = "objeto1";

    public Vector2 pos_1;
    public Vector2 pos_2;
    public Vector2 pos_3;

    float last_pos_1_timer;
    float last_pos_2_timer;
    float last_pos_3_timer;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        print("OSCExampleReceiver 1");
        
        //receiver.Bind("/pos_x", OnReceiveMessageX);
        receiver.Bind("/" + key, OnPos1);
    }
    void OnPos1(OSCMessage message)
    {
        if (last_pos_1_timer != 0 && last_pos_1_timer + delayTimer > Time.time) return;
        last_pos_1_timer = Time.time;
        if (message.Address == "/" + key)
        {
            Vector2 newValue = Vector2.zero;
            for (int i = 0; i < message.Values.Count; i++)
            {
                float value = (float)message.Values[i].FloatValue;
                Debug.Log("Recibido float: " + value);
                //field.text += value.ToString();
                newValue[i] = value;
            }
            if(pos_1 == Vector2.zero || Vector2.Distance(newValue, pos_1)>offset)
            {
                pos_1 = newValue;
                last_pos_1_timer = Time.time;
                inputManager.OnHit(newValue);
                field.text = newValue.ToString();
            }
        }
    }
    //void OnReceiveMessageX(OSCMessage message)
    //{
    //    print("OSCExampleReceiver" + message);
    //    if (message.Values.Count > 0)
    //    {
    //        var value = message.Values[0].FloatValue; // O .IntValue, .StringValue, etc.
    //       // Debug.Log("Mensaje OSC recibido: " + value);
    //        field.text = value.ToString();
    //        pos_1.x = value;
    //    }
    //}

}
