using System.Windows.Controls;
using System.Reflection;

namespace Utilities
{
    public class Videos
    {
        #region This can actually get playing status! (full disclosure, this is a stackoverflow copy/paste)
        public static MediaState GetMediaState(MediaElement myMedia)
        {
            FieldInfo hlp = typeof(MediaElement).GetField("_helper", BindingFlags.NonPublic | BindingFlags.Instance);
            object helperObject = hlp.GetValue(myMedia);
            FieldInfo stateField = helperObject.GetType().GetField("_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            MediaState state = (MediaState)stateField.GetValue(helperObject);
            return state;
        }
        #endregion

    }
}
