using Android.OS;
using OxincNotification.Android;
using static OxincNotification.Views.TeacherPortalMainPage;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMethods))]
namespace OxincNotification.Android
{
    public class AndroidMethods : IAndroidMethods
    {
        public void CloseApp()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}