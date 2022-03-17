using System.Runtime.InteropServices;

public static class JavascriptNetworkManager 
{
    [DllImport("__Internal")]
    public static extern void ModalOn ();
}
