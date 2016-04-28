
using Improbable.Player;
using Improbable.Player.Notification;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;

class NotificationVisualizer : MonoBehaviour
{
    public Text TextToWriteTo;

    [Require]
    protected LocalPlayerCheckStateWriter LocalPlayerCheck;

    [Require]
    protected NotificationStateReader Notification;

    void OnEnable()
    {
        TextToWriteTo.text = "";
        Notification.NotificationEvent += Notification_NotificationEvent;
    }

    private void Notification_NotificationEvent(NotificationEvent obj)
    {
        TextToWriteTo.text = obj.Message + "\n" + TextToWriteTo.text;
    }
}

