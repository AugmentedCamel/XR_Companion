using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomAnchorManager : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        var anchors = new List<OVRAnchor>();
        await OVRAnchor.FetchAnchorsAsync<OVRRoomLayout>(anchors);

        // no rooms - call Space Setup or check Scene permission
        if (anchors.Count == 0)
            return;

        // get the component to access its data
        var room = anchors.First();
        if (!room.TryGetComponent(out OVRAnchorContainer container))
            return;

        // use the component helper function to access all child anchors
        await container.FetchChildrenAsync(anchors);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
