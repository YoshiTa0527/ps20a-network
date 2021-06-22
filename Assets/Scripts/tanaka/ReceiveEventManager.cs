using Photon.Realtime;    // IOnEventCallback を使うため
using ExitGames.Client.Photon;  // EventData を使うため
using Photon.Pun;   // PhotonNetwork を使うため

public class ReceiveEventManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code < 200)
        {
            if ((int)e.Code == 1)
            {

            }
        }
    }
}
